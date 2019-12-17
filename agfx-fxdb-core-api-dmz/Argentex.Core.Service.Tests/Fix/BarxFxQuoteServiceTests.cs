using Argentex.ClientSite.Service.Http;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.Fix;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Models.Fix;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Argentex.Core.Service.Tests.Fix
{
    public class BarxFxQuoteServiceTests
    {
        [Fact]
        public void GetFixOrderRequestAsync_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var mockConfig = new Mock<IAppSettingService>();
            mockConfig.Setup(x => x.GetBarxFXFixQuoteUrl())
                .Returns(It.IsAny<string>());
            
            var responseMessage = new HttpResponseMessage();

            var mockHttpService = new Mock<IHttpService>();
            mockHttpService.Setup(x => x.SendAsync())
                .Returns(Task.FromResult(responseMessage));
            mockHttpService.Setup(x => x.GetResponseObject<FixQuoteResponseModel>(responseMessage))
                .Returns(Task.FromResult(new FixQuoteResponseModel()));

            var service = new BarxFxService(mockHttpService.Object, mockConfig.Object, null);

            var orderRequest = new FixQuoteRequestModel
            {
                TradeCode = "Code",
                LHSCCY = "GBP",
                RHSCCY = "EUR",
                BrokerMajorAmount = 1000,
                MajorCurrency = "GBP",
                Side = 1,
                ValueDate = "2018/01/01",
                TimeOut = 15000,
                Duration = 35
            };

            //Act
            var result = service.GetQuoteAsync(orderRequest).Result;

            //Assert
            Assert.IsType<FixQuoteResponseModel>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetFixOrderRequestAsync_Unreachable_Fix_API()
        {
            //Arrange
            var mockConfig = new Mock<IAppSettingService>();
            mockConfig.Setup(x => x.GetBarxFXFixQuoteUrl())
                .Returns(It.IsAny<string>());

            var responseMessage = new HttpResponseMessage();

            var mockHttpService = new Mock<IHttpService>();
            mockHttpService.Setup(x => x.SendAsync())
                .Throws(new HttpRequestException());

            var service = new BarxFxService(mockHttpService.Object, mockConfig.Object, null);

            var orderRequest = new FixQuoteRequestModel
            {
                TradeCode = "Code",
                LHSCCY = "GBP",
                RHSCCY = "EUR",
                BrokerMajorAmount = 1000,
                MajorCurrency = "GBP",
                Side = 1,
                ValueDate = "2018/01/01",
                TimeOut = 15000,
                Duration = 35
            };

            //Act
            Exception ex = await Assert.ThrowsAsync<HttpRequestException>(() => service.GetQuoteAsync(orderRequest));

            Assert.Contains("Synetec FIX API is unreachable", ex.Message);
        }

        [Fact]
        public async Task GetFixOrderRequestAsync_Unsuccessful_When_It_Has_Invalid_Input_BadRequest()
        {
            //Arrange
            var mockConfig = new Mock<IAppSettingService>();
            mockConfig.Setup(x => x.GetBarxFXFixQuoteUrl())
                .Returns(It.IsAny<string>());

            var responseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };

            var mockHttpService = new Mock<IHttpService>();
            mockHttpService.Setup(x => x.SendAsync())
                .Returns(Task.FromResult(responseMessage));
            mockHttpService.Setup(x => x.GetResponseObject<FixQuoteResponseModel>(responseMessage))
                .Returns(Task.FromResult(new FixQuoteResponseModel()));

            var service = new BarxFxService(mockHttpService.Object, mockConfig.Object, null);

            var orderRequest = new FixQuoteRequestModel
            {
                TradeCode = "Code",
                LHSCCY = "GBP",
                RHSCCY = "EUR",
                BrokerMajorAmount = 1000,
                MajorCurrency = "GBP",
                Side = 1,
                ValueDate = "2018/01/01",
                TimeOut = 15000,
                Duration = 35
            };

            //Act
            Exception ex = await Assert.ThrowsAsync<HttpRequestException>(() => service.GetQuoteAsync(orderRequest));

            Assert.Contains("Invalid http request to Synetec FIX API.", ex.Message);
        }

        [Fact]
        public async Task GetFixOrderRequestAsync_Unsuccessful_When_BarxFX_Out_Of_Operating_Hours()
        {
            //Arrange
            var mockConfig = new Mock<IAppSettingService>();
            mockConfig.Setup(x => x.GetBarxFXFixQuoteUrl())
                .Returns(It.IsAny<string>());

            var responseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.ServiceUnavailable
            };

            var mockHttpService = new Mock<IHttpService>();
            mockHttpService.Setup(x => x.SendAsync())
                .Returns(Task.FromResult(responseMessage));
            mockHttpService.Setup(x => x.GetResponseObject<FixQuoteResponseModel>(responseMessage))
                .Returns(Task.FromResult(new FixQuoteResponseModel()));

            var service = new BarxFxService(mockHttpService.Object, mockConfig.Object, null);

            var orderRequest = new FixQuoteRequestModel
            {
                TradeCode = "Code",
                LHSCCY = "GBP",
                RHSCCY = "EUR",
                BrokerMajorAmount = 1000,
                MajorCurrency = "GBP",
                Side = 1,
                ValueDate = "2018/01/01",
                TimeOut = 15000,
                Duration = 35
            };

            //Act
            Exception ex = await Assert.ThrowsAsync<HttpRequestException>(() => service.GetQuoteAsync(orderRequest));

            Assert.Contains("BarxFX is not available. Reason: ", ex.Message);
        }

        [Fact]
        public async Task GetFixOrderRequestAsync_Unsuccessful_When_SynetecApi_Is_Available_And_Error_Returned_By_BarxFX()
        {
            //Arrange
            var mockConfig = new Mock<IAppSettingService>();
            mockConfig.Setup(x => x.GetBarxFXFixQuoteUrl())
                .Returns(It.IsAny<string>());

            var responseMessage = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };

            var mockHttpService = new Mock<IHttpService>();
            mockHttpService.Setup(x => x.SendAsync())
                .Returns(Task.FromResult(responseMessage));
            mockHttpService.Setup(x => x.GetResponseObject<FixQuoteResponseModel>(responseMessage))
                .Returns(Task.FromResult(new FixQuoteResponseModel
                {
                    ErrorMessage = "NO_RESPONSE"
                }));

            var service = new BarxFxService(mockHttpService.Object, mockConfig.Object, null);

            var orderRequest = new FixQuoteRequestModel
            {
                TradeCode = "Code",
                LHSCCY = "GBP",
                RHSCCY = "EUR",
                BrokerMajorAmount = 1000,
                MajorCurrency = "GBP",
                Side = 1,
                ValueDate = "2018/01/01",
                TimeOut = 15000,
                Duration = 35
            };

            //Act
            Exception ex = await Assert.ThrowsAsync<HttpRequestException>(() => service.GetQuoteAsync(orderRequest));

            Assert.Contains("Error getting quote from BarxFX. Reason:", ex.Message);
        }

    }
}
