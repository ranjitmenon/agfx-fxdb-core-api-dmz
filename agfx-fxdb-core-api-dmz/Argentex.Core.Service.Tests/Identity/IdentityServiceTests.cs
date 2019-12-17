using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using Argentex.Core.Service.Email.EmailSender;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Identity;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.UnitsOfWork.Users;
using Microsoft.AspNetCore.Identity;
using Moq;
using Synetec.Data.UnitOfWork.GenericRepo;
using SynetecLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Argentex.Core.Service.Tests.Identity
{
    public class IdentityServiceTests
    {
        [Fact]
        public async Task ResetPassword_Success()
        {
            var uow = new Mock<IUserUow>();
            uow.Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser {IsDeleted = false, IsApproved = true, LockoutEnabled = false});

            uow.Setup(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendPasswordChangedEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var service = new IdentityService(null, uow.Object, mockEmailService.Object, null);

            var result = await service.ResetPasswordAsync("", "", "");

            result.Succeeded.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task ResetPassword_Should_Throw_An_Exception_When_User_Does_Not_Exist()
        {
            // Given
            var username = "rtest";
            var uow = new Mock<IUserUow>();
            uow.Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser>(null));

            var service = new IdentityService(null, uow.Object, null, null);

            var expectedMessage = $"User with username {username} does not exist";

            // When
            var result = await service.ResetPasswordAsync(username, "", "");

            // Then
            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
            result.Errors.Should().Contain(e => e.Code == IdentityResultCodes.UserNotFound);
        }

        [Fact]
        public void ResetPassword_Fail()
        {
            var uow = new Mock<IUserUow>();
            uow.Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new ApplicationUser()));

            uow.Setup(x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed()));
            
            var service = new IdentityService(null, uow.Object, null, null);

            var result = service.ResetPasswordAsync("", "", "").Result;

            Assert.False(result.Succeeded);
        }

        [Fact(Skip = "Useless test needs to be fixed")]
        public void AuthenticateAsyncTest_NewPassword_Success()
        {
            IdentityService service = SetUpMocks();

            var serviceModel = new LoginServiceModel
            {
                Username = "rado",
                Password = "Radovan1#",
                Grant_Type = "password",
                ClientId = "clientId"

            };

            var result = service.AuthenticateAsync(serviceModel).Result;
        }

        [Fact(Skip = "Needs to be fixed")]
        public void AuthenticateAsyncTest_RefreshToken_Success()
        {         
            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var repo = new Mock<IGenericRepo<Token>>();
            repo.Setup(x => x.Insert(It.IsAny<Token>()));
            repo.Setup(x => x.GetQueryable(It.IsAny<Expression<Func<Token, bool>>>(), It.IsAny<Func<IQueryable<Token>, IOrderedQueryable<Token>>>(), It.IsAny<string>()))
                .Returns((new List<Token>
                {
                    new Token
                    {
                        UserId = 1,
                        ClientId = "clientId",
                        Value = "refreshToken"
                    }
                }).AsQueryable());

            var mockUserUow = new Mock<IUserUow>();
            
            mockUserUow.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
             .Returns(Task.FromResult(SignInResult.Success));

            mockUserUow.Setup(x => x.SaveContextAsync())
                .Returns(Task.FromResult(1));

            var activityRepo = new Mock<IGenericRepo<Activity>>();
            activityRepo.Setup(x => x.GetQueryable(It.IsAny<Expression<Func<Activity, bool>>>(), It.IsAny<Func<IQueryable<Activity>, IOrderedQueryable<Activity>>>(), It.IsAny<string>()))
                .Returns((new List<Activity>
                {
                    new Activity{ActivityId = 1}
                }).AsQueryable());

            mockUserUow.SetupGet(x => x.ActivityRepo).Returns(activityRepo.Object);

            mockUserUow.Setup(x => x.SaveContextAsync())
                .Returns(Task.FromResult(1));

            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(new ApplicationUser
            {
                Id = 1,
                UserName = "rado",
                Email = "rluptak@synetec.co.uk"
            }));
            IList<string> roles = new string[] { "manager" };
            mockUserUow.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult(roles));

            var mockConfig = new Mock<IConfigWrapper>();
            mockConfig.Setup(x => x.Get(It.IsAny<string>())).Returns(It.IsAny<string>());

            var service = new IdentityService(mockConfig.Object, mockUserUow.Object, null, null); //it will fire an exception.

            var result = service.AuthenticateAsync(new LoginServiceModel
            {
                Username = "rado",
                Password = "Radovan1#",
                Grant_Type = "refresh_token",
                ClientId = "clientId",
                RefreshToken = "refreshToken"

            }).Result;

            Assert.Null(result);
        }

        [Fact]
        public void AuthenticateAsyncTest_RefreshToken_Fail_TokenNotFound()
        {
            IdentityService service = SetUpMocks();

            var result = service.AuthenticateAsync(new LoginServiceModel
            {
                Username = "rado",
                Password = "Radovan1#",
                Grant_Type = "refresh_token",
                ClientId = "clientId"

            }).Result;

            Assert.Null(result);
        }

        [Fact]
        public void AuthenticateAsyncTest_RefreshToken_Fail_UserNotFound()
        {
            var logger = new Mock<ILogWrapper>();
            logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var repo = new Mock<IGenericRepo<Token>>();
            repo.Setup(x => x.Insert(It.IsAny<Token>()));
            repo.Setup(x => x.GetQueryable(It.IsAny<Expression<Func<Token, bool>>>(), It.IsAny<Func<IQueryable<Token>, IOrderedQueryable<Token>>>(), It.IsAny<string>()))
                .Returns((new List<Token>
                {
                    new Token
                    {
                        UserId = 1,
                        ClientId = "clientId",
                        Value = "refreshToken"
                    }
                }).AsQueryable());

            var mockUserUow = new Mock<IUserUow>();
           
            mockUserUow.Setup(m => m.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Boolean>(), It.IsAny<Boolean>()))
             .Returns(Task.FromResult(SignInResult.Success));

            mockUserUow.Setup(x => x.SaveContextAsync())
                .Returns(Task.FromResult(1));

            var activityRepo = new Mock<IGenericRepo<Activity>>();
            activityRepo.Setup(x => x.GetQueryable(It.IsAny<Expression<Func<Activity, bool>>>(), It.IsAny<Func<IQueryable<Activity>, IOrderedQueryable<Activity>>>(), It.IsAny<string>()))
                .Returns((new List<Activity>
                {
                    new Activity{ActivityId = 1}
                }).AsQueryable());

            mockUserUow.SetupGet(x => x.ActivityRepo).Returns(activityRepo.Object);

            mockUserUow.Setup(x => x.SaveContextAsync())
                .Returns(Task.FromResult(1));

            mockUserUow.Setup(x => x.IsUserByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockUserUow.Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(new ApplicationUser
            {
                Id = 1,
                UserName = "rado",
                Email = "rluptak@synetec.co.uk",
                AuthUserId = 1000,
                IsDeleted = true
            }));

            var service = new IdentityService(null, mockUserUow.Object, null, null);

            var result = service.AuthenticateAsync(new LoginServiceModel
            {
                Username = "rado",
                Password = "Radovan1#",
                Grant_Type = "password",
                ClientId = "clientId",
                RefreshToken = "refreshToken"
            }).Result;

            Assert.Null(result);
        }

        [Fact(Skip="Useless test needs to be fixed")]
        public void BuildTokenTest_Success()
        {
            var userServiceModel = new UserModel
            {
                Email = "rluptak@synetec.co.uk",
                Name = "rado",
                Roles = new List<string> { "manager", "admin"}
            };

            var mockConfig = new Mock<IConfigWrapper>();
            mockConfig.Setup(x => x.Get(It.IsAny<string>())).Returns(It.IsAny<string>());

            //var service = new IdentityService(mockConfig.Object, null, null);

            //var result = service.BuildToken(userServiceModel);
        } //no assert

        [Fact(Skip = "Useless test needs to be fixed")]
        public void BuildTokenTest_Fail()
        {
            var userServiceModel = new UserModel
            {
                Email = "rluptak@synetec.co.uk",
                Name = "rado",
                Roles = new List<string> { "manager", "admin" }
            };

            var mockConfig = new Mock<IConfigWrapper>();
            mockConfig.Setup(x => x.Get(It.IsAny<string>())).Returns(It.IsAny<string>());

            //var service = new IdentityService(mockConfig.Object, null, null);

            //var result = service.BuildToken(userServiceModel);
        } //no assert

        [Fact]
        public void ChangePasswordAsync_Success_With_Correct_And_Different_Password()
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
                PasswordHash = "AQAAAAEAACcQAAAAEOWPaTOV6BsIYqsXovwkD8z9mxu51bAUQXkDgdM/8yUwGm4KQtU21diwdb9UWm5HlA==", //password hash for "Abcd1234"
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                ClientCompanyContactId = 100
            };

            mockUserUow.Setup(uow => uow.AuthUserRepository.GetByPrimaryKey(It.IsAny<int>()))
                .Returns(new AuthUser
                {
                    Id = 300,
                    UserName = "testaccount",
                    Email = "testaccount@synetec.co.uk",
                    Password = "AQAAAAEAACcQAAAAEOWPaTOV6BsIYqsXovwkD8z9mxu51bAUQXkDgdM/8yUwGm4KQtU21diwdb9UWm5HlA==" //password hash for "Abcd1234"
                });
            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            mockUserUow.Setup(uow => uow.ChangePasswordAsync(user, It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(s => s.SendPasswordChangedEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success));
            
            var service = new IdentityService(null, mockUserUow.Object, mockEmailService.Object, null);

            //Act
            var updateResult = service.ChangePasswordAsync("1", "Abcd1234", "Changed1234", "Changed1234").Result;

            //Assert
            Assert.Equal(IdentityResult.Success, updateResult);
        }

        [Fact]
        public void ChangePasswordAsync_Should_Throw_An_Exception_When_Passwords_Dont_Match()
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
                PasswordHash = "AQAAAAEAACcQAAAAEOWPaTOV6BsIYqsXovwkD8z9mxu51bAUQXkDgdM/8yUwGm4KQtU21diwdb9UWm5HlA==", //password hash for "Abcd1234"
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                ClientCompanyContactId = 100
            };

            var mockUserUow = new Mock<IUserUow>();

            var service = new IdentityService(null, mockUserUow.Object, null, null);

            var expectedMessage = "Passwords do not match";

            //Act
            var exception = Assert.ThrowsAsync<PasswordsDoNotMatchException>(() => service.ChangePasswordAsync("1", "WrongPassword1234", "Changed1234", "Changed4321"));

            //Assert
            Assert.NotNull(exception.Result);
            Assert.Equal(expectedMessage, exception.Result.Message);
        }

        [Fact]
        public void ChangePasswordAsync_Failed_With_Identical_Old_And_New_Password()
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
                PasswordHash = "AQAAAAEAACcQAAAAEOWPaTOV6BsIYqsXovwkD8z9mxu51bAUQXkDgdM/8yUwGm4KQtU21diwdb9UWm5HlA==", //password hash for "Abcd1234"
                ClientCompanyId = 439,
                UpdatedByAuthUserId = 111,
                AuthUserId = 300,
                ClientCompanyContactId = 100
            };

            var mockUserUow = new Mock<IUserUow>();

            mockUserUow.Setup(uow => uow.AuthUserRepository.GetByPrimaryKey(It.IsAny<int>()))
                .Returns(new AuthUser
                {
                    Id = 300,
                    UserName = "testaccount",
                    Email = "testaccount@synetec.co.uk",
                    Password = "AQAAAAEAACcQAAAAEOWPaTOV6BsIYqsXovwkD8z9mxu51bAUQXkDgdM/8yUwGm4KQtU21diwdb9UWm5HlA==" //password hash for "Abcd1234"
                });
            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            var service = new IdentityService(null, mockUserUow.Object, null, null);

            //Act
            var updateResult = service.ChangePasswordAsync("1", "Abcd1234", "Abcd1234", "Abcd1234").Result;

            //Assert
            Assert.Equal(IdentityResult.Failed().Succeeded, updateResult.Succeeded);
        }

        [Fact]
        public void Change_Password_Should_Throw_An_Exception_When_User_Tries_To_Use_Previous_3_Password()
        {
            // Given
            var mockUserUow = new Mock<IUserUow>();

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
                ClientCompanyContactId = 100
            };

            mockUserUow.Setup(uow => uow.AuthUserRepository.GetByPrimaryKey(It.IsAny<int>()))
                .Returns(new AuthUser
                {
                    Id = 300,
                    UserName = "testaccount",
                    Email = "testaccount@synetec.co.uk",
                    Password = "AQAAAAEAACcQAAAAEOWPaTOV6BsIYqsXovwkD8z9mxu51bAUQXkDgdM/8yUwGm4KQtU21diwdb9UWm5HlA==" //password hash for "Abcd1234"
                });
            mockUserUow.Setup(x => x.GetUserByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(user));
            mockUserUow.Setup(x => x.GetLastPasswords(It.IsAny<long>())).Returns(new List<PreviousPassword>
            {
                new PreviousPassword()
                {
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEEf//OG/47x/U3dvlW64+UkPxGL3Lp9DudwKzxONYeEkaPBIxgU9ATNv2qvKzWTIqg==" //Abcd123456
                }
            }.AsQueryable);
            mockUserUow.Setup(uow => uow.ChangePasswordAsync(user, It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var service = new IdentityService(null, mockUserUow.Object, null, null);

            var expectedMessage = "Password already been used within the past 3 passwords";

            // When
            var exception = Assert.ThrowsAsync<PasswordAlreadyUsedException>(() => service.ChangePasswordAsync("1", "Abcd1234", "Abcd123456", "Abcd123456"));

            // Then
            Assert.NotNull(exception.Result);
            Assert.Equal(expectedMessage, exception.Result.Message);
        }
        
        private static IdentityService SetUpMocks()
        {
            var mockTokenRepository = new Mock<IGenericRepo<Token>>();
            var mockUserUow = new Mock<IUserUow>();
            
            mockUserUow.Setup(x => x.SaveContextAsync())
                .Returns(Task.FromResult(1));

            mockUserUow.Setup(x => x.IsUserByNameAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            mockUserUow.Setup(x => x.GetUserByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(new ApplicationUser
            {
                Id = 1,
                UserName = "rado",
                Email = "rluptak@synetec.co.uk",
                AuthUserId = 1000
            }));
            IList<string> roles = new string[] { "manager" };
            mockUserUow.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult(roles));

            var activityRepo = new Mock<IGenericRepo<Activity>>();
            activityRepo.Setup(x => x.GetQueryable(It.IsAny<Expression<Func<Activity, bool>>>(), It.IsAny<Func<IQueryable<Activity>, IOrderedQueryable<Activity>>>(), It.IsAny<string>()))
                .Returns((new List<Activity>
                {
                    new Activity{ActivityId = 1}
                }).AsQueryable());

            mockUserUow.SetupGet(x => x.ActivityRepo).Returns(activityRepo.Object);

            mockUserUow.Setup(x => x.SaveContextAsync())
                .Returns(Task.FromResult(1));

            mockUserUow.Setup(x => x.LogActivity(It.IsAny<ActivityLog>())).Returns(Task.FromResult(1));
            var service = new IdentityService(null, mockUserUow.Object, null, null);

            return service;
        }
    }
}
