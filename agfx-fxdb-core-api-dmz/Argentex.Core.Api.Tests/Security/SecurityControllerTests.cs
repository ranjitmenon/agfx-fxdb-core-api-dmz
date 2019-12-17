using Argentex.Core.Api.Controllers.Security;
using Argentex.Core.Api.Models.AccountViewModels;
using Argentex.Core.Api.Models.SecurityModels;
using Argentex.Core.Service;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Identity.Services;
using Argentex.Core.Service.Models.Identity;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecLogger;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Argentex.Core.Api.Tests.Security
{
    public class SecurityControllerTests
    {
        [Fact]
        public void ForgotPassword_ModelNotValid()
        {
            var service = new Mock<IIdentityService>();
            
            var controller = new SecurityController(service.Object, null, null, null, null);
            controller.ModelState.AddModelError("", "Error");

            var result = controller.ForgotPassword(new ForgotPasswordViewModel()).Result;

            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        [Fact]
        public void ResetPasswordAsync_Model_not_valid()
        {
            var service = new Mock<IIdentityService>();

            var controller = new SecurityController(service.Object, null, null, null, null);
            controller.ModelState.AddModelError("", "Error");

            var result = controller.ResetPasswordAsync(new ResetPasswordViewModel()).Result;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateTokenTest_InvalidModel()
        {
            var service = new Mock<IIdentityService>();

            service.Setup(x => x.AuthenticateAsync(It.IsAny<LoginServiceModel>()))
                .Returns(Task.FromResult<TokenModel>(null));

            var controller = new SecurityController(service.Object, null, null, null, null);
            controller.ModelState.AddModelError("", "Error");

            var result = controller.CreateToken(new OpenIdConnectRequest()).Result;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateTokenTest_BadRequest()
        {
            var service = new Mock<IIdentityService>();

            service.Setup(x => x.AuthenticateAsync(It.IsAny<LoginServiceModel>()))
                .Returns(Task.FromResult<TokenModel>(null));

            var controller = new SecurityController(service.Object, null, null, null, null);

            var result = controller.CreateToken(new OpenIdConnectRequest { Username = "rado", Password = "rado" }).Result;

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateTokenTest_Success()
        {
            var service = new Mock<IIdentityService>();
            var smsService = new Mock<ISmsService>();

            var request = new OpenIdConnectRequest { Username = "rado", Password = "rado" };
            string tokenValidationCode = "ksd832nrkfksd";

            service.Setup(x => x.AuthenticateAsync(It.IsAny<LoginServiceModel>()))
                .Returns(Task.FromResult(new TokenModel { Access_token = "token", Expires_in = 3600 }));
            smsService.Setup(x => x.Send2FAMessage(It.IsAny<string>()))
                .Returns(Task.FromResult(tokenValidationCode));

            var controller = new SecurityController(service.Object, null, null, null, smsService.Object);

            var result = controller.CreateToken(request).Result;

            Assert.IsType<TokenModel>(((OkObjectResult)result).Value);
        }

        [Fact]
        public void ChangePassword_Success_With_Valid_Password_Input()
        {
            //Arrange
            var mockIdentityService = new Mock<IIdentityService>();
            mockIdentityService.Setup(x => x.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            mockIdentityService.Setup(x => x.AuthenticateAsync(It.IsAny<LoginServiceModel>()))
                .Returns(Task.FromResult(new TokenModel { Access_token = "token", Expires_in = 3600 }));

            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x => x.Action(It.IsAny<Microsoft.AspNetCore.Mvc.Routing.UrlActionContext>()))
                .Returns("colbackUrl")
                .Verifiable();

            var mockLogger = new Mock<ILogWrapper>();
            mockLogger.Setup(x => x.Error(It.IsAny<Exception>()));

            var controller = new SecurityController(mockIdentityService.Object, mockLogger.Object, null, null, null);
            controller.Url = mockUrlHelper.Object;
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Request.Scheme = "test";

            //Act
            var model = new ChangePasswordModel
            {
                UserId = 1,
                UserName = "Chuck Norris",
                CurrentPassword = "Abcd1234",
                NewPassword = "Changed1234",
            };

            var result = controller.ChangePassword(model).Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ChangePassword_Failed_With_Service_Returns_Failed()
        {
            //Arrange
            var changePasswordModel = new ChangePasswordModel()
            {
                UserId = 42,
                UserName = "chucknorris",
                CurrentPassword = "Abcd1234",
                NewPassword = "Abcd12345",
                ConfirmPassword = "Abcd12345"
            };
            var mockIdentityService = new Mock<IIdentityService>();
            var mockLogger = new Mock<ILogWrapper>();

            mockIdentityService.Setup(x => x.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));
            mockIdentityService.Setup(x => x.AuthenticateAsync(It.IsAny<LoginServiceModel>()))
                .Returns(Task.FromResult(new TokenModel { Access_token = "token", Expires_in = 3600 }));
            
            var controller = new SecurityController(mockIdentityService.Object, mockLogger.Object, null, null, null);

            var expectedStatusCode = HttpStatusCode.BadRequest;

            //Act
            var result = controller.ChangePassword(changePasswordModel).Result;
            var objectResult = result as BadRequestObjectResult;

            //Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int) expectedStatusCode, objectResult.StatusCode);
        }

        [Fact]
        public void ChangePassword_Should_Return_An_OK_Result()
        {
            //Arrange
            var model = new ChangePasswordModel
            {
                UserId = 1,
                UserName = "testaccount",
                CurrentPassword = "Abcd1234",
                NewPassword = "Changed1234",
                ConfirmPassword = "Changed1234"
            };


            var mockLogger = new Mock<ILogWrapper>();
            var mockIdentityService = new Mock<IIdentityService>();

            mockIdentityService.Setup(x => x.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
           
            var controller = new SecurityController(mockIdentityService.Object, mockLogger.Object, null, null, null);
            
            //Act
            var result = controller.ChangePassword(model).Result;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ChangePassword_Should_Return_A_Bad_Request_When_PasswordDoNotMatchException_Is_Caught()
        {
            // Given
            var model = new ChangePasswordModel
            {
                UserId = 1,
                UserName = "testaccount",
                CurrentPassword = "Abcd1234",
                NewPassword = "Changed1234",
                ConfirmPassword = "Changed1234"
            };
            
            var mockLogger = new Mock<ILogWrapper>();
            var mockIdentityService = new Mock<IIdentityService>();

            mockIdentityService.Setup(x => x.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new PasswordsDoNotMatchException());

            var controller = new SecurityController(mockIdentityService.Object, mockLogger.Object, null, null, null);

            // When
            var result = controller.ChangePassword(model).Result;

            // Then
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ChangePassword_Should_Return_A_Bad_Request_When_ApplicationUserNotFoundException_Is_Caught()
        {
            // Given
            var model = new ChangePasswordModel
            {
                UserId = 1,
                UserName = "testaccount",
                CurrentPassword = "Abcd1234",
                NewPassword = "Changed1234",
                ConfirmPassword = "Changed1234"
            };

            var mockLogger = new Mock<ILogWrapper>();
            var mockIdentityService = new Mock<IIdentityService>();

            mockIdentityService.Setup(x => x.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new ApplicationUserNotFoundException());

            var controller = new SecurityController(mockIdentityService.Object, mockLogger.Object, null, null, null);

            // When
            var result = controller.ChangePassword(model).Result;

            // Then
            Assert.IsType<BadRequestObjectResult>(result);
        }

      
        [Fact]
        public void ChangePassword_Should_Return_A_Bad_Request_When_PasswordAlreadyUsedException_Is_Caught()
        {
            // Given
            var model = new ChangePasswordModel
            {
                UserId = 1,
                UserName = "testaccount",
                CurrentPassword = "Abcd1234",
                NewPassword = "Changed1234",
                ConfirmPassword = "Changed1234"
            };

            var mockLogger = new Mock<ILogWrapper>();
            var mockIdentityService = new Mock<IIdentityService>();

            mockIdentityService.Setup(x => x.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new PasswordAlreadyUsedException());

            var controller = new SecurityController(mockIdentityService.Object, mockLogger.Object, null, null, null);

            // When
            var result = controller.ChangePassword(model).Result;

            // Then
            Assert.IsType<BadRequestObjectResult>(result);
        }

        //[Fact]
        //public void ForgotPassword_Success_With_Valid_UserId_Input()
        //{
        //    //Arrange
        //    var model = new ForgotPasswordViewModel
        //    {
        //        Email = "test@synetec.co.uk",
        //        SiteUrl = "http://www.aUrl.co.uk/"
        //    };

        //    var mockIdentityService = new Mock<IIdentityService>();
        //    mockIdentityService.Setup(x => x.SendResetPasswordEmailAsync(model.Email, model.SiteUrl))
        //        .Returns(Task.FromResult(IdentityResult.Success));

        //    var mockUrlHelper = new Mock<IUrlHelper>();
        //    mockUrlHelper.Setup(x => x.Action(It.IsAny<Microsoft.AspNetCore.Mvc.Routing.UrlActionContext>()))
        //        .Returns("colbackUrl")
        //        .Verifiable();

        //    var mockLogger = new Mock<ILogWrapper>();
        //    mockLogger.Setup(x => x.Error(It.IsAny<Exception>()));

        //    var controller = new SecurityController(mockIdentityService.Object, mockLogger.Object, null, null);
        //    controller.Url = mockUrlHelper.Object;
        //    controller.ControllerContext = new ControllerContext();
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext();
        //    controller.ControllerContext.HttpContext.Request.Scheme = "test";

        //    //Act
        //    var result = controller.ForgotPassword(model).Result;

        //    //Assert
        //    Assert.IsType<OkObjectResult>(result);
        //}

        //[Fact]
        //public void ForgotPassword_Failed_With_Service_Returns_Failed()
        //{
        //    //Arrange
        //    var model = new ForgotPasswordViewModel
        //    {
        //        Email = "test@synetec.co.uk",
        //        SiteUrl = "http://www.aUrl.co.uk/"
        //    };

        //    var mockIdentityService = new Mock<IIdentityService>();
        //    mockIdentityService.Setup(x => x.SendResetPasswordEmailAsync(model.Email, model.SiteUrl))
        //        .Returns(Task.FromResult(IdentityResult.Failed()));
        //    mockIdentityService.Setup(x => x.AuthenticateAsync(It.IsAny<LoginServiceModel>()))
        //        .Returns(Task.FromResult(new TokenModel { Access_token = "token", Expires_in = 3600 }));

        //    var mockUrlHelper = new Mock<IUrlHelper>();
        //    mockUrlHelper.Setup(x => x.Action(It.IsAny<Microsoft.AspNetCore.Mvc.Routing.UrlActionContext>()))
        //        .Returns("colbackUrl")
        //        .Verifiable();

        //    var mockLogger = new Mock<ILogWrapper>();
        //    mockLogger.Setup(x => x.Error(It.IsAny<Exception>()));

        //    var controller = new SecurityController(mockIdentityService.Object, mockLogger.Object, null, null);
        //    controller.Url = mockUrlHelper.Object;
        //    controller.ControllerContext = new ControllerContext();
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext();
        //    controller.ControllerContext.HttpContext.Request.Scheme = "test";

        //    //Act
        //    var result = controller.ForgotPassword(model).Result;
        //    //Assert
        //    Assert.IsNotType<OkObjectResult>(result);
        //}

        //[Fact]
        //public void ForgotPassword_Failed_When_Identity_Returns_BadRequest()
        //{
        //    //Arrange
        //    var model = new ForgotPasswordViewModel
        //    {
        //        Email = "test@synetec.co.uk",
        //        SiteUrl = "http://www.aUrl.co.uk/"
        //    };

        //    var mockIdentityService = new Mock<IIdentityService>();
        //    mockIdentityService.Setup(x => x.SendResetPasswordEmailAsync(model.Email, model.SiteUrl))
        //        .Returns(Task.FromResult(IdentityResult.Failed()));
        //    mockIdentityService.Setup(x => x.AuthenticateAsync(It.IsAny<LoginServiceModel>()))
        //        .Returns(Task.FromResult(new TokenModel { Access_token = "token", Expires_in = 3600 }));

        //    var mockUrlHelper = new Mock<IUrlHelper>();
        //    mockUrlHelper.Setup(x => x.Action(It.IsAny<Microsoft.AspNetCore.Mvc.Routing.UrlActionContext>()))
        //        .Returns("colbackUrl")
        //        .Verifiable();

        //    var mockLogger = new Mock<ILogWrapper>();
        //    mockLogger.Setup(x => x.Error(It.IsAny<Exception>()));

        //    var controller = new SecurityController(mockIdentityService.Object, mockLogger.Object, null, null);
        //    controller.Url = mockUrlHelper.Object;
        //    controller.ControllerContext = new ControllerContext();
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext();
        //    controller.ControllerContext.HttpContext.Request.Scheme = "test";

        //    //Act
        //    var result = controller.ForgotPassword(model).Result;

        //    //Assert
        //    Assert.IsType<BadRequestObjectResult>(result);
        //}

    }
}
