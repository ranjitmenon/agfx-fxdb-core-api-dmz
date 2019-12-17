using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using Microsoft.AspNetCore.Identity;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using OpenIddict.EntityFrameworkCore.Models;
using Argentex.Core.UnitsOfWork.Users.Model;


namespace Argentex.Core.UnitsOfWork.Users
{
    public interface IClientApplicationUow : IBaseUow
    {
        Task<OpenIddictApplication> GetClientCredentialsAsync(string clientId);
    }
    public interface IUserUow : IBaseUow
    {
        IGenericRepo<AuthUser> AuthUserRepository { get; }
        IGenericRepo<ClientCompanyContact> ClientCompanyContactRepository { get; }
        IGenericRepo<ActivityLog> ActivityLogRepo { get; }
        IGenericRepo<Activity> ActivityRepo { get; }
        IGenericRepo<ApplicationUser> ApplicationUserRepo { get; }
        IGenericRepo<AppUser> AppUserRepository { get; }
        IGenericRepo<PreviousPassword> PreviousPasswordsRepository { get; }
        IGenericRepo<ApplicationUserRole> ApplicationUserRoleRepository { get; }
        IGenericRepo<AuthApplication> AuthApplicationRepository { get; }
        Task<SignInResult> PasswordSignInAsync(string user, string password, bool isPersistent, bool lockoutOnFailure);
        Task CurrentUserSignOutAsync();
        Task<IdentityResult> AddUserAsync(ClientUserModel user, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser userToUpdate, ApplicationUser originalUser);
        Task<IdentityResult> UpdateUserAsync(ClientUserModel userToUpdate);
        UserChangeRequestResponse ValidateUserMobileChangeRequest(ClientUserModel updatedClientUser, ClientUserModel originalClientUser, int daysPeriod);
        UserChangeRequestResponse ValidateUserEmailChangeRequest(ClientUserModel updatedClientUser, ClientUserModel originalClientUser, int daysPeriod);

        Task<IdentityResult> ApproveUserAsync(ApplicationUser user);
        Task<IdentityResult> AuthoriseSignatoryAsync(ApplicationUser user);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        IQueryable<ApplicationUser> GetUnapprovedUsers();
        IQueryable<ApplicationUser> GetUsersByCompanyId(int clientCompanyId);
        ApplicationUser GetUserByClientCompanyContactId(int clientCompanyContactId);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByNameAsync(string userId);
        AuthUser GetAuthUserByAuthUserId(int authUserId);
        Task<bool> IsUserByNameAsync(string userId);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string newPasswordHash);
        Task PersistToken(Token rt);
        Task ReplaceToken(Token newRefreshToken, Token oldRefreshToken);
        Task RemoveToken(Token newRefreshToken);
        Token GetRefreshToken(int userID, string refreshToken);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<bool> VerifyToken(ApplicationUser user, string tokenProvider, string tokenPurpose, string tokenCode);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password);
        IQueryable<PreviousPassword> GetLastPasswords(long userId);
        Task<string> HashPasswordAsync(string password);
        Task SetRoleForUser(long userId, long roleId);
        IQueryable<ApplicationRole> GetRole(string role);
        IQueryable<ApplicationUser> GetApplicationUserByAuthUserId(int authUserId);
        IQueryable<IGrouping<int, ActivityLog>> GetActivityLog(IList<int> clientCompanyIDs, string activityType);
        IQueryable<ActivityLog> GetUserActivityLog(int authUserId);

        Task LogActivity(ActivityLog log);
        AppUser GetAppUserById(int authUserId);

        IdentityResult ValidateUserDetails(UserValidationModel user);

        ClientUserModel GetClientUserModelByContactId(int clientCompanyContactId);
        UserChangeRequest GetUserChangeRequest(int authUserID, string changeStatus, string changeValueType);
        UserChangeRequest GetUserChangeRequest(int userChangeRequestId);
        IEnumerable<PendingApprovalUserChangeRequest> GetPendingChangeRequest();
        Task<ApproveUserChangeResponse> ApproveUserChangeRequest(ApproveUserChangeRequest approveUserChangeRequest);
        Task<IdentityResult> ProcessUserChangeRequest(UserChangeRequest changeRequest);
        IEnumerable<AppUser> GetAllDirectorsAsList();
        string GetSendersEmailAddress(int authUserId);
        string GetSendersPhoneNumber(int authUserId);
        string GenerateUniqueUsername(string initialValue = "");
    }
}