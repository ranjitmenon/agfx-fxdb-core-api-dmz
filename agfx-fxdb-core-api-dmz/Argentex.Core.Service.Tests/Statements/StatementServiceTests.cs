using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Models.Statements;
using Argentex.Core.Service.Statements;
using Argentex.Core.UnitsOfWork.Statements;
using Moq;
using Synetec.Data.UnitOfWork.GenericRepo;
using Xunit;

namespace Argentex.Core.Service.Tests.Statements
{
    public class StatementServiceTests
    {
        [Fact(Skip = "Needs updating")]
        public void Given_Start_Date_Is_Posterior_To_End_Date_When_Getting_Statements_Then_An_Exception_Should_Be_Thrown()
        {
            // Given
            var statementUoWMock = new Mock<IStatementUoW>();
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(-1);

            var expectedMessage = "Start date cannot be posterior to end date";

            var statementService = new StatementService(statementUoWMock.Object);

            // When
            var exception =
                Assert.Throws<ArgumentException>(() => statementService.GetStatements(0, startDate, endDate));

            // Then
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact(Skip = "Needs updating")]
        public void Given_There_Is_No_Company_With_The_Provided_Id_When_Getting_Statements_Then_An_Exception_Should_Be_Thrown()
        {
            // Given
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddMonths(1);
            var clientCompanyId = 42;
            var statementUoWMock = new Mock<IStatementUoW>();
            var clientCompanyRepositoryMock = new Mock<IGenericRepo<ClientCompany>>();

            clientCompanyRepositoryMock.Setup(x => x.GetByPrimaryKey(It.IsAny<int>())).Returns((ClientCompany) null);
            statementUoWMock.Setup(x => x.ClientCompanyRepository).Returns(clientCompanyRepositoryMock.Object);

            var expectedMessage = $"Client company with id {clientCompanyId} does not exist";

            var statementService = new StatementService(statementUoWMock.Object);

            // When
            var exception =
                Assert.Throws<ArgumentException>(() => statementService.GetStatements(clientCompanyId, startDate, endDate));

            // Then
            Assert.NotNull(exception);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact(Skip = "Needs updating")]
        public void Given_There_Is_No_Transaction_When_Getting_Statements_Then_An_Empty_Collection_Should_Be_Returned()
        {
            // Given
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddMonths(1);
            var clientCompany = new ClientCompany()
            {
                Id = 42
            };

            var currencyRepositoryMock = new Mock<IGenericRepo<Currency>>();
            var clientCompanyRepositoryMock = new Mock<IGenericRepo<ClientCompany>>();
            var bankAccountTransactionRepositoryMock = new Mock<IGenericRepo<BankAccountTransaction>>();
            var tradeRepositoryMock = new Mock<IGenericRepo<FxforwardTrade>>();
            var paymentRepositoryMock = new Mock<IGenericRepo<Payment>>();
            var statementUoWMock = new Mock<IStatementUoW>();

            currencyRepositoryMock.Setup(x => x.GetAllAsList()).Returns(new List<Currency>());
            clientCompanyRepositoryMock.Setup(x => x.GetByPrimaryKey(It.IsAny<int>())).Returns(clientCompany);
            bankAccountTransactionRepositoryMock.Setup(x =>
                    x.Get(It.IsAny<Expression<Func<BankAccountTransaction, bool>>>(),
                        It.IsAny<Func<IQueryable<BankAccountTransaction>,
                            IOrderedQueryable<BankAccountTransaction>>>(), ""))
                .Returns(new List<BankAccountTransaction>());
            tradeRepositoryMock.Setup(x =>
                    x.Get(It.IsAny<Expression<Func<FxforwardTrade, bool>>>(),
                        It.IsAny<Func<IQueryable<FxforwardTrade>,
                            IOrderedQueryable<FxforwardTrade>>>(), ""))
                .Returns(new List<FxforwardTrade>());
            paymentRepositoryMock.Setup(x => x.Get(It.IsAny<Expression<Func<Payment, bool>>>(),
                    It.IsAny<Func<IQueryable<Payment>,
                        IOrderedQueryable<Payment>>>(), ""))
                .Returns(new List<Payment>());

            statementUoWMock.Setup(x => x.ClientCompanyRepository).Returns(clientCompanyRepositoryMock.Object);
            statementUoWMock.Setup(x => x.CurrencyRepository).Returns(currencyRepositoryMock.Object);
            statementUoWMock.Setup(x => x.BankAccountTransactionRepository).Returns(bankAccountTransactionRepositoryMock.Object);
            statementUoWMock.Setup(x => x.FxForwardTradeRepository).Returns(tradeRepositoryMock.Object);
            statementUoWMock.Setup(x => x.PaymentRepository).Returns(paymentRepositoryMock.Object);

            var expectedCount = 0;
            var expectedType = typeof(ConcurrentDictionary<string, List<StatementModel>>);

            var statementService = new StatementService(statementUoWMock.Object);

            // When
            var result = statementService.GetStatements(clientCompany.Id, startDate, endDate);

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedType, result.GetType());
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact(Skip = "Needs updating")]
        public void Given_The_Parameters_Are_Valid_When_Getting_Statements_A_Non_Empty_Collection_Should_Be_Returned()
        {
            // Given
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddMonths(1);
            var clientCompany = new ClientCompany()
            {
                Id = 42
            };
            var trade = new FxforwardTrade()
            {
                Code = "TradeCode42",
                ValueDate = DateTime.Today.AddDays(10)
            };
            var payment = new Payment()
            {
                Id = 404,
                Code = "PaymentCode",
                ValueDate = DateTime.Now.AddDays(5)
            };
            var transactions = new List<BankAccountTransaction>()
            {
                new BankAccountTransaction()
                {
                    Currency = new Currency()
                    {
                        Id = 42,
                        Code = "GBP"
                    },
                    FxforwardTradeCodeNavigation = trade,
                    Payment = payment,
                    IsDebit = true,
                    Amount = 25m,
                    CurrencyId = 42,
                    FxforwardTradeCode = "TradeCode42",
                    PaymentId = 404
                },
            };


            var currencyRepositoryMock = new Mock<IGenericRepo<Currency>>();
            var clientCompanyRepositoryMock = new Mock<IGenericRepo<ClientCompany>>();
            var bankAccountTransactionRepositoryMock = new Mock<IGenericRepo<BankAccountTransaction>>();
            var tradeRepositoryMock = new Mock<IGenericRepo<FxforwardTrade>>();
            var paymentRepositoryMock = new Mock<IGenericRepo<Payment>>();
            var statementUoWMock = new Mock<IStatementUoW>();


            currencyRepositoryMock.Setup(x => x.GetAllAsList()).Returns(new List<Currency>
            {
                new Currency()
                {
                    Id = 42,
                    Code = "GBP"
                }
            });
            clientCompanyRepositoryMock.Setup(x => x.GetByPrimaryKey(It.IsAny<int>())).Returns(clientCompany);
            bankAccountTransactionRepositoryMock.Setup(x =>
                    x.Get(It.IsAny<Expression<Func<BankAccountTransaction, bool>>>(),
                        It.IsAny<Func<IQueryable<BankAccountTransaction>,
                            IOrderedQueryable<BankAccountTransaction>>>(), ""))
                .Returns(transactions);
            tradeRepositoryMock.Setup(x =>
                    x.Get(It.IsAny<Expression<Func<FxforwardTrade, bool>>>(),
                        It.IsAny<Func<IQueryable<FxforwardTrade>,
                            IOrderedQueryable<FxforwardTrade>>>(), ""))
                .Returns(new List<FxforwardTrade>
                {
                    trade
                });
            paymentRepositoryMock.Setup(x => x.Get(It.IsAny<Expression<Func<Payment, bool>>>(),
                    It.IsAny<Func<IQueryable<Payment>,
                        IOrderedQueryable<Payment>>>(), ""))
                .Returns(new List<Payment>
                {
                    payment
                });


            statementUoWMock.Setup(x => x.ClientCompanyRepository).Returns(clientCompanyRepositoryMock.Object);
            statementUoWMock.Setup(x => x.BankAccountTransactionRepository).Returns(bankAccountTransactionRepositoryMock.Object);
            statementUoWMock.Setup(x => x.CurrencyRepository).Returns(currencyRepositoryMock.Object);
            statementUoWMock.Setup(x => x.FxForwardTradeRepository).Returns(tradeRepositoryMock.Object);
            statementUoWMock.Setup(x => x.PaymentRepository).Returns(paymentRepositoryMock.Object);

            var expectedCount = 1;
            var expectedTransactionsCount = 2;
            var expectedKey = "GBP";
            var expectedType = typeof(ConcurrentDictionary<string, List<StatementModel>>);

            var statementService = new StatementService(statementUoWMock.Object);

            // When
            var result = statementService.GetStatements(clientCompany.Id, startDate, endDate);

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedType, result.GetType());
            Assert.Equal(expectedCount, result.Count);
            var gbpTransactions = result.First();
            Assert.Equal(expectedKey, gbpTransactions.Key);
            var transactionsResult = gbpTransactions.Value;
            Assert.Equal(expectedTransactionsCount, transactionsResult.Count);
        }
    }
}
