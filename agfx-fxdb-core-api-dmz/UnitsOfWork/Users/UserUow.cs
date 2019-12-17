using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using Microsoft.AspNetCore.Identity;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using SynetecLogger;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Argentex.Core.UnitsOfWork.Users.Model;
using Argentex.Core.UnitsOfWork.AppSettings;
using Argentex.Core.UnitsOfWork.Extensions;
using Microsoft.EntityFrameworkCore.Internal;

namespace Argentex.Core.UnitsOfWork.Users
{
    public class UserUow : BaseUow, IUserUow
    {
        private IAppSettingUow _appSettingUow;
        private IGenericRepo<AppSetting> _appSettingRepository;
        private IGenericRepo<AuthUser> _authUserRepository;
        private IGenericRepo<LogAuthUser> _logAuthUserRepository;
        private IGenericRepo<AppUser> _appUserRepository;
        private IGenericRepo<ClientCompanyContact> _clientCompanyContactRepository;
        private IGenericRepo<LogClientCompanyContact> _logClientCompanyContactRepository;
        private IGenericRepo<ClientCompanyContactCategory> _clientCompanyContactCategoryRepository;
        private IGenericRepo<LogClientCompanyContactCategory> _logClientCompanyContactCategoryRepository;
        private IGenericRepo<ActivityLog> _activityLogRepository;
        private IGenericRepo<Activity> _activityRepository;
        private IGenericRepo<ApplicationUser> _applicationUserRepository;
        private IGenericRepo<Token> _tokenRepository;
        private readonly ILogWrapper _logger; // REVISIT, do we need a wrapper?
        private IGenericRepo<PreviousPassword> _previousPasswordsRepository;
        private IGenericRepo<ApplicationUserRole> _applicationUserRoleRepository;
        private IGenericRepo<ApplicationRole> _applicationRoleRepository;
        private IGenericRepo<UserChangeRequest> _userChangeRequestRepository;
        private IGenericRepo<UserChangeRequestApproval> _userChangeRequestApprovalRepository;
        private IGenericRepo<ClientCompany> _clientCompanyRepository;
        private IGenericRepo<AuthApplication> _authApplicationRepository;
        private IGenericRepo<ClientCompanyStatus> _clientCompanyStatusRepository;

        private readonly SecurityDbContext _securityContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserUow(FXDB1Context context,
            IAppSettingUow settingUow,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SecurityDbContext securityContext,
            ILogWrapper logger)
            : base(context)
        {
            _appSettingUow = settingUow;
            _userManager = userManager;
            _signInManager = signInManager;
            _securityContext = securityContext;
            _logger = logger;
        }

        #region Properties
        public IGenericRepo<AppSetting> AppSettingRepository =>
            _appSettingRepository = _appSettingRepository ?? new GenericRepo<AppSetting>(Context);

        public IGenericRepo<AuthUser> AuthUserRepository =>
            _authUserRepository = _authUserRepository ?? new GenericRepo<AuthUser>(Context);

        public IGenericRepo<LogAuthUser> LogAuthUserRepository =>
            _logAuthUserRepository = _logAuthUserRepository ?? new GenericRepo<LogAuthUser>(Context);

        public IGenericRepo<AppUser> AppUserRepository =>
            _appUserRepository = _appUserRepository ?? new GenericRepo<AppUser>(Context);

        public IGenericRepo<ClientCompanyContact> ClientCompanyContactRepository =>
            _clientCompanyContactRepository = _clientCompanyContactRepository ?? new GenericRepo<ClientCompanyContact>(Context);

        public IGenericRepo<ClientCompany> ClientCompanyRepository =>
          _clientCompanyRepository = _clientCompanyRepository ?? new GenericRepo<ClientCompany>(Context);

        public IGenericRepo<LogClientCompanyContact> LogClientCompanyContactRepository =>
            _logClientCompanyContactRepository = _logClientCompanyContactRepository ?? new GenericRepo<LogClientCompanyContact>(Context);

        public IGenericRepo<ClientCompanyContactCategory> ClientCompanyContactCategoryRepository =>
            _clientCompanyContactCategoryRepository = _clientCompanyContactCategoryRepository ?? new GenericRepo<ClientCompanyContactCategory>(Context);

        public IGenericRepo<LogClientCompanyContactCategory> LogClientCompanyContactCategoryRepository =>
            _logClientCompanyContactCategoryRepository = _logClientCompanyContactCategoryRepository ?? new GenericRepo<LogClientCompanyContactCategory>(Context);

        public IGenericRepo<UserChangeRequest> UserChangeRequestRepository =>
            _userChangeRequestRepository = _userChangeRequestRepository ?? new GenericRepo<UserChangeRequest>(Context);

        public IGenericRepo<UserChangeRequestApproval> UserChangeRequestApprovalRepository =>
            _userChangeRequestApprovalRepository = _userChangeRequestApprovalRepository ?? new GenericRepo<UserChangeRequestApproval>(Context);

        public IGenericRepo<ActivityLog> ActivityLogRepo =>
            _activityLogRepository = _activityLogRepository ?? new GenericRepo<ActivityLog>(_securityContext);

        public IGenericRepo<Activity> ActivityRepo =>
            _activityRepository = _activityRepository ?? new GenericRepo<Activity>(_securityContext);

        public IGenericRepo<ApplicationUser> ApplicationUserRepo =>
            _applicationUserRepository = _applicationUserRepository ?? new GenericRepo<ApplicationUser>(_securityContext);

        private IGenericRepo<Token> TokenRepo =>
            _tokenRepository = _tokenRepository ?? new GenericRepo<Token>(_securityContext);

        public IGenericRepo<PreviousPassword> PreviousPasswordsRepository => _previousPasswordsRepository =
            _previousPasswordsRepository ?? new GenericRepo<PreviousPassword>(_securityContext);

        public IGenericRepo<ApplicationUserRole> ApplicationUserRoleRepository =>
            _applicationUserRoleRepository = _applicationUserRoleRepository ?? new GenericRepo<ApplicationUserRole>(_securityContext);

        public IGenericRepo<ApplicationRole> ApplicationRoleRepository =>
            _applicationRoleRepository = _applicationRoleRepository ?? new GenericRepo<ApplicationRole>(_securityContext);

        public IGenericRepo<AuthApplication> AuthApplicationRepository =>
         _authApplicationRepository = _authApplicationRepository ?? new GenericRepo<AuthApplication>(Context);

        public IGenericRepo<ClientCompanyStatus> ClientCompanyStatusRepository =>
       _clientCompanyStatusRepository = _clientCompanyStatusRepository ?? new GenericRepo<ClientCompanyStatus>(Context);

        #endregion

        public ClientUserModel GetClientUserModelByContactId(int clientCompanyContactId)
        {
            ClientCompanyContact clientCompanyContact = ClientCompanyContactRepository.GetByPrimaryKey(clientCompanyContactId);
            if (clientCompanyContact == null) return null;

            ApplicationUser appUser = GetUserByClientCompanyContactId(clientCompanyContactId);
            if (appUser == null) throw new NullReferenceException($"User is not tied to a valid ClientCompanyContactId: {clientCompanyContactId}. Database out of sync.");

            AuthUser authUser = GetAuthUserByAuthUserId(appUser.AuthUserId);
            if (authUser == null) throw new NullReferenceException($"User is not tied to a valid ClientCompanyContactId: {clientCompanyContactId}. Database out of sync.");

            ClientUserModel clientUser = MapClientUser(clientCompanyContact, authUser, appUser);

            return clientUser;
        }

        public ClientUserModel MapClientUser(ClientCompanyContact clientCompanyContact, AuthUser authUser, ApplicationUser appUser)
        {
            return new ClientUserModel()
            {
                Title = clientCompanyContact.Title,
                Forename = clientCompanyContact.Forename,
                Surname = clientCompanyContact.Surname,
                Username = appUser.UserName,
                PasswordHash = appUser.PasswordHash,
                Email = clientCompanyContact.Email,
                ClientCompanyId = clientCompanyContact.ClientCompanyId,
                ClientCompanyContactId = clientCompanyContact.Id,
                AuthUserId = authUser.Id,
                UpdatedByAuthUserId = clientCompanyContact.UpdatedByAuthUserId,
                Position = clientCompanyContact.Position,
                PhoneNumberDirect = clientCompanyContact.TelephoneDirect,
                PhoneNumberMobile = clientCompanyContact.TelephoneMobile,
                PhoneNumberOther = clientCompanyContact.TelephoneOther,
                Birthday = clientCompanyContact.Birthday ?? DateTime.MinValue,
                IsApproved = appUser.IsApproved,
                PrimaryContact = clientCompanyContact.PrimaryContact ?? false,
                Notes = clientCompanyContact.Notes,
                LastPasswordChangeDate = appUser.LastPasswordChange,
                LastPhoneNumberMobileChangeDate = clientCompanyContact.LastTelephoneChangeDate,
                LastEmailChangeDate = clientCompanyContact.LastEmailChangeDate,
                ASPNumber = clientCompanyContact.Aspnumber,
                ASPCreationDate = clientCompanyContact.AspcreationDate,
                Fullname = clientCompanyContact.Fullname,
                Authorized = clientCompanyContact.Authorized,
                RecNotification = clientCompanyContact.RecNotifications,
                RecAmReport = clientCompanyContact.RecAmreport,
                RecActivityReport = clientCompanyContact.RecActivityReport,
                IsDeleted = clientCompanyContact.IsDeleted,
                BloombergGpi = clientCompanyContact.BloombergGpi,
                NiNumber = clientCompanyContact.NiNumber,
                //AssignedCategoryIds = clientCompanyContact,
                IsLockedOut = authUser.IsLockedOut,
                Comment = authUser.Comment,
                CreateDate = authUser.CreateDate,
                LastLoginDate = authUser.LastLoginDate,
                LastActivityDate = authUser.LastActivityDate,
                LastLockOutDate = authUser.LastLockOutDate,
                FailedPasswordAttemptCount = authUser.FailedPasswordAttemptCount,
                FailedPasswordAttemptWindowStart = authUser.FailedPasswordAttemptWindowStart,
                ApplicationId = authUser.ApplicationId,
                EmailConfirmed = appUser.EmailConfirmed,
                IsAdmin = appUser.IsAdmin,
                IsSignatory = appUser.IsSignatory,
                IsAuthorisedSignatory = appUser.IsAuthorisedSignatory,
                ApprovedByAuthUserId = appUser.ApprovedByAuthUserId
            };
        }

        public async Task<IdentityResult> AddUserAsync(ClientUserModel newClientUser, string password)
        {
            using (var userTransaction = Context.Database.BeginTransaction())
            {
                using (var securityTransaction = _securityContext.Database.BeginTransaction())
                {
                    try
                    {
                        AuthUser authUser = MapAuthUser(new AuthUser(), newClientUser);
                        authUser.Password = password; //set temporary password
                        authUser.CreateDate = newClientUser.UpdatedDateTime;
                        authUser.FailedPasswordAttemptCount = 0;
                        authUser.ApplicationId = 2;
                        authUser.LastPasswordChangeDate = newClientUser.UpdatedDateTime;
                        authUser.FailedPasswordAttemptWindowStart = newClientUser.UpdatedDateTime;
                        AuthUserRepository.Insert(authUser);
                        InsertAuthUserLog(authUser, "CREATE");


                        newClientUser.LastEmailChangeDate = newClientUser.UpdatedDateTime;
                        newClientUser.LastPhoneNumberMobileChangeDate = newClientUser.UpdatedDateTime;

                        //create new Client Company Contact
                        ClientCompanyContact clientCompanyContact = MapClientCompanyContact(new ClientCompanyContact(), newClientUser);
                        clientCompanyContact.AuthUser = authUser;
                        ClientCompanyContactRepository.Insert(clientCompanyContact);
                        InsertClientCompanyContactLog(clientCompanyContact, "CREATE");
                        newClientUser.ClientCompanyContactId = clientCompanyContact.Id;

                        await SaveContextAsync();
                        ApplicationUser user = MapApplicationUser(new ApplicationUser(), newClientUser);
                        user.AuthUserId = authUser.Id;
                        user.ClientCompanyContactId = clientCompanyContact.Id;

                        IdentityResult result = await _userManager.CreateAsync(user, password);
                        if (!result.Succeeded) throw new Exception($"Unable to create an Application User for {newClientUser.Forename} {newClientUser.Surname}.");

                        authUser.Password = user.PasswordHash; // when the password is hashed, store hashed password instead
                        AuthUserRepository.Update(authUser);
                        InsertAuthUserLog(authUser, "UPDATE");
                        await SaveContextAsync();

                        await InsertInPreviousPasswords(user.PasswordHash, user);

                        userTransaction.Commit();
                        securityTransaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        //TODO remove previous password for the user to be deleted
                        userTransaction.Rollback();
                        securityTransaction.Rollback();
                        _logger.Error(ex);

                        return IdentityResult.Failed(new IdentityError { Description = ex.Message });
                    }
                }
            }
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser userToUpdate, ApplicationUser originalUser)
        {
            using (var userTransaction = Context.Database.BeginTransaction())
            {
                using (var securityTransaction = _securityContext.Database.BeginTransaction())
                {
                    try
                    {
                        IdentityResult result = await _userManager.UpdateAsync(userToUpdate);
                        if (!result.Succeeded) return result;

                        AuthUser authUser = AuthUserRepository.GetByPrimaryKey(userToUpdate.AuthUserId);
                        if (authUser == null) throw new NullReferenceException($"User is not tied to a valid AuthUserId: {userToUpdate.AuthUserId}. Database out of sync.");
                        authUser = MapAuthUser(authUser, userToUpdate);
                        AuthUserRepository.Update(authUser);
                        InsertAuthUserLog(authUser, "UPDATE");
                        await SaveContextAsync();

                        ClientCompanyContact clientCompanyContact = ClientCompanyContactRepository.GetByPrimaryKey(userToUpdate.ClientCompanyContactId);
                        if (clientCompanyContact == null) throw new NullReferenceException($"User is not tied to a valid ClientCompanyContactId: {userToUpdate.ClientCompanyContactId}. Database out of sync.");
                        clientCompanyContact = MapClientCompanyContact(clientCompanyContact, userToUpdate);
                        ClientCompanyContactRepository.Update(clientCompanyContact);
                        InsertClientCompanyContactLog(clientCompanyContact, "UPDATE");
                        await SaveContextAsync();

                        userTransaction.Commit();
                        securityTransaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        userTransaction.Rollback();
                        securityTransaction.Rollback();
                        _logger.Error(ex);

                        return IdentityResult.Failed(new IdentityError { Description = ex.Message });
                    }
                }
            }
        }

        public async Task<IdentityResult> UpdateUserAsync(ClientUserModel userToUpdate)
        {
            bool existingSecurityContext = true;
            bool existingFXDBContext = true;

            if (Context.Database.CurrentTransaction == null)
            {
                existingFXDBContext = false;
                Context.Database.BeginTransaction();
            }


            if (_securityContext.Database.CurrentTransaction == null)
            {
                existingSecurityContext = false;
                _securityContext.Database.BeginTransaction();
            }


            try
            {
                //Get Entities
                ApplicationUser originalUser = GetUserByClientCompanyContactId(userToUpdate.ClientCompanyContactId);

                ClientCompanyContact clientCompanyContact = ClientCompanyContactRepository.GetByPrimaryKey(userToUpdate.ClientCompanyContactId);
                if (clientCompanyContact == null) throw new NullReferenceException($"User is not tied to a valid ClientCompanyContactId: {userToUpdate.ClientCompanyContactId}. Database out of sync.");

                AuthUser authUser = AuthUserRepository.GetByPrimaryKey(userToUpdate.AuthUserId);
                if (authUser == null) throw new NullReferenceException($"User is not tied to a valid AuthUserId: {userToUpdate.AuthUserId}. Database out of sync.");

                //prevent certain fields from being modified from update call
                userToUpdate.PasswordHash = authUser.Password;

                //prevent modification to certain fields until functionality is added on trader
                userToUpdate.IsSignatory = originalUser.IsSignatory;
                userToUpdate.IsAuthorisedSignatory = originalUser.IsAuthorisedSignatory;
                userToUpdate.IsAdmin = originalUser.IsAdmin;

                ApplicationUser updatedUser = MapApplicationUser(originalUser, userToUpdate);
                IdentityResult result = await _userManager.UpdateAsync(updatedUser);
                if (!result.Succeeded) return result;

                authUser = MapAuthUser(authUser, userToUpdate);
                AuthUserRepository.Update(authUser);
                InsertAuthUserLog(authUser, "UPDATE");
                await SaveContextAsync();

                clientCompanyContact = MapClientCompanyContact(clientCompanyContact, userToUpdate);
                ClientCompanyContactRepository.Update(clientCompanyContact);
                InsertClientCompanyContactLog(clientCompanyContact, "UPDATE");
                await SaveContextAsync();

                if (!existingFXDBContext)
                    Context.Database.CurrentTransaction.Commit();

                if (!existingSecurityContext)
                    _securityContext.Database.CurrentTransaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                if (!existingFXDBContext)
                    Context.Database.CurrentTransaction.Rollback();

                if (!existingSecurityContext)
                    _securityContext.Database.CurrentTransaction.Rollback();
                _logger.Error(ex);

                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<IdentityResult> ApproveUserAsync(ApplicationUser user)
        {
            using (var securityTransaction = _securityContext.Database.BeginTransaction())
            {
                try
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded) return result;

                    AuthUser authUser = AuthUserRepository.GetByPrimaryKey(user.AuthUserId);
                    if (authUser == null) throw new NullReferenceException("User is not tied to a valid AuthUserId. Database out of sync.");
                    authUser.IsApproved = user.IsApproved;
                    AuthUserRepository.Update(authUser);
                    await SaveContextAsync();

                    securityTransaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    securityTransaction.Rollback();
                    _logger.Error(ex);

                    return IdentityResult.Failed(new IdentityError { Description = ex.Message });
                }
            }
        }

        public async Task<IdentityResult> AuthoriseSignatoryAsync(ApplicationUser user)
        {
            try
            {
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) return result;

                await SaveContextAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            using (var userTransaction = Context.Database.BeginTransaction())
            {
                using (var securityTransaction = _securityContext.Database.BeginTransaction())
                {
                    try
                    {
                        /* Identity Framework has a unique username constraint built into the IdentityContext
                         * User Entity which prevents soft-deleted usernames to be reused natively*/
                        user.UserName = GenerateUniqueUsername(user.UserName);

                        user.LockoutEnabled = true;
                        user.IsDeleted = true;
                        IdentityResult result = await _userManager.UpdateAsync(user);
                        if (!result.Succeeded) return result;

                        AuthUser authUser = AuthUserRepository.GetByPrimaryKey(user.AuthUserId);
                        if (authUser == null) throw new NullReferenceException("User is not tied to a valid AuthUserId. Database out of sync.");
                        authUser.UserName = user.UserName;
                        authUser.IsLockedOut = true;
                        AuthUserRepository.Update(authUser);
                        InsertAuthUserLog(authUser, "DELETE");
                        await SaveContextAsync();

                        ClientCompanyContact clientCompanyContact = ClientCompanyContactRepository.GetByPrimaryKey(user.ClientCompanyContactId);
                        if (clientCompanyContact == null) throw new NullReferenceException("User is not tied to a valid ClientCompanyContactId. Database out of sync.");
                        clientCompanyContact.IsDeleted = true;
                        ClientCompanyContactRepository.Update(clientCompanyContact);
                        InsertClientCompanyContactLog(clientCompanyContact, "DELETE");
                        await SaveContextAsync();

                        userTransaction.Commit();
                        securityTransaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        userTransaction.Rollback();
                        securityTransaction.Rollback();
                        _logger.Error(ex);

                        return IdentityResult.Failed(new IdentityError { Description = ex.Message });
                    }
                }
            }
        }

        /// <summary>
        /// Get an unique username using the provided initial value (e.g. email) and a GUID
        /// Max length restriction due to AuthUser.Username length
        /// </summary>
        /// <param name="initialValue"></param>
        /// <returns></returns>
        public string GenerateUniqueUsername(string initialValue = "")
        {
            var username = $"{initialValue}-{Guid.NewGuid()}";
            int size = DatabaseConstant.Setting_UserManagement_UsernameCharacterLimit;
            if (username.Length > size) username = username.Substring(0, size);
            return username;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPasswordHash)
        {
            using (var userTransaction = Context.Database.BeginTransaction())
            {
                using (var securityTransaction = _securityContext.Database.BeginTransaction())
                {
                    try
                    {
                        user.PasswordHash = newPasswordHash;
                        user.LastPasswordChange = DateTime.Today;
                        IdentityResult result = await _userManager.UpdateAsync(user);
                        if (!result.Succeeded) return result;

                        AuthUser authUser = AuthUserRepository.GetByPrimaryKey(user.AuthUserId);
                        if (authUser == null) throw new NullReferenceException("User is not tied to a valid AuthUserId. Database out of sync.");
                        authUser.Password = newPasswordHash;
                        AuthUserRepository.Update(authUser);

                        await InsertInPreviousPasswords(newPasswordHash, user);

                        await SaveContextAsync();

                        userTransaction.Commit();
                        securityTransaction.Commit();

                        return result;
                    }
                    catch (Exception ex)
                    {
                        userTransaction.Rollback();
                        securityTransaction.Rollback();
                        _logger.Error(ex);

                        return IdentityResult.Failed(new IdentityError { Description = ex.Message });
                    }
                }
            }
        }

        public IQueryable<ApplicationUser> GetUnapprovedUsers()
        {
            var applicationUserList = ApplicationUserRepo.GetQueryable(x =>
            (x.IsApproved == false || (x.IsSignatory == true && x.IsAuthorisedSignatory == false))
            && x.IsDeleted == false);

            return applicationUserList;
        }

        public IQueryable<ApplicationUser> GetUsersByCompanyId(int clientCompanyId)
        {
            var applicationUserList = ApplicationUserRepo.GetQueryable(x => x.ClientCompanyId == clientCompanyId && x.IsDeleted == false);

            return applicationUserList;
        }

        public ApplicationUser GetUserByClientCompanyContactId(int clientCompanyContactId)
        {
            ApplicationUser applicationUser = ApplicationUserRepo.GetQueryable(x =>
                x.ClientCompanyContactId == clientCompanyContactId
                && x.IsDeleted == false).FirstOrDefault();

            return applicationUser;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            ApplicationUser originalUser = await _userManager.FindByIdAsync(userId);

            return originalUser;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private bool CheckUniqueEmail(int clientCompanyId, string email, int clientCompanyContactId = 0)
        {
            bool anyMatching = ApplicationUserRepo.Get()
                 .Any(x => x.NormalizedEmail == email.ToUpperInvariant()
                 && x.ClientCompanyId == clientCompanyId
                 && !x.IsDeleted
                 && x.ClientCompanyContactId != clientCompanyContactId);
            return !anyMatching;
        }

        private bool CheckUniqueUsername(string username, int clientCompanyContactId = 0)
        {
            bool anyMatching = ApplicationUserRepo.Get()
                .Any(x => x.NormalizedUserName == username.ToUpperInvariant()
                && !x.IsDeleted
                && x.ClientCompanyContactId != clientCompanyContactId);

            return !anyMatching;
        }

        public IdentityResult ValidateUserDetails(UserValidationModel user)
        {
            //validate only if the value exists and it is set to true
            if (!user.ValidateUserDetails.HasValue ||
                !user.ValidateUserDetails.Value)
            {
                return IdentityResult.Success;
            }

            List<IdentityError> validationErrors = new List<IdentityError>();
            if (!CheckUniqueUsername(user.Username, user.ClientCompanyContactId))
            {
                _logger.Info($"Username: {user.Username} must be unique");
                validationErrors.Add(new IdentityError { Description = "Username must be unique" });
            }

            if (!CheckUniqueEmail(user.ClientCompanyId, user.Email, user.ClientCompanyContactId))
            {
                _logger.Info($"Email: {user.Email} must be unique within the Client Company ID: {user.ClientCompanyId}");
                validationErrors.Add(new IdentityError { Description = "Email must be unique within the Client Company Account" });
            }

            return validationErrors.Count > 0 ? IdentityResult.Failed(validationErrors.ToArray()) : IdentityResult.Success;
        }
        public async Task<ApplicationUser> GetUserByNameAsync(string username)
        {
            var originalUser = await _userManager.FindByNameAsync(username);
            if (originalUser == null)
                return null;
            return originalUser;
        }


        /// <summary>
        /// Checking if username exists
        /// </summary>
        /// <param name="username">UserName or Email</param>
        /// <returns>bool</returns>
        public async Task<bool> IsUserByNameAsync(string username)
        {
            var userName = GetUserName(username);
            var originalUser = await _userManager.FindByNameAsync(userName);
            return originalUser != null;
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null) throw new NullReferenceException("Model is missing an ID");

            return roles;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }


        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password)
        {
            var result = await _userManager.ResetPasswordAsync(user, code, password);
            if (result.Succeeded)
            {
                await InsertInPreviousPasswords(user.PasswordHash, user);
            }

            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(string user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public async Task<bool> VerifyToken(ApplicationUser user, string tokenProvider, string tokenPurpose, string tokenCode)
        {
            var isTokenValid = await _userManager.VerifyUserTokenAsync(user, tokenProvider, tokenPurpose, tokenCode);

            return isTokenValid;
        }

        public async Task PersistToken(Token refreshToken)
        {
            _securityContext.Add(refreshToken);
            await _securityContext.SaveChangesAsync();
            //TokenRepo.Insert(refreshToken);
            //await SaveContextAsync();
        }

        public async Task ReplaceToken(Token newRefreshToken, Token oldRefreshToken)
        {
            //invalidate old token
            _securityContext.Remove(oldRefreshToken);
            //insert new token
            _securityContext.Add(newRefreshToken);
            //persist
            await _securityContext.SaveChangesAsync();
        }

        public async Task RemoveToken(Token token)
        {
            //invalidate old token
            _securityContext.Remove(token);
            //persist
            await _securityContext.SaveChangesAsync();
        }

        public Token GetRefreshToken(int userID, string refreshToken)
        {
            var token = TokenRepo.GetQueryable(x => x.UserId == userID && x.Value == refreshToken)
                .FirstOrDefault();

            return token;
        }

        public async Task CurrentUserSignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        private string GetUserName(string userNameOrEmail)
        {
            if (!userNameOrEmail.Contains("@"))
                return userNameOrEmail;

            var response = _userManager.FindByEmailAsync(userNameOrEmail);

            return response.Result != null ? response.Result.UserName : string.Empty;
        }

        public IQueryable<PreviousPassword> GetLastPasswords(long userId)
        {
            return PreviousPasswordsRepository
                .GetQueryable(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate);
        }

        public async Task<string> HashPasswordAsync(string password)
        {
            var user = new ApplicationUser { PasswordHash = password };
            var success = await _userManager.HasPasswordAsync(user);

            if (success)
            {
                return user.PasswordHash;
            }
            return null;
        }

        public async Task SetRoleForUser(long userId, long roleId)
        {
            var userRole = new ApplicationUserRole
            {
                UserId = userId,
                RoleId = roleId,
            };

            ApplicationUserRoleRepository.Insert(userRole);
            await _securityContext.SaveChangesAsync();
        }

        public IQueryable<ApplicationRole> GetRole(string role)
        {
            return ApplicationRoleRepository
                .GetQueryable(x => x.NormalizedName == role.Trim().ToUpper());
        }

        public IQueryable<ApplicationUser> GetApplicationUserByAuthUserId(int authUserId)
        {
            return ApplicationUserRepo
                .GetQueryable(x => x.AuthUserId == authUserId);
        }

        public AuthUser GetAuthUserByAuthUserId(int authUserId)
        {
            return AuthUserRepository.GetByPrimaryKey(authUserId);
        }

        public IQueryable<IGrouping<int, ActivityLog>> GetActivityLog(IList<int> clientCompanyIDs, string activityType)
        {
            return ActivityLogRepo
                       .GetQueryable(x => x.Activity.Type == activityType
                           && x.AuthUserId.HasValue
                           && clientCompanyIDs.Contains(x.ApplicationUser.ClientCompanyId)
                           && x.IsSuccess,
                           orderBy: null, includeProperties: "Activity,ApplicationUser")
                       .OrderByDescending(x => x.LogDate)
                       .GroupBy(x => x.ApplicationUser.ClientCompanyId);
        }

        public IQueryable<ActivityLog> GetUserActivityLog(int authUserId)
        {
            return ActivityLogRepo
                .GetQueryable(x => x.Activity.Type == "login"
                    && x.AuthUserId.HasValue
                    && x.AuthUserId == authUserId,
                        orderBy: null, includeProperties: "Activity")
                .OrderByDescending(x => x.LogDate);
        }

        public async Task LogActivity(ActivityLog log)
        {
            _securityContext.Add(log);
            await _securityContext.SaveChangesAsync();
        }

        public AppUser GetAppUserById(int appUserId)
        {
            var appUser = AppUserRepository
                .GetQueryable(x => x.Id == appUserId)
                .FirstOrDefault();
            return appUser;
        }

        public IEnumerable<AppUser> GetAllDirectorsAsList()
        {
            return AppUserRepository.GetQueryable(x => x.IsDirector == true);
        }

        public UserChangeRequestResponse ValidateUserMobileChangeRequest(ClientUserModel updatedClientUser, ClientUserModel originalClientUser, int daysPeriod)
        {
            UserChangeRequestResponse response = new UserChangeRequestResponse()
            {
                InsertOrUpdateUserChangeRequest = false,
                SendUserChangeAlerts = false,
                WarningMessage = string.Empty,
            };

            if (updatedClientUser.PhoneNumberMobile == originalClientUser.PhoneNumberMobile || string.IsNullOrEmpty(originalClientUser.PhoneNumberMobile))
                return response;

            if (GetUserChangeRequest(originalClientUser.AuthUserId, "Pending", "Telephone") != null)
            {
                response.InsertOrUpdateUserChangeRequest = true;
                response.WarningMessage = "There is already a change request pending";
            }
            else
            {
                //Insert a change request for the mobile if the user's email has been recently changed
                if (originalClientUser.LastEmailChangeDate.HasValue
                    && originalClientUser.LastEmailChangeDate.Value.AddDays(daysPeriod) > DateTime.Now)
                {
                    response.InsertOrUpdateUserChangeRequest = true;
                    response.SendUserChangeAlerts = true;
                    response.WarningMessage = $"Email was changed within the past {daysPeriod} days";
                }
            }

            return response;
        }

        public UserChangeRequestResponse ValidateUserEmailChangeRequest(ClientUserModel updatedClientUser, ClientUserModel originalClientUser, int daysPeriod)
        {
            UserChangeRequestResponse response = new UserChangeRequestResponse()
            {
                InsertOrUpdateUserChangeRequest = false,
                SendUserChangeAlerts = false,
                WarningMessage = string.Empty,
            };

            if (updatedClientUser.Email == originalClientUser.Email || string.IsNullOrEmpty(originalClientUser.Email))
                return response;

            if (GetUserChangeRequest(originalClientUser.AuthUserId, "Pending", "Email") != null)
            {
                response.InsertOrUpdateUserChangeRequest = true; //to update the existing change request
                response.WarningMessage = "There is already a change request pending";
            }
            else
            {
                bool phoneNumberModified = (updatedClientUser.PhoneNumberMobile != originalClientUser.PhoneNumberMobile);

                //Insert a change request for the email if the user's mobile is also recently changed
                if (phoneNumberModified || originalClientUser.LastPhoneNumberMobileChangeDate.HasValue
                    && (originalClientUser.LastPhoneNumberMobileChangeDate.Value.AddDays(daysPeriod) > DateTime.Now))
                {
                    response.InsertOrUpdateUserChangeRequest = true;
                    response.SendUserChangeAlerts = true;
                    response.WarningMessage = $"Mobile was changed within the past {daysPeriod} days";
                }
            }

            return response;
        }

        public UserChangeRequest GetUserChangeRequest(int userChangeRequestId)
        {
            return UserChangeRequestRepository.GetQueryable(x =>
                x.Id == userChangeRequestId)
                .FirstOrDefault();
        }

        public IEnumerable<PendingApprovalUserChangeRequest> GetPendingChangeRequest()
        {
            var externalUserApprovals =
                _appSettingUow.GetAppSetting(AppSettingEnum.ExternalUserChangeRequestApprovalsRequired).ValueAs<int>();
            var internalUserapprovals =
                _appSettingUow.GetAppSetting(AppSettingEnum.InternalUserChangeRequestApprovalsRequired).ValueAs<int>();
            var requests = UserChangeRequestRepository.GetQueryable().Where(r => r.ChangeStatus == "Pending")
                .Select(r =>
                new PendingApprovalUserChangeRequest
                {
                    UserChangeRequestID = r.Id,
                    AuthUserID = r.AuthUserId,
                    AuthUserName = r.AuthUser.UserName,
                    CurrentValue = r.CurrentValue.Replace('|', char.MinValue),
                    ProposedValue = r.ProposedValue.Replace('|', char.MinValue),
                    ChangeValueType = r.ChangeValueType,
                    ChangeDateTime = r.ChangeDateTime,
                    ChangedByAuthUserID = r.ChangedByAuthUserId,
                    ChangedByAuthUserName = r.ChangedByAuthUser.UserName,
                    ChangeStatus = r.ChangeStatus,
                    AuthApplicationDescription = r.AuthUser.Application.Description,
                    ApprovedBy = string.Join(", ", r.UserChangeRequestApproval.Select(a => a.ApprovedByAuthUser.UserName)),
                    Company = r.AuthUser.ClientCompanyContactAuthUser.SingleOrDefault().ClientCompany.Name ?? "Argentex",
                    Forename = r.AuthUser.ClientCompanyContactAuthUser.SingleOrDefault().Forename ?? r.AuthUser.AppUser.SingleOrDefault().Forename,
                    Surname = r.AuthUser.ClientCompanyContactAuthUser.SingleOrDefault().Surname ?? r.AuthUser.AppUser.SingleOrDefault().Surname
                }).ToList();

            requests.ForEach(r => r.ApprovalsRequired = r.Company == "Argentex" ? internalUserapprovals : externalUserApprovals);

            return requests;
        }

        public async Task<ApproveUserChangeResponse> ApproveUserChangeRequest(ApproveUserChangeRequest approveUserChangeRequest)
        {
            using (var userTransaction = Context.Database.BeginTransaction())
            {
                using (var identityTransaction = _securityContext.Database.BeginTransaction())
                {
                    ApproveUserChangeResponse approveUserChangeResponse = new ApproveUserChangeResponse();

                    try
                    {
                        approveUserChangeResponse.Result = IdentityResult.Success;
                        var userChangeRequest = GetUserChangeRequest(approveUserChangeRequest.UserChangeRequestID);

                        // Check if UserChangeRequest is still Pending
                        bool isUserChangeRequestPending = UserChangeRequestRepository.GetQueryable(x => x.Id == userChangeRequest.Id && x.ChangeStatus == "Pending").Any();

                        if (isUserChangeRequestPending && approveUserChangeRequest.ApprovedByAuthUserId > 0)
                        {
                            // Inserting record in the UserChangeRequestApproval table
                            InsertUserChangeRequestApproval(userChangeRequest);
                            await SaveContextAsync();

                            //Updating the user details in IdentityDB, ClientCompanyContact and AuthUser tables 
                            approveUserChangeResponse = await UpdateUserDetails(userChangeRequest);
                        }
                        else
                        {
                            approveUserChangeResponse.Result = IdentityResult.Failed(new IdentityError { Description = "No userchange request to approve" });
                        }

                        if (approveUserChangeResponse.Result.Succeeded)
                        {
                            identityTransaction.Commit();
                            userTransaction.Commit();
                            approveUserChangeResponse.Result = IdentityResult.Success;
                            approveUserChangeResponse.UserChangeRequest = GetUserChangeRequest(approveUserChangeRequest.UserChangeRequestID);
                        }
                        else
                        {
                            identityTransaction.Rollback();
                            userTransaction.Rollback();
                            approveUserChangeResponse.Result = IdentityResult.Failed();
                        }

                        // return idResult;
                        return approveUserChangeResponse;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                        identityTransaction.Rollback();
                        userTransaction.Rollback();
                        approveUserChangeResponse.Result = IdentityResult.Failed(new IdentityError { Description = ex.Message });
                        return approveUserChangeResponse;
                    }
                }
            }

        }


        public async Task<ApproveUserChangeResponse> UpdateUserDetails(UserChangeRequest userChangeRequest)
        {

            var approveUserChangeResponse = new ApproveUserChangeResponse();
            approveUserChangeResponse.Result = IdentityResult.Success;

            try
            {
                //Get the Number of approval required for External Users
                int? externalApprovalsRequired = _appSettingUow
                    .GetAppSetting(AppSettingEnum.ExternalUserChangeRequestApprovalsRequired)
                    .ValueAs<int>();


                //Get the Number of approvals for the request
                var numberOfAprovals = UserChangeRequestApprovalRepository.GetQueryable(x => x.UserChangeRequestId == userChangeRequest.Id).Count();

                //If the number of approvals is equal or greater than the Approvals required then Update AppUser or Contact details

                if (numberOfAprovals >= externalApprovalsRequired)
                {
                    int clientContactId = ClientCompanyContactRepository.GetQueryable(x => x.AuthUserId == userChangeRequest.AuthUserId)
                                                                        .Select(x => x.Id).FirstOrDefault();

                    var clientUser = GetClientUserModelByContactId(clientContactId);

                    if (clientUser != null)
                    {

                        if (userChangeRequest.ChangeValueType == "Email")
                        {
                            clientUser.Email = userChangeRequest.ProposedValue;
                            clientUser.LastEmailChangeDate = DateTime.Now;

                        }
                        else if (userChangeRequest.ChangeValueType == "Telephone")
                        {
                            clientUser.PhoneNumberMobile = userChangeRequest.ProposedValue;
                            clientUser.LastPhoneNumberMobileChangeDate = DateTime.Now;
                        }
                        clientUser.UpdatedDateTime = DateTime.Now;
                        //Updating the UserChangeRequest table
                        userChangeRequest.ChangeStatus = "Approved";
                        UserChangeRequestRepository.Update(userChangeRequest);

                        await SaveContextAsync();
                        approveUserChangeResponse.Result = await UpdateUserAsync(clientUser);

                        approveUserChangeResponse.SendNotification = approveUserChangeResponse.Result.Succeeded;

                    }
                }
                return approveUserChangeResponse;
            }

            catch (Exception ex)
            {
                _logger.Error(ex);
                approveUserChangeResponse.Result = IdentityResult.Failed(new IdentityError { Description = ex.Message });
                return approveUserChangeResponse;

            }
        }
        public UserChangeRequest GetUserChangeRequest(int authUserID, string changeStatus, string changeValueType)
        {
            return UserChangeRequestRepository.GetQueryable(x =>
                x.AuthUserId == authUserID
                && x.ChangeStatus == changeStatus
                && x.ChangeValueType == changeValueType)
                .FirstOrDefault();
        }

        public async Task<IdentityResult> ProcessUserChangeRequest(UserChangeRequest changeRequest)
        {
            using (var userTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    //cancel any existing request
                    var existingPendingRequest = GetUserChangeRequest(changeRequest.AuthUserId, "Pending", changeRequest.ChangeValueType);
                    if (existingPendingRequest != null)
                    {
                        existingPendingRequest.ChangeStatus = "Cancelled";
                        UserChangeRequestRepository.Update(existingPendingRequest);
                        await SaveContextAsync();
                    }

                    //Insert new change request
                    UserChangeRequestRepository.Insert(changeRequest);
                    await SaveContextAsync();

                    userTransaction.Commit();
                    return IdentityResult.Success;
                }
                catch (Exception ex)
                {
                    userTransaction.Rollback();
                    _logger.Error(ex);

                    return IdentityResult.Failed(new IdentityError { Description = ex.Message });
                }
            }
        }

        public string GetSendersEmailAddress(int authUserId)
        {
            string emailTo = string.Empty;
            // Only Send "Mobile Modified" email alert to contacts from Companies that are Clients
            if (GetClientCompanyStatus(authUserId) == "Client")
            {
                emailTo = ApplicationUserRepo.GetQueryable(x => x.AuthUserId == authUserId).Select(x => x.Email).FirstOrDefault();
            }
            return emailTo;
        }

        public string GetSendersPhoneNumber(int authUserId)
        {
            string phoneNumberTo = string.Empty;

            // Only Send "Email Modified" text message alert to contacts from Companies that are Clients
            if (GetClientCompanyStatus(authUserId) == "Client")
            {
                phoneNumberTo = ApplicationUserRepo.GetQueryable(x => x.AuthUserId == authUserId).Select(x => x.PhoneNumberMobile).FirstOrDefault();
            }

            return phoneNumberTo;
        }


        private string GetClientCompanyStatus(int authUserId)
        {
            return ClientCompanyContactRepository.GetQueryable(a => a.AuthUserId == authUserId)
                                  .Select(x => x.ClientCompany.ClientCompanyStatus.Description).FirstOrDefault();
        }

        #region Private Methods
        private AuthUser MapAuthUser(AuthUser authUser, ApplicationUser user)
        {
            authUser.Id = user.AuthUserId;
            authUser.UserName = user.UserName;
            authUser.Password = user.PasswordHash;
            authUser.Email = user.Email;
            authUser.IsApproved = user.IsApproved;
            authUser.CreateDate = DateTime.Now;
            authUser.FailedPasswordAttemptCount = 0;
            authUser.ApplicationId = 2;
            authUser.FailedPasswordAttemptWindowStart = DateTime.Now;
            authUser.IsLockedOut = user.LockoutEnabled;

            return authUser;
        }

        private ClientCompanyContact MapClientCompanyContact(ClientCompanyContact clientCompanyContact, ApplicationUser user)
        {
            //clientCompanyContact.AuthUserId = user.AuthUserId;
            clientCompanyContact.ClientCompanyId = user.ClientCompanyId;
            clientCompanyContact.Title = user.Title;
            clientCompanyContact.Forename = user.Forename;
            clientCompanyContact.Surname = user.Surname;
            clientCompanyContact.Fullname = user.Title + " " + user.Forename + " " + user.Surname;
            clientCompanyContact.Email = user.Email;
            clientCompanyContact.Position = user.Position;
            clientCompanyContact.PrimaryContact = user.PrimaryContact;
            clientCompanyContact.TelephoneDirect = user.PhoneNumber;
            clientCompanyContact.TelephoneMobile = user.PhoneNumberMobile;
            clientCompanyContact.TelephoneOther = user.PhoneNumberOther;
            clientCompanyContact.Aspnumber = user.ASPNumber;
            clientCompanyContact.AspcreationDate = user.ASPCreationDate;
            clientCompanyContact.Authorized = user.IsApproved;
            clientCompanyContact.UpdatedByAuthUserId = user.UpdatedByAuthUserId;
            clientCompanyContact.UpdatedDateTime = DateTime.Now;
            clientCompanyContact.Notes = user.Notes;
            clientCompanyContact.RecNotifications = false;
            clientCompanyContact.RecAmreport = false;
            clientCompanyContact.RecActivityReport = false;
            clientCompanyContact.IsDeleted = user.IsDeleted;

            return clientCompanyContact;
        }

        private ApplicationUser MapApplicationUser(ApplicationUser appUser, ClientUserModel user)
        {
            appUser.Title = user.Title;
            appUser.Forename = user.Forename;
            appUser.Surname = user.Surname;
            appUser.UserName = user.Username;
            appUser.Email = user.Email;
            appUser.ClientCompanyId = user.ClientCompanyId;
            appUser.ClientCompanyContactId = user.ClientCompanyContactId;
            appUser.UpdatedByAuthUserId = user.UpdatedByAuthUserId;
            appUser.Position = user.Position;
            appUser.PhoneNumber = user.PhoneNumberDirect;
            appUser.PhoneNumberMobile = user.PhoneNumberMobile;
            appUser.PhoneNumberOther = user.PhoneNumberOther;
            appUser.Birthday = user.Birthday;
            appUser.Notes = user.Notes;
            appUser.PrimaryContact = user.PrimaryContact;
            appUser.LockoutEnabled = user.IsLockedOut ?? false;
            appUser.IsApproved = user.IsApproved;
            appUser.IsSignatory = user.IsSignatory;
            appUser.IsAuthorisedSignatory = user.IsAuthorisedSignatory;
            appUser.IsAdmin = user.IsAdmin;
            appUser.ApprovedByAuthUserId = user.ApprovedByAuthUserId;

            return appUser;
        }

        private AuthUser MapAuthUser(AuthUser authUser, ClientUserModel user)
        {
            authUser.UserName = user.Username;
            authUser.Password = user.PasswordHash;
            authUser.Email = user.Email ?? "NoEmail";
            authUser.IsApproved = user.IsApproved;
            authUser.IsLockedOut = user.IsLockedOut ?? false;
            authUser.Comment = user.Comment;

            return authUser;
        }

        private ClientCompanyContact MapClientCompanyContact(ClientCompanyContact clientCompanyContact, ClientUserModel user)
        {
            clientCompanyContact.ClientCompanyId = user.ClientCompanyId;
            clientCompanyContact.Title = user.Title;
            clientCompanyContact.Forename = user.Forename;
            clientCompanyContact.Surname = user.Surname;
            clientCompanyContact.Fullname = $"{user.Title} {user.Forename} {user.Surname}";
            clientCompanyContact.Email = user.Email;
            clientCompanyContact.LastEmailChangeDate = user.LastEmailChangeDate;
            clientCompanyContact.Position = user.Position;
            clientCompanyContact.PrimaryContact = user.PrimaryContact;
            clientCompanyContact.TelephoneDirect = user.PhoneNumberDirect;
            clientCompanyContact.TelephoneMobile = user.PhoneNumberMobile;
            clientCompanyContact.TelephoneOther = user.PhoneNumberOther;
            clientCompanyContact.LastTelephoneChangeDate = user.LastPhoneNumberMobileChangeDate;
            clientCompanyContact.Birthday = user.Birthday;
            clientCompanyContact.Aspnumber = user.ASPNumber;
            clientCompanyContact.AspcreationDate = user.ASPCreationDate;
            clientCompanyContact.UpdatedByAuthUserId = user.UpdatedByAuthUserId;
            clientCompanyContact.UpdatedDateTime = user.UpdatedDateTime;
            clientCompanyContact.Notes = user.Notes;
            clientCompanyContact.Authorized = user.Authorized;
            clientCompanyContact.RecNotifications = user.RecNotification;
            clientCompanyContact.RecAmreport = user.RecAmReport;
            clientCompanyContact.RecActivityReport = user.RecActivityReport;
            clientCompanyContact.BloombergGpi = user.BloombergGpi;
            clientCompanyContact.NiNumber = user.NiNumber;
            clientCompanyContact.IsDeleted = user.IsDeleted;

            return clientCompanyContact;
        }

        private void InsertAuthUserLog(AuthUser authUser, string action)
        {
            var log = new LogAuthUser()
            {
                LogAction = action,
                Id = authUser.Id,
                UserName = authUser.UserName,
                Password = authUser.Password,
                Email = authUser.Email,
                IsApproved = authUser.IsApproved,
                IsLockedOut = authUser.IsLockedOut,
                Comment = authUser.Comment,
                CreateDate = authUser.CreateDate,
                LastPasswordChangeDate = authUser.LastPasswordChangeDate,
                LastLoginDate = authUser.LastLoginDate,
                LastActivityDate = authUser.LastActivityDate,
                LastLockOutDate = authUser.LastLockOutDate,
                FailedPasswordAttemptCount = authUser.FailedPasswordAttemptCount,
                FailedPasswordAttemptWindowStart = authUser.FailedPasswordAttemptWindowStart,
                ApplicationId = authUser.ApplicationId
            };

            LogAuthUserRepository.Insert(log);
        }


        private void InsertClientCompanyContactLog(ClientCompanyContact clientCompanyContact, string action)
        {
            var log = new LogClientCompanyContact()
            {
                LogAction = action,
                Id = clientCompanyContact.Id,
                ClientCompanyId = clientCompanyContact.ClientCompanyId,
                Title = clientCompanyContact.Title,
                Forename = clientCompanyContact.Forename,
                Surname = clientCompanyContact.Surname,
                Email = clientCompanyContact.Email,
                TelephoneDirect = clientCompanyContact.TelephoneDirect,
                TelephoneMobile = clientCompanyContact.TelephoneMobile,
                TelephoneOther = clientCompanyContact.TelephoneOther,
                Birthday = clientCompanyContact.Birthday,
                Authorized = clientCompanyContact.Authorized,
                UpdateTimeStamp = clientCompanyContact.UpdateTimeStamp,
                UpdatedByAuthUserId = clientCompanyContact.UpdatedByAuthUserId,
                UpdatedDateTime = clientCompanyContact.UpdatedDateTime,
                Notes = clientCompanyContact.Notes,
                Fullname = clientCompanyContact.Fullname,
                RecNotifications = clientCompanyContact.RecNotifications,
                RecAmreport = clientCompanyContact.RecAmreport,
                AuthUserId = clientCompanyContact.AuthUserId,
                Position = clientCompanyContact.Position,
                PrimaryContact = clientCompanyContact.PrimaryContact,
                RecActivityReport = clientCompanyContact.RecActivityReport,
                IsDeleted = clientCompanyContact.IsDeleted,
                Aspnumber = clientCompanyContact.Aspnumber,
                AspcreationDate = clientCompanyContact.AspcreationDate,
                LastTelephoneChangeDate = clientCompanyContact.LastTelephoneChangeDate,
                LastEmailChangeDate = clientCompanyContact.LastEmailChangeDate,
                BloombergGpi = clientCompanyContact.BloombergGpi,
                NiNumber = clientCompanyContact.NiNumber
            };

            LogClientCompanyContactRepository.Insert(log);
        }

        private void InsertUserChangeRequestApproval(UserChangeRequest userChangeRequest)
        {
            UserChangeRequestApproval userChangeRequestApproval = new UserChangeRequestApproval()
            {
                UserChangeRequestId = userChangeRequest.Id,
                ApprovedByAuthUserId = userChangeRequest.ChangedByAuthUserId,
                ApprovedDateTime = DateTime.Now,
                IsActive = true

            };
            UserChangeRequestApprovalRepository.Insert(userChangeRequestApproval);


        }
        private async Task InsertInPreviousPasswords(string passwordHash, ApplicationUser user)
        {
            var previousPassword = new PreviousPassword()
            {
                UserId = user.Id,
                CreatedDate = DateTime.Now,
                PasswordHash = passwordHash
            };

            PreviousPasswordsRepository.Insert(previousPassword);
            await _securityContext.SaveChangesAsync();
        }
        #endregion

        #region Disposing
        //TODO - this has to be refactored that each unit of work handles only one context
        //or not inherit from BaseUow
        private bool _disposed = false;
        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _securityContext.Dispose();
            }
            _disposed = true;
        }

        #endregion
    }
}