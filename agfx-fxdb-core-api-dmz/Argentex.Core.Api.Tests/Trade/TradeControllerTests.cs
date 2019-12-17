using Argentex.Core.Api.Controllers.Trade;
using Argentex.Core.Service.Models.Trade;
using Argentex.Core.Service.Models.Trades;
using Argentex.Core.Service.Trade;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecLogger;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Argentex.Core.Api.Tests.Trade
{

    public class TradeControllerTests
    {
        [Fact]
        public void GetUnsettledTrades_Success_With_Valid_ClientCompanyId_Input()
        {
            //Arrange
            var mockObject = new TradeModel();
            var mockList = new List<TradeModel>();
            mockList.Add(mockObject);

            var mockService = new Mock<ITradeService>();
            mockService.Setup(x => x.GetUnsettledTrades(1))
                .Returns(mockList);

            var controller = new TradeController(mockService.Object, null);

            //Act
            var result = controller.GetUnsettledTrades(1);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Given_A_Trade_Model_Is_Returned_An_Ok_Object_Result_Should_Be_Returned()
        {
            // Given
            var tradeCode = "Trade 42";
            var tradeServiceMock = new Mock<ITradeService>();
            var loggerMock = new Mock<ILogWrapper>();

            tradeServiceMock.Setup(x => x.GetTradeInformation(It.IsAny<string>()))
                .Returns(new FxForwardTradeInformationModel());

            var controller = new TradeController(tradeServiceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;

            // When
            var response = controller.GetTradeInformation(tradeCode);
            var result = response as OkObjectResult;

            // Then
            Assert.NotNull(result);
            Assert.Equal((int)expectedStatusCode, result.StatusCode);
            var content = result.Value as FxForwardTradeInformationModel;
            Assert.NotNull(content);
        }

        [Fact]
        public void GetClosedTrades_Success_With_Valid_ClientCompanyId_Input()
        {
            //Arrange
            var mockObject = new TradeModel();
            var mockList = new List<TradeModel>();
            mockList.Add(mockObject);

            var mockService = new Mock<ITradeService>();
            mockService.Setup(x => x.GetClosedTrades(1)).Returns(mockList);

            var controller = new TradeController(mockService.Object, null);

            //Act
            var result = controller.GetClosedTrades(1);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
