using System;
using System.Collections.Generic;
using System.Net;
using Argentex.Core.Api.Controllers.Statements;
using Argentex.Core.Service.Models.Statements;
using Argentex.Core.Service.Statements;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecLogger;
using Xunit;

namespace Argentex.Core.Api.Tests.Statements
{
    public class StatementsControllerTests
    {
        [Fact(Skip = "Needs updating")]
        public void Given_There_Are_No_Results_When_Getting_Statements_A_No_Content_Should_Be_Returned()
        {
            // Given
            var statementServiceMock = new Mock<IStatementService>();
            var loggerMock = new Mock<ILogWrapper>();

            statementServiceMock
                .Setup(x => x.GetStatements(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new Dictionary<string, List<StatementModel>>());

            var expectedStatusCode = HttpStatusCode.NoContent;

            var controller = new StatementsController(statementServiceMock.Object, loggerMock.Object);

            // When
            var response = controller.GetStatements(0, DateTime.Now, DateTime.Now);
            var result = response as NoContentResult;

            // Then
            Assert.NotNull(result);
            Assert.Equal((int)expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void Given_There_Are_Results_When_Getting_Statements_An_Ok_Response_Should_Be_Returned()
        {
            // Given
            var statements = new Dictionary<string, List<StatementModel>>()
            {
                {"GBP", new List<StatementModel>()
                {
                    new StatementModel()
                    {
                        ValueDate = DateTime.Today,
                        Event = "Who cares",
                        IsDebit = false,
                        Amount = 10000m
                    }
                } }
            };

            var statementServiceMock = new Mock<IStatementService>();
            var loggerMock = new Mock<ILogWrapper>();

            statementServiceMock
                .Setup(x => x.GetStatements(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(statements);

            var expectedStatusCode = HttpStatusCode.OK;

            var controller = new StatementsController(statementServiceMock.Object, loggerMock.Object);

            // When
            var response = controller.GetStatements(0, DateTime.Now, DateTime.Now);
            var result = response as OkObjectResult;

            // Then
            Assert.NotNull(result);
            Assert.Equal((int)expectedStatusCode, result.StatusCode);
        }
    }
}
