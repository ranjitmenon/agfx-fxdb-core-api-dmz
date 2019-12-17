using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Models.Payments;
using Argentex.Core.Service.Models.Settlements;
using Argentex.Core.Service.Payments;
using Argentex.Core.Service.Trade;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.ClientCompanies;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts;
using Argentex.Core.UnitsOfWork.Currencies;
using Argentex.Core.UnitsOfWork.Payments;
using Argentex.Core.UnitsOfWork.Settlements;
using Argentex.Core.UnitsOfWork.Trades;
using Moq;
using Xunit;

namespace Argentex.Core.Service.Tests.Payments
{
    public class PaymentServiceTests
    {
        [Fact]
        public void Given_There_Is_No_Payment_Associated_With_The_Code_An_Exception_Should_Be_Thrown()
        {
            // Given
            var paymentCode = "PC 42";
            var payments = new List<Payment>();

            var paymentUoWMock = new Mock<IPaymentUoW>();
            var currencyUoW = new Mock<ICurrencyUoW>();
            var clientCompanyUow = new Mock<IClientCompanyUow>();
            var clientCompanyAccountsUoW = new Mock<IClientCompanyAccountsUoW>();

            paymentUoWMock.Setup(x => x.GetPayment(It.IsAny<string>())).Returns(payments.AsQueryable);

            var service = new SettlementService(paymentUoWMock.Object, currencyUoW.Object, clientCompanyUow.Object, clientCompanyAccountsUoW.Object, null, null, null, null, null, null, null, null);

            var expectedMessage = $"Payment with code {paymentCode} does not exist";

            // When
            var result = Assert.Throws<PaymentNotFoundException>(() => service.GetPaymentInformation(paymentCode));

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void Given_A_Payment_Is_Associated_With_The_Code_And_It_Is_Not_Payment_Out_A_Payment_Information_Model_Should_Be_Returned()
        {
            // Given
            var now = DateTime.Now;
            var today = DateTime.Today;
            var payment = new Payment
            {
                Code = "PC 42",
                PaymentTypeId = 1,
                ValueDate = today,
                CreatedDate = now,
                Amount = 42000,
                Reference = "Ref 42",
                Currency = new Currency
                {
                    Id = 42,
                    Code = "GBP"
                }
            };
            var payments = new List<Payment>
            {
                payment
            };
            
            var paymentUoWMock = new Mock<IPaymentUoW>();
            var currencyUoW = new Mock<ICurrencyUoW>();
            var clientCompanyUow = new Mock<IClientCompanyUow>();
            var clientCompanyAccountsUoW = new Mock<IClientCompanyAccountsUoW>();

            paymentUoWMock.Setup(x => x.GetPayment(It.IsAny<string>())).Returns(payments.AsQueryable);

            var service = new SettlementService(paymentUoWMock.Object, currencyUoW.Object, clientCompanyUow.Object, clientCompanyAccountsUoW.Object, null, null, null, null, null, null, null, null);

            var expectedType = typeof(PaymentInformationModel);
            var expectedPaymentCode = payment.Code;
            var expectedPaymentType = "In";
            var expectedValueDate = today;
            var expectedCreatedDateTime = now;
            var expectedAmount = payment.Amount.Value;
            var expectedReference = payment.Reference;
            var expectedCurrency = "GBP";

            // When
            var result = service.GetPaymentInformation(payment.Code);

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedType, result.GetType());
            Assert.Equal(expectedPaymentCode, result.PaymentCode);
            Assert.Equal(expectedPaymentType, result.PaymentType);
            Assert.Equal(expectedValueDate, result.ValueDate);
            Assert.Equal(expectedCreatedDateTime, result.CreatedDateTime);
            Assert.Equal(expectedAmount, result.Amount);
            Assert.Equal(expectedReference, result.Reference);
            Assert.Equal(expectedCurrency, result.Currency);
        }

        [Fact]
        public void Given_There_Is_No_Opi_Transaction_An_Exception_Should_Be_Thrown()
        {
            // Given
            var now = DateTime.Now;
            var today = DateTime.Today;
            var payment = new Payment
            {
                Id = 42,
                Code = "PC 42",
                PaymentTypeId = 1,
                ValueDate = today,
                CreatedDate = now,
                Amount = 42000,
                Reference = "Ref 42",
                CurrencyId = 42
            };
            var payments = new List<Payment>
            {
                payment
            };
            var opiTransactions = new List<ClientCompanyOpitransaction>();

            var paymentUoWMock = new Mock<IPaymentUoW>();
            var currencyUoW = new Mock<ICurrencyUoW>();
            var clientCompanyUow = new Mock<IClientCompanyUow>();
            var clientCompanyAccountsUoW = new Mock<IClientCompanyAccountsUoW>();

            paymentUoWMock.Setup(x => x.GetPayment(It.IsAny<string>())).Returns(payments.AsQueryable);
            paymentUoWMock.Setup(x => x.GetClientCompanyOpiTransaction(It.IsAny<string>()))
                .Returns(opiTransactions.AsQueryable);

            var service = new SettlementService(paymentUoWMock.Object, currencyUoW.Object, clientCompanyUow.Object, clientCompanyAccountsUoW.Object, null, null, null, null, null, null, null, null); ;

            var expectedMessage = $"Opi transaction for payment code {payment.Code} does not exist";

            // When
            var result = Assert.Throws<ClientCompanyOpiTransactionNotFoundException>(() => service.GetPaymentInformation(payment.Code, true));

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public async void AssignAsync_Should_Throw_Exception_When_maxSettlementCreateDateForTrade_Is_Different_In_Both_Checks()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            var tradeUowMock = new Mock<ITradeUow>();
            var settlementUowMock = new Mock<ISettlementUow>();
            var tradeServiceMock = new Mock<ITradeService>();

            AssignSettlementModel settlementModelResult = new AssignSettlementModel()
            {
                ValueDate = DateTime.Today.ToString(),
                SettlementId = 1414,
                TradedCurrency = "GBPEUR",
                Account = new AccountModel(),
                Amount = 14500,
                IsPayTotal = false,
                Status = 0
            };

            AssignSettlementRequestModel settlementModel = new AssignSettlementRequestModel()
            {
                AuthUserId = 1,
                ClientCompanyId = 111,
                Trade = new Models.Trade.TradeModel()
                {
                    TradeId = "10",
                    ValueDate = DateTime.Today,
                    Balance = 14500
                },
                SettlementModels = new List<AssignSettlementModel>()
                {
                    settlementModelResult
                }
            };

            var trades = new List<FxforwardTrade>();

            userServiceMock.Setup(x => x.GetAuthUserById(It.IsAny<int>())).Returns(new AuthUser());
            tradeUowMock.Setup(x => x.GetTrade(It.IsAny<string>())).Returns(trades.AsQueryable);
            settlementUowMock.SetupSequence(x => x.GetMaxCreateDateForTrade(It.IsAny<string>())).Returns(DateTime.Today.AddDays(-1)).Returns(DateTime.Today);
            settlementUowMock.Setup(x => x.GetTradeOpis(It.IsAny<string>())).Returns(new List<FxforwardTrade2Opi>());
            settlementUowMock.Setup(x => x.GetTradeSwaps(It.IsAny<string>())).Returns(new Dictionary<FxforwardTrade, DataAccess.Entities.ClientSiteAction>());
            tradeServiceMock.Setup(x => x.GetTradeBalance(It.IsAny<int>(), It.IsAny<string>())).Returns(14500);

            var expectedException = typeof(TransactionAbortedException);
            var expectedExceptionMessage = "The transaction has been aborted.";

            var service = new SettlementService(null, null, null,
                null, settlementUowMock.Object, tradeUowMock.Object, userServiceMock.Object, null, null, null, null, tradeServiceMock.Object);

            // Act
            var result = await Assert.ThrowsAsync<TransactionAbortedException>(() => service.AssignAsync(settlementModel));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedExceptionMessage, result.Message);
            Assert.Equal(expectedException, result.GetType());
        }
    }
}
