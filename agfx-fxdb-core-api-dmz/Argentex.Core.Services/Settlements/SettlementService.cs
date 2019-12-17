using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.ClientSiteAction;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Enums;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Models.Email;
using Argentex.Core.Service.Models.Payments;
using Argentex.Core.Service.Models.Settlements;
using Argentex.Core.Service.Settlements;
using Argentex.Core.Service.Trade;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.ClientCompanies;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts;
using Argentex.Core.UnitsOfWork.Currencies;
using Argentex.Core.UnitsOfWork.Payments;
using Argentex.Core.UnitsOfWork.Settlements;
using Argentex.Core.UnitsOfWork.Trades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Argentex.Core.Service.Payments
{
    public class SettlementService : ISettlementService
    {
        private const int DecimalPlaces = 2;
        private bool _isTotalSettlementAmountGreaterThanTradeBalance = false;
        private const string _warningMessage = "Total settlement amount cannot exceed the total outstanding balance.";

        private readonly IPaymentUoW _paymentUoW;
        private readonly ITradeUow _tradeUow;
        private readonly ICurrencyUoW _currencyUoW;
        private readonly IClientCompanyUow _clientCompanyUow;
        private readonly IClientCompanyAccountsUoW _clientCompanyAccountsUoW;
        private readonly ISettlementUow _settlementUow;
        private readonly IUserService _userService;
        private readonly ICurrencyService _currencyService;
        private readonly IClientSiteActionService _clientSiteActionService;
        private readonly IAppSettingService _appSettingService;
        private readonly IEmailService _emailService;
        private readonly ITradeService _tradeService;

        private bool _disposed;

        public SettlementService(IPaymentUoW paymentUoW,
            ICurrencyUoW currencyUoW, 
            IClientCompanyUow clientCompanyUow,
            IClientCompanyAccountsUoW clientCompanyAccountsUoW,
            ISettlementUow settlementUow,
            ITradeUow tradeUow,
            IUserService userService,
            ICurrencyService currencyService,
            IClientSiteActionService clientSiteActionService,
            IAppSettingService appSettingService,
            IEmailService emailService,
            ITradeService tradeService)
        {
            _paymentUoW = paymentUoW;            
            _currencyUoW = currencyUoW;
            _clientCompanyUow = clientCompanyUow;
            _clientCompanyAccountsUoW = clientCompanyAccountsUoW;
            _settlementUow = settlementUow;
            _tradeUow = tradeUow;
            _userService = userService;
            _currencyService = currencyService;
            _clientSiteActionService = clientSiteActionService;
            _appSettingService = appSettingService;
            _emailService = emailService;
            _tradeService = tradeService;
        }

        public PaymentInformationModel GetPaymentInformation(string paymentCode, bool isPaymentOut = false)
        {
            if (!isPaymentOut) return GetPaymentmentInInformation(paymentCode);

            var opiTransaction = GetOpiTransaction(paymentCode);
            
            return GetPaymentmentOutInformation(paymentCode, opiTransaction);
        }

        private bool CheckIfTradeBalanceIsExceededBySettlementAmounts(AssignSettlementRequestModel requestModel)
        {
            decimal totalAcceptedSettlementsAmount = _settlementUow.GetSettlementAmountForTrade(requestModel.Trade.TradeId);

            decimal totalSwapSettlementsAmount = GetSwaps(requestModel.Trade.TradeId)
                .Where(x => x.Status != (int)ClientSiteActionStatuses.Rejected && x.Status != (int)ClientSiteActionStatuses.Declined).Sum(x => x.Amount);

            decimal totalNewSettlementsAmount = requestModel.SettlementModels
                .Where(x => x.Status == (int)SettlementStatus.New).Sum(x => x.Amount);

            decimal tradeBalance = _tradeService.GetTradeBalance(requestModel.ClientCompanyId, requestModel.Trade.TradeId);

            return (totalNewSettlementsAmount + totalAcceptedSettlementsAmount + totalSwapSettlementsAmount) > tradeBalance;
        }

        public async Task<IList<AssignSettlementModel>> AssignAsync(AssignSettlementRequestModel requestModel)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _isTotalSettlementAmountGreaterThanTradeBalance = CheckIfTradeBalanceIsExceededBySettlementAmounts(requestModel);

                if (!_isTotalSettlementAmountGreaterThanTradeBalance)
                {
                    var settlementNoteModelList = new List<SettlementNoteModel>();
                    var authUser = _userService.GetAuthUserById(requestModel.AuthUserId); //temporary
                    var parentTrade = _tradeUow.GetTrade(requestModel.Trade.TradeId).FirstOrDefault();

                    foreach (var assignSettlement in requestModel.SettlementModels)
                    {
                        var maxSettlementCreateDateForTrade = _settlementUow.GetMaxCreateDateForTrade(requestModel.Trade.TradeId);

                        if (assignSettlement.Status == (int)SettlementStatus.New)
                        {
                            if (maxSettlementCreateDateForTrade != _settlementUow.GetMaxCreateDateForTrade(requestModel.Trade.TradeId))
                                throw new TransactionAbortedException("The transaction has been aborted.");

                            if (assignSettlement.ValueDate == ((DateTime)requestModel.Trade.ValueDate).ToString("dd'/'MM'/'yyyy"))
                            {
                                CreatePaymentRequest(assignSettlement, requestModel.AuthUserId, requestModel.Trade.TradeId, (DateTime)requestModel.Trade.ValueDate);
                            }
                            else
                            {
                                CreateSwap(assignSettlement, requestModel.AuthUserId, requestModel.Trade.TradeId);
                            }

                            var settlementNoteModel = CreateSettlementNoteModel(
                                assignSettlement, authUser.UserName, requestModel.Trade.TradeId, parentTrade.IsBuy ? 1 / requestModel.Trade.ClientRate : requestModel.Trade.ClientRate);
                            settlementNoteModelList.Add(settlementNoteModel);
                        }
                    }

                    //var emailList = _someService.GetSettlementNotificationEmailList(requestModel.ClientCompanyId);
                    var emailList = new List<string> { authUser.Email }; //temporary

                    await _emailService.SendSettlementEmailsAsync(settlementNoteModelList, emailList);
                }
                scope.Complete();
            }

            var result = GetAssignedSettlements(requestModel.Trade.TradeId);
            return result;
        }

        private SettlementNoteModel CreateSettlementNoteModel(AssignSettlementModel assignSettlement, string UserName, string tradeCode, decimal? clientRate)
        {
            var settlementNoteModel = new SettlementNoteModel
            {
                Amount = assignSettlement.Amount,
                ValueDate = DateTime.Parse(assignSettlement.ValueDate),
                InstructedDateTime = DateTime.Now,
                InstructedBy = UserName,
                TradedCurrency = assignSettlement.TradedCurrency,
                ParentTradeCode = tradeCode,
                AccountName = assignSettlement.Account.accountName,
                SettlementAmount = assignSettlement.Amount * clientRate,
                AccountCurrency = assignSettlement.Account.currency
            };

            return settlementNoteModel;
        }

        public IList<AssignSettlementModel> GetAssignedSettlements(string tradeCode)
        {
            var result = new List<AssignSettlementModel>();

            result.AddRange(GetPaymetRequests(tradeCode));
            result.AddRange(GetSwaps(tradeCode));

            return result; 
        }

        public void DeleteAssignedSettlements(long settlementId)
        {
            //In order to delete an Assigned settlement, its link to the action must first be deleted and then the actual Trade2Opi
            _clientSiteActionService.DeleteAction2AssignedSettlementLink(settlementId);
            _settlementUow.DeleteAssignedSettlement(settlementId);
        }


        #region Private methods

        #region Swap methods        

        private void CreateSwap(AssignSettlementModel model, int authUserID, string parentTradeCode)
        {
            var swapCount = _settlementUow.GetTradeSwapCount(parentTradeCode).SwapCount;
            var parentTrade = _tradeUow.GetTrade(parentTradeCode).SingleOrDefault();
            swapCount++;
            var deliveryLegTrade = GetFxForwardTrade(model, authUserID, parentTrade, swapCount, SwapType.DeliveryLeg);
            var reversalLegTrade = GetFxForwardTrade(model, authUserID, parentTrade, swapCount, SwapType.ReversalLeg);

            
            deliveryLegTrade = PopulateDeliveryLeg(deliveryLegTrade, model, parentTrade);
            reversalLegTrade = PopulateReversalLeg(reversalLegTrade, model, parentTrade);

            var swapID = _settlementUow.Assign(deliveryLegTrade, reversalLegTrade, parentTradeCode, authUserID);
            _clientSiteActionService.LogActionSwapCreation(authUserID, swapID);
        }

        private FxforwardTrade PopulateDeliveryLeg(FxforwardTrade deliveryLegTrade, AssignSettlementModel model, FxforwardTrade parentTrade)
        {

            deliveryLegTrade.ValueDate = Convert.ToDateTime(model.ValueDate);

            if ((bool)deliveryLegTrade.IsRhsmajor)
            {
                deliveryLegTrade.ClientRhsamt = model.Amount;
                deliveryLegTrade.BrokerRhsamt = model.Amount;
            }
            else
            {
                deliveryLegTrade.ClientLhsamt = model.Amount;
                deliveryLegTrade.BrokerLhsamt = model.Amount;
            }

            deliveryLegTrade.EmirUti = _appSettingService.GetEmirUtiCode() + deliveryLegTrade.Code.Replace("-", string.Empty).Replace("/", "");

            return deliveryLegTrade;
        }

        private FxforwardTrade PopulateReversalLeg(FxforwardTrade reversalLegTrade, AssignSettlementModel model, FxforwardTrade parentTrade)
        {
            reversalLegTrade.IsBuy = !parentTrade.IsBuy;
            reversalLegTrade.ValueDate = parentTrade.ValueDate;

            if ((bool)reversalLegTrade.IsRhsmajor)
            {
                reversalLegTrade.ClientRhsamt = model.Amount;
                reversalLegTrade.BrokerRhsamt = model.Amount;
            }
            else
            {
                reversalLegTrade.ClientLhsamt = model.Amount;
                reversalLegTrade.BrokerLhsamt = model.Amount;
            }

            reversalLegTrade.EmirUti = _appSettingService.GetEmirUtiCode() + reversalLegTrade.Code.Replace("-", string.Empty).Replace("/", "");

            return reversalLegTrade;
        }

        private FxforwardTrade GetFxForwardTrade(AssignSettlementModel model, int authUserID, FxforwardTrade parentTrade, int swapCount, SwapType swapType)
        {
            var pendingStatus = _tradeUow.GetEmirStatus("Pending");
            var pendingFxStatus = _tradeUow.GetFxForwardStatus("Pending");
            var tradeInstructionMethod = _tradeUow.GetTradeInstructionMethod("Online");
            var tradeCode = CodeBuilder.FormatSwapTradeCode(parentTrade.Code, swapCount, swapType);
            var valueDate = DateTime.ParseExact(model.ValueDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var fxTrade = new FxforwardTrade()
            {
                Code = tradeCode,
                CreatedDate = DateTime.Now,
                CreatedByAuthUserId = authUserID,
                ClientCompanyId = model.Account.clientCompanyId,
                AuthorisedByClientCompanyContactId = GetClientCompanyContactId(authUserID),
                ContractDate = DateTime.Now,
                ContractNoteSentToClientDateTime = DateTime.Now,
                ValueDate = valueDate,
                IsOrder = false,
                CurrencyPair = parentTrade.CurrencyPair,
                IsBuy = parentTrade.IsBuy,
                Lhsccyid = parentTrade.Lhsccyid,
                Rhsccyid = parentTrade.Rhsccyid,
                ClientRate = parentTrade.ClientRate,
                BrokerRate = parentTrade.BrokerRate,
                CollateralPerc = 0,
                UpdatedByAuthUserId = authUserID,
                IsRhsmajor = parentTrade.IsRhsmajor,
                ProfitConsolidated = false,
                Deleted = false,
                EmirReported = false,
                IsComplianceSupported = true,
                IsComplianceRegulated = false,
                EmirDelegatedReported = false,
                IsEmirreportable = true,
                IsApareportable = true,
                IsArmreportable = true,
                Armreported = false,
                EmirstatusId = pendingStatus.Id,
                ApastatusId = pendingStatus.Id,
                ArmstatusId = pendingStatus.Id,
                Reference = model.Reference,
                TradeInstructionMethodId = tradeInstructionMethod.Id,
                FxforwardTradeStatusId = pendingFxStatus.Id,
                FilledByAuthUserId = authUserID,
                FilledDateTime = DateTime.Now,
                ClientCompanyOpiid = model.Account.clientCompanyOpiId,
                BrokerId = parentTrade.BrokerId,
            };
            CalculateAmountsAndProfit(model, fxTrade);

            return fxTrade;
        }

        private int GetClientCompanyContactId(int authUserId)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                var clientCompanyContactId = _userService
                .GetApplicationUserByAuthUserId(authUserId)
                .ClientCompanyContactId;

                scope.Complete();

                return clientCompanyContactId;
            }
        }

        private List<AssignSettlementModel> GetSwaps(string parentTradeCode)
        {
            var result = new List<AssignSettlementModel>();
            result = _settlementUow.GetTradeSwaps(parentTradeCode)
                .Select(swap => new AssignSettlementModel
                {
                    Reference = swap.Key.Reference,
                    Status = swap.Value.ClientSiteActionStatusId,
                    TradedCurrency = swap.Key.IsRhsmajor.HasValue
                                        ? swap.Key.IsRhsmajor.Value
                                            ? swap.Key.Rhsccy.Code
                                            : swap.Key.Lhsccy.Code
                                        : null,
                    ValueDate = ((DateTime)swap.Key.ValueDate).ToString("dd/MM/yyyy"),
                    Amount = (decimal)((bool)swap.Key.IsRhsmajor ? swap.Key.ClientRhsamt : swap.Key.ClientLhsamt),
                    Account = MapAccountModel(swap.Key.ClientCompanyOpi),
                    IsWarning = _isTotalSettlementAmountGreaterThanTradeBalance,
                    WarningMessage = _isTotalSettlementAmountGreaterThanTradeBalance ? _warningMessage : string.Empty
                }).ToList<AssignSettlementModel>();

            return result;
        }

        private void CalculateAmountsAndProfit(AssignSettlementModel model, FxforwardTrade trade)
        {
            if ((bool)trade.IsRhsmajor)
            {
                trade.ClientLhsamt = decimal.Round(model.Amount / trade.ClientRate.Value, DecimalPlaces);             
                trade.BrokerLhsamt = decimal.Round(model.Amount / trade.BrokerRate.Value, DecimalPlaces);
                trade.ClientRhsamt = model.Amount;
                trade.BrokerRhsamt = model.Amount;

                trade.Profit = trade.IsBuy ?
                                        trade.BrokerLhsamt - trade.ClientLhsamt :
                                        trade.ClientLhsamt - trade.BrokerLhsamt;
            }
            else
            {
                trade.ClientRhsamt = decimal.Round(model.Amount * trade.ClientRate.Value, DecimalPlaces);
                trade.BrokerRhsamt = decimal.Round(model.Amount * trade.BrokerRate.Value, DecimalPlaces);
                trade.BrokerLhsamt = model.Amount;
                trade.ClientLhsamt = model.Amount;

                trade.Profit = trade.IsBuy ?
                                        trade.ClientRhsamt - trade.BrokerRhsamt :
                                        trade.BrokerRhsamt - trade.ClientRhsamt;
            }

            //trade.RemainingClientLhsamt = trade.ClientLhsamt;
            //trade.RemainingClientRhsamt = trade.ClientRhsamt;
        }

        private AccountModel MapAccountModel(ClientCompanyOpi input)
        {
            return new AccountModel()
            {
                accountName = input.AccountName,
                accountNumber = input.AccountNumber,
                clientCompanyId = input.ClientCompanyId,
                clientCompanyOpiId = input.Id,
                currency = input.Currency.Code
            };
        }

        #endregion

        #region TradeOpi's

        private void CreatePaymentRequest(AssignSettlementModel model, int authUserID, string tradeCode, DateTime valueDate)
        {
            var trade2opi = new FxforwardTrade2Opi
            {
                Amount = model.Amount,
                Details = model.Reference,
                ClientCompanyOpiid = model.Account.clientCompanyOpiId,
                CreatedByAuthUserId = authUserID,
                CreatedDateTime = DateTime.UtcNow,
                FxforwardTradeCode = tradeCode,
                TradeValueDate = valueDate
            };

            _settlementUow.AddTrade2Opi(trade2opi);

            _clientSiteActionService.LogActionOpiPayment(authUserID, trade2opi.Id, model.Account.accountName, trade2opi.Amount);
        }

        private List<AssignSettlementModel> GetPaymetRequests(string parentTradeCode)
        {
            var result = _settlementUow.GetTradeOpis(parentTradeCode)
                .Select(tradeOpis => new AssignSettlementModel
                {
                    SettlementId = tradeOpis.Id,
                    Reference = tradeOpis.Details,
                    Status = (int)SettlementStatus.Accepted, // accepted status
                    TradedCurrency = tradeOpis.FxforwardTradeCodeNavigation.IsRhsmajor.HasValue
                                        ? tradeOpis.FxforwardTradeCodeNavigation.IsRhsmajor.Value 
                                            ? tradeOpis.FxforwardTradeCodeNavigation.Rhsccy.Code 
                                            : tradeOpis.FxforwardTradeCodeNavigation.Lhsccy.Code
                                        : null,
                    ValueDate = (tradeOpis.TradeValueDate).ToString("dd/MM/yyyy"),
                    Amount = tradeOpis.Amount,
                    Account = MapAccountModel(tradeOpis.ClientCompanyOpi),
                    IsWarning = _isTotalSettlementAmountGreaterThanTradeBalance,
                    WarningMessage = _isTotalSettlementAmountGreaterThanTradeBalance ? _warningMessage : string.Empty
                }).ToList();

            return result;
        }

        #endregion

        private PaymentOutResponseModel ProcessResult(DataTable result)
        {
            var paymentOutResponseModel = new PaymentOutResponseModel();

            if (result != null && result.Rows != null && result.Rows.Count > 0)
            {
                paymentOutResponseModel.Code = result.Rows[0]["Code"].ToString();
                paymentOutResponseModel.IsSuccessful = true;
            }
            else
            {
                paymentOutResponseModel.Code = null;
                paymentOutResponseModel.IsSuccessful = false;
            }

            return paymentOutResponseModel;
        }

        //private Payment CreatePayment(PaymentOutModel model, ClientCompany clientCompany)
        //{
        //    PaymentType paymentType = _paymentUoW.GetPaymentType("OUT").SingleOrDefault();
        //    paymentType.DefaultSendToSwift = false;
        //    Currency currency = _currencyUoW.GetCurrency(model.Currency).SingleOrDefault();

        //    Payment payment = new Payment
        //    {
        //        PaymentType = paymentType,
        //        Amount = model.Amount,
        //        Currency = currency,
        //        ValueDate = DateTime.ParseExact(model.ValueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
        //        NotifyClient = false,
        //        Comments = "FX SETTLEMENT for Trade code: " + model.TradeCode,
        //        Reference = model.Reference,
        //        ClientCompany = clientCompany
        //    };

        //    return payment;
        //}

        private PaymentOutInformationModel GetPaymentmentOutInformation(string paymentCode, ClientCompanyOpitransaction opiTransaction)
        {
            var paymentInformation = _paymentUoW.GetPayment(paymentCode)
                .Select(payment => new PaymentOutInformationModel
                {
                    PaymentCode = payment.Code,
                    PaymentType = Enum.GetName(typeof(PaymentTypes), payment.PaymentTypeId),
                    ValueDate = payment.ValueDate,
                    CreatedDateTime = payment.CreatedDate,
                    Amount = payment.Amount.Value,
                    Reference = !string.IsNullOrWhiteSpace(payment.Reference) ? payment.Reference : string.Empty,
                    Currency = payment.Currency.Code,
                    OpiDescription = opiTransaction.ClientCompanyOpi.Description,
                    OpiAccountName = opiTransaction.ClientCompanyOpi.AccountName,
                    OpiSortCode = opiTransaction.ClientCompanyOpi.SortCode,
                    OpiAccountNumber = opiTransaction.ClientCompanyOpi.AccountNumber,
                    OpiBankName = opiTransaction.ClientCompanyOpi.BankName,
                    OpiSwiftCode = opiTransaction.ClientCompanyOpi.SwiftCode,
                    OpiReference = opiTransaction.ClientCompanyOpi.Reference,
                    OpiIban = opiTransaction.ClientCompanyOpi.Iban
                }).SingleOrDefault();

            if (paymentInformation == null)
                throw new PaymentNotFoundException($"Payment with code {paymentCode} does not exist");

            return paymentInformation;
        }

        private PaymentInformationModel GetPaymentmentInInformation(string paymentCode)
        {
            var paymentInformation = _paymentUoW.GetPayment(paymentCode)
                .Select(payment => new PaymentInformationModel
                {
                    PaymentCode = payment.Code,
                    PaymentType = Enum.GetName(typeof(PaymentTypes), payment.PaymentTypeId),
                    ValueDate = payment.ValueDate,
                    CreatedDateTime = payment.CreatedDate,
                    Amount = payment.Amount.Value,
                    Reference = !string.IsNullOrWhiteSpace(payment.Reference) ? payment.Reference : string.Empty,
                    Currency = payment.Currency.Code
                }).SingleOrDefault();

            if (paymentInformation == null)
                throw new PaymentNotFoundException($"Payment with code {paymentCode} does not exist");

            return paymentInformation;
        }

        private ClientCompanyOpitransaction GetOpiTransaction(string paymentCode)
        {
            var opiTransaction = _paymentUoW.GetClientCompanyOpiTransaction(paymentCode).FirstOrDefault();

            if (opiTransaction == null)
                throw new ClientCompanyOpiTransactionNotFoundException($"Opi transaction for payment code {paymentCode} does not exist");

            return opiTransaction;
        }

        #endregion

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //_paymentUoW?.Dispose();
                    //_tradeUow?.Dispose();
                    //_currencyUoW?.Dispose();
                    //_clientCompanyUow?.Dispose();
                    //_clientCompanyAccountsUoW?.Dispose();
                    //_settlementUow?.Dispose();
                    //_userService?.Dispose();
                    //_currencyService?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
