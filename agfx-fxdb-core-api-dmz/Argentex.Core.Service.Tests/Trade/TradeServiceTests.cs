using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Fix;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Models.Email;
using Argentex.Core.Service.Models.Fix;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.Service.Models.Trade;
using Argentex.Core.Service.Models.Trades;
using Argentex.Core.Service.Trade;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.ClientCompanies;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts;
using Argentex.Core.UnitsOfWork.Trades;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Argentex.Core.Service.Tests.Trade
{
    public class TradeServiceTests
    {
        [Fact]
        public void GetUnsettledTrades_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var mockTradeUow = new Mock<ITradeUow>();
            var mockClientCompanyAccountsUoW = new Mock<IClientCompanyAccountsUoW>();
            var mockFixService = new Mock<IBarxFxService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockConfig = new Mock<IConfigWrapper>();
            var mockDataTable = new DataTable();

            mockTradeUow.Setup(x => x.GetUnsettledTrades(1))
                .Returns(mockDataTable);

            var service = new TradeService(mockTradeUow.Object, mockFixService.Object, mockEmailService.Object, mockConfig.Object, mockClientCompanyAccountsUoW.Object, null, null, null, null, null, null);

            //Act
            var result = service.GetUnsettledTrades(1);

            //Assert
            Assert.IsType<List<TradeModel>>(result);
        }

        [Fact]
        public void Given_There_Is_No_Trade_Associated_With_The_Code_An_Exception_Should_Be_Thrown()
        {
            // Given
            var tradeCode = "TestTradeCode";
            var tradeUowMock = new Mock<ITradeUow>();
            var clientCompanyAccountsUoWMock = new Mock<IClientCompanyAccountsUoW>();
            var fixServiceMock = new Mock<IBarxFxService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockConfig = new Mock<IConfigWrapper>();

            var trades = new List<FxforwardTrade>();
            
            tradeUowMock.Setup(x => x.GetTrade(It.IsAny<string>())).Returns(trades.AsQueryable);

            var service = new TradeService(tradeUowMock.Object, fixServiceMock.Object, mockEmailService.Object, mockConfig.Object, clientCompanyAccountsUoWMock.Object, null, null, null, null, null, null);

            var expectedMessage = $"Trade with code {tradeCode} does not exist";

            // When
            var result = Assert.Throws<TradeNotFoundException>(() => service.GetTradeInformation(tradeCode));

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void Given_There_Is_A_Trade_Associated_With_The_Code_A_Model_Should_Be_Returned_With_Trade_Information()
        {
            // Given
            var now = DateTime.Now;
            var today = DateTime.Today;

            var trade = new FxforwardTrade
            {
                Code = "Trade 42",
                Lhsccy = new Currency { Id = 1, Code = "GBP" },
                Rhsccy = new Currency { Id = 3, Code = "EUR" },
                TradeInstructionMethodId = 2,
                IsBuy = true,
                IsRhsmajor = true,                
                CreatedDate = now,
                ValueDate = today,
                ClientLhsamt = 25000,
                ClientRhsamt = 15000,
                CollateralPerc = 25,
                CurrencyPair = "GBPEUR",
                BrokerRate = 1.2m,
                ClientRate = 1.3m,                
                AuthorisedByClientCompanyContact = new DataAccess.Entities.ClientCompanyContact
                {
                    Id = 1,
                    ClientCompany = new ClientCompany { Id = 1496, Name = "17 Capital" },
                    Email = "test@17capital.com"
                },
                ClientCompanyOpi = new ClientCompanyOpi() { Id = 10}            
            };

            var tradeUowMock = new Mock<ITradeUow>();
            var clientCompanyAccountsUoWMock = new Mock<IClientCompanyAccountsUoW>();
            var fixServiceMock = new Mock<IBarxFxService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockConfig = new Mock<IConfigWrapper>();

            var trades = new List<FxforwardTrade> { trade };

            tradeUowMock.Setup(x => x.GetTrade(It.IsAny<string>())).Returns(trades.AsQueryable);

            var mockClientCompanyService = new Mock<IClientCompanyService>();
            mockClientCompanyService.Setup(s => s.GetClientCompanySpread(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(0);


            var service = new TradeService(tradeUowMock.Object, fixServiceMock.Object, mockEmailService.Object, mockConfig.Object, clientCompanyAccountsUoWMock.Object, null, null, null, null, mockClientCompanyService.Object, null);

            // When
            var result = service.GetTradeInformation(trade.Code);

            // Then
            Assert.NotNull(result);
            Assert.Equal(typeof(FxForwardTradeInformationModel), result.GetType());
            Assert.Equal(string.Empty, result.InstructedBy);
            Assert.Equal(now, result.InstructedDateTime);
            Assert.Equal("Email", result.Method);
            Assert.Equal(trade.Code, result.TradeRef);
            Assert.Equal("EUR", result.SellCcy);
            Assert.Equal(trade.ClientLhsamt.Value, result.BuyAmount);
            Assert.Equal("GBP", result.BuyCcy);
            Assert.Equal(trade.ClientRhsamt.Value, result.SellAmount);
            Assert.Equal(1.3, result.Rate);
            Assert.Equal(today, result.ValueDate);
            Assert.Equal(3750, result.Collateral);
            Assert.Equal("EUR", result.CollateralCcy);
            Assert.Equal("GBPEUR", result.CurrencyPair);
        }

        [Fact]
        public void GetClosedTrades_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var mockTradeUow = new Mock<ITradeUow>();
            var mockClientCompanyAccountsUoW = new Mock<IClientCompanyAccountsUoW>();
            var mockFixService = new Mock<IBarxFxService>();
            var mockEmailService = new Mock<IEmailService>();
            var mockConfig = new Mock<IConfigWrapper>();
            var mockDataTable = new DataTable();
            mockDataTable.Columns.Add(new DataColumn("Code", typeof(string)));
            mockDataTable.Columns.Add(new DataColumn("ContractDate", typeof(DateTime)));
            mockDataTable.Columns.Add(new DataColumn("ValueDate", typeof(DateTime)));
            mockDataTable.Columns.Add(new DataColumn("ClientRate", typeof(decimal)));
            mockDataTable.Columns.Add(new DataColumn("SellCurrencyCode", typeof(string)));
            mockDataTable.Columns.Add(new DataColumn("BuyCurrencyCode", typeof(string)));
            mockDataTable.Columns.Add(new DataColumn("BuyClientAmount", typeof(decimal)));
            mockDataTable.Columns.Add(new DataColumn("SellClientAmount", typeof(decimal)));
            mockDataTable.Columns.Add(new DataColumn("Reference", typeof(string)));
            mockDataTable.Columns.Add(new DataColumn("RemainingVolume", typeof(decimal)));
            mockDataTable.Columns.Add(new DataColumn("FXForwardTradeStatusDescription", typeof(string)));
            mockDataTable.Columns.Add(new DataColumn("IsDefaultOPI", typeof(bool)));

            const string expectedTradeCode1 = "AG1000-0001";
            DateTime expectedContractDate1 = new DateTime(2018, 6, 1);
            DateTime expectedValueDate1 = new DateTime(2018, 7, 1);
            const decimal expectedClientRate1 = 1.3733m;
            const string expectedBuyCurrencyCode1 = "EUR";
            const string expectedSellCurrencyCode1 = "GBP";
            const decimal expectedBuyClientAmount1 = 294000.00m;
            decimal expectedSellClientAmount1 = expectedBuyClientAmount1 / expectedClientRate1;
            const string expectedReference1 = "REF-" + expectedTradeCode1;
            const decimal expectedRemainingVolume1 = 4000.00m;
            const string expectedStatus1 = "Brokered";
            bool expectedIsDefaultOPI1 = true;


            const string expectedTradeCode2 = "AG1000-0002";
            DateTime expectedContractDate2 = new DateTime(2018, 6, 4);
            DateTime expectedValueDate2 = new DateTime(2018, 7, 2);
            const decimal expectedClientRate2 = 1.2701m;
            const string expectedSellCurrencyCode2 = "EUR";
            const string expectedBuyCurrencyCode2 = "GBP";
            const decimal expectedSellClientAmount2  = 220000.00m;
            decimal expectedBuyClientAmount2 = expectedSellClientAmount2 / expectedClientRate2;
            const string expectedReference2 = "REF-" + expectedTradeCode2;
            const decimal expectedRemainingVolume2 = 1000.00m;
            const string expectedStatus2 = "Brokered";
            bool expectedIsDefaultOPI2 = false;

            DataRow mockDataRow = mockDataTable.NewRow();
            mockDataRow["Code"] = expectedTradeCode1;            
            mockDataRow["ContractDate"] = expectedContractDate1;            
            mockDataRow["ValueDate"] = expectedValueDate1;
            mockDataRow["ClientRate"] = expectedClientRate1;
            mockDataRow["SellCurrencyCode"] = expectedSellCurrencyCode1;
            mockDataRow["BuyCurrencyCode"] = expectedBuyCurrencyCode1;            
            mockDataRow["BuyClientAmount"] = expectedBuyClientAmount1;
            mockDataRow["SellClientAmount"] = expectedSellClientAmount1;
            mockDataRow["Reference"] = expectedReference1;
            mockDataRow["RemainingVolume"] = expectedRemainingVolume1;
            mockDataRow["FXForwardTradeStatusDescription"] = expectedStatus1;
            mockDataRow["IsDefaultOPI"] = expectedIsDefaultOPI1;

            mockDataTable.Rows.Add(mockDataRow);
            
            mockDataRow = mockDataTable.NewRow();
            mockDataRow["Code"] = expectedTradeCode2;
            mockDataRow["ContractDate"] = expectedContractDate2;
            mockDataRow["ValueDate"] = expectedValueDate2;
            mockDataRow["ClientRate"] = expectedClientRate2;
            mockDataRow["SellCurrencyCode"] = expectedSellCurrencyCode2;
            mockDataRow["BuyCurrencyCode"] = expectedBuyCurrencyCode2;
            mockDataRow["BuyClientAmount"] = expectedBuyClientAmount2;
            mockDataRow["SellClientAmount"] = expectedSellClientAmount2;
            mockDataRow["Reference"] = expectedReference2;
            mockDataRow["RemainingVolume"] = expectedRemainingVolume2;
            mockDataRow["FXForwardTradeStatusDescription"] = expectedStatus2;
            mockDataRow["IsDefaultOPI"] = expectedIsDefaultOPI2;

            mockDataTable.Rows.Add(mockDataRow);
            
            mockTradeUow.Setup(x => x.GetClosedTrades(1)).Returns(mockDataTable);

            var service = new TradeService(mockTradeUow.Object, mockFixService.Object, mockEmailService.Object, mockConfig.Object, mockClientCompanyAccountsUoW.Object, null, null, null, null, null, null);

            //Act
            var result = service.GetClosedTrades(1);

            //Assert
            Assert.IsType<List<TradeModel>>(result);

            List<TradeModel> list = (List<TradeModel>)result;
            Assert.Equal(2, list.Count);

            TradeModel tradeModel = list[0];

            Assert.Equal(expectedTradeCode1, tradeModel.TradeId);
            Assert.Equal(expectedContractDate1, tradeModel.ContractDate);
            Assert.Equal(expectedValueDate1, tradeModel.ValueDate);
            Assert.Equal(expectedClientRate1, tradeModel.ClientRate);
            Assert.Equal(expectedSellCurrencyCode1, tradeModel.SellCcy);
            Assert.Equal(expectedBuyCurrencyCode1, tradeModel.BuyCcy);
            Assert.Equal(expectedSellClientAmount1, tradeModel.ClientSellAmount);
            Assert.Equal(expectedBuyClientAmount1, tradeModel.ClientBuyAmount);
            Assert.Equal(expectedReference1, tradeModel.Reference);
            Assert.Equal(expectedRemainingVolume1, tradeModel.Balance);
            Assert.Equal(expectedStatus1, tradeModel.Status);
            Assert.Equal(expectedIsDefaultOPI1, tradeModel.PayToDefaultOPI);

            tradeModel = list[1];
            Assert.Equal(expectedTradeCode2, tradeModel.TradeId);
            Assert.Equal(expectedContractDate2, tradeModel.ContractDate);
            Assert.Equal(expectedValueDate2, tradeModel.ValueDate);
            Assert.Equal(expectedClientRate2, tradeModel.ClientRate);
            Assert.Equal(expectedSellCurrencyCode2, tradeModel.SellCcy);
            Assert.Equal(expectedBuyCurrencyCode2, tradeModel.BuyCcy);
            Assert.Equal(expectedSellClientAmount2, tradeModel.ClientSellAmount);
            Assert.Equal(expectedBuyClientAmount2, tradeModel.ClientBuyAmount);
            Assert.Equal(expectedReference2, tradeModel.Reference);
            Assert.Equal(expectedRemainingVolume2, tradeModel.Balance);
            Assert.Equal(expectedStatus2, tradeModel.Status);
            Assert.Equal(expectedIsDefaultOPI2, tradeModel.PayToDefaultOPI);
        }

        [Fact]
        public void GetQuotesAsync_Successful_When_It_Has_The_Correct_Input()
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

            var mockTradeUow = new Mock<ITradeUow>();
            mockTradeUow.Setup(uow => uow.ExecuteOrder(It.IsAny<FxforwardTrade>(), tradeCountObject))
                .Returns(true);
            mockTradeUow.Setup(uow => uow.RejectOrder(It.IsAny<FxforwardTrade>()));
            mockTradeUow.Setup(uow => uow.GetTradeCountByPrimaryKey(1))
                .Returns(tradeCountObject);

            var mockFixService = new Mock<IBarxFxService>();
            mockFixService.Setup(s => s.GetQuoteAsync(It.IsAny<FixQuoteRequestModel>()))
                .Returns(Task.FromResult(quoteResponse));

            var mockAppSettingService = new Mock<IAppSettingService>();
            mockAppSettingService.Setup(x => x.GetTimeOut()).Returns(15000);
            mockAppSettingService.Setup(x => x.GetStreamingQuoteDuration()).Returns(35);

            var mockClientCompanyService = new Mock<IClientCompanyService>();
            mockClientCompanyService.Setup(s => s.GetClientCompanySpread(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(0);

            var service = new TradeService(mockTradeUow.Object, mockFixService.Object, null, null, null, null, null, mockAppSettingService.Object, null, mockClientCompanyService.Object, null);

            var quoteModel = new QuoteModel
            {
                Amount = 1000,
                RhsCcy = "GBP",
                LhsCcy = "EUR",
                ValueDate = DateTime.Now                
            };

            QuoteRequestModel quoteRequest = new QuoteRequestModel
            {
                AuthUserId = 1,
                ClientCompanyId = 1,
                QuoteModels = new List<QuoteModel> { quoteModel }
            };

            //Act
            var taskOutput = service.GetQuotesAsync(quoteRequest);
            var isCompleted = taskOutput.IsCompletedSuccessfully;

            //Assert
            Assert.True(isCompleted);
            Assert.NotNull(taskOutput);
            Assert.True(taskOutput.Result.Any());
            var firstEntryCode = taskOutput.Result[0].ErrorMessage;
            Assert.True(String.IsNullOrEmpty(taskOutput.Result[0].ErrorMessage));
        }

        [Fact]
        public void ExecuteDeals_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var trade = new FxforwardTrade
            {
                Code = "Trade 42",
                Lhsccy = new Currency { Id = 42, Code = "GBP" },
                Rhsccy = new Currency { Id = 24, Code = "EUR" },
                TradeInstructionMethodId = 2,
                IsBuy = true,
                CreatedDate = DateTime.Now,
                ValueDate = DateTime.Now,
                ClientLhsamt = 25000,
                ClientRhsamt = 15000,
                CollateralPerc = 25,
                CurrencyPair = "GBPEUR",
                BrokerRate = 1.2m,
                ClientRate = 1.3m,
                AuthorisedByClientCompanyContact = new DataAccess.Entities.ClientCompanyContact { Id = 1 }
            };

            var quoteResponse = new FixNewOrderResponseModel
            {
                BarclaysAssignedId = "101",
                BarclaysTradeId = "BAR101",
                ErrorMessage = ""
            };

            var dealResponse = new DealResponseModel
            {
                Code = "AG0001-0002",
                IsSuccessful = true
            };

            var tradeCountObject = new ClientCompanyTradeCount
            {
                TradeCount = 1
            };

            var mockFixService = new Mock<IBarxFxService>();
            mockFixService.Setup(s => s.NewOrderSingleAsync(It.IsAny<FixNewOrderRequestModel>()))
                .Returns(Task.FromResult(quoteResponse));

            var mockTradeUow = new Mock<ITradeUow>();
            mockTradeUow.Setup(uow => uow.CreateDeal(It.IsAny<FxforwardTrade>(), tradeCountObject))
                .Returns(true);
            mockTradeUow.Setup(uow => uow.BrokerDeal(It.IsAny<FxforwardTrade>(), tradeCountObject))
                .Returns(true);
            mockTradeUow.Setup(uow => uow.GetTradeCountByPrimaryKey(1))
                .Returns(tradeCountObject);
            mockTradeUow.Setup(uow => uow.GetEmirStatus("Pending"))
                .Returns(new Emirstatus { Id = 2 });
            mockTradeUow.Setup(uow => uow.GetFxForwardStatus("Filled"))
                .Returns(new FxforwardTradeStatus { Id = 2 });
            mockTradeUow.Setup(uow => uow.GetTradeInstructionMethod("Online"))
                .Returns(new TradeInstructionMethod { Id = 4 });
            mockTradeUow.Setup(uow => uow.GetFxForwardStatus("Brokered"))
                .Returns(new FxforwardTradeStatus { Id = 3 });
            mockTradeUow.Setup(uow => uow.GetBroker("Barclays"))
                .Returns(new Broker { Id = 1 });

            var trades = new List<FxforwardTrade> { trade };
            mockTradeUow.Setup(uow => uow.GetTrade("AG0001-0002"))
                .Returns(trades.AsQueryable);

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetApplicationUserByAuthUserId(It.IsAny<int>()))
                .Returns(new ApplicationServiceUser { ClientCompanyContactId = 1 });

            var mockCurrencyService = new Mock<ICurrencyService>();
            mockCurrencyService.Setup(x => x.GetCurrencyId(It.IsAny<string>()))
                .Returns(1);

            var appSettingConfig = new Mock<IAppSettingService>();
            appSettingConfig.Setup(x => x.GetEmirUtiCode())
                .Returns("0875415");

            var mockClientCompanyUow = new Mock<IClientCompanyUow>();
            mockClientCompanyUow.Setup(uow => uow.GetClientCompany(1))
            .Returns((new List<ClientCompany> { new ClientCompany { AssignNewTrades = false } }).AsQueryable);
            mockClientCompanyUow.Setup(uow => uow.UpdateCompanyFirstTradeDate(1, 1));

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendFailedFIXTradeEmailAsync(It.IsAny<FailedFIXTradeModel>())).Returns(Task.CompletedTask);

            var mockClientCompanyService = new Mock<IClientCompanyService>();
            ClientCompanyAccountModel clientCompanyAccountModel = null;
            mockClientCompanyService.Setup(s => s.GetClientCompanyDefaultAccount(It.IsAny<int>(), It.IsAny<int>())).Returns(clientCompanyAccountModel);
            
            var service = new TradeService(mockTradeUow.Object, mockFixService.Object, mockEmailService.Object, null, null, mockUserService.Object, mockCurrencyService.Object, appSettingConfig.Object, mockClientCompanyUow.Object, mockClientCompanyService.Object, null);

            var dealModel = new DealModel
            {
                Amount = 1000,
                RhsCcy = "GBP",
                LhsCcy = "EUR",
                ValueDate = DateTime.Now,
                ExpirationDateTime = DateTime.Now.AddMinutes(2),
                IsBuy = true,
                IsRhsMajor = true,
                Rate = 1.3m,
                BrokerRate = 1.2m
            };

            DealRequestModel dealRequest = new DealRequestModel
            {
                AuthUserId = 1,
                ClientCompanyId = 1,
                DealModels = new List<DealModel> { dealModel }
            };

            //Act
            var results = service.Deal(dealRequest).Result;

            //Assert
            Assert.NotNull(results);
            Assert.True(results.Any());
            var firstEntryCode = results[0].Code;
            Assert.Equal("AG0001-0002", firstEntryCode);
            Assert.True(results[0].IsSuccessful);
        }
    }
}
