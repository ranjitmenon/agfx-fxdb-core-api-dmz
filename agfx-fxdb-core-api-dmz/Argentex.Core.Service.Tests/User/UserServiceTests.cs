using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Identity.Services;
using Argentex.Core.Service.Models.ClientCompany;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.Users;
using Argentex.Core.UnitsOfWork.Users.Model;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Argentex.Core.Service.AppSettings;
using FluentAssertions;
using Xunit;
using Argentex.Core.Service.Sms.SmsSender;
using Argentex.Core.Service.Sms.Models;

namespace Argentex.Core.Service.Tests.User
{
    public class UserServiceTests
    {
        [Fact]
        public void GetApplicationUserAsync_Success_With_Correct_And_Different_Password()
        {
            //Arrange
            Mock<IUserUow> mockUserUow = mockUserFactory();

            var user = new ApplicationUser
            {
                Id = 1,
                UserName = "testaccount",
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                PasswordHash = "AQAAAAEAACcQAAAAEOWPaTOV6BsIYqsXovwkD8z9mxu51bAUQXkDgdM/8yUwGm4KQtU21diwdb9UWm5HlA==", //password hash for "Abcd1234"
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                ClientCompanyContactId = 100,
                PrimaryContact = false,
                Birthday = DateTime.Now
            };

            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            var emailSenderMock = new Mock<IEmailSender>();

            var service = new UserService(null, mockUserUow.Object, emailSenderMock.Object, null, null, null, null);

            //Act
            var result = service.GetApplicationUserAsync("1").Result;

            //Assert
            Assert.IsType<ApplicationServiceUser>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void AddUnapprovedUserAsync_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();
            mockUserUow.Setup(uow => uow.AddUserAsync(It.IsAny<ClientUserModel>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            mockUserUow.Setup(x => x.ValidateUserDetails(It.IsAny<UserValidationModel>())).Returns(IdentityResult.Success);

            var mockConfig = new Mock<IConfigWrapper>();
            mockConfig.Setup(x => x.Get(It.IsAny<string>())).Returns(It.IsAny<string>());

            var mockIdentityService = new Mock<IIdentityService>();
            var emailSenderMock = new Mock<IEmailSender>();

            var service = new UserService(mockConfig.Object, mockUserUow.Object, emailSenderMock.Object, mockIdentityService.Object, null, null, null);

            //Act
            ApplicationServiceUser testServiceUser = new ApplicationServiceUser
            {
                Username = "testaccount",
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                Password = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                Birthday = "10/10/2010"

            };
            IdentityResult result = service.AddUnapprovedUserAsync(testServiceUser).Result;

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Fact]
        public void AddUnapprovedUserAsync_Successful_Set_User_As_Admin()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();
            var configMock = new Mock<IConfigWrapper>();
            mockUserUow.Setup(x => x.ValidateUserDetails(It.IsAny<UserValidationModel>()))
                .Returns(IdentityResult.Success);
            mockUserUow.Setup(uow => uow.AddUserAsync(It.IsAny<ClientUserModel>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            mockUserUow.Setup(x => x.GetRole(It.IsAny<string>()))
                .Returns((new List<ApplicationRole>
                {
                    new ApplicationRole()
                    {
                        Id = 42
                    }
                }).AsQueryable());

            mockUserUow.Setup(x => x.SetRoleForUser(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(true));
            configMock.Setup(x => x.Get("GeneratedPassword")).Returns("P@55w0Rd!");

            mockUserUow.Setup(x => x.SetRoleForUser(It.IsAny<long>(), It.IsAny<long>()));

            var mockIdentityService = new Mock<IIdentityService>();

            var service = new UserService(configMock.Object, mockUserUow.Object, null, mockIdentityService.Object, null, null, null);

            //Act
            ApplicationServiceUser testServiceUser = new ApplicationServiceUser
            {
                Id = 1,
                Username = "testaccount",
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                Password = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                IsAdmin = true,
                Birthday = "10/10/2010"

            };
            var result = service.AddUnapprovedUserAsync(testServiceUser).Result;

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Fact]
        public void AddUnapprovedUserAsync_Failed_When_It_Has_Duplicate_Email_And_Username_Input()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();
            mockUserUow.Setup(uow => uow.AddUserAsync(It.IsAny<ClientUserModel>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            IdentityResult identityResult = IdentityResult.Failed(
                new IdentityError() { Description = "Username must be unique" },
                new IdentityError() { Description = "Email must be unique within the Client Company Account" });

            mockUserUow.Setup(x => x.ValidateUserDetails(It.IsAny<UserValidationModel>())).Returns(identityResult);

            var mockConfig = new Mock<IConfigWrapper>();
            mockConfig.Setup(x => x.Get(It.IsAny<string>())).Returns(It.IsAny<string>());

            var mockIdentityService = new Mock<IIdentityService>();
            var emailSenderMock = new Mock<IEmailSender>();

            var service = new UserService(mockConfig.Object, mockUserUow.Object, emailSenderMock.Object, mockIdentityService.Object, null, null, null);

            //Act
            ApplicationServiceUser testServiceUser = new ApplicationServiceUser
            {
                Username = "testaccount",
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                Password = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                Birthday = "10/10/2010"

            };
            IdentityResult result = service.AddUnapprovedUserAsync(testServiceUser).Result;

            //Assert

            result.Should().BeEquivalentTo(identityResult);
        }

        [Fact]
        public void UpdateUserAsync_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            var originalUser = new ApplicationUser
            {
                Id = 1,
                UserName = "testaccount",
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                PasswordHash = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                ClientCompanyContactId = 100,
                Birthday = DateTime.Now
            };

            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(originalUser));
            mockUserUow.Setup(x => x.ValidateUserDetails(It.IsAny<UserValidationModel>()))
                .Returns(IdentityResult.Success);
            mockUserUow.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationUser>(), originalUser))
                .Returns(Task.FromResult(IdentityResult.Success));

            var emailSenderMock = new Mock<IEmailSender>();

            var service = new UserService(null, mockUserUow.Object, emailSenderMock.Object, null, null, null, null);

            //Act
            ApplicationServiceUser userToUpdate = new ApplicationServiceUser
            {
                Id = 1,
                Username = "testaccount",
                Email = "updatedaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                Password = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                Birthday = "10/10/2010"
            };

            IdentityResult result = service.UpdateUserAsync(userToUpdate).Result;

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Fact]
        public void UpdateUserAsync_Failed_When_It_Has_Duplicate_Email_And_Username_Input()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            var originalUser = new ApplicationUser
            {
                Id = 1,
                UserName = "testaccount",
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                PasswordHash = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                ClientCompanyContactId = 100,
                Birthday = DateTime.Now
            };

            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(originalUser));

            IdentityResult identityResult = IdentityResult.Failed(
                new IdentityError() { Description = "Username must be unique" },
                new IdentityError() { Description = "Email must be unique within the Client Company Account" });

            mockUserUow.Setup(x => x.ValidateUserDetails(It.IsAny<UserValidationModel>()))
                .Returns(identityResult);

            mockUserUow.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var emailSenderMock = new Mock<IEmailSender>();

            var service = new UserService(null, mockUserUow.Object, emailSenderMock.Object, null, null, null, null);

            //Act
            ApplicationServiceUser userToUpdate = new ApplicationServiceUser
            {
                Id = 1,
                Username = "testaccount",
                Email = "updatedaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                Password = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                Birthday = "10/10/2010"
            };

            IdentityResult result = service.UpdateUserAsync(userToUpdate).Result;

            //Assert
            result.Should().BeEquivalentTo(identityResult);
        }

        [Fact]
        public void UpdateMyAccountAsync_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            var originalUser = new ApplicationUser
            {
                Id = 1,
                UserName = "testaccount",
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                PasswordHash = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                ClientCompanyContactId = 100,
                Birthday = DateTime.Now
            };

            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(originalUser));
            mockUserUow.Setup(x => x.ValidateUserDetails(It.IsAny<UserValidationModel>()))
                .Returns(IdentityResult.Success);
            mockUserUow.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationUser>(), originalUser))
                .Returns(Task.FromResult(IdentityResult.Success));

            var service = new UserService(null, mockUserUow.Object, null, null, null, null, null);

            //Act
            ApplicationServiceUser userToUpdate = new ApplicationServiceUser
            {
                Id = 1,
                Username = "testaccount",
                Email = "updatedaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                Password = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                Birthday = "10/10/2010"
            };

            IdentityResult result = service.UpdateUserAsync(userToUpdate).Result;

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Fact]
        public void ApproveUsersAsync_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var user = new ApplicationUser
            {
                Id = 1,
                UserName = "testaccount",
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                PasswordHash = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                ClientCompanyContactId = 100,
                Birthday = DateTime.Now
            };

            List<ClientCompaniesModel> clientCompanies = new List<ClientCompaniesModel>
            {
                new ClientCompaniesModel {ClientCompanyId = 22, ClientCompanyName = "Abc"},
                new ClientCompaniesModel {ClientCompanyId = 23, ClientCompanyName = "Xyz"},
            };

            var mockUserUow = new Mock<IUserUow>();
            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            mockUserUow.Setup(x => x.ApproveUserAsync(user))
                .Returns(Task.FromResult(IdentityResult.Success));

            var mockConfig = new Mock<IConfigWrapper>();
            mockConfig.Setup(x => x.Get(It.IsAny<string>())).Returns("Url");

            var mockIdentityService = new Mock<IIdentityService>();
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendUserNewPasswordEmailAsync(It.IsAny<string>(), null)).
                Returns(Task.FromResult(IdentityResult.Success));

            var service = new UserService(mockConfig.Object, mockUserUow.Object, null, mockIdentityService.Object, mockEmailService.Object, null, null);

            //Act
            ApproveUsersRequest approveUsersRequest = new ApproveUsersRequest
            {
                ApproverAuthUserId = 1,
                UserIdsToApprove = new List<int> { 1 }
            };

            IdentityResult result = service.ApproveUsersAsync(approveUsersRequest, clientCompanies).Result[0];

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Fact]
        public void DeleteUserAsync_Successful_When_It_Has_The_Correct_Input()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            var user = new ApplicationUser
            {
                Id = 1,
                UserName = "testaccount",
                Email = "testaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                PasswordHash = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                ClientCompanyContactId = 100
            };

            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            mockUserUow.Setup(x => x.DeleteUserAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var emailSenderMock = new Mock<IEmailSender>();

            var service = new UserService(null, mockUserUow.Object, emailSenderMock.Object, null, null, null, null);

            //Act
            IdentityResult result = service.DeleteUserAsync("email@email.com").Result;

            //Assert
            Assert.Equal(IdentityResult.Success, result);
        }

        [Fact]
        public void UpdateUserAsync_Identity_Fails_When_Inputted_With_An_Empty_Or_Invalid_Id()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((ApplicationUser)null));
            mockUserUow.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var emailSenderMock = new Mock<IEmailSender>();
            var service = new UserService(null, mockUserUow.Object, emailSenderMock.Object, null, null, null, null);

            //Act
            ApplicationServiceUser userToUpdate = new ApplicationServiceUser
            {
                Id = 1,
                Username = "testaccount",
                Email = "updatedaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                Password = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111
            };

            IdentityResult result = service.UpdateUserAsync(userToUpdate).Result;

            //Assert
            Assert.Equal(IdentityResult.Failed().Succeeded, result.Succeeded);
        }

        [Fact]
        public async Task Approve_UserChangeRequest_SuccessAsync()
        {

            var userChangeRequest = new UserChangeRequest()
            {
                Id = 60,
                AuthUserId = 1400,
                CurrentValue = "rmenon9@synetec.co.uk",
                ProposedValue = "rmenon94@synetec.co.uk",
                ChangeValueType = "Email",
                ChangeDateTime = DateTime.Now,
                ChangedByAuthUserId = 1352,
                ChangeStatus = "Pending"
            };

            var approveUserChangeResponse = new ApproveUserChangeResponse()
            {
                Result = IdentityResult.Success,
                SendNotification = true,
                UserChangeRequest = null
            };

            var approveUserChangeRequest = new ApproveUserChangeRequest
            {
                UserChangeRequestID = 60,
                ApprovedByAuthUserId = 1352
            };

            //Arrange
            var mockUserUow = new Mock<IUserUow>();
            mockUserUow.Setup(x => x.GetUserChangeRequest(It.IsAny<int>()))
                .Returns(userChangeRequest);
            mockUserUow.Setup(x => x.ApproveUserChangeRequest(It.IsAny<ApproveUserChangeRequest>()))
                .Returns(Task.FromResult(approveUserChangeResponse));

            var emailSenderMock = new Mock<IEmailSender>();
            var service = new UserService(null, mockUserUow.Object, emailSenderMock.Object, null, null, null, null);

            var result = await service.ApproveUserChangeRequest(approveUserChangeRequest);

            //Assert
            Assert.Equal(IdentityResult.Success.Succeeded, result.Result.Succeeded);

        }


        [Fact]
        public async void Approve_UserChangeRequest_Failed()
        {
            var userChangeRequest = new UserChangeRequest()
            {
                Id = 60,
                AuthUserId = 1400,
                CurrentValue = "rmenon9@synetec.co.uk",
                ProposedValue = "rmenon94@synetec.co.uk",
                ChangeValueType = "Email",
                ChangeDateTime = DateTime.Now,
                ChangedByAuthUserId = 1352,
                ChangeStatus = "Pending"
            };

            var approveUserChangeResponse = new ApproveUserChangeResponse()
            {
                Result = IdentityResult.Failed(),
                SendNotification = true,
                UserChangeRequest = null
            };

            var approveUserChangeRequest = new ApproveUserChangeRequest
            {
                UserChangeRequestID = 60,
                ApprovedByAuthUserId = 1352
            };

            //Arrange
            var mockUserUow = new Mock<IUserUow>();
            mockUserUow.Setup(x => x.GetUserChangeRequest(It.IsAny<int>()))
                .Returns(userChangeRequest);
            mockUserUow.Setup(x => x.ApproveUserChangeRequest(It.IsAny<ApproveUserChangeRequest>()))
                .ReturnsAsync(approveUserChangeResponse);

            var emailSenderMock = new Mock<IEmailSender>();
            var service = new UserService(null, mockUserUow.Object, emailSenderMock.Object, null, null, null, null);

            var result = await service.ApproveUserChangeRequest(approveUserChangeRequest);

            //Assert
            Assert.Equal(IdentityResult.Failed().Succeeded, result.Result.Succeeded);

        }

        [Fact]
        public async Task Approve_UserChangeRequest_SendSMSSuccess_with_no_default_mobile_number_provided()
        {

            var userChangeRequest = new UserChangeRequest()
            {
                Id = 60,
                AuthUserId = 1400,
                CurrentValue = "test1@synetec.co.uk",
                ProposedValue = "test2@synetec.co.uk",
                ChangeValueType = "Email",
                ChangeDateTime = DateTime.Now,
                ChangedByAuthUserId = 1352,
                ChangeStatus = "Pending"
            };

            var approveUserChangeResponse = new ApproveUserChangeResponse()
            {
                Result = IdentityResult.Success,
                SendNotification = true,
                UserChangeRequest = userChangeRequest
            };

            var approveUserChangeRequest = new ApproveUserChangeRequest
            {
                UserChangeRequestID = 60,
                ApprovedByAuthUserId = 1352
            };

            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendUserNewPasswordEmailAsync(It.IsAny<string>(), null)).
                Returns(Task.FromResult(IdentityResult.Success));

            mockUserUow.Setup(x => x.GetSendersPhoneNumber(It.IsAny<int>())).Returns("442222222222");

            mockUserUow.Setup(x => x.ApproveUserChangeRequest(approveUserChangeRequest))
                .Returns(Task.FromResult(approveUserChangeResponse));

            var emailSenderMock = new Mock<IEmailSender>();
            var smsSenderMock = new Mock<ISmsSender>();
            var configWrapperMock = new Mock<IConfigWrapper>();
            configWrapperMock.Setup(x => x.Get("Sms:DefaultPhoneNumber")).Returns("");


            var service = new UserService(configWrapperMock.Object, mockUserUow.Object, emailSenderMock.Object,null, mockEmailService.Object, null, smsSenderMock.Object);
            var result = await service.ApproveUserChangeRequest(approveUserChangeRequest);

            //Assert
            mockUserUow.Verify(m => m.GetSendersPhoneNumber(It.IsAny<int>()), Times.Once);
            smsSenderMock.Verify(m => m.SendMessage(It.IsAny<SmsModel>(), Enums.SmsProviders.TextMagic),Times.Once);

            mockUserUow.Verify(m => m.GetSendersEmailAddress(It.IsAny<int>()), Times.Never);
            mockEmailService.Verify(m =>  m.SendMobileChangeEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }


        [Fact]
        public async Task Approve_UserChangeRequest_SendSMSSuccess_with_default_mobile_number_provided()
        {

            var userChangeRequest = new UserChangeRequest()
            {
                Id = 60,
                AuthUserId = 1400,
                CurrentValue = "test1@synetec.co.uk",
                ProposedValue = "test2@synetec.co.uk",
                ChangeValueType = "Email",
                ChangeDateTime = DateTime.Now,
                ChangedByAuthUserId = 1352,
                ChangeStatus = "Pending"
            };

            var approveUserChangeResponse = new ApproveUserChangeResponse()
            {
                Result = IdentityResult.Success,
                SendNotification = true,
                UserChangeRequest = userChangeRequest
            };

            var approveUserChangeRequest = new ApproveUserChangeRequest
            {
                UserChangeRequestID = 60,
                ApprovedByAuthUserId = 1352
            };

            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendUserNewPasswordEmailAsync(It.IsAny<string>(), null)).
                Returns(Task.FromResult(IdentityResult.Success));

            mockUserUow.Setup(x => x.ApproveUserChangeRequest(approveUserChangeRequest))
                .Returns(Task.FromResult(approveUserChangeResponse));

            var emailSenderMock = new Mock<IEmailSender>();
            var smsSenderMock = new Mock<ISmsSender>();
            var configWrapperMock = new Mock<IConfigWrapper>();
            configWrapperMock.Setup(x => x.Get("Sms:DefaultPhoneNumber")).Returns("442222222222");


            var service = new UserService(configWrapperMock.Object, mockUserUow.Object, emailSenderMock.Object, null, mockEmailService.Object, null, smsSenderMock.Object);
            var result = await service.ApproveUserChangeRequest(approveUserChangeRequest);

            //Assert
            mockUserUow.Verify(m => m.GetSendersPhoneNumber(It.IsAny<int>()), Times.Never);
            smsSenderMock.Verify(m => m.SendMessage(It.IsAny<SmsModel>(), Enums.SmsProviders.TextMagic), Times.Once);

            mockUserUow.Verify(m => m.GetSendersEmailAddress(It.IsAny<int>()), Times.Never);
            mockEmailService.Verify(m => m.SendMobileChangeEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Approve_UserChangeRequest_SendEmailSuccess_with_default_email_provided()
        {

            var userChangeRequest = new UserChangeRequest()
            {
                Id = 60,
                AuthUserId = 1400,
                CurrentValue = "442222222222",
                ProposedValue = "443333333333",
                ChangeValueType = "Telephone",
                ChangeDateTime = DateTime.Now,
                ChangedByAuthUserId = 1352,
                ChangeStatus = "Pending"
            };

            var approveUserChangeResponse = new ApproveUserChangeResponse()
            {
                Result = IdentityResult.Success,
                SendNotification = true,
                UserChangeRequest = userChangeRequest
            };

            var approveUserChangeRequest = new ApproveUserChangeRequest
            {
                UserChangeRequestID = 60,
                ApprovedByAuthUserId = 1352
            };

            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendUserNewPasswordEmailAsync(It.IsAny<string>(), null)).
                Returns(Task.FromResult(IdentityResult.Success));

            mockUserUow.Setup(x => x.ApproveUserChangeRequest(approveUserChangeRequest))
                .Returns(Task.FromResult(approveUserChangeResponse));

            var emailSenderMock = new Mock<IEmailSender>();
            var smsSenderMock = new Mock<ISmsSender>();
            var configWrapperMock = new Mock<IConfigWrapper>();
            configWrapperMock.Setup(x => x.Get("Emails:DefaultEmail")).Returns("test@synetec.co.uk");


            var service = new UserService(configWrapperMock.Object, mockUserUow.Object, emailSenderMock.Object, null, mockEmailService.Object, null, smsSenderMock.Object);
            var result = await service.ApproveUserChangeRequest(approveUserChangeRequest);

            //Assert
            mockUserUow.Verify(m => m.GetSendersEmailAddress(It.IsAny<int>()), Times.Never);
            mockEmailService.Verify(m => m.SendMobileChangeEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            mockUserUow.Verify(m => m.GetSendersPhoneNumber(It.IsAny<int>()), Times.Never);
            smsSenderMock.Verify(m => m.SendMessage(It.IsAny<SmsModel>(), Enums.SmsProviders.TextMagic), Times.Never);
        }


        [Fact]
        public async Task Approve_UserChangeRequest_SendEmailSuccess_with_no_default_email_provided()
        {

            var userChangeRequest = new UserChangeRequest()
            {
                Id = 60,
                AuthUserId = 1400,
                CurrentValue = "442222222222",
                ProposedValue = "443333333333",
                ChangeValueType = "Telephone",
                ChangeDateTime = DateTime.Now,
                ChangedByAuthUserId = 1352,
                ChangeStatus = "Pending"
            };

            var approveUserChangeResponse = new ApproveUserChangeResponse()
            {
                Result = IdentityResult.Success,
                SendNotification = true,
                UserChangeRequest = userChangeRequest
            };

            var approveUserChangeRequest = new ApproveUserChangeRequest
            {
                UserChangeRequestID = 60,
                ApprovedByAuthUserId = 1352
            };

            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            mockUserUow.Setup(x => x.GetSendersEmailAddress(It.IsAny<int>())).Returns("test@synetec.co.uk");

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendUserNewPasswordEmailAsync(It.IsAny<string>(), null)).
                Returns(Task.FromResult(IdentityResult.Success));

            mockUserUow.Setup(x => x.ApproveUserChangeRequest(approveUserChangeRequest))
                .Returns(Task.FromResult(approveUserChangeResponse));

            var emailSenderMock = new Mock<IEmailSender>();
            var smsSenderMock = new Mock<ISmsSender>();
            var configWrapperMock = new Mock<IConfigWrapper>();
            configWrapperMock.Setup(x => x.Get("Emails:DefaultEmail")).Returns("");


            var service = new UserService(configWrapperMock.Object, mockUserUow.Object, emailSenderMock.Object, null, mockEmailService.Object, null, smsSenderMock.Object);
            var result = await service.ApproveUserChangeRequest(approveUserChangeRequest);

            //Assert
            mockUserUow.Verify(m => m.GetSendersEmailAddress(It.IsAny<int>()), Times.Once);
            mockEmailService.Verify(m => m.SendMobileChangeEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            mockUserUow.Verify(m => m.GetSendersPhoneNumber(It.IsAny<int>()), Times.Never);
            smsSenderMock.Verify(m => m.SendMessage(It.IsAny<SmsModel>(), Enums.SmsProviders.TextMagic), Times.Never);
        }


        [Fact]
        public async Task Approve_UserChangeRequest_NoEmail_WithLessthanTwoApprovals()
        {

            var userChangeRequest = new UserChangeRequest()
            {
                Id = 60,
                AuthUserId = 1400,
                CurrentValue = "442222222222",
                ProposedValue = "443333333333",
                ChangeValueType = "Telephone",
                ChangeDateTime = DateTime.Now,
                ChangedByAuthUserId = 1352,
                ChangeStatus = "Pending"
            };

            var approveUserChangeResponse = new ApproveUserChangeResponse()
            {
                Result = IdentityResult.Success,
                SendNotification = false,
                UserChangeRequest = userChangeRequest
            };

            var approveUserChangeRequest = new ApproveUserChangeRequest
            {
                UserChangeRequestID = 60,
                ApprovedByAuthUserId = 1352
            };

            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendUserNewPasswordEmailAsync(It.IsAny<string>(), null)).
                Returns(Task.FromResult(IdentityResult.Success));

            mockUserUow.Setup(x => x.ApproveUserChangeRequest(approveUserChangeRequest))
                .Returns(Task.FromResult(approveUserChangeResponse));

            var emailSenderMock = new Mock<IEmailSender>();
            var smsSenderMock = new Mock<ISmsSender>();
            var configWrapperMock = new Mock<IConfigWrapper>();
            configWrapperMock.Setup(x => x.Get("Emails:DefaultEmail")).Returns("test@synetec.co.uk");


            var service = new UserService(configWrapperMock.Object, mockUserUow.Object, emailSenderMock.Object, null, mockEmailService.Object, null, smsSenderMock.Object);
            var result = await service.ApproveUserChangeRequest(approveUserChangeRequest);

            //Assert
            mockUserUow.Verify(m => m.GetSendersEmailAddress(It.IsAny<int>()), Times.Never);
            mockEmailService.Verify(m => m.SendMobileChangeEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            mockUserUow.Verify(m => m.GetSendersPhoneNumber(It.IsAny<int>()), Times.Never);
            smsSenderMock.Verify(m => m.SendMessage(It.IsAny<SmsModel>(), Enums.SmsProviders.TextMagic), Times.Never);
        }

        [Fact]
        public async Task Approve_UserChangeRequest_NoSMS_WithLessthanTwoApprovals()
        {

            var userChangeRequest = new UserChangeRequest()
            {
                Id = 60,
                AuthUserId = 1400,
                CurrentValue = "test1@synetec.co.uk",
                ProposedValue = "test2@synetec.co.uk",
                ChangeValueType = "Email",
                ChangeDateTime = DateTime.Now,
                ChangedByAuthUserId = 1352,
                ChangeStatus = "Pending"
            };

            var approveUserChangeResponse = new ApproveUserChangeResponse()
            {
                Result = IdentityResult.Success,
                SendNotification = false,
                UserChangeRequest = userChangeRequest
            };

            var approveUserChangeRequest = new ApproveUserChangeRequest
            {
                UserChangeRequestID = 60,
                ApprovedByAuthUserId = 1352
            };

            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendUserNewPasswordEmailAsync(It.IsAny<string>(), null)).
                Returns(Task.FromResult(IdentityResult.Success));

            mockUserUow.Setup(x => x.ApproveUserChangeRequest(approveUserChangeRequest))
                .Returns(Task.FromResult(approveUserChangeResponse));

            var emailSenderMock = new Mock<IEmailSender>();
            var smsSenderMock = new Mock<ISmsSender>();
            var configWrapperMock = new Mock<IConfigWrapper>();
            configWrapperMock.Setup(x => x.Get("Sms:DefaultPhoneNumber")).Returns("442222222222");


            var service = new UserService(configWrapperMock.Object, mockUserUow.Object, emailSenderMock.Object, null, mockEmailService.Object, null, smsSenderMock.Object);
            var result = await service.ApproveUserChangeRequest(approveUserChangeRequest);

            //Assert
            mockUserUow.Verify(m => m.GetSendersPhoneNumber(It.IsAny<int>()), Times.Never);
            smsSenderMock.Verify(m => m.SendMessage(It.IsAny<SmsModel>(), Enums.SmsProviders.TextMagic), Times.Never);

            mockUserUow.Verify(m => m.GetSendersEmailAddress(It.IsAny<int>()), Times.Never);
            mockEmailService.Verify(m => m.SendMobileChangeEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }


        [Fact]
        public void UpdateMyAccountAsync_Identity_Fails_When_Inputted_With_An_Empty_Or_Invalid_Id()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((ApplicationUser)null));
            mockUserUow.Setup(x => x.UpdateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var service = new UserService(null, mockUserUow.Object, null, null, null, null, null);

            //Act
            ApplicationServiceUser userToUpdate = new ApplicationServiceUser
            {
                Id = 1,
                Username = "testaccount",
                Email = "updatedaccount@synetec.co.uk",
                Title = "Mr",
                Forename = "Test",
                Surname = "Account",
                Password = "password",
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111
            };

            IdentityResult result = service.UpdateUserAsync(userToUpdate).Result;

            //Assert
            Assert.Equal(IdentityResult.Failed().Succeeded, result.Succeeded);
        }

        [Fact]
        public void DeleteUserAsync_Identity_Fails_When_Inputted_With_An_Empty_Or_Invalid_Id()
        {
            //Arrange
            var mockUserUow = new Mock<IUserUow>();

            mockUserUow.Setup(x => x.GetUserByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((ApplicationUser)null));
            mockUserUow.Setup(x => x.DeleteUserAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var emailSenderMock = new Mock<IEmailSender>();

            var service = new UserService(null, mockUserUow.Object, emailSenderMock.Object, null, null, null, null);

            //Act
            IdentityResult result = service.DeleteUserAsync("0").Result;

            //Assert
            Assert.Equal(IdentityResult.Failed().Succeeded, result.Succeeded);
        }

        [Fact]
        public async Task UpdateUser_That_Doesnt_Exist()
        {
            //Arrange
            const int contactId = 5;
            var mockUow = new Mock<IUserUow>(MockBehavior.Strict); //throw errors if the UoW gets used without us setting it up
            mockUow.Setup(u => u.GetClientUserModelByContactId(contactId)).Returns((ClientUserModel)null);
            var service = new UserService(null, mockUow.Object, null, null, null, null,null);
            var updateUser = new ApplicationServiceUser
            {
                ClientCompanyContactId = contactId
            };
            //Act

            var result = await service.UpdateUserContactAsync(updateUser);

            //Assert

            result.Succeeded.Should().BeFalse("Because it shouldn't have found any record to update");
            result.Errors.Should().Contain(e => e.Code == "ContactNotFound", "return code ContactNotFound if the user cannot be found");
        }

        [Fact]
        public async Task UpdateUser_By_User_Who_Doesnt_Exist()
        {
            //arrange
            const int contactId = 5;
            const int updatedBy = 999;
            var mockUow = new Mock<IUserUow>(MockBehavior.Strict); //throw errors if the UoW gets used without us setting it up
            var userModel = new ClientUserModel
            {
                ApplicationId = 2,
                ClientCompanyContactId = contactId,
                ClientCompanyId = 1
            };

            mockUow.Setup(u => u.GetClientUserModelByContactId(contactId)).Returns(userModel);
            mockUow.Setup(u => u.GetAuthUserByAuthUserId(updatedBy)).Returns((AuthUser)null);
            var service = new UserService(null, mockUow.Object, null, null, null, null,null);
            var updateUser = new ApplicationServiceUser
            {
                ClientCompanyContactId = contactId,
                UpdatedByAuthUserId = updatedBy
            };
            //act
            var result = await service.UpdateUserContactAsync(updateUser);

            //assert
            result.Succeeded.Should().BeFalse("Because the specified auth user for the update does not exist");
            result.Errors.Should().Contain(e => e.Code == "InvalidAuthUser", "The update user was invalid.");
        }

        [Fact]
        public async Task UpdateUser_With_No_Email_Or_Phone_Changes()
        {
            //Arrange
            const int contactId = 5;
            const int updatedBy = 999;
            const int dayPeriod = 10;
            var userModel = new ClientUserModel
            {
                ApplicationId = 2,
                ClientCompanyContactId = contactId,
                ClientCompanyId = 1,
                PhoneNumberDirect = "441952271509",
                PhoneNumberMobile = "447718977121",
                Email = "sandwich@cafe.com",
                LastEmailChangeDate = new DateTime(2019, 3, 3),
                LastPhoneNumberMobileChangeDate = new DateTime(2019, 3, 3)
            };
            var authUser = new AuthUser
            {
                Id = updatedBy
            };

            var mockUow = new Mock<IUserUow>(MockBehavior.Strict); //throw errors if the UoW gets used without us setting it up
            mockUow.Setup(u => u.GetClientUserModelByContactId(contactId)).Returns(userModel);
            mockUow.Setup(u => u.GetAuthUserByAuthUserId(updatedBy)).Returns(authUser);
            mockUow.Setup(u => u.ValidateUserMobileChangeRequest(It.IsAny<ClientUserModel>(),
                It.IsAny<ClientUserModel>(), dayPeriod)).Returns(new UserChangeRequestResponse { InsertOrUpdateUserChangeRequest = false });
            mockUow.Setup(u => u.ValidateUserEmailChangeRequest(It.IsAny<ClientUserModel>(),
                It.IsAny<ClientUserModel>(), dayPeriod)).Returns(new UserChangeRequestResponse { InsertOrUpdateUserChangeRequest = false });
            mockUow.Setup(u => u.UpdateUserAsync(It.IsAny<ClientUserModel>())).ReturnsAsync(IdentityResult.Success);
            mockUow.Setup(u => u.ValidateUserDetails(It.IsAny<UserValidationModel>())).Returns(IdentityResult.Success);

            var mockSettingService = new Mock<IAppSettingService>(MockBehavior.Strict);
            mockSettingService.Setup(s => s.GetUserChangeDaysRequiredForApproval()).Returns(dayPeriod);

            var service = new UserService(null, mockUow.Object, null, null, null, mockSettingService.Object,null);
            var updateUser = new ApplicationServiceUser
            {
                ClientCompanyContactId = contactId,
                UpdatedByAuthUserId = updatedBy,
                PhoneNumberDirect = "441952271509",
                PhoneNumberMobile = "447718977121",
                Email = "sandwich@cafe.com"
            };

            //Act
            var result = await service.UpdateUserContactAsync(updateUser);

            //Assert
            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateUser_With_Email_Change()
        {
            //Arrange
            const int contactId = 5;
            const int updatedBy = 999;
            const int dayPeriod = 10;
            const int contactAuthUserId = 12;
            const string originalEmail = "sandwich@cafe.com";
            const string newEmail = "jacketpotato@cafe.com";
            var updatedTime = DateTime.Now;
            var userModel = new ClientUserModel
            {
                ApplicationId = 2,
                ClientCompanyContactId = contactId,
                ClientCompanyId = 1,
                PhoneNumberDirect = "441952271509",
                PhoneNumberMobile = "447718977121",
                Email = originalEmail,
                LastEmailChangeDate = new DateTime(2019, 3, 3),
                LastPhoneNumberMobileChangeDate = new DateTime(2019, 3, 3),
                UpdatedDateTime = new DateTime(2019, 10, 15),
                AuthUserId = contactAuthUserId
            };
            var authUser = new AuthUser
            {
                Id = updatedBy
            };

            var mockUow = new Mock<IUserUow>(MockBehavior.Strict); //throw errors if the UoW gets used without us setting it up
            mockUow.Setup(u => u.GetClientUserModelByContactId(contactId)).Returns(userModel);
            mockUow.Setup(u => u.GetAuthUserByAuthUserId(updatedBy)).Returns(authUser);
            mockUow.Setup(u => u.ValidateUserMobileChangeRequest(It.IsAny<ClientUserModel>(),
                It.IsAny<ClientUserModel>(), dayPeriod)).Returns(new UserChangeRequestResponse { InsertOrUpdateUserChangeRequest = false });
            mockUow.Setup(u => u.ValidateUserEmailChangeRequest(It.IsAny<ClientUserModel>(),
                It.IsAny<ClientUserModel>(), dayPeriod)).Returns(new UserChangeRequestResponse { InsertOrUpdateUserChangeRequest = true });
            mockUow.Setup(u => u.UpdateUserAsync(It.IsAny<ClientUserModel>())).ReturnsAsync(IdentityResult.Success);
            UserChangeRequest request = null;
            mockUow.Setup(u => u.ProcessUserChangeRequest(It.IsAny<UserChangeRequest>())).ReturnsAsync(IdentityResult.Success)
                .Callback((UserChangeRequest r) => request = r);
            mockUow.Setup(u => u.ValidateUserDetails(It.IsAny<UserValidationModel>())).Returns(IdentityResult.Success);

            var mockSettingService = new Mock<IAppSettingService>(MockBehavior.Strict);
            mockSettingService.Setup(s => s.GetUserChangeDaysRequiredForApproval()).Returns(dayPeriod);

            var service = new UserService(null, mockUow.Object, null, null, null, mockSettingService.Object,null);
            var updateUser = new ApplicationServiceUser
            {
                ClientCompanyContactId = contactId,
                UpdatedByAuthUserId = updatedBy,
                PhoneNumberDirect = "441952271509",
                PhoneNumberMobile = "447718977121",
                Email = newEmail,
                AuthUserId = contactAuthUserId,
                UpdatedDateTime = updatedTime
            };

            //Act
            var result = await service.UpdateUserContactAsync(updateUser);

            //Assert
            request.Should().NotBeNull();
            request.AuthUserId.Should().Be(contactAuthUserId);
            request.CurrentValue.Should().Be(originalEmail);
            request.ProposedValue.Should().Be(newEmail);
            request.ChangeValueType.Should().Be("Email");
            request.ChangeStatus.Should().Be("Pending");
            request.ChangedByAuthUserId.Should().Be(updatedBy);
            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateUser_With_Phone_Change()
        {
            //Arrange
            const int contactId = 5;
            const int updatedBy = 999;
            const int dayPeriod = 10;
            const int contactAuthUserId = 12;
            const string originalEmail = "sandwich@cafe.com";
            const string originalPhone = "447718977121";
            const string newPhone = "4477189771111";
            var updatedTime = DateTime.Now;
            var userModel = new ClientUserModel
            {
                ApplicationId = 2,
                ClientCompanyContactId = contactId,
                ClientCompanyId = 1,
                PhoneNumberDirect = "441952271509",
                PhoneNumberMobile = originalPhone,
                Email = originalEmail,
                LastEmailChangeDate = new DateTime(2019, 3, 3),
                LastPhoneNumberMobileChangeDate = new DateTime(2019, 3, 3),
                UpdatedDateTime = new DateTime(2019, 10, 15),
                AuthUserId = contactAuthUserId
            };
            var authUser = new AuthUser
            {
                Id = updatedBy
            };

            var mockUow = new Mock<IUserUow>(MockBehavior.Strict); //throw errors if the UoW gets used without us setting it up
            mockUow.Setup(u => u.GetClientUserModelByContactId(contactId)).Returns(userModel);
            mockUow.Setup(u => u.GetAuthUserByAuthUserId(updatedBy)).Returns(authUser);
            mockUow.Setup(u => u.ValidateUserMobileChangeRequest(It.IsAny<ClientUserModel>(),
                It.IsAny<ClientUserModel>(), dayPeriod)).Returns(new UserChangeRequestResponse { InsertOrUpdateUserChangeRequest = true });
            mockUow.Setup(u => u.ValidateUserEmailChangeRequest(It.IsAny<ClientUserModel>(),
                It.IsAny<ClientUserModel>(), dayPeriod)).Returns(new UserChangeRequestResponse { InsertOrUpdateUserChangeRequest = false });
            mockUow.Setup(u => u.UpdateUserAsync(It.IsAny<ClientUserModel>())).ReturnsAsync(IdentityResult.Success);
            UserChangeRequest request = null;
            mockUow.Setup(u => u.ProcessUserChangeRequest(It.IsAny<UserChangeRequest>())).ReturnsAsync(IdentityResult.Success)
                .Callback((UserChangeRequest r) => request = r);
            mockUow.Setup(u => u.ValidateUserDetails(It.IsAny<UserValidationModel>())).Returns(IdentityResult.Success);

            var mockSettingService = new Mock<IAppSettingService>(MockBehavior.Strict);
            mockSettingService.Setup(s => s.GetUserChangeDaysRequiredForApproval()).Returns(dayPeriod);

            var service = new UserService(null, mockUow.Object, null, null, null, mockSettingService.Object,null);
            var updateUser = new ApplicationServiceUser
            {
                ClientCompanyContactId = contactId,
                UpdatedByAuthUserId = updatedBy,
                PhoneNumberDirect = "441952271509",
                PhoneNumberMobile = newPhone,
                Email = originalEmail,
                AuthUserId = contactAuthUserId,
                UpdatedDateTime = updatedTime
            };

            //Act
            var result = await service.UpdateUserContactAsync(updateUser);

            //Assert
            request.Should().NotBeNull();
            request.AuthUserId.Should().Be(contactAuthUserId);
            request.CurrentValue.Should().Be(originalPhone);
            request.ProposedValue.Should().Be(newPhone);
            request.ChangeValueType.Should().Be("Telephone");
            request.ChangeStatus.Should().Be("Pending");
            request.ChangedByAuthUserId.Should().Be(updatedBy);
            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task Delete_Contact_Who_Doesnt_Exist()
        {
            //Arrange
            const int contactId = 20;
            var mockUow = new Mock<IUserUow>(MockBehavior.Strict); //throw errors if the UoW gets used without us setting it up
            var service = new UserService(null, mockUow.Object, null, null, null, null,null);

            mockUow.Setup(m => m.GetUserByClientCompanyContactId(contactId)).Returns((ApplicationUser)null);
            //Act
            var result = await service.DeleteUserContactAsync(contactId);
            //Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse("No user could be found, so it should fail");
            result.Errors.Should().Contain(e => e.Code == "ContactNotFound");
            mockUow.Verify(u => u.GetUserByClientCompanyContactId(contactId), Times.Once);
        }

        [Fact]
        public async Task DeleteContact()
        {
            //Arrange
            const int contactId = 20;
            var mockUow = new Mock<IUserUow>(MockBehavior.Strict); //throw errors if the UoW gets used without us setting it up
            var service = new UserService(null, mockUow.Object, null, null, null, null,null);
            var applicationUser = new ApplicationUser { ClientCompanyContactId = contactId };
            mockUow.Setup(m => m.GetUserByClientCompanyContactId(contactId)).Returns(applicationUser);
            mockUow.Setup(m => m.DeleteUserAsync(applicationUser)).ReturnsAsync(IdentityResult.Success);
            //Act
            var result = await service.DeleteUserContactAsync(contactId);
            //Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue("The user should be able to be deleted");
            mockUow.Verify(u => u.GetUserByClientCompanyContactId(contactId), Times.Once);
            mockUow.Verify(u => u.DeleteUserAsync(applicationUser), Times.Once);
        }

        private static Mock<IUserUow> mockUserFactory()
        {
            var mockUserUow = new Mock<IUserUow>();

            mockUserUow.Setup(uow => uow.AuthUserRepository.GetByPrimaryKey(It.IsAny<int>()))
                .Returns(new AuthUser
                {
                    Id = 300,
                    UserName = "testaccount",
                    Email = "testaccount@synetec.co.uk",
                    Password = "AQAAAAEAACcQAAAAEOWPaTOV6BsIYqsXovwkD8z9mxu51bAUQXkDgdM/8yUwGm4KQtU21diwdb9UWm5HlA==" //password hash for "Abcd1234"
                });

            return mockUserUow;
        }
    }
}