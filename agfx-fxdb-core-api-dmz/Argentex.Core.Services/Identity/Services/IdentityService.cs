using Argentex.Core.Identity.DataAccess;
using Argentex.Core.Service.Email.EmailSender;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Identity.Helpers;
using Argentex.Core.Service.Identity.Services;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.UnitsOfWork.Users;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OpenIddict.EntityFrameworkCore.Models;

namespace Argentex.Core.Service.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfigWrapper _config;
        private readonly IUserUow _userUow;
        private readonly IClientApplicationUow _clientApplicationUow;
        private readonly IEmailService _emailService;
        private const string REGISTER = "register";
        private const string LOGIN = "login";
        private const string ADMIN = "admin";
        private bool _disposed;

        public IdentityService(
            IConfigWrapper config,
            IUserUow userUow,
            IEmailService emailService,
            IClientApplicationUow clientApplicationUow

            )
        {
            _config = config;
            _userUow = userUow;
            _emailService = emailService;
            _clientApplicationUow = clientApplicationUow;
        }

        private IEnumerable<Claim> BuildStandardClaims(string subject) => new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, subject),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString())
        };

        private IEnumerable<Claim> BuildUserClaims(UserModel user) => new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(OpenIdConnectConstants.Claims.Name, user.Name),
            new Claim("fullname" , user.Forename + " " + user.Surname),
            new Claim("clientCompanyId", user.ClientCompanyId.ToString()),
            new Claim("authUserId", user.AuthUserId.ToString()),
            new Claim("role", JsonExtensions.SerializeToJson(user.Roles)),
            new Claim("daysBeforePasswordExpiration", CalculateDaysBeforePasswordExpiration(user.PasswordLastChanged).ToString()),
            new Claim("isAdmin", user.IsAdmin.ToString()),
            new Claim("grantType", "password"), 
        };

        private IEnumerable<Claim> BuildUserClaims() => new[]
        {
            new Claim("grantType", "client_credentials"),
        };

        public JwtSecurityToken BuildToken(string tokenSubject, bool tokenCanExpire,
            IEnumerable<Claim> additionalClaims = null)
        {
            DateTime now = DateTime.UtcNow;
            var claims = BuildStandardClaims(tokenSubject).ToList();
            if (additionalClaims != null) claims.AddRange(additionalClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Get("Jwt:SecurityKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            double.TryParse(_config.Get("Jwt:Expires"), out double expires);
            var token = new JwtSecurityToken(
              issuer: _config.Get("Jwt:Issuer"),//TODO - get this from DB
              audience: _config.Get("Jwt:Issuer"), //TODO - get this from DB
              claims: claims,
              notBefore: now,
              expires: tokenCanExpire ? (DateTime?)now.AddSeconds(expires) : null,//TODO get this from DB
              signingCredentials: creds);

            return token;
        }

        public async Task<TokenModel> AuthenticateAsync(LoginServiceModel login)
        {
            switch (login.Grant_Type)
            {
                case OpenIdConnectConstants.GrantTypes.Password:
                    return await GetTokenFromPassword(login);
                case OpenIdConnectConstants.GrantTypes.ClientCredentials:
                    return await GetTokenFromCredentials(login);
                default:
                    await LogCurrentActivity(login.Username, LOGIN, false, login.PrimaryIP, login.SecondaryIP);
                    return null;
            }

        }

        public async Task<TokenModel> RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            var refreshToken = _userUow.GetRefreshToken(refreshTokenModel.UserID, refreshTokenModel.RefreshToken);
            if (refreshToken == null) return null;

            ApplicationUser user = await _userUow.GetUserByIdAsync(refreshToken.UserId.ToString());
            if (user == null) return null;

            var newRefreshToken = CreateRefreshToken(refreshToken.ClientId, refreshToken.UserId);

            await _userUow.ReplaceToken(newRefreshToken, refreshToken);

            var appUser = await CreateUserServiceModel(user);
            var token = CreateToken(appUser, newRefreshToken);

            return token;
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
                throw new PasswordsDoNotMatchException("Passwords do not match");

            var user = await _userUow.GetUserByIdAsync(userId);
            if (user == null)
                throw new ApplicationUserNotFoundException($"User with id {userId} does not exist");

            var hasher = new PasswordHasher<ApplicationUser>();
            var newPasswordHash = hasher.HashPassword(user, newPassword);
            var result = PasswordValidation(hasher, user, oldPassword, newPassword);

            if (!result.Succeeded) return result;

            ValidatePasswordWithHistory(user, newPassword);
            result = await _userUow.ChangePasswordAsync(user, newPasswordHash);

            if (result.Succeeded) await _emailService.SendPasswordChangedEmailAsync(user.UserName);

            return result;
        }




        public async Task<IdentityResult> ResetPasswordAsync(string userName, string code, string password)
        {
            var user = await _userUow.GetUserByNameAsync(userName);
            if (user == null || user.IsDeleted)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = IdentityResultCodes.UserNotFound,
                    Description = $"User with username {userName} does not exist"
                });

            if (!user.IsApproved || user.LockoutEnabled)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = IdentityResultCodes.InvalidUserState,
                    Description = $"{userName} is in an invalid state"
                });

            code = SanitizeCode(code);

            ValidatePasswordWithHistory(user, password);

            var result = await _userUow.ResetPasswordAsync(user, code, password);
            if (result.Succeeded) await _emailService.SendPasswordChangedEmailAsync(user.UserName);

            return result;
        }

        public async Task<bool> VerifyUserToken(string userName, string tokenCode)
        {
            var tokenPurpose = "ResetPassword";
            var appUser = await _userUow.GetUserByNameAsync(userName);

            tokenCode = SanitizeCode(tokenCode);

            var isValid = await _userUow.VerifyToken(appUser, TokenOptions.DefaultProvider, tokenPurpose, tokenCode);

            return isValid;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationServiceUser user)
        {
            ApplicationUser appUser = MapAppUser(new ApplicationUser(), user);
            string confirmationToken = await _userUow.GenerateEmailConfirmationTokenAsync(appUser);

            return confirmationToken;
        }

        public async Task SetUserAsAdmin(string username)
        {
            var adminId = _userUow.GetRole(ADMIN)
                .Select(x => x.Id)
                .FirstOrDefault();

            var user = _userUow.ApplicationUserRepo.Get(x =>
                x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

            await _userUow.SetRoleForUser(user.Id, adminId);
        }

        public async Task LogoutAsync(string identityUserName, string refreshToken)
        {
            // remove token from database
            try
            {
                var user = await _userUow.GetUserByNameAsync(identityUserName);
                Token token;
                if (user != null && !string.IsNullOrEmpty(refreshToken))
                {
                    token = _userUow.GetRefreshToken((int)user.Id, refreshToken);

                    if (token != null)
                    {
                        await _userUow.RemoveToken(token);
                    }
                }
                await LogCurrentActivity(identityUserName, "logout", true, null, null);
                await _userUow.CurrentUserSignOutAsync();
            }
            catch (Exception ex)
            {
                /* The catch block is intentionally kept empty to avoid the DB from being bombarded with errors.
                 The exception occurs when the user tries to logout from any page from the angular client site.
                 The exception thrown is Database operation expected to affect 1 row(s) but actually affected 0 row(s). Data may have been modified or deleted since entities were loaded. 
                 */
            }
        }

        public async Task<string> GetUserPhoneNumber(string username)
        {
            var applicationUser = await _userUow.GetUserByNameAsync(username);
            return applicationUser.PhoneNumberMobile;
        }

        #region Private

        private int CalculateDaysBeforePasswordExpiration(DateTime userPasswordLastChanged)
        {
            var today = DateTime.Today;
            var daysDifference = DateHelpers.GetDaysDifferencreBetween(userPasswordLastChanged, today);
            var remainingDays = 30 - daysDifference;

            return remainingDays <= 0 ? 0 : remainingDays;
        }

        private async Task<TokenModel> GetTokenFromPassword(LoginServiceModel login)
        {
            var userModel = await PasswordSignInAsync(login);

            // adding to activity log
            await LogCurrentActivity(login.Username, LOGIN, userModel != null, login.PrimaryIP, login.SecondaryIP);

            if (userModel == null) return null;

            var refreshToken = CreateRefreshToken(login.ClientId, userModel.UserId);

            await _userUow.PersistToken(refreshToken);
            var token = CreateToken(userModel, refreshToken);

            return token;
        }

        private async Task<TokenModel> GetTokenFromCredentials(LoginServiceModel login)
        {
            var clientApplication = await _clientApplicationUow.GetClientCredentialsAsync(login.ClientId);
            await LogCurrentActivity(login.ClientId, LOGIN, clientApplication != null, login.PrimaryIP, login.SecondaryIP);

            if (clientApplication == null) return null;

            var token = CreateToken(clientApplication);
            return token;
        }

        private TokenModel CreateToken(OpenIddictApplication clientApplication)
        {
            var token = BuildToken(clientApplication.ClientId, false, BuildUserClaims());
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenModel
            {
                Token_type = "Bearer",
                Access_token = stringToken,
                Id_token = stringToken,
            };

        }

        private TokenModel CreateToken(UserModel userModel, Token rt)
        {
            //create token
            var token = BuildToken(userModel.UserId.ToString(), true, BuildUserClaims(userModel));

            int.TryParse(_config.Get("Jwt:Expires"), out var expiresIn);
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenModel
            {
                Token_type = "Bearer",
                Access_token = stringToken,
                Expires_in = expiresIn,
                Refresh_token = rt.Value,
                //TODO this is hack to make client app login - either change the way
                Id_token = stringToken,
            };
        }

        private Token CreateRefreshToken(string clientId, long userId)
        {
            return new Token()
            {
                ClientId = clientId,
                UserId = userId,
                Type = 0,
                Value = Guid.NewGuid().ToString("N"),
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Logging user login activity into ActivityLog table
        /// </summary>
        /// <param name="username">Username or Email of the user</param>
        /// <param name="type">Activity type</param>
        /// <param name="success">Is the login successfull</param>
        /// <param name="primaryIP">Primary ip address</param>
        /// <param name="secondaryIP">Secondary ip address</param>
        /// <returns></returns>
        private async Task LogCurrentActivity(string username, string type, bool success, string primaryIP, string secondaryIP)
        {
            // checking if user exists
            var isUser = await _userUow.IsUserByNameAsync(username);
            ApplicationUser applicationUser = null;

            // getting application user if exists
            string userName;
            if (isUser)
            {
                applicationUser = await _userUow.GetUserByNameAsync(username);
                if (applicationUser == null)
                {
                    userName = String.Empty;
                }
                else
                {
                    userName = String.IsNullOrEmpty(applicationUser.UserName) ? applicationUser.Email : string.Empty;
                }
            }
            else
            {
                userName = username;
            }

            // getting Activity object
            var activity = _userUow.ActivityRepo.GetQueryable(x => x.Type == type).FirstOrDefault();

            var log = new ActivityLog
            {
                ActivityId = activity.ActivityId,
                IsSuccess = success,
                LogDate = DateTime.Now,
                UserName = userName,
                PrimaryIP = primaryIP,
                SecondaryIP = secondaryIP,
                AuthUserId = applicationUser?.AuthUserId,
                Id = applicationUser?.Id,
                ApplicationUser = applicationUser ?? null,
                Activity = activity
            };
            await _userUow.LogActivity(log);
        }

        private string SanitizeCode(string code)
        {
            string decodedCode = code
                    .Replace(SanitizeForwardSlash.New, SanitizeForwardSlash.Old)
                    .Replace(SanitizeDoubleEqual.New, SanitizeDoubleEqual.Old)
                    .Replace(SanitizePlus.New, SanitizePlus.Old);

            code = HttpUtility.HtmlDecode(decodedCode);

            return code;
        }

        private static ApplicationUser MapAppUser(ApplicationUser appUser, ApplicationServiceUser serviceUser)
        {
            appUser.Id = serviceUser.Id;
            appUser.Title = serviceUser.Title;
            appUser.Forename = serviceUser.Forename;
            appUser.Surname = serviceUser.Surname;
            appUser.UserName = serviceUser.Username;
            appUser.Email = serviceUser.Email;
            appUser.ClientCompanyId = serviceUser.ClientCompanyId;
            appUser.UpdatedByAuthUserId = serviceUser.UpdatedByAuthUserId;
            appUser.Position = serviceUser.Position;
            appUser.PhoneNumber = serviceUser.PhoneNumberDirect;
            appUser.PhoneNumberMobile = serviceUser.PhoneNumberMobile;
            appUser.PhoneNumberOther = serviceUser.PhoneNumberOther;
            appUser.Birthday = DateTime.Parse(serviceUser.Birthday);
            appUser.IsApproved = serviceUser.IsApproved;
            appUser.PrimaryContact = serviceUser.PrimaryContact;
            appUser.IsAdmin = serviceUser.IsAdmin;

            return appUser;
        }

        private static IdentityResult PasswordValidation(PasswordHasher<ApplicationUser> hasher, ApplicationUser user, string oldPassword, string newPassword)
        {
            var idResult = IdentityResult.Success;

            var verify = hasher.VerifyHashedPassword(user, user.PasswordHash, newPassword);
            if (verify == PasswordVerificationResult.Failed)
            {
                verify = hasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);
                if (verify == PasswordVerificationResult.Failed)
                {
                    idResult = IdentityResult.Failed(new IdentityError { Description = "Cannot change password. Incorrect current password inserted." });
                }
            }
            else
            {
                idResult = IdentityResult.Failed(new IdentityError { Description = "Cannot change password. Duplicate new password inserted." });
            }

            return idResult;
        }

        private void ValidatePasswordWithHistory(ApplicationUser user, string password)
        {
            var passwords = _userUow.GetLastPasswords(user.Id)
                .Select(x => x.PasswordHash)
                .Take(3)
                .ToList();

            var hasher = new PasswordHasher<ApplicationUser>();

            foreach (var passwordHash in passwords)
            {
                var used = hasher.VerifyHashedPassword(user, passwordHash, password);
                if (used == PasswordVerificationResult.Success)
                    throw new PasswordAlreadyUsedException("Password already been used within the past 3 passwords");
            }
        }

        private async Task<UserModel> PasswordSignInAsync(LoginServiceModel login)
        {

            var result = await _userUow.PasswordSignInAsync(login.Username, login.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded) return null;
            var identityUser = await _userUow.GetUserByNameAsync(login.Username);

            if (!identityUser.IsApproved || identityUser.LockoutEnabled || identityUser.IsDeleted)
                return null;

            return await CreateUserServiceModel(identityUser);
        }

        private async Task<UserModel> CreateUserServiceModel(ApplicationUser identityUser)
        {
            return identityUser != null
                ? new UserModel
                {
                    UserId = identityUser.Id,
                    Name = identityUser.UserName,
                    Email = identityUser.Email,
                    Forename = identityUser.Forename,
                    Surname = identityUser.Surname,
                    ClientCompanyId = identityUser.ClientCompanyId,
                    AuthUserId = identityUser.AuthUserId,
                    Roles = await _userUow.GetRolesAsync(identityUser),
                    PasswordLastChanged = identityUser.LastPasswordChange,
                    IsAdmin = identityUser.IsAdmin
                }
                : null;
        }

        #endregion

        #region Dispose

        /// <summary>
        /// disposing == true coming from Dispose()
        /// disposig == false coming from finaliser
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _userUow?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
