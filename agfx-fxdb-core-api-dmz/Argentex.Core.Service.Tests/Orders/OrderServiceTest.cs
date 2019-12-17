using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Fix;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Models.Email;
using Argentex.Core.Service.Models.Fix;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.Service.Models.Order;
using Argentex.Core.Service.Order;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.Trades;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Argentex.Core.Service.Tests.Orders
{
    public class OrderServiceTest
    {
        [Fact(Skip = "To be reviewed after FIX service update")]
        public void ExecuteOrdersAsync_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var quoteResponse = new FixQuoteResponseModel
            {
                BrokerRate = 1.3m
            };

            var tradeCountObject = new ClientCompanyTradeCount
            {
                TradeCount = 1
            };

            List<FxforwardTrade> list = new List<FxforwardTrade>()
            {
                new FxforwardTrade()
                {
                    Code = "AG0001-0002",
                    Lhsccy = new Currency { Id = 1, Code = "GBP" },
                    Rhsccy = new Currency { Id = 3, Code = "EUR" },
                    TradeInstructionMethodId = 5,
                    IsBuy = true,
                    CreatedDate = DateTime.Now,
                    ValueDate = DateTime.Now,
                    ClientLhsamt = 1000,
                    ClientRhsamt = 1100,
                    CollateralPerc = 0,
                    CurrencyPair = "GBPEUR",
                    BrokerRate = 1.2m,
                    ClientRate = 1.3m,
                    AuthorisedByClientCompanyContact = new DataAccess.Entities.ClientCompanyContact { Id = 1 }
                }
            };

            var mockTradeUow = new Mock<ITradeUow>();
            mockTradeUow.Setup(uow => uow.ExecuteOrder(It.IsAny<FxforwardTrade>(), tradeCountObject))
                .Returns(true);
            mockTradeUow.Setup(uow => uow.RejectOrder(It.IsAny<FxforwardTrade>()));
            mockTradeUow.Setup(uow => uow.GetTradeCountByPrimaryKey(1))
                .Returns(tradeCountObject);
            mockTradeUow.Setup(uow => uow.GetTrade(It.IsAny<string>()))
                .Returns(list.AsQueryable());

            var mockFixService = new Mock<IBarxFxService>();
            mockFixService.Setup(s => s.GetQuoteAsync(It.IsAny<FixQuoteRequestModel>()))
                .Returns(Task.FromResult(quoteResponse));

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetApplicationUserByAuthUserId(It.IsAny<int>()))
                .Returns(new ApplicationServiceUser { ClientCompanyContactId = 1 });

            var mockCurrencyService = new Mock<ICurrencyService>();
            mockCurrencyService.Setup(x => x.GetCurrencyId(It.IsAny<string>()))
                .Returns(1);

            var mockAppSettingService = new Mock<IAppSettingService>();
            mockAppSettingService.Setup(x => x.GetTimeOut()).Returns(15000);
            mockAppSettingService.Setup(x => x.GetStreamingQuoteDuration()).Returns(35);

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendOrderNoteEmailAsync(It.IsAny<OrderNoteModel>())).Returns(Task.CompletedTask);
            mockEmailService.Setup(s => s.SendDealerOrderNoteEmailAsync(It.IsAny<OrderNoteModel>())).Returns(Task.CompletedTask);

            var mockConfig = new Mock<IConfigWrapper>();

            var mockClientCompanyService = new Mock<IClientCompanyService>();
            mockClientCompanyService.Setup(s => s.GetClientCompanySpread(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(0);

            var service = new OrderService(mockTradeUow.Object, 
                mockFixService.Object, mockUserService.Object, 
                mockCurrencyService.Object, mockAppSettingService.Object,
                mockEmailService.Object, mockConfig.Object,
                mockClientCompanyService.Object);

            var orderModel = new OrderModel
            {                
                ClientAmount = 1000,
                RhsCcy = "GBP",
                LhsCcy = "EUR",
                ValueDate = DateTime.Now,
                ClientRate = 1.3m
            };

            OrderRequestModel orderRequest = new OrderRequestModel
            {
                AuthUserId = 1,
                ClientCompanyId = 1,
                OrderModels = new List<OrderModel> { orderModel }
            };

            //Act
            var taskOutput = service.ExecuteOrdersAsync(orderRequest);
            var isCompleted = taskOutput.IsCompletedSuccessfully;

            //Assert
            Assert.True(isCompleted);
            Assert.NotNull(taskOutput);
            Assert.True(taskOutput.Result.Any());
            var firstEntryCode = taskOutput.Result[0].Code;
            Assert.Equal("AG0001-0002", firstEntryCode);
            Assert.True(taskOutput.Result[0].IsSuccessful);
        }

        [Fact(Skip ="To be reviewed after FIX service update")]
        public void ExecuteOrdersAsync_Fails_When_Rate_Is_Not_Favourable()
        {
            //Arrange
            var quoteResponse = new FixQuoteResponseModel
            {
                BrokerRate = 0.8m
            };

            var tradeCountObject = new ClientCompanyTradeCount
            {
                TradeCount = 1
            };

            List<FxforwardTrade> list = new List<FxforwardTrade>()
            {
                new FxforwardTrade()
                {
                    Code = "AG0017-32893",
                    Lhsccy = new Currency { Id = 1, Code = "GBP" },
                    Rhsccy = new Currency { Id = 3, Code = "EUR" },
                    TradeInstructionMethodId = 5,
                    IsBuy = true,
                    CreatedDate = DateTime.Now,
                    ValueDate = DateTime.Now,
                    ClientLhsamt = 1000,
                    ClientRhsamt = 1100,
                    CollateralPerc = 0,
                    CurrencyPair = "GBPEUR",
                    BrokerRate = 1.5m,
                    AuthorisedByClientCompanyContact = new DataAccess.Entities.ClientCompanyContact { Id = 1 }
                }
            };
            var mockTradeUow = new Mock<ITradeUow>();
            mockTradeUow.Setup(uow => uow.ExecuteOrder(It.IsAny<FxforwardTrade>(), tradeCountObject))
                .Returns(true);
            mockTradeUow.Setup(uow => uow.RejectOrder(It.IsAny<FxforwardTrade>()));
            mockTradeUow.Setup(uow => uow.GetTradeCountByPrimaryKey(1))
                .Returns(tradeCountObject);
            mockTradeUow.Setup(uow => uow.GetTrade(It.IsAny<string>()))
                .Returns(list.AsQueryable());

            var mockFixService = new Mock<IBarxFxService>();
            mockFixService.Setup(s => s.GetQuoteAsync(It.IsAny<FixQuoteRequestModel>()))
                .Returns(Task.FromResult(quoteResponse));

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetApplicationUserByAuthUserId(It.IsAny<int>()))
                .Returns(new ApplicationServiceUser { ClientCompanyContactId = 1 });

            var mockCurrencyService = new Mock<ICurrencyService>();
            mockCurrencyService.Setup(x => x.GetCurrencyId(It.IsAny<string>()))
                .Returns(1);

            var mockAppSettingService = new Mock<IAppSettingService>();
            mockAppSettingService.Setup(x => x.GetTimeOut()).Returns(15000);
            mockAppSettingService.Setup(x => x.GetStreamingQuoteDuration()).Returns(35);


            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendOrderNoteEmailAsync(It.IsAny<OrderNoteModel>())).Returns(Task.CompletedTask);
            mockEmailService.Setup(s => s.SendDealerOrderNoteEmailAsync(It.IsAny<OrderNoteModel>())).Returns(Task.CompletedTask);

            var mockConfig = new Mock<IConfigWrapper>();

            var mockClientCompanyService = new Mock<IClientCompanyService>();
            mockClientCompanyService.Setup(s => s.GetClientCompanySpread(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(0);

            var service = new OrderService(mockTradeUow.Object,
                mockFixService.Object, mockUserService.Object,
                mockCurrencyService.Object, mockAppSettingService.Object,
                mockEmailService.Object, mockConfig.Object,
                mockClientCompanyService.Object);

            
            var orderModel = new OrderModel
            {
                ClientAmount = 1000,
                ClientRate = 1.5m,
                RhsCcy = "GBP",
                LhsCcy = "EUR",
                ValueDate = DateTime.Now
            };

            OrderRequestModel orderRequest = new OrderRequestModel
            {
                AuthUserId = 1,
                ClientCompanyId = 1,
                OrderModels = new List<OrderModel> { orderModel }
            };
            
            //Act
            var taskOutput = service.ExecuteOrdersAsync(orderRequest);
            var isCompleted = taskOutput.IsCompletedSuccessfully;
            //Assert
            Assert.True(isCompleted);
            Assert.NotNull(taskOutput);
            Assert.True(taskOutput.Result[0].IsSuccessful);
        }

        [Fact(Skip = "To be reviewed after FIX service update")]
        public void GetOpenOrders()
        {
            DateTime createdDate = new DateTime(2018, 8, 10);
            DateTime valueDate = new DateTime(2018, 8, 20);
            DateTime validityDate = new DateTime(2018, 9, 1);

            var trades = new List<FxforwardTrade>();
            Currency currencyEUR = new Currency { Code = "EUR" };
            Currency currencGBP = new Currency { Code = "GBP" };

            const string TradeCode1 = "ARG0067-0001";
            const string TradeCode2 = "ARG0067-0002";

            trades.Add(new FxforwardTrade()
            {
                Code = TradeCode1,
                CreatedDate = createdDate,
                ValueDate = valueDate,
                OpenValueDate = validityDate,
                Rhsccy = currencyEUR,
                Lhsccy = currencGBP,
                IsBuy = true,
                ClientRhsamt = 1000,
                ClientLhsamt = 1200,
                ClientRate = 1.2m
            });

            
            trades.Add(new FxforwardTrade()
            {
                Code = TradeCode2,
                CreatedDate = createdDate,
                ValueDate = valueDate,
                OpenValueDate = validityDate,
                Rhsccy = currencyEUR,
                Lhsccy = currencGBP,
                IsBuy = false,
                ClientRhsamt = 2000,
                ClientLhsamt = 2200,
                ClientRate = 1.5m
            });

            var mockTradeUow = new Mock<ITradeUow>();
            mockTradeUow.Setup(uow => uow.GetOpenOrders(It.IsAny<int>())).Returns(trades.AsQueryable);
            var mockFixService = new Mock<IBarxFxService>();            
            var mockUserService = new Mock<IUserService>();
            var mockCurrencyService = new Mock<ICurrencyService>();            
            var mockAppSettingService = new Mock<IAppSettingService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockConfig = new Mock<IConfigWrapper>();

            var service = new OrderService(mockTradeUow.Object,
                mockFixService.Object, mockUserService.Object,
                mockCurrencyService.Object, mockAppSettingService.Object,
                mockEmailService.Object, mockConfig.Object,
                null);

            var list = service.GetOpenOrders(1);

            Assert.NotNull(list);
            Assert.IsType<List<Models.Trade.TradeModel>>(list);
            Assert.Equal(2, list.Count);

            Models.Trade.TradeModel tradeModel = list[0];

            Assert.Equal(TradeCode1, tradeModel.TradeId);
            Assert.Equal(1.2m, tradeModel.ClientRate);
            Assert.Equal(createdDate, tradeModel.CreatedDate);
            Assert.Equal(valueDate, tradeModel.ValueDate);
            Assert.Equal(validityDate, tradeModel.ValidityDate);
            Assert.Equal(1000, tradeModel.ClientSellAmount);
            Assert.Equal("EUR", tradeModel.SellCcy);
            Assert.Equal(1200, tradeModel.ClientBuyAmount);
            Assert.Equal("GBP", tradeModel.BuyCcy);

            tradeModel = list[1];

            Assert.Equal(TradeCode2, tradeModel.TradeId);
            Assert.Equal(1.5m, tradeModel.ClientRate);
            Assert.Equal(createdDate, tradeModel.CreatedDate);
            Assert.Equal(valueDate, tradeModel.ValueDate);
            Assert.Equal(validityDate, tradeModel.ValidityDate);
            Assert.Equal(2200, tradeModel.ClientSellAmount);
            Assert.Equal("GBP", tradeModel.SellCcy);
            Assert.Equal(2000, tradeModel.ClientBuyAmount);
            Assert.Equal("EUR", tradeModel.BuyCcy);
        }
        [Fact]
        public void GetOpenOrders_No_Trades()
        {
            var trades = new List<FxforwardTrade>();
         
            var mockTradeUow = new Mock<ITradeUow>();
            mockTradeUow.Setup(uow => uow.GetOpenOrders(It.IsAny<int>())).Returns(trades.AsQueryable);
            var mockFixService = new Mock<IBarxFxService>();
            var mockUserService = new Mock<IUserService>();
            var mockCurrencyService = new Mock<ICurrencyService>();
            var mockAppSettingService = new Mock<IAppSettingService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockConfig = new Mock<IConfigWrapper>();

            var service = new OrderService(mockTradeUow.Object,
                mockFixService.Object, mockUserService.Object,
                mockCurrencyService.Object, mockAppSettingService.Object,
                mockEmailService.Object, mockConfig.Object,
                null);

            var list = service.GetOpenOrders(1);

            Assert.NotNull(list);
            Assert.Equal(0, list.Count);           
        }
    }
}