using System.Net;
using Argentex.Core.Api.Controllers;
using Argentex.Core.Api.Controllers.Currencies;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecLogger;
using Xunit;

namespace Argentex.Core.Api.Tests.Currencies
{
    public class CurrencyControllerTests
    {
        [Fact]
        public void Given_And_Exception_Is_Caught_A_Bad_Request_Should_Be_Returned()
        {
            // Given
            var currencyPair = "GBPEUR";
            var currencyServiceMock = new Mock<ICurrencyService>();
            var loggerMock = new Mock<ILogWrapper>();

            currencyServiceMock.Setup(x => x.GetCurrencyPairRate(It.IsAny<string>()))
                .Throws(new CurrencyPairPricingNotFoundException());

            var expectedStatusCode = HttpStatusCode.BadRequest;

            var currencyController = new CurrencyController(currencyServiceMock.Object, loggerMock.Object);

            // When
            var result = currencyController.GetCurrencyPairRate(currencyPair);
            var badRequest = result as BadRequestObjectResult;

            // Then
            Assert.NotNull(badRequest);
            Assert.Equal((int) expectedStatusCode, badRequest.StatusCode);
        }

        [Fact]
        public void Given_The_Rate_Is_Returned_From_The_Service_An_Ok_Result_Should_Be_Returned()
        {
            // Given
            var currencyPair = "GBPEUR";
            var rate = 1.5;
            var currencyServiceMock = new Mock<ICurrencyService>();
            var loggerMock = new Mock<ILogWrapper>();

            currencyServiceMock.Setup(x => x.GetCurrencyPairRate(It.IsAny<string>()))
                .Returns(rate);

            var expectedStatusCode = HttpStatusCode.OK;

            var currencyController = new CurrencyController(currencyServiceMock.Object, loggerMock.Object);

            // When
            var result = currencyController.GetCurrencyPairRate(currencyPair);
            var badRequest = result as OkObjectResult;

            // Then
            Assert.NotNull(badRequest);
            Assert.Equal((int)expectedStatusCode, badRequest.StatusCode);
        }
    }
}
