using Argentex.Core.Api.Controllers.ClientCompanies;
using Argentex.Core.Api.Models.ClientCompanies;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service;
using Argentex.Core.Service.ClientCompanies;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Models.ClientCompany;
using Argentex.Core.Service.Settlements;
using Argentex.Core.UnitsOfWork.ClientCompanies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecLogger;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Argentex.Core.Api.Tests.ClientCompanies
{
    public class ClientCompanyAccountsControllerTests
    {
        [Fact]
        public void Given_There_Are_No_Accounts_For_A_company_A_No_Content_Result_Should_Be_Returned()
        {
            // Given
            var clientCompanyId = 42;
            var clientCompanyAccountsServiceMock = new Mock<IClientCompanyAccountsService>();
            var settlementServiceMock = new Mock<ISettlementService>();
            var loggerMock = new Mock<ILogWrapper>();

            clientCompanyAccountsServiceMock.Setup(x => x.GetClientCompanyAccounts(It.IsAny<int>()))
                .Returns(new List<ClientCompanyAccountModel>());

            var controller = new ClientCompanyAccountsController(clientCompanyAccountsServiceMock.Object, settlementServiceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.NoContent;

            // When
            var response = controller.GetClientCompanyAccounts(clientCompanyId);
            var result = response as NoContentResult;

            // Then
            Assert.NotNull(result);
            Assert.Equal((int) expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public void Given_A_ClientCompanyNotFoundException_Is_Caught_A_Bad_Request_Result_Should_Be_Returned()
        {
            // Given
            var clientCompanyId = 42;
            var clientCompanyAccountsServiceMock = new Mock<IClientCompanyAccountsService>();
            var settlementServiceMock = new Mock<ISettlementService>();
            var loggerMock = new Mock<ILogWrapper>();

            clientCompanyAccountsServiceMock.Setup(x => x.GetClientCompanyAccounts(It.IsAny<int>()))
                .Throws(new ClientCompanyNotFoundException($"Client company with id {clientCompanyId} does not exist"));

            var controller = new ClientCompanyAccountsController(clientCompanyAccountsServiceMock.Object, settlementServiceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedValue = $"Client company with id {clientCompanyId} does not exist";

            // When
            var response = controller.GetClientCompanyAccounts(clientCompanyId);
            var result = response as BadRequestObjectResult;

            // Then
            Assert.NotNull(result);
            Assert.Equal((int)expectedStatusCode, result.StatusCode);
            Assert.Equal(expectedValue, result.Value);
        }

        [Fact]
        public void Given_A_Company_Has_A_List_Of_Accounts_An_Ok_Object_Result_Should_Be_Returned()
        {
            // Given
            var clientCompanyId = 42;
            var clientCompanyAccounts = new List<ClientCompanyAccountModel>
            {
                new ClientCompanyAccountModel
                {
                    ClientCompanyId = 42,
                    AccountName = "Bank Account",
                    AccountNumber = "123456",
                    Currency = "GBP"
                }
            };
            var clientCompanyAccountsServiceMock = new Mock<IClientCompanyAccountsService>();
            var settlementServiceMock = new Mock<ISettlementService>();
            var loggerMock = new Mock<ILogWrapper>();

            clientCompanyAccountsServiceMock.Setup(x => x.GetClientCompanyAccounts(It.IsAny<int>()))
                .Returns(clientCompanyAccounts);

            var controller = new ClientCompanyAccountsController(clientCompanyAccountsServiceMock.Object, settlementServiceMock.Object, loggerMock.Object);

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedClientCompanyId = 42;
            var expectedAccountName = "Bank Account";
            var expectedAccountNumber = "123456";
            var expectedCurrency = "GBP";

            // When
            var response = controller.GetClientCompanyAccounts(clientCompanyId);
            var result = response as OkObjectResult;

            // Then
            Assert.NotNull(result);
            Assert.Equal((int)expectedStatusCode, result.StatusCode);
            var content = result.Value as IEnumerable<ClientCompanyAccountDto>;
            Assert.NotNull(content);
            Assert.True(content.Any());
            var firstAccount = content.First();
            Assert.Equal(expectedClientCompanyId, firstAccount.ClientCompanyId);
            Assert.Equal(expectedAccountName, firstAccount.AccountName);
            Assert.Equal(expectedAccountNumber, firstAccount.AccountNumber);
            Assert.Equal(expectedCurrency, firstAccount.Currency);
        }

        [Fact]
        public void AddClientCompanyAccount_Success_With_Valid_Model_Input()
        {
            //Arrange
            var mockService = new Mock<IClientCompanyAccountsService>();
            var settlementServiceMock = new Mock<ISettlementService>();
            mockService.Setup(x => x.AddSettlementAccount(It.IsAny<SettlementAccountModel>()));
           
            var controller = new ClientCompanyAccountsController(mockService.Object, settlementServiceMock.Object, null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            var account = new SettlementAccountModel
            {
                CurrencyId = 1,
                CountryId = 1,
                AccountName = "Account Name",
                AccountNumber = 1,
                UpdatedByAuthUserId = 1,
                ClientCompanyId = 1
            };

            //Act
            var result = controller.AddClientCompanyAccount(account);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteSettlementAccount_Should_Fail_If_clientCompanyOpiId_Is_Zero()
        {
            //Arrange
            int clientCompanyOpiId = 0;
            int authUserId = 10;
            var controller = new ClientCompanyAccountsController(null, null, null);
            
            //Act
            var result = controller.DeleteSettlementAccount(clientCompanyOpiId, authUserId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteSettlementAccount_Success_With_Valid_clientCompanyOpiId_Input()
        {
            //Arrange
            int clientCompanyOpiId = 110;
            int authUserId = 10;

            var clientCompanyAccountsServiceMock = new Mock<IClientCompanyAccountsService>();
            var clientCompanyAccountsUowMock = new Mock<IClientCompanyAccountsUoW>();
            var settlementServiceMock = new Mock<ISettlementService>();

            clientCompanyAccountsUowMock.Setup(x => x.GetClientCompanyAccount(It.IsAny<int>())).Returns(new ClientCompanyOpi());
            clientCompanyAccountsUowMock.Setup(x => x.UpdateAccount(It.IsAny<ClientCompanyOpi>()));
            clientCompanyAccountsUowMock.Setup(x => x.GetSettlementIDs(It.IsAny<int>())).Returns(new List<long>());
            settlementServiceMock.Setup(x => x.DeleteAssignedSettlements(It.IsAny<long>()));

            var controller = new ClientCompanyAccountsController(clientCompanyAccountsServiceMock.Object, settlementServiceMock.Object, null);
            
            //Act
            var result = controller.DeleteSettlementAccount(clientCompanyOpiId, authUserId);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void GetNumberOfAssignedSettlements_Should_Fail_If_clientCompanyOpiId_Is_Zero()
        {
            //Arrange
            int clientCompanyOpiId = 0;
            var controller = new ClientCompanyAccountsController(null, null, null);

            //Act
            var result = controller.GetNumberOfAssignedSettlements(clientCompanyOpiId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetNumberOfAssignedSettlements_Success_With_Valid_clientCompanyOpiId_Input()
        {
            //Arrange
            int clientCompanyOpiId = 110;

            var clientCompanyAccountsServiceMock = new Mock<IClientCompanyAccountsService>();
            var clientCompanyAccountsUowMock = new Mock<IClientCompanyAccountsUoW>();

            clientCompanyAccountsUowMock.Setup(x => x.GetAssociatedTradesCount(It.IsAny<int>(), It.IsAny<int>()));

            var controller = new ClientCompanyAccountsController(clientCompanyAccountsServiceMock.Object, null, null);

            //Act
            var result = controller.GetNumberOfAssignedSettlements(clientCompanyOpiId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
