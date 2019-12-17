using Argentex.Core.Service.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Identity.Services
{
    public interface IIdentityService : IDisposable
    {
        Task<TokenModel> AuthenticateAsync(LoginServiceModel login);
        Task<TokenModel> RefreshToken(RefreshTokenModel login);
        JwtSecurityToken BuildToken(string tokenSubject, bool tokenCanExpire,
            IEnumerable<Claim> additionalClaims = null);
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword, string confirmPassword);
        Task<IdentityResult> ResetPasswordAsync(string email, string code, string password);
        Task<bool> VerifyUserToken(string userName, string tokenCode);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationServiceUser user);
        Task SetUserAsAdmin(string username);
        Task LogoutAsync(string identityUserName, string refreshToken);
        Task<string> GetUserPhoneNumber(string username);
        
    }
}
