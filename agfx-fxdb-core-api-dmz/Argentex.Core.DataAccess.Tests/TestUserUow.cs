using System;
using System.Linq;
using System.Threading.Tasks;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using Argentex.Core.UnitsOfWork.Users;
using Argentex.Core.UnitsOfWork.Users.Model;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SynetecLogger;
using Xunit;

namespace Argentex.Core.DataAccess.Tests
{
    public class TestUserUow : IDisposable
    {
        private readonly UserUow _userUow;
        private readonly InMemoryDbContext<FXDB1Context> _fxdbContext = new InMemoryDbContext<FXDB1Context>("FXDB1_InMemory");
        private readonly IServiceProvider _serviceProvider;
        private readonly Mock<ILogWrapper> _mockedLogger = new Mock<ILogWrapper>();

        public TestUserUow()
        {
            var testContext = new UserIdentityTestContext();
            _serviceProvider = testContext.ServiceProvider;
            _userUow = new UserUow(_fxdbContext.GetDbContext(),null,
                testContext.ServiceProvider.GetService<UserManager<ApplicationUser>>(),
                testContext.ServiceProvider.GetService<SignInManager<ApplicationUser>>(),
                testContext.ServiceProvider.GetService<SecurityDbContext>(), 
                _mockedLogger.Object);
        }

        [Fact]
        public async Task Verify_User_Gets_Added_To_DbTables()
        {
            //Arrange
            const string userName = "TestUser";
            var clientModel = new ClientUserModel
            {
                ApplicationId = 2,
                Username = userName,
                Email = $"{userName}@synetec.co.uk",
                Forename = userName,
                Surname = "Last Name",
                Title = "Mr",
                ClientCompanyId = 5,
                UpdatedByAuthUserId = 2,
                UpdatedDateTime = DateTime.Now,
                Position = "Boss",
                PhoneNumberDirect = "441111111111",
                PhoneNumberMobile = "441111111111",
                PhoneNumberOther = string.Empty,
                PrimaryContact = false,
                Notes = string.Empty,
                Authorized = true,
                RecNotification = false,
                RecAmReport = true,
                RecActivityReport = false,
                NiNumber = string.Empty,
                BloombergGpi = string.Empty,
                AssignedCategoryIds = new int[0],
                IsLockedOut = false,
                IsAdmin = false,
                IsSignatory = false,
                IsAuthorisedSignatory = false,
                IsApproved = true,
                ApprovedByAuthUserId = 2

            };
            var password = "Test_Password1";

            //Act
            var result = await _userUow.AddUserAsync(clientModel, password);

            //Assert
            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue("We want the user to be successfully added");
            result.Errors.Should().BeNullOrEmpty("We should have no errors");
            using (var assertContext = _fxdbContext.GetDbContext()) 
            using (var securityAssertContext = _serviceProvider.GetService<SecurityDbContext>())
            {
                var authUser = assertContext.AuthUser.Single(u => u.UserName == userName);
                authUser.Should().BeEquivalentTo(clientModel, GetEquivalencyOptionsForInsertion, "An Authuser should be created as part of creating a Client User");

                var authUserCreateLog = assertContext.LogAuthUser.SingleOrDefault(x => x.Id == authUser.Id && x.LogAction == "CREATE");
                authUserCreateLog.Should().NotBeNull("A Create log should be created for the AuthUser");
                authUserCreateLog.Should().BeEquivalentTo(clientModel, GetEquivalencyOptionsForInsertion, "AuthUserLog should match the details provided");

                var authUserUpdateLog = assertContext.LogAuthUser.SingleOrDefault(x => x.Id == authUser.Id && x.LogAction == "UPDATE");
                authUserUpdateLog.Should().NotBeNull("An Update log should be created for the AuthUser after the password gets updated");
                authUserUpdateLog.Should().BeEquivalentTo(clientModel, GetEquivalencyOptionsForInsertion, "AuthUserLog should match the details provided");

                var contactUser = assertContext.ClientCompanyContact.Single(c => c.AuthUserId == authUser.Id);
                contactUser.Should().BeEquivalentTo(clientModel, GetEquivalencyOptionsForInsertion, "An ClientCompanyContact should be created as part of creating a Client User");

                var contactUserLog = assertContext.LogClientCompanyContact.SingleOrDefault(x => x.Id == contactUser.Id);
                contactUserLog.Should().NotBeNull("A create log should be created for the Client Company Contact");
                contactUserLog.Should().BeEquivalentTo(clientModel, GetEquivalencyOptionsForInsertion, "Client Company contact log should match the details provided");

                var appUser = securityAssertContext.Users.Single(u => u.UserName == userName);
                appUser.Should().BeEquivalentTo(clientModel, GetEquivalencyOptionsForInsertion, "An IdentityDB User should be created as part of creating a Client User");
            }
        }

        [Fact]
        public async Task Verify_User_Gets_Deleted_From_DbTables()
        {
            //Arrange
            const string userName = "TestUser";
            var clientModel = new ClientUserModel
            {
                ID = 1,
                Username = userName,
                Email = $"{userName}@synetec.co.uk",
                Forename = userName,
                Surname = "Last Name",
                Title = "Mr",
                ClientCompanyId = 5,
                ClientCompanyContactId = 1,
                AuthUserId = 1,
                UpdatedByAuthUserId = 1,
                UpdatedDateTime = DateTime.Now,
                Position = "Boss",
                PhoneNumberDirect = "441111111111",
                PhoneNumberMobile = "441111111111",
                PhoneNumberOther = string.Empty,
                PrimaryContact = false,
                Notes = string.Empty,
                Authorized = true,
                RecNotification = false,
                RecAmReport = true,
                RecActivityReport = false,
                NiNumber = string.Empty,
                BloombergGpi = string.Empty,
                AssignedCategoryIds = new int[0],
                IsAdmin = false,
                IsSignatory = false,
                IsAuthorisedSignatory = false,
                IsApproved = true,
                ApprovedByAuthUserId = 2,
                ApplicationId = 2,
                IsLockedOut = false,
                IsDeleted = false
            };
            var appUserModel = GetAppUserFromClientUserModel(clientModel);
            var authUserModel = GetAuthUserFromClientUserModel(clientModel);
            var clientCompanyContactModel = GetContactFromClientUserModel(clientModel);

            var mockContext = _fxdbContext.GetDbContext();
            var mockSecurityContext = _serviceProvider.GetService<SecurityDbContext>();
            _fxdbContext.AddEntities(authUserModel);
            _fxdbContext.AddEntities(clientCompanyContactModel);
            mockSecurityContext.Users.Add(appUserModel);
            mockSecurityContext.SaveChanges();

            //Act
            var userResult = await _userUow.DeleteUserAsync(appUserModel);

            //Assert
            using (var assertContext = _fxdbContext.GetDbContext())
            using (var securityAssertContext = _serviceProvider.GetService<SecurityDbContext>())
            {
                var appUser = securityAssertContext.Users.Single(u => u.Id == appUserModel.Id);
                Assert.Equal(($"{userName}-{Guid.NewGuid().ToString()}").Length, appUser.UserName.Length);
                Assert.True(appUser.IsDeleted);
                Assert.True(appUser.LockoutEnabled);

                var authUser = assertContext.AuthUser.Single(u => u.Id == appUser.AuthUserId);
                Assert.Equal(appUser.UserName, authUser.UserName);
                Assert.True(authUser.IsLockedOut);

                var authUserCreateLog = assertContext.LogAuthUser.SingleOrDefault(x => x.Id == authUser.Id && x.LogAction == "DELETE");
                authUserCreateLog.Should().NotBeNull("A delete log should be created for the AuthUser");
                authUserCreateLog.Should().BeEquivalentTo(clientModel, x =>
                {
                    x = GetEquivalencyOptionsForInsertion(x);
                    x.Excluding(f => f.Username);
                    x.Excluding(f => f.IsLockedOut);
                    return x;
                }, "AuthUserLog should match the details provided");

                var contactUser = assertContext.ClientCompanyContact.Single(c => c.Id == appUser.ClientCompanyContactId);
                Assert.True(contactUser.IsDeleted);

                var contactUserLog = assertContext.LogClientCompanyContact.SingleOrDefault(x => x.Id == contactUser.Id);
                contactUserLog.Should().NotBeNull("A delete log should be created for the Client Company Contact");
                contactUserLog.Should().BeEquivalentTo(clientModel, x =>
                {
                    x = GetEquivalencyOptionsForInsertion(x);
                    x.Excluding(f => f.IsDeleted);
                    return x;
                }, "Client Company contact log should match the details provided");
            }
            
        }

        private static EquivalencyAssertionOptions<ClientUserModel> GetEquivalencyOptionsForInsertion(EquivalencyAssertionOptions<ClientUserModel> equivalencyOptions)
        {
            equivalencyOptions.Excluding(x => x.LastPasswordChangeDate); //Autogenerated
            equivalencyOptions.Excluding(x => x.CreateDate); //AutoGenerated
            equivalencyOptions.Excluding(x => x.FailedPasswordAttemptWindowStart); //autogenerated
            equivalencyOptions.Excluding(x => x.AuthUserId); //autogenerated
            equivalencyOptions.Excluding(x => x.Fullname); //auto generated
            equivalencyOptions.Excluding(x => x.PasswordHash); //generated by the application
            equivalencyOptions.ExcludingMissingMembers(); //miss out the fields not being used
            return equivalencyOptions;
        }

        private AuthUser GetAuthUserFromClientUserModel(ClientUserModel clientModel)
        {
            return new AuthUser
            {
                Id = clientModel.AuthUserId,
                UserName = clientModel.Username,
                Password = clientModel.PasswordHash,
                Email = clientModel.Email ?? "NoEmail",
                IsApproved = clientModel.IsApproved,
                IsLockedOut = clientModel.IsLockedOut ?? false,
                Comment = clientModel.Comment,
                ApplicationId = clientModel.ApplicationId
            };
        }

        private ClientCompanyContact GetContactFromClientUserModel(ClientUserModel clientModel)
        {
            return new ClientCompanyContact
            {
                Id = clientModel.ClientCompanyContactId,
                AuthUserId = clientModel.AuthUserId,
                ClientCompanyId = clientModel.ClientCompanyId,
                Title = clientModel.Title,
                Forename = clientModel.Forename,
                Surname = clientModel.Surname,
                Fullname = $"{clientModel.Title} {clientModel.Forename} {clientModel.Surname}",
                Email = clientModel.Email,
                LastEmailChangeDate = clientModel.LastEmailChangeDate,
                Position = clientModel.Position,
                PrimaryContact = clientModel.PrimaryContact,
                TelephoneDirect = clientModel.PhoneNumberDirect,
                TelephoneMobile = clientModel.PhoneNumberMobile,
                TelephoneOther = clientModel.PhoneNumberOther,
                LastTelephoneChangeDate = clientModel.LastPhoneNumberMobileChangeDate,
                Birthday = clientModel.Birthday,
                Aspnumber = clientModel.ASPNumber,
                AspcreationDate = clientModel.ASPCreationDate,
                UpdatedByAuthUserId = clientModel.UpdatedByAuthUserId,
                UpdatedDateTime = clientModel.UpdatedDateTime,
                Notes = clientModel.Notes,
                Authorized = clientModel.Authorized,
                RecNotifications = clientModel.RecNotification,
                RecAmreport = clientModel.RecAmReport,
                RecActivityReport = clientModel.RecActivityReport,
                BloombergGpi = clientModel.BloombergGpi,
                NiNumber = clientModel.NiNumber,
                IsDeleted = clientModel.IsDeleted
            };
        }

        private ApplicationUser GetAppUserFromClientUserModel(ClientUserModel clientModel)
        {
            return new ApplicationUser
            {
                Id = clientModel.ID,
                AuthUserId = clientModel.AuthUserId,
                ClientCompanyContactId = clientModel.ClientCompanyContactId,
                UserName = clientModel.Username,
                Email = clientModel.Email,
                Forename = clientModel.Forename,
                Surname = clientModel.Surname,
                Title = clientModel.Title,
                ClientCompanyId = clientModel.ClientCompanyId,
                UpdatedByAuthUserId = clientModel.UpdatedByAuthUserId,
                LastUpdate = clientModel.UpdatedDateTime,
                Position = clientModel.Position,
                PhoneNumber = clientModel.PhoneNumberDirect,
                PhoneNumberMobile = clientModel.PhoneNumberMobile,
                PhoneNumberOther = clientModel.PhoneNumberOther,
                PrimaryContact = clientModel.PrimaryContact,
                Notes = clientModel.Notes,
                LockoutEnabled = clientModel.IsLockedOut ?? false,
                IsAdmin = clientModel.IsAdmin,
                IsSignatory = clientModel.IsSignatory,
                IsAuthorisedSignatory = clientModel.IsAuthorisedSignatory,
                IsApproved = clientModel.IsApproved,
                ApprovedByAuthUserId = clientModel.ApprovedByAuthUserId,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
        }

        public void Dispose()
        {
            _fxdbContext.GetDbContext().Database.EnsureDeleted();
        }
    }

    public class UserIdentityTestContext
    {
        public IServiceProvider ServiceProvider { get; }
        public UserIdentityTestContext()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<SecurityDbContext>(options =>
            {
                options.UseInMemoryDatabase("IdentityDB_InMemory");
                options.UseOpenIddict();
                options.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            serviceCollection.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<SecurityDbContext>()
                .AddDefaultTokenProviders();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }

}
