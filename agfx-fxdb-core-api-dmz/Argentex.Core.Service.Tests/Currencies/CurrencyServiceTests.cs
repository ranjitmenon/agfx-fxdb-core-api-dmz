using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Models.Currencies;
using Argentex.Core.UnitsOfWork.Currencies;
using Moq;
using Synetec.Data.UnitOfWork.GenericRepo;
using Xunit;

namespace Argentex.Core.Service.Tests.Currencies
{
    public class CurrencyServiceTests
    {
        [Fact]
        public void Given_Currency_Pair_Does_Not_Exist_An_Exception_Should_Be_Thrown()
        {
            // Given
            var currencyPair = "GBPUSD";
            var currencyPairPricingRepositoryMock = new Mock<IGenericRepo<CurrencyPairPricing>>();

            var currencyUoWMock = new Mock<ICurrencyUoW>();

            currencyPairPricingRepositoryMock.Setup(x =>
                    x.Get(It.IsAny<Expression<Func<CurrencyPairPricing, bool>>>(),
                        It.IsAny<Func<IQueryable<CurrencyPairPricing>,
                            IOrderedQueryable<CurrencyPairPricing>>>(), ""))
                .Returns(new List<CurrencyPairPricing>());
            currencyUoWMock.Setup(x => x.CurrencyPairPricingRepository)
                .Returns(currencyPairPricingRepositoryMock.Object);

            var currencyService = new CurrencyService(currencyUoWMock.Object);

            var expectedMessage = $"{currencyPair} does not exist";

            // When
            var result =
                Assert.Throws<CurrencyPairPricingNotFoundException>(() =>
                    currencyService.GetCurrencyPairRate(currencyPair));

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void Given_That_Currency_Pair_Exists_The_Rate_Should_Be_Returned()
        {
            // Given
            var currencyPair = "GBPUSD";
            var currencyPairPricing = new CurrencyPairPricing()
            {
                CurrencyPair = currencyPair,
                Rate = 1.5
            };
            var currencyPairPricingRepositoryMock = new Mock<IGenericRepo<CurrencyPairPricing>>();

            var currencyUoWMock = new Mock<ICurrencyUoW>();

            currencyPairPricingRepositoryMock.Setup(x =>
                    x.Get(It.IsAny<Expression<Func<CurrencyPairPricing, bool>>>(),
                        It.IsAny<Func<IQueryable<CurrencyPairPricing>,
                            IOrderedQueryable<CurrencyPairPricing>>>(), ""))
                .Returns(new List<CurrencyPairPricing>{ currencyPairPricing });
            currencyUoWMock.Setup(x => x.CurrencyPairPricingRepository)
                .Returns(currencyPairPricingRepositoryMock.Object);

            var currencyService = new CurrencyService(currencyUoWMock.Object);

            var expectedResult = 1.5;

            // When
            var result = currencyService.GetCurrencyPairRate(currencyPair);

            // Then
            Assert.Equal(expectedResult, result);
        }


        [Fact]
        public void Given_There_Is_No_Currency_Associated_With_The_Id_An_Exception_Should_Be_Thrown()
        {
            // Given
            var currencyId = 42;
            var currencies = new List<Currency>();
            var currencyRepositoryMock = new Mock<IGenericRepo<Currency>>();

            var currencyUoWMock = new Mock<ICurrencyUoW>();
            

            currencyUoWMock.Setup(x => x.GetCurrency(It.IsAny<int>())).Returns(currencies.AsQueryable);

            var service = new CurrencyService(currencyUoWMock.Object);

            var expectedMessage = $"Currency with id {currencyId} does not exist";

            // When
            var result = Assert.Throws<CurrencyNotFoundException>(() => service.GetCurrency(currencyId));

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedMessage, result.Message);
        }

        [Fact]
        public void Given_A_Currency_Is_Found_A_Currency_Model_Should_Be_Returned()
        {

            // Given
            var currency = new Currency
            {
                Id = 42,
                Code = "GBP"
            };
            var currencies = new List<Currency> { currency };
            var currencyRepositoryMock = new Mock<IGenericRepo<Currency>>();

            var currencyUoWMock = new Mock<ICurrencyUoW>();
            
            currencyUoWMock.Setup(x => x.GetCurrency(It.IsAny<int>())).Returns(currencies.AsQueryable);

            var service = new CurrencyService(currencyUoWMock.Object);

            var expectedType = typeof(CurrencyModel);
            var expectedCode = "GBP";

            // When
            var result = service.GetCurrency(currency.Id);

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedType, result.GetType());
            Assert.Equal(expectedCode, result.Code);
        }
    }
}
