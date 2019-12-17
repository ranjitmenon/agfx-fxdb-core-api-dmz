using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Email.EmailSender;
using Argentex.Core.Service.Enums;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Fix;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Models.Email;
using Argentex.Core.Service.Models.Fix;
using Argentex.Core.Service.Models.Order;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.Trades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly ITradeUow _tradeUow;
        private readonly IBarxFxService _fixQuoteService;
        private readonly IUserService _userService;
        private readonly ICurrencyService _currencyService;
        private readonly IAppSettingService _appSettingService;
        private readonly IEmailService _emailService;
        private readonly IConfigWrapper _config;
        private readonly IClientCompanyService _clientCompanyService;

        public OrderService(ITradeUow uow
            , IBarxFxService fixQuoteService
            , IUserService userService
            , ICurrencyService currencyService
            , IAppSettingService appSetting
            , IEmailService emailService
            , IConfigWrapper config
            , IClientCompanyService clientCompanyService)
        {
            _tradeUow = uow;
            _fixQuoteService = fixQuoteService;
            _userService = userService;
            _currencyService = currencyService;
            _appSettingService = appSetting;
            _emailService = emailService;
            _config = config;
            _clientCompanyService = clientCompanyService;
        }

        public async Task<IList<OrderResponseModel>> ExecuteOrdersAsync(OrderRequestModel orderRequest)
        {
            //IMPORTANT: The call to the FIX service to get the quotes will be temporarily disabled 
            //(other related service calls and related methods have been commented out too)
            //Do not activate this without a prior discussion with the Product Owner!


            List<OrderResponseModel> responseList = new List<OrderResponseModel>();
            //_fixQuoteService.SetHttpTimeout(new TimeSpan(0, 0, 0, 0, _appSettingService.GetTimeOut()));

            foreach (var order in orderRequest.OrderModels)
            {
                var response = new OrderResponseModel { IsSuccessful = false };
                var tradeCountObject = _tradeUow.GetTradeCountByPrimaryKey(orderRequest.ClientCompanyId.Value);

                tradeCountObject.TradeCount++;
                response.Code = CodeBuilder.FormatTradeCode(tradeCountObject.TradeCount, orderRequest.ClientCompanyId.Value);

                var quoteRequest = CreateQuoteRequestModel(order, response.Code);
                //var orderResponse = await _fixQuoteService.GetQuoteAsync(quoteRequest);
                //bool isOrderFavourable = CheckOrderValidity(order, orderResponse, orderRequest.ClientCompanyId.Value);

                bool isOrderFavourable = true;
                if (isOrderFavourable)
                {
                    var orderEntity = CreateTradeEntity(order, response.Code, orderRequest.ClientCompanyId.Value, orderRequest.AuthUserId.Value);
                    response.IsSuccessful = _tradeUow.ExecuteOrder(orderEntity, tradeCountObject);
                    // sending email to notify client and dealer
                    if (response.IsSuccessful)
                    {
                        var orderNoteModel = GetOrderNote(response.Code);
                        await _emailService.SendOrderNoteEmailAsync(orderNoteModel);
                        await _emailService.SendDealerOrderNoteEmailAsync(orderNoteModel);
                    }
                }
                else
                {
                    response.Code = "";
                    response.ErrorMessage = "You have attempted to create an order with a worse rate than the current forward rate. Please alter the rate before re-submitting your order. ";
                }

                response.OrderIndex = order.OrderIndex;
                responseList.Add(response);
            }

            return responseList;
        }

        public IList<Models.Trade.TradeModel> GetOpenOrders(int clientCompanyId)
        {
            var list = new List<Models.Trade.TradeModel>();

            list = _tradeUow
                .GetOpenOrders(clientCompanyId)
                .Select(trade => new Models.Trade.TradeModel
                {
                    TradeId = trade.Code,
                    ClientRate = trade.ClientRate,
                    CreatedDate = trade.CreatedDate,
                    ValueDate = trade.ValueDate.Value,
                    ValidityDate = trade.OpenValueDate.Value,
                    SellCcy = trade.IsBuy
                                ? trade.Rhsccy.Code 
                                : trade.Lhsccy.Code,
                    ClientSellAmount = trade.IsBuy 
                                ? trade.ClientRhsamt.Value
                                : trade.ClientLhsamt.Value,
                    BuyCcy = trade.IsBuy 
                                ? trade.Lhsccy.Code 
                                : trade.Rhsccy.Code,
                    ClientBuyAmount = trade.IsBuy 
                                ? trade.ClientLhsamt.Value 
                                : trade.ClientRhsamt.Value
                }).ToList();

            return list;
        }

        /// <summary>
        /// Getting open orders that have the validity date expired
        /// </summary>
        /// <returns>CancelOrderModel for send cancel order email</returns>
        public IList<CancelOrderModel> GetExpiredValidityOrders()
        {
            var list = new List<CancelOrderModel>();

            list = _tradeUow
                .GetExpiredValidityOrders()
                .Select(trade => new CancelOrderModel
                {
                    InstructedBy = trade.AuthorisedByClientCompanyContactId.HasValue ? trade.AuthorisedByClientCompanyContact.Fullname : string.Empty,
                    InstructedDateTime = trade.CreatedDate,
                    Method = Enum.GetName(typeof(TradeInstructionMethods), trade.TradeInstructionMethodId ?? 0),
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
                    Rate = (double?)trade.BrokerRate ?? 0,
                    Collateral =
                        trade.IsBuy
                            ? CalculateCollateralValue(trade.ClientRhsamt ?? 0, trade.CollateralPerc ?? 0)
                            : CalculateCollateralValue(trade.ClientLhsamt ?? 0, trade.CollateralPerc ?? 0),
                    CollateralCcy = trade.IsBuy ? trade.Rhsccy.Code : trade.Lhsccy.Code,
                    CurrencyPair = trade.CurrencyPair,
                    ClientEmail = trade.AuthorisedByClientCompanyContactId.HasValue ? trade.AuthorisedByClientCompanyContact.Email : string.Empty,
                    ClientCompany = trade.AuthorisedByClientCompanyContact.ClientCompany,
                    SettlementAccountDetails = trade.ClientCompanyOpi
                }).ToList();
            return list;
        }

        /// <summary>
        /// Canceling an order (Trade) that is in pending status
        /// This is done by user action
        /// </summary>
        /// <param name="tradeCode">Unique Id for Order/Trade</param>
        /// <returns></returns>
        public async Task<bool> CancelOrderAsync(string tradeCode)
        {
            var orderIsDeleted = await _tradeUow.CancelOrder(tradeCode);
            if (orderIsDeleted)
            {
                var model = GetOrderInformation(tradeCode);
                await _emailService.SendCancelOrderEmailAsync(model);
            }
            return orderIsDeleted;
        }

        /// <summary>
        /// Canceling an order (Trade) that is in pending status
        /// This is done by service in the scheduled hours
        /// </summary>
        /// <param name="model">CancelOrderModel that is used to send the email for the client</param>
        /// <returns></returns>
        public async Task<bool> CancelOrderAsync(CancelOrderModel model)
        {
            var orderIsDeleted = await _tradeUow.CancelOrder(model.TradeRef);
            if (orderIsDeleted)
                await _emailService.SendCancelOrderEmailAsync(model);
            return orderIsDeleted;
        }

        #region Private methods

        /// <summary>
        /// Getting order informations for the sending email
        /// </summary>
        /// <param name="tradeCode">Unique Id for Order/Trade</param>
        /// <returns></returns>
        private OrderNoteModel GetOrderNote(string tradeCode)
        {
            var orderNote = _tradeUow
                .GetTrade(tradeCode)
                .Select(trade => new OrderNoteModel
                {
                    InstructedBy = trade.AuthorisedByClientCompanyContactId.HasValue ? trade.AuthorisedByClientCompanyContact.Fullname : string.Empty,
                    InstructedDateTime = trade.CreatedDate,
                    Method = Enum.GetName(typeof(TradeInstructionMethods), trade.TradeInstructionMethodId ?? 0),
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
                    Rate = (double?)trade.ClientRate ?? 0,
                    CurrencyPair = trade.CurrencyPair,
                    ClientEmail = trade.AuthorisedByClientCompanyContactId.HasValue ? trade.AuthorisedByClientCompanyContact.Email : string.Empty,
                    ClientCompany = trade.AuthorisedByClientCompanyContact.ClientCompany,
                    ValidityDate = trade.OpenValueDate,
                    CreatedDate = trade.CreatedDate,
                }).FirstOrDefault();

            if (orderNote == null)
            {
                throw new TradeNotFoundException($"Trade with code {tradeCode} does not exist");
            }
            else if (orderNote.ClientCompany != null && orderNote.ClientCompany.DealerAppUserId > 0)
            {
                var appUser = _userService.GetFXDBAppUserById((int)orderNote.ClientCompany.DealerAppUserId);
                var authUser = _userService.GetApplicationUserByAuthUserId(appUser.AuthUserId);
                orderNote.DealerAuthUser = authUser;
            }

            return orderNote;
        }

        /// <summary>
        /// Getting order informations for the sending email
        /// </summary>
        /// <param name="tradeCode">Unique Id for Order/Trade</param>
        /// <returns></returns>
        private CancelOrderModel GetOrderInformation(string tradeCode)
        {
            var tradeInformation = _tradeUow
                .GetTrade(tradeCode)
                .Select(trade => new CancelOrderModel
                {
                    InstructedBy = trade.AuthorisedByClientCompanyContactId.HasValue ? trade.AuthorisedByClientCompanyContact.Fullname : string.Empty,
                    InstructedDateTime = trade.CreatedDate,
                    Method = Enum.GetName(typeof(TradeInstructionMethods), trade.TradeInstructionMethodId ?? 0),
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
                    Rate = (double?)trade.ClientRate ?? 0,
                    Collateral =
                        trade.IsBuy
                            ? CalculateCollateralValue(trade.ClientRhsamt ?? 0, trade.CollateralPerc ?? 0)
                            : CalculateCollateralValue(trade.ClientLhsamt ?? 0, trade.CollateralPerc ?? 0),
                    CollateralCcy = trade.IsBuy ? trade.Rhsccy.Code : trade.Lhsccy.Code,
                    CurrencyPair = trade.CurrencyPair,
                    ClientEmail = trade.AuthorisedByClientCompanyContactId.HasValue ? trade.AuthorisedByClientCompanyContact.Email : string.Empty,
                    ClientCompany = trade.AuthorisedByClientCompanyContact.ClientCompany,
                    SettlementAccountDetails = trade.ClientCompanyOpi,

                    ValidityDate = trade.OpenValueDate,
                    CreatedDate = trade.CreatedDate
                }).SingleOrDefault();

            if (tradeInformation == null)
                throw new TradeNotFoundException($"Trade with code {tradeCode} does not exist");

            return tradeInformation;
        }

        private FixQuoteRequestModel CreateQuoteRequestModel(OrderModel order, string tradeCode)
        {
            return new FixQuoteRequestModel
            {
                TradeCode = tradeCode,
                LHSCCY = order.LhsCcy,
                RHSCCY = order.RhsCcy,
                MajorCurrency = order.IsRhsMajor ? order.RhsCcy : order.LhsCcy,
                Side = order.IsBuy ? 1 : 2,
                BrokerMajorAmount = order.ClientAmount,
                TimeOut = _appSettingService.GetTimeOut(),
                Duration = _appSettingService.GetStreamingQuoteDuration(),
                ValueDate = order.ValueDate.ToString("yyyy-MM-dd")
            };
        }

        private FxforwardTrade CreateTradeEntity(OrderModel orderModel, string code, int clientCompanyId, int authUserId)
        {
            const int Pending = 1;//TODO - possibly retrieve from FXDB
            const int Generating = 2;//TODO - possibly retrieve from FXDB
            var tradeModel = new FxforwardTrade()
            {
                Code = code,
                CreatedDate = DateTime.Now,
                CreatedByAuthUserId = authUserId,
                ClientCompanyId = clientCompanyId,
                AuthorisedByClientCompanyContactId = GetClientCompanyContactId(authUserId),
                Verified = false,
                ContractDate = DateTime.Now,
                ValueDate = orderModel.ValueDate,
                OpenValueDate = orderModel.ValidityDate,
                IsOrder = true,
                CurrencyPair = orderModel.LhsCcy + orderModel.RhsCcy,
                IsBuy = orderModel.IsBuy,
                Lhsccyid = _currencyService.GetCurrencyId(orderModel.LhsCcy),
                Rhsccyid = _currencyService.GetCurrencyId(orderModel.RhsCcy),
                ClientRate = orderModel.ClientRate,
                UpdatedByAuthUserId = authUserId,
                FxforwardTradeStatusId = Pending,
                IsRhsmajor = orderModel.IsRhsMajor,
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
                EmirstatusId = Generating,
                ApastatusId = Generating,
                ArmstatusId = Generating,
                TradeInstructionMethodId = (int)TradeInstructionMethods.Online
            };
            CalculateClientAmounts(orderModel, tradeModel);

            return tradeModel;
        }

        private int GetClientCompanyContactId(int authUserId)
        {
            return _userService
                .GetApplicationUserByAuthUserId(authUserId)
                .ClientCompanyContactId;
        }

        private void CalculateClientAmounts(OrderModel orderModel, FxforwardTrade tradeModel)
        {
            if (orderModel.IsRhsMajor)
            {
                tradeModel.ClientLhsamt = orderModel.ClientAmount / orderModel.ClientRate;
                tradeModel.ClientRhsamt = orderModel.ClientAmount;
                tradeModel.BrokerRhsamt = orderModel.ClientAmount;
            }
            else
            {
                tradeModel.ClientLhsamt = orderModel.ClientAmount;
                tradeModel.ClientRhsamt = orderModel.ClientAmount * orderModel.ClientRate;
                tradeModel.BrokerLhsamt = orderModel.ClientAmount;
            }
        }

        private bool CheckOrderValidity(OrderModel orderModel, FixQuoteResponseModel fixQuoteResponse, int clientCompanyId)
        {
            decimal finalRate;
            bool isOrderValid;

            int spread = _clientCompanyService.GetClientCompanySpread(clientCompanyId,
                orderModel.LhsCcy, orderModel.RhsCcy, orderModel.IsBuy,
                orderModel.ValueDate, DateTime.Now);

            if (orderModel.IsBuy)
            {
                finalRate = (fixQuoteResponse.BrokerRate + ((decimal)spread / 10000)) * 1.1m;

                isOrderValid = (orderModel.ClientRate <= finalRate);
            }
            else
            {
                finalRate = (fixQuoteResponse.BrokerRate + ((decimal)spread / 10000)) * 0.9m;

                isOrderValid = (orderModel.ClientRate >= finalRate);
            }

            return isOrderValid;
        }

        private static decimal CalculateCollateralValue(decimal value, decimal collateralPercentage)
        {
            return value * (collateralPercentage / 100);
        }

        #endregion

    }
}