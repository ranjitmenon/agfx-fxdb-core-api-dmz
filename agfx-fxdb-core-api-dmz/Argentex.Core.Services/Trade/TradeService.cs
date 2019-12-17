using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.ClientSiteAction;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Enums;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Fix;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Models.Email;
using Argentex.Core.Service.Models.Fix;
using Argentex.Core.Service.Models.Trade;
using Argentex.Core.Service.Models.Trades;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.ClientCompanies;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts;
using Argentex.Core.UnitsOfWork.Trades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Trade
{
    public class TradeService : ITradeService
    {
        private const int DecimalPlaces = 2;

        private readonly ITradeUow _tradeUow;
        private readonly IClientCompanyAccountsUoW _clientCompanyAccountsUow;
        private readonly IBarxFxService _fixQuoteService;
        private readonly IEmailService _emailService;
        private readonly IConfigWrapper _config;
        private readonly IUserService _userService;
        private readonly ICurrencyService _currencyService;
        private readonly IAppSettingService _appSettingService;
        private readonly IClientCompanyUow _clientCompanyUow;
        private readonly IClientCompanyService _clientCompanyService;
        private readonly IClientSiteActionService _clientSiteActionService;

        private bool _disposed;

        public TradeService(ITradeUow tradeUow,
            IBarxFxService fixQuoteService,
            IEmailService emailService,
            IConfigWrapper config,
            IClientCompanyAccountsUoW clientCompanyAccountsUow,
            IUserService userService,
            ICurrencyService currencyService,
            IAppSettingService appSettingService,
            IClientCompanyUow clientCompanyUow,
            IClientCompanyService clientCompanyService,
            IClientSiteActionService clientSiteActionService)
        {
            _tradeUow = tradeUow;
            _clientCompanyAccountsUow = clientCompanyAccountsUow;
            _fixQuoteService = fixQuoteService;
            _emailService = emailService;
            _config = config;
            _userService = userService;
            _currencyService = currencyService;
            _appSettingService = appSettingService;
            _clientCompanyUow = clientCompanyUow;
            _clientCompanyService = clientCompanyService;
            _clientSiteActionService = clientSiteActionService;
        }

        public IList<TradeModel> GetUnsettledTrades(int clientCompanyId)
        {
            var table = _tradeUow.GetUnsettledTrades(clientCompanyId);
            var list = new List<TradeModel>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                TradeModel tradeModel = MapTradeModelFromDataRow(row);
                list.Add(tradeModel);
            }
            list.Sort((x, y) => Nullable.Compare(y.ValueDate, x.ValueDate));
            return list;
        }

        public IEnumerable<string> GetCurrencyCodes()
        {
            return _tradeUow.GetCurrencies()
                .Select(x => x.Code)
                .ToList();
        }

        public IEnumerable<string> GetAllowedCurrencyPairs()
        {
            var pairs = _tradeUow.GetCurrencyPairValidation()
                .Select(x => x.CurrencyPair)
                .ToList();

            return pairs;
        }

        public async Task<FxForwardTradeInformationModel> GetTradeNote(string tradeCode)
        {
            // getting trade data
            var tradeNote = GetTradeInformation(tradeCode);

            // getting broker data
            var brokerTradeNote = GetBrokerTradeInformation(tradeCode);

            // sending email to client
            await _emailService.SendTradeReceiptEmailAsync(tradeNote);
            // sending email to broker and dealer
            await _emailService.SendBrokerTradeNoteEmailAsync(brokerTradeNote);

            return tradeNote;
        }

        public FxForwardTradeInformationModel GetTradeInformation(string tradeCode)
        {
            var tradeInformation = _tradeUow
                .GetTrade(tradeCode)
                .Select(trade => new FxForwardTradeInformationModel
                {
                    InstructedBy = trade.AuthorisedByClientCompanyContactId.HasValue ? trade.AuthorisedByClientCompanyContact.Fullname : string.Empty,
                    InstructedDateTime = trade.CreatedDate,
                    Method = Enum.GetName(typeof(TradeInstructionMethods), trade.TradeInstructionMethodId.Value),
                    TradeRef = trade.Code,
                    SellCcy = trade.IsBuy 
                                ? trade.Rhsccy.Code 
                                : trade.Lhsccy.Code,
                    SellAmount = trade.IsBuy 
                                ? trade.ClientRhsamt.Value 
                                : trade.ClientLhsamt.Value,
                    BuyCcy = trade.IsBuy
                                ? trade.Lhsccy.Code
                                : trade.Rhsccy.Code,
                    BuyAmount = trade.IsBuy
                                ? trade.ClientLhsamt.Value
                                : trade.ClientRhsamt.Value,                  
                    ValueDate = trade.ValueDate.Value,
                    Rate = (double)trade.ClientRate.Value,
                    Collateral =
                        trade.IsBuy
                            ? CalculateCollateralValue(trade.ClientRhsamt.Value, trade.CollateralPerc.Value)
                            : CalculateCollateralValue(trade.ClientLhsamt.Value, trade.CollateralPerc.Value),
                    CollateralCcy = trade.IsBuy ? trade.Rhsccy.Code : trade.Lhsccy.Code,
                    CurrencyPair = trade.CurrencyPair,
                    ClientEmail = trade.AuthorisedByClientCompanyContactId.HasValue ? trade.AuthorisedByClientCompanyContact.Email : string.Empty,
                    ClientCompany = trade.AuthorisedByClientCompanyContact.ClientCompany,
                    //Must be reworked to allow multiple OPIs
                    SettlementAccountDetails = GetClientCompanyOpi(trade)
                }).SingleOrDefault();

            if (tradeInformation == null)
                throw new TradeNotFoundException($"Trade with code {tradeCode} does not exist");

            return tradeInformation;
        }

        public BrokerTradeNoteModel GetBrokerTradeInformation(string tradeCode)
        {
            var tradeInformation = _tradeUow
                .GetTrade(tradeCode)
                .Select(trade => new BrokerTradeNoteModel
                {
                    InstructedBy = trade.AuthorisedByClientCompanyContactId.HasValue ? trade.AuthorisedByClientCompanyContact.Fullname : string.Empty,
                    InstructedDateTime = trade.CreatedDate,
                    Method = Enum.GetName(typeof(TradeInstructionMethods), trade.TradeInstructionMethodId.Value),
                    TradeCode = trade.Code,
                    SellCcy = trade.IsBuy
                                ? trade.Rhsccy.Code
                                : trade.Lhsccy.Code,
                    SellAmount = trade.IsBuy
                                ? trade.ClientRhsamt.Value
                                : trade.ClientLhsamt.Value,
                    BuyCcy = trade.IsBuy
                                ? trade.Lhsccy.Code
                                : trade.Rhsccy.Code,
                    BuyAmount = trade.IsBuy
                                ? trade.ClientLhsamt.Value
                                : trade.ClientRhsamt.Value,                    
                    ValueDate = trade.ValueDate.Value,
                    Rate = (double)trade.BrokerRate.Value,
                    ClientCompany = trade.AuthorisedByClientCompanyContact.ClientCompany,
                    SettlementAccountDetails = trade.ClientCompanyOpi,
                    Broker = trade.Broker,
                    Collateral =
                        trade.IsBuy
                            ? CalculateCollateralValue(trade.ClientRhsamt.Value, trade.CollateralPerc.Value)
                            : CalculateCollateralValue(trade.ClientLhsamt.Value, trade.CollateralPerc.Value),
                    CollateralCcy = trade.IsBuy ? trade.Rhsccy.Code : trade.Lhsccy.Code,
                    CurrencyPair = trade.CurrencyPair
                }).SingleOrDefault();

            if (tradeInformation == null)
            {
                throw new TradeNotFoundException($"Trade with code {tradeCode} does not exist");
            }
            else if (tradeInformation.ClientCompany != null && tradeInformation.ClientCompany.DealerAppUserId > 0)
            {
                // getting app user from fxdb
                var appUser = _userService.GetFXDBAppUserById((int)tradeInformation.ClientCompany.DealerAppUserId);
                if(appUser != null)
                {
                    // getting auth user from fxdb
                    var authUser = _userService.GetAuthUserById(appUser.AuthUserId);
                    tradeInformation.DealerAuthUser = authUser;
                }
            }

            return tradeInformation;
        }

        private string GetGeneratedTradeCode(int clientCompanyId)
        {
            var tradeCount = _tradeUow.GetClientCompanyTradeCount(clientCompanyId)
                .Select(x => x.TradeCount)
                .SingleOrDefault();

            return CodeBuilder.FormatTradeCode(tradeCount + 1, clientCompanyId);
        }

        public async Task<IList<FixQuoteResponseModel>> GetQuotesAsync(QuoteRequestModel quoteRequest)
        {
            List<FixQuoteResponseModel> responseList = new List<FixQuoteResponseModel>();

            foreach (var request in quoteRequest.QuoteModels)
            {
                var tradeCode = GetGeneratedTradeCode(quoteRequest.ClientCompanyId.Value);
                var quoteRequestModel = CreateFixQuoteRequestModel(request, tradeCode);

                var response = await _fixQuoteService.GetQuoteAsync(quoteRequestModel);

                if (response == null) throw new FixQuoteException("Cannot get quote");
                response.QuoteIndex = request.QuoteIndex;

                response.ClientRate = CalculateClientRate(quoteRequest.ClientCompanyId.Value, request, response.BrokerRate);
                responseList.Add(response);
            }

            return responseList;
        }

        private decimal CalculateClientRate(int clientCompanyId, QuoteModel request, decimal brokerRate)
        {
            string currency1 = string.Empty;
            string currency2 = string.Empty;
            
            //determine the Buy and Sell to get the correct spread
            if (request.IsRhsMajor)
            {
                if (request.IsBuy)
                {
                    currency1 = request.RhsCcy;
                    currency2 = request.LhsCcy;                    
                }
                else
                {
                    currency1 = request.LhsCcy;
                    currency2 = request.RhsCcy;
                }
            }
            else
            {
                if (request.IsBuy)
                {
                    currency1 = request.LhsCcy;
                    currency2 = request.RhsCcy;
                }
                else
                {
                    currency1 = request.RhsCcy;
                    currency2 = request.LhsCcy;
                }
            }
            
            int spread = _clientCompanyService.GetClientCompanySpread(clientCompanyId,
                currency1, currency2, request.IsBuy,
                request.ValueDate, request.ContractDate);

            decimal clientRate = brokerRate;

            if (request.IsBuy)
            {                
                clientRate = brokerRate + ((decimal)spread / 10000 * brokerRate);                
            }
            else
            {                
                clientRate = brokerRate - ((decimal)spread / 10000 * brokerRate);
            }

            return clientRate;
        }

        public async Task<IList<DealResponseModel>> Deal(DealRequestModel dealRequest)
        {
            List<DealResponseModel> responseList = new List<DealResponseModel>();

            foreach (var deal in dealRequest.DealModels)
            {
                DealResponseModel response = await GetDealResponse(deal, dealRequest.AuthUserId.Value, dealRequest.ClientCompanyId.Value);
                response.TradeIndex = deal.TradeIndex;

                responseList.Add(response);
            }

            return responseList;
        }

        private async Task<DealResponseModel> GetDealResponse(DealModel deal, int authUserId, int clientCompanyId)
        {
            var dealResponse = new DealResponseModel
            {
                IsSuccessful = false,
                ErrorMessage = ""
            };

            if (deal.ExpirationDateTime.AddMinutes(2) <= DateTime.UtcNow) //TODO Remove the AddMinutes and modify the Fix Acceptor instead.
            {
                dealResponse.ErrorMessage = "Quote has expired.";
                return dealResponse;
            }

            var tradeCountObject = _tradeUow.GetTradeCountByPrimaryKey(clientCompanyId);

            tradeCountObject.TradeCount++;
            dealResponse.Code = CodeBuilder.FormatTradeCode(tradeCountObject.TradeCount, clientCompanyId);

            var filledtradeEntity = CreateFilledTradeEntity(deal, dealResponse.Code, clientCompanyId, authUserId);

            dealResponse.IsSuccessful = _tradeUow.CreateDeal(filledtradeEntity, tradeCountObject);

            if (!dealResponse.IsSuccessful)
            {
                dealResponse.ErrorMessage = "Database error while creating Deal";
                return dealResponse;
            }

            bool isFixSuccessful = await FixExecuteQuote(deal, dealResponse, filledtradeEntity);
            FxforwardTrade brokerdtradeEntity = null;
            if (isFixSuccessful)
            {
                brokerdtradeEntity = CreateBrokeredTradeEntity(dealResponse.Code, authUserId);
                dealResponse.IsSuccessful = _tradeUow.BrokerDeal(brokerdtradeEntity, tradeCountObject);

                if (!dealResponse.IsSuccessful) dealResponse.ErrorMessage = "Database error while creating Deal";
            }
            else
            {
                var failedFIXTradeModel = GetFailedFIXTradeInfo(filledtradeEntity.Code);
                _clientSiteActionService.LogActionUnconfirmedTrade(authUserId, failedFIXTradeModel.TradeCode);
                await _emailService.SendFailedFIXTradeEmailAsync(failedFIXTradeModel);
                dealResponse.Code = "";
            }

            if (dealResponse.IsSuccessful)
            {
                SetClientCompanyTradeDetails(brokerdtradeEntity, clientCompanyId, authUserId);
            }

            return dealResponse;
        }

        /// <summary>
        /// Getting trade informations for the sending email
        /// </summary>
        /// <param name="tradeCode">Unique Id for Order/Trade</param>
        /// <returns></returns>
        private FailedFIXTradeModel GetFailedFIXTradeInfo(string tradeCode)
        {
            var aa = _tradeUow.GetTrade(tradeCode);
            var tradeInformation = _tradeUow
                .GetTrade(tradeCode)
                .Select(trade => new FailedFIXTradeModel
                {
                    TradeCode = trade.Code,
                    SellCcy = trade.IsBuy ? trade.Rhsccy.Code : trade.Lhsccy.Code,
                    BuyCcy = trade.IsBuy ? trade.Lhsccy.Code : trade.Rhsccy.Code,
                    ValueDate = trade.ValueDate.Value,
                    Rate = (double?)trade.ClientRate ?? 0,
                    ClientCompany = trade.AuthorisedByClientCompanyContact.ClientCompany,
                }).SingleOrDefault();
            tradeInformation.CurrencyPair = $"{tradeInformation.SellCcy}/{tradeInformation.BuyCcy}";

            if (tradeInformation == null)
                throw new TradeNotFoundException($"Trade with code {tradeCode} does not exist");

            return tradeInformation;
        }

        private async Task<bool> FixExecuteQuote(DealModel deal, DealResponseModel dealResponse, FxforwardTrade tradeEntity)
        {
            try
            {
                var dealRequestModel = CreateFixNewOrderRequestModel(deal, tradeEntity);
                var fixResponse = await _fixQuoteService.NewOrderSingleAsync(dealRequestModel);

                if (fixResponse == null)
                {
                    dealResponse.ErrorMessage = "Deal not done due to an unexpected error, please try again";
                }
                else if (!string.IsNullOrEmpty(fixResponse.ErrorMessage))
                {
                    dealResponse.ErrorMessage = fixResponse.ErrorMessage;
                    dealResponse.IsSuccessful = false;
                }
                else
                {
                    dealResponse.BarclaysAssignedId = fixResponse.BarclaysAssignedId;
                    dealResponse.BarclaysTradeId = fixResponse.BarclaysTradeId;
                    //dealResponse.IsSuccessful comes up as true
                }
            }
            catch (Exception ex)
            {
                //notify user and/or send email
                dealResponse.ErrorMessage = "Deal not done due to an unexpected error, please try again";
                dealResponse.IsSuccessful = false;
            }

            return dealResponse.IsSuccessful;
        }

        private FxforwardTrade CreateFilledTradeEntity(DealModel dealModel, string code, int clientCompanyId, int authUserId)
        {
            var pendingStatus = _tradeUow.GetEmirStatus("Pending");               
            var filledStatus = _tradeUow.GetFxForwardStatus("Filled");
            var tradeInstructionMethod = _tradeUow.GetTradeInstructionMethod("Online");

            var tradeModel = new FxforwardTrade()
            {
                Code = code,
                CreatedDate = DateTime.Now,
                CreatedByAuthUserId = authUserId,
                ClientCompanyId = clientCompanyId,
                AuthorisedByClientCompanyContactId = GetClientCompanyContactId(authUserId),
                ContractDate = DateTime.Now,
                ContractNoteSentToClientDateTime = DateTime.Now,
                ValueDate = dealModel.ValueDate,
                IsOrder = false,
                CurrencyPair = (dealModel.LhsCcy + dealModel.RhsCcy).ToUpper(),
                IsBuy = dealModel.IsBuy,
                Lhsccyid = _currencyService.GetCurrencyId(dealModel.LhsCcy),
                Rhsccyid = _currencyService.GetCurrencyId(dealModel.RhsCcy),
                ClientRate = dealModel.Rate,
                BrokerRate = dealModel.BrokerRate,
                CollateralPerc = 0,
                UpdatedByAuthUserId = authUserId,
                IsRhsmajor = dealModel.IsRhsMajor,
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

                TradeInstructionMethodId = tradeInstructionMethod.Id,
                FxforwardTradeStatusId = filledStatus.Id,
                FilledByAuthUserId = authUserId,
                FilledDateTime = DateTime.Now
            };

            //determine the default account currency needed
            int? currencyId = null;
            
            if (tradeModel.IsRhsmajor.HasValue)
            {
                if (tradeModel.IsRhsmajor.Value)
                {                    
                    if (tradeModel.IsBuy)
                    {
                        currencyId = tradeModel.Rhsccyid;
                    }
                    else
                    {
                        currencyId = tradeModel.Lhsccyid;
                    }                    
                }
                else
                {
                    if (tradeModel.IsBuy)
                    {
                        currencyId = tradeModel.Lhsccyid;
                    }
                    else
                    {
                        currencyId = tradeModel.Rhsccyid;
                    }
                }
            }

            if (currencyId.HasValue)
            {
                var clientCompanyDefaultAccount = _clientCompanyService.GetClientCompanyDefaultAccount(clientCompanyId, currencyId.Value);

                if (clientCompanyDefaultAccount != null)
                {
                    tradeModel.ClientCompanyOpiid = clientCompanyDefaultAccount.ClientCompanyOpiId;
                }
            }

            CalculateAmountsAndProfit(dealModel, tradeModel);

            return tradeModel;
        }

        private FxforwardTrade CreateBrokeredTradeEntity(string code, int authUserId)
        {
            var brokeredStatus = _tradeUow.GetFxForwardStatus("Brokered");
            var broker = _tradeUow.GetBroker("Barclays");

            FxforwardTrade trade = _tradeUow.GetTrade(code).FirstOrDefault();
        
            trade.FxforwardTradeStatusId = brokeredStatus.Id;
            trade.EmirUti = _appSettingService.GetEmirUtiCode() + code.Replace("-", string.Empty).Replace("/", "");
            trade.BrokeredDate = DateTime.Now;
            trade.BrokerId = broker.Id;
            trade.Verified = true;
            trade.VerifiedByAuthUserId = authUserId;

            return trade;
        }

        private int GetClientCompanyContactId(int authUserId)
        {
            return _userService
                .GetApplicationUserByAuthUserId(authUserId)
                .ClientCompanyContactId;
        }

        private void CalculateAmountsAndProfit(DealModel dealModel, FxforwardTrade tradeModel)
        {
            if (dealModel.IsRhsMajor)
            {
                tradeModel.ClientLhsamt = decimal.Round(dealModel.Amount / tradeModel.ClientRate.Value, DecimalPlaces);
                tradeModel.BrokerLhsamt = decimal.Round(dealModel.Amount / tradeModel.BrokerRate.Value, DecimalPlaces);
                tradeModel.ClientRhsamt = dealModel.Amount;
                tradeModel.BrokerRhsamt = dealModel.Amount;

                tradeModel.Profit = tradeModel.IsBuy ? 
                                        tradeModel.BrokerLhsamt - tradeModel.ClientLhsamt : 
                                        tradeModel.ClientLhsamt - tradeModel.BrokerLhsamt;
            }
            else
            {
                tradeModel.ClientLhsamt = dealModel.Amount;
                tradeModel.BrokerLhsamt = dealModel.Amount;
                tradeModel.ClientRhsamt = decimal.Round(dealModel.Amount * tradeModel.ClientRate.Value, DecimalPlaces);
                tradeModel.BrokerRhsamt = decimal.Round(dealModel.Amount * tradeModel.BrokerRate.Value, DecimalPlaces);

                tradeModel.Profit = tradeModel.IsBuy ? 
                                        tradeModel.ClientRhsamt - tradeModel.BrokerRhsamt : 
                                        tradeModel.BrokerRhsamt - tradeModel.ClientRhsamt;
            }

            tradeModel.RemainingClientLhsamt = tradeModel.ClientLhsamt;
            tradeModel.RemainingClientRhsamt = tradeModel.ClientRhsamt;            
        }
        
        private FixQuoteRequestModel CreateFixQuoteRequestModel(QuoteModel quoteRequest, string tradeCode)
        {
            var model = new FixQuoteRequestModel
            {
                TradeCode = tradeCode,
                LHSCCY = quoteRequest.LhsCcy,
                RHSCCY = quoteRequest.RhsCcy,
                MajorCurrency = quoteRequest.IsRhsMajor ? quoteRequest.RhsCcy : quoteRequest.LhsCcy,
                Side = quoteRequest.IsBuy ? 1 : 2,
                BrokerMajorAmount = quoteRequest.Amount,
                ValueDate = quoteRequest.ValueDate.ToString("yyyy-MM-dd"),
                TimeOut = _appSettingService.GetFixTimeout(),
                Duration = _appSettingService.GetStreamingDuration()
            };

            return model;
        }

        private FixNewOrderRequestModel CreateFixNewOrderRequestModel(DealModel deal, FxforwardTrade trade)
        {
            var model = new FixNewOrderRequestModel
            {
                TradeCode = trade.Code,
                QuoteId = deal.QuoteId,
                QuoteReqId = deal.QuoteReqId,
                Price = trade.BrokerRate.Value,
                ClientPrice = trade.ClientRate.Value,
                LHSCCY = deal.LhsCcy,
                RHSCCY = deal.RhsCcy,
                MajorCurrency = trade.IsRhsmajor.Value ? deal.RhsCcy : deal.LhsCcy,
                Side = trade.IsBuy ? 1 : 2,
                BrokerMajorAmount = trade.IsRhsmajor.Value ? trade.BrokerRhsamt.Value : trade.BrokerLhsamt.Value,
                ValueDate = deal.ValueDate.ToString("yyyy-MM-dd"),
                TimeOut = _appSettingService.GetFixTimeout(),
                Duration = _appSettingService.GetStreamingDuration()
            };

            return model;
        }

        public ClientCompanyOpi GetClientCompanyOpi(FxforwardTrade trade)
        {
            return trade.FxforwardTrade2Opi.FirstOrDefault()?.ClientCompanyOpi ?? null;
        }

        public bool SetTradeDefaultOPI(string tradeCode, int clientCompanyId, bool setAsDefault)
        {
            bool isSuccessful = false;
            bool updateTrade = false;

            FxforwardTrade trade = _tradeUow.GetTrade(tradeCode, true);                                
            Currency buyCCY = !trade.IsBuy ? trade.Rhsccy : trade.Lhsccy;

            ClientCompanyCurrencyDefaultOpi clientCompanyCurrencyDefaultOpi = _clientCompanyAccountsUow.GetClientCompanyDefaultAccount(clientCompanyId, buyCCY.Id).SingleOrDefault();
            if (clientCompanyCurrencyDefaultOpi != null)
            {
                if (setAsDefault)
                {
                    trade.ClientCompanyOpi = clientCompanyCurrencyDefaultOpi.ClientCompanyOpi;
                    updateTrade = true;
                }
                else
                {
                    //check if the default OPI is assigned
                    //if assigned, remove it
                    if (trade.ClientCompanyOpi != null && 
                        trade.ClientCompanyOpi.Id == clientCompanyCurrencyDefaultOpi.ClientCompanyOpi.Id)
                    {
                        trade.ClientCompanyOpi = null;
                        updateTrade = true;
                    }
                }
            }

            if (updateTrade)
            {
                _tradeUow.UpdateTrade(trade);                
                isSuccessful = true;
            }                
            
            return isSuccessful;
        }

        public IList<TradeModel> GetClosedTrades(int clientCompanyId)
        {
            var table = _tradeUow.GetClosedTrades(clientCompanyId);
            var list = new List<TradeModel>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                TradeModel tradeModel = MapTradeModelFromDataRow(row);
                list.Add(tradeModel);
            }
            return list;
        }

        private static TradeModel MapTradeModelFromDataRow(DataRow row)
        {
            var tradeModel = new TradeModel()
            {
                TradeId = (string)row["Code"],
                ContractDate = (DateTime)row["ContractDate"],
                ValueDate = (DateTime)row["ValueDate"],
                ClientRate = (decimal)row["ClientRate"],
                SellCcy = (string)row["SellCurrencyCode"],
                BuyCcy = (string)row["BuyCurrencyCode"],
                ClientBuyAmount = (decimal)row["BuyClientAmount"],
                ClientSellAmount = (decimal)row["SellClientAmount"],                
                IsFullPayment = true                
            };

            if (row.Table.Columns.Contains("MajorCCYCode"))
            {
                tradeModel.MajorCcy = (string)row["MajorCCYCode"];
            }

            if (row.Table.Columns.Contains("Reference"))
            {
                tradeModel.Reference = row["Reference"] == DBNull.Value ? string.Empty : (string)row["Reference"];
            }

            if (row.Table.Columns.Contains("RemainingVolume"))
            {
                tradeModel.Balance = (decimal)row["RemainingVolume"];
            }

            if (row.Table.Columns.Contains("FXForwardTradeStatusClientDescription"))
            {
                tradeModel.Status = row["FXForwardTradeStatusClientDescription"] == DBNull.Value ? string.Empty : (string)row["FXForwardTradeStatusClientDescription"];
            }
            else
            {
                if (row.Table.Columns.Contains("FXForwardTradeStatusDescription"))
                {
                    tradeModel.Status = row["FXForwardTradeStatusDescription"] == DBNull.Value ? string.Empty : (string)row["FXForwardTradeStatusDescription"];                    
                }
            }

            if (row.Table.Columns.Contains("IsDefaultOPI"))
            {
                tradeModel.PayToDefaultOPI = (bool)row["IsDefaultOPI"];
            }
            
            return tradeModel;
        }

        private static decimal CalculateCollateralValue(decimal value, decimal collateralPercentage)
        {
            return value * (collateralPercentage / 100);
        }

        private void SetClientCompanyTradeDetails(FxforwardTrade trade, int clientCompanyId, int authUserId)
        {                       
            //update the client company Qualified Trade (if needed)
            ClientCompany clientCompany = _clientCompanyUow.GetClientCompany(clientCompanyId).SingleOrDefault();
            if (clientCompany.AssignNewTrades.HasValue && clientCompany.AssignNewTrades.Value)
            {
                IList<Models.Currencies.CurrencyModel> currencies = _currencyService.GetCurrencies().ToList();
                if (CheckTradeValueThreshold(trade, currencies))
                {
                    if(string.IsNullOrWhiteSpace(clientCompany.QualifiedNewTradeCode))
                    {
                        _clientCompanyUow.UpdateCompanyQualifiedTradeDetails(clientCompanyId, trade.Code, authUserId);
                    }
                }              
            }
            
            //Update client company first trade date if necessary
            _clientCompanyUow.UpdateCompanyFirstTradeDate(clientCompanyId, authUserId);

            //Update client company Last Contract Date
            _clientCompanyUow.UpdateCompanyLastContractDate(clientCompanyId, trade.ContractDate, authUserId);
        }

        /// <summary>
        /// Check if the value of the given trade is over £100k 
        /// </summary>
        /// <param name="trade"></param>
        /// <param name="currencies"></param>
        /// <returns></returns>
        private bool CheckTradeValueThreshold(FxforwardTrade trade, IList<Models.Currencies.CurrencyModel> currencies)
        {
            const decimal ThresholdValue = 100000m;
            const string GBP = "GBP";

            decimal? valueGBP;
            string valueCCYCode = null;
            int valueCCYID;

            decimal? valueAmount;
            decimal? valueGBPRate;
            
            valueCCYID = trade.IsRhsmajor.HasValue ?
                trade.IsRhsmajor.Value ? trade.Rhsccyid.Value : trade.Lhsccyid.Value : 0;

            valueCCYCode = currencies.Where(x => x.Id == valueCCYID).SingleOrDefault().Code;
             
            valueAmount = trade.IsRhsmajor.HasValue ? trade.IsRhsmajor.Value ? trade.ClientRhsamt : trade.ClientLhsamt : null;
            valueGBPRate = trade.PrevailingRate2;
                        
            if (string.Equals(valueCCYCode, GBP, StringComparison.InvariantCultureIgnoreCase))
            {
                valueGBP = valueAmount ?? 0;
            }
            else
            {
                if (valueGBPRate == null)
                {
                    double? currencyPairRate = _currencyService.GetCurrencyPairRate(GBP + valueCCYCode);                    
                    valueGBPRate = currencyPairRate.HasValue ? Convert.ToDecimal(currencyPairRate) : (decimal?)null;
                }

                valueGBP = valueAmount / (valueGBPRate ?? 1);
            }
                        
            return valueGBP.HasValue ? valueGBP.Value >= ThresholdValue : false;            
        }

        private IList<TradeModel> GetUnsettledTradesForBalanceCalculation(int clientCompanyId)
        {
            var table = _tradeUow.GetUnsettledTradesForBalanceCalculation(clientCompanyId);
            var list = new List<TradeModel>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                TradeModel tradeModel = MapTradeModelFromDataRow(row);
                list.Add(tradeModel);
            }
            list.Sort((x, y) => Nullable.Compare(y.ValueDate, x.ValueDate));
            return list;
        }

        public decimal GetTradeBalance(int clientCompanyId, string tradeCode) =>
            GetUnsettledTradesForBalanceCalculation(clientCompanyId).Where(x => x.TradeId == tradeCode).Sum(x => x.Balance);
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _tradeUow?.Dispose();
                    _fixQuoteService?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
