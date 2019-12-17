using Argentex.Core.Api.Controllers.User;
using Argentex.Core.Api.Models.SecurityModels;
using Argentex.Core.Service;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.Users.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Argentex.Core.Api.Models;
using Argentex.Core.Service.Enums;
using FluentAssertions;
using Xunit;
using Argentex.Core.DataAccess.Entities;

namespace Argentex.Core.Api.Tests.User
{
    public class UserControllerTests
    {

        [Fact]
        public void GetApplicationUsersOfCompany_Success_With_Valid_ClientCompanyId_Input()
        {
            //Arrange
            var applicationServiceUser = new ApplicationServiceUser();
            var applicationServiceUserList = new List<ApplicationServiceUser>();
            applicationServiceUserList.Add(applicationServiceUser);

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetApplicationUsersOfCompany(1))
                .Returns(applicationServiceUserList);

            var controller = new UserController(mockUserService.Object, null, null,null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var result = controller.GetApplicationUsersOfCompany(1);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetApplicationUsersOfCompany_Returns_No_Content_When_Service_Returns_Empty_List()
        {
            //Arrange
            var applicationServiceUserList = new List<ApplicationServiceUser>();
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetApplicationUsersOfCompany(1))
                .Returns(applicationServiceUserList);

            var controller = new UserController(mockUserService.Object, null, null,null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var result = controller.GetApplicationUsersOfCompany(1);
            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetApplicationUser_Success_With_Valid_ClientCompanyId_Input()
        {
            //Arrange
            var applicationServiceUser = new ApplicationServiceUser();

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetApplicationUserAsync("1"))
                .Returns((Task.FromResult(applicationServiceUser)));

            var controller = new UserController(mockUserService.Object, null, null,null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var result = controller.GetApplicationUser(1).Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetApplicationUser_Failed_With_Bad_Request_When_Service_Returns_Null_User()
        {
            //Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetApplicationUserAsync("email@email.com"))
                .Returns(It.IsAny<Task<ApplicationServiceUser>>());

            var controller = new UserController(mockUserService.Object, null, null,null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var result = controller.GetApplicationUser(1).Result;
            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void AddUser_Success_With_Valid_Model_Input()
        {
            //Arrange
            var service = new Mock<IUserService>();
            service.Setup(x => x.AddUnapprovedUserAsync(It.IsAny<ApplicationServiceUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            service.Setup(x => x.GetRequestOrigin(It.IsAny<IIdentity>())).Returns(RequestOrigin.ClientSite);

            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(service.Object, logger.Object, null,null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);
            var expectedType = typeof(ResponseModel);

            //Act
            var response = controller.AddUser(new AddUserModel { Username = "user", Email = "user@email.co.uk" });
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Info");
        }


        [Fact]
        public async Task ApproveUserChangeRequest_Success_With_Valid_Model_Input()
        {
            //Arrange
            var service = new Mock<IUserService>();


            var approveUserChangeResponse = new ApproveUserChangeResponse()
            {
                Result = IdentityResult.Success,
                SendNotification = true,
                UserChangeRequest = null
            };


            service.Setup(x => x.ApproveUserChangeRequest(It.IsAny<ApproveUserChangeRequest>()))
                .ReturnsAsync(approveUserChangeResponse);

            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(service.Object, logger.Object, null, null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var result = await controller.ApproveUserChangeRequest(new ApproveUserChangeRequest() { UserChangeRequestID = 48, ApprovedByAuthUserId = 123 });

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void ApproveUserChangeRequest_Failed_When_InvalidModel_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IUserService>();
            var controller = new UserController(service.Object, null, null, null);
            controller.ModelState.AddModelError("", "Error");

            //Act
            var result = await controller.ApproveUserChangeRequest(null);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        

        [Fact]
        public void AddUser_Failed_When_Identity_Returns_Failed_Result_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IUserService>();
            service.Setup(x => x.AddUnapprovedUserAsync(It.IsAny<ApplicationServiceUser>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));

            service.Setup(x => x.GetRequestOrigin(It.IsAny<IIdentity>())).Returns(RequestOrigin.ClientSite);

            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(service.Object, logger.Object, null,null);
            
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()));

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);
            
            //Act
            var response = controller.AddUser(new AddUserModel { Username = "user", Email = "user@email.co.uk" });
            var result = response.Result as BadRequestObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
        }

        [Fact]
        public void AddUser_Failed_When_NoModel()
        {
            //Arrange
            var service = new Mock<IUserService>();
            var controller = new UserController(service.Object, null, null,null);

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            //Act
            var response = controller.AddUser(null);
            var result = response.Result as BadRequestObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            var responseModel = (ResponseModel)result.Value;
            responseModel.ResponseMessages.Should().NotBeEmpty();
            responseModel.ResponseMessages.Should().ContainKey("Errors");
            responseModel.ResponseMessages.Should().HaveCount(1);

        }

        [Fact]
        public void Edit_Success_With_Valid_Model_Input()
        {
            //Arrange
            var service = new Mock<IUserService>();
            service.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationServiceUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            service.Setup(x => x.GetRequestOrigin(It.IsAny<IIdentity>())).Returns(RequestOrigin.ClientSite);

            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(service.Object, logger.Object, null,null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
            
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);
            var expectedType = typeof(ResponseModel);

            //Act
            var response = controller.Update(new AddUserModel { Username = "rado", Email = "rado" });
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Info");
        }

        [Fact]
        public void Edit_Failed_When_Identity_Returns_Failed_Result_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IUserService>();
            service.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationServiceUser>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));

            service.Setup(x => x.GetRequestOrigin(It.IsAny<IIdentity>())).Returns(RequestOrigin.ClientSite);

            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(service.Object, logger.Object, null,null);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            //Act
            var response = controller.Update(new AddUserModel { Username = "rado", Email = "rado" });
            var result = response.Result as BadRequestObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
        }

        [Fact]
        public void Edit_Failed_When_InvalidModel_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IUserService>();
            var controller = new UserController(service.Object, null, null,null);
            controller.ModelState.AddModelError("", "Error");

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            //Act
            var response = controller.Update(null);
            var result = response.Result as BadRequestObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            var responseModel = (ResponseModel)result.Value;
            responseModel.ResponseMessages.Should().NotBeEmpty();
            responseModel.ResponseMessages.Should().ContainKey("Errors");
            responseModel.ResponseMessages.Should().HaveCount(1);
        }

        [Fact]
        public void UpdateMyAccount_Success_With_Valid_Model_Input()
        {
            //Arrange
            var service = new Mock<IUserService>();
            service.Setup(x => x.UpdateMyAccountAsync(It.IsAny<ApplicationServiceUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(service.Object, logger.Object, null,null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var result = controller.UpdateMyAccount(new UpdateUserModel { Username = "rado", Email = "rado" }).Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateMyAccount_Failed_When_Identity_Returns_Failed_Result_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IUserService>();
            service.Setup(x => x.UpdateMyAccountAsync(It.IsAny<ApplicationServiceUser>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));

            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(service.Object, logger.Object, null,null);

            //Act
            var result = controller.UpdateMyAccount(new UpdateUserModel { Username = "rado", Email = "rado" }).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateMyAccount_Failed_When_InvalidModel_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IUserService>();
            var controller = new UserController(service.Object, null, null,null);
            controller.ModelState.AddModelError("", "Error");

            //Act
            var result = controller.UpdateMyAccount(null).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void GetUnsettledClientCompanyContacts_Success_With_Valid_ClientCompanyId_Input()
        {
            //Arrange
            var mockObject = new ClientCompanyContactModel();
            var mockList = new List<ClientCompanyContactModel>();
            mockList.Add(mockObject);

            var mockService = new Mock<IUserService>();
            mockService.Setup(x => x.GetAuthorisedSignatories(1))
                .Returns(mockList);

            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x => x.Action(It.IsAny<Microsoft.AspNetCore.Mvc.Routing.UrlActionContext>()))
                .Returns("colbackUrl")
                .Verifiable();

            var mockLogger = new Mock<ILogWrapper>();
            mockLogger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(mockService.Object, mockLogger.Object, null,null);
            controller.Url = mockUrlHelper.Object;
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var result = controller.GetAuthorisedSignatories(1);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UpdateContact_Success_With_Valid_Model_Input()
        {
            //Arrange
            var service = new Mock<IUserService>();
            service.Setup(x => x.UpdateUserContactAsync(It.IsAny<ApplicationServiceUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            service.Setup(x => x.GetRequestOrigin(It.IsAny<IIdentity>())).Returns(RequestOrigin.ClientSite);
            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(service.Object, logger.Object, null, null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            var expectedStatusCode = HttpStatusCode.OK;
            var expectedResultType = typeof(OkObjectResult);
            var expectedType = typeof(ResponseModel);

            //Act
            var response = controller.UpdateContact(new AddUserModel { Username = "rado", Email = "rado" });
            var result = response.Result as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Info");
        }

        [Fact]
        public void UpdateContact_Failed_When_Identity_Returns_Failed_Result_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IUserService>();
            service.Setup(x => x.UpdateUserContactAsync(It.IsAny<ApplicationServiceUser>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));

            service.Setup(x => x.GetRequestOrigin(It.IsAny<IIdentity>())).Returns(RequestOrigin.ClientSite);

            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new UserController(service.Object, logger.Object, null, null);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            //Act
            var response = controller.UpdateContact(new AddUserModel { Username = "rado", Email = "rado" });
            var result = response.Result as BadRequestObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            ((ResponseModel)result.Value).ResponseMessages.Should().NotBeEmpty();
            ((ResponseModel)result.Value).ResponseMessages.Should().ContainKey("Errors");
        }

        [Fact]
        public void UpdateContact_Failed_When_InvalidModel_With_Bad_Request()
        {
            //Arrange
            var service = new Mock<IUserService>();
            var controller = new UserController(service.Object, null, null, null);
            controller.ModelState.AddModelError("", "Error");

            var expectedStatusCode = HttpStatusCode.BadRequest;
            var expectedResultType = typeof(BadRequestObjectResult);
            var expectedType = typeof(ResponseModel);

            //Act
            var response = controller.UpdateContact(null);
            var result = response.Result as BadRequestObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(expectedResultType);
            result.StatusCode.Should().Be((int)expectedStatusCode);
            result.Value.Should().BeOfType(expectedType);

            var responseModel = (ResponseModel)result.Value;
            responseModel.ResponseMessages.Should().NotBeEmpty();
            responseModel.ResponseMessages.Should().ContainKey("Errors");
            responseModel.ResponseMessages.Should().HaveCount(1);
        }
    }
}
