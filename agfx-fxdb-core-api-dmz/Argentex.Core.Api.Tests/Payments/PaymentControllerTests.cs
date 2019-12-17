using Argentex.Core.Api.Controllers.Settlements;
using Argentex.Core.Service.Models.Payments;
using Argentex.Core.Service.Settlements;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecLogger;
using System.Net;
using Xunit;

namespace Argentex.Core.Api.Tests.Payments
{
    public class PaymentControllerTests
    {
        [Fact]
        public void Given_A_Payment_Is_Returned_An_Ok_Object_Result_Should_Be_Returned()
        {
            // Given
            var paymentCode = "PC 42";
            var paymentServiceMock = new Mock<ISettlementService>();
            var loggerMock = new Mock<ILogWrapper>();

            paymentServiceMock.Setup(x => x.GetPaymentInformation(It.IsAny<string>(), It.IsAny<bool>())).Returns(new PaymentInformationModel());

            var controller = new SettlementController(paymentServiceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedType = typeof(PaymentInformationModel);

            // When
            var response = controller.GetPaymentOutInformation(paymentCode);
            var result = response as OkObjectResult;

            // Then
            Assert.NotNull(result);
            Assert.Equal((int) expectedStatusCode, result.StatusCode);
            Assert.Equal(expectedType, result.Value.GetType());
        }
    }
}
