using Argentex.Core.Api.Controllers.Settlements;
using Argentex.Core.Service.Models.Settlements;
using Argentex.Core.Service.Settlements;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecLogger;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Argentex.Core.Service.Tests.Controllers
{
    public class SettlementControllerTests
    {
        [Fact]
        public async void Assign_Should_Return_BadRequest_When_Model_Is_Not_Valid()
        {
            // Arrange
            AssignSettlementRequestModel settlementModel = new AssignSettlementRequestModel()
            {
                AuthUserId = 1,
                ClientCompanyId = 111,
                Trade = new Models.Trade.TradeModel(),
                SettlementModels = new List<AssignSettlementModel>()
            };
            var settlementServiceMock = new Mock<ISettlementService>();
            var loggerMock = new Mock<ILogWrapper>();

            settlementServiceMock.Setup(x => x.AssignAsync(It.IsAny<AssignSettlementRequestModel>())).ThrowsAsync(new Exception());

            var controller = new SettlementController(settlementServiceMock.Object, loggerMock.Object);
            controller.ModelState.AddModelError("Test", "Model is not valid");

            var expectedStatusCode = HttpStatusCode.BadRequest;

            // Act
            var response = await controller.Assign(settlementModel);
            var result = response as StatusCodeResult;

            // Assert
            Assert.Equal((int)expectedStatusCode, result.StatusCode);
            Assert.False(controller.ModelState.IsValid);
            Assert.Single(controller.ModelState);
            Assert.True(controller.ModelState.ContainsKey("Test"));
        }

        [Fact]
        public async void Assign_Should_Return_An_Ok_Response_When_Passed_Object_Is_Valid()
        {
            // Arrange
            AssignSettlementRequestModel settlementModel = new AssignSettlementRequestModel()
            {
                AuthUserId = 4,
                ClientCompanyId = 1,
                Trade = new Models.Trade.TradeModel(),
                SettlementModels = new List<AssignSettlementModel>()
            };

            AssignSettlementModel assignSettlementModel = new AssignSettlementModel()
            {
                SettlementId = 1515,
                TradedCurrency = "GBPEUR",
                Account = new AccountModel()
                {
                },
                Amount = 14700,
                ValueDate = DateTime.Today.ToString(),
                Reference = "Test Reference",
                IsPayTotal = false,
                Status = 0,
                IsWarning = false,
                WarningMessage = string.Empty
            };

            var settlementServiceMock = new Mock<ISettlementService>();
            var loggerMock = new Mock<ILogWrapper>();

            settlementServiceMock.Setup(x => x.AssignAsync(It.IsAny<AssignSettlementRequestModel>())).ReturnsAsync(new List<AssignSettlementModel>() { assignSettlementModel });

            var controller = new SettlementController(settlementServiceMock.Object, loggerMock.Object);
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedType = typeof(IList<AssignSettlementModel>);
            var expectedListCount = 1;
            var expectedAmount = 14700;
            var expectedsettlementId = 1515;
            var expectedTradedCurrency = "GBPEUR";
            var expectedValueDate = DateTime.Today.ToString();
            var expectedReference = "Test Reference";
            var expectedIsPayTotal = false;
            var expectedStatus = 0;
            var expectedIsWarning = false;
            var expectedWarningMessage = string.Empty;

            // Act
            var response = await controller.Assign(settlementModel);
            var result = response as OkObjectResult;
            var returnedList = result.Value as IList<AssignSettlementModel>;
            
            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)expectedStatusCode, result.StatusCode);
            Assert.NotNull(returnedList);
            Assert.Equal(expectedListCount, returnedList.Count);
            Assert.Equal(expectedAmount, returnedList[0].Amount);
            Assert.Equal(expectedsettlementId, returnedList[0].SettlementId);
            Assert.Equal(expectedTradedCurrency, returnedList[0].TradedCurrency);
            Assert.Equal(expectedValueDate, returnedList[0].ValueDate);
            Assert.Equal(expectedReference, returnedList[0].Reference);
            Assert.Equal(expectedIsPayTotal, returnedList[0].IsPayTotal);
            Assert.Equal(expectedStatus, returnedList[0].Status);
            Assert.Equal(expectedIsWarning, returnedList[0].IsWarning);
            Assert.Equal(expectedWarningMessage, returnedList[0].WarningMessage);
        }
    }
}
