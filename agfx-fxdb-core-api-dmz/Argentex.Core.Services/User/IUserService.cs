using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Models.ClientCompany;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.UnitsOfWork.Users.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Argentex.Core.Service.Enums;

namespace Argentex.Core.Service.User
{
    public interface IUserService : IDisposable
    {
        IEnumerable<ApplicationServiceUser> GetApplicationUsersOfCompany(int clientCompanyId);
        IEnumerable<ApplicationServiceUser> GetUnapprovedApplicationUsers();
        Task<ApplicationServiceUser> GetApplicationUserAsync(string userId);
        Task<IdentityResult> AddUnapprovedUserAsync(ApplicationServiceUser serviceUser);
        Task<IdentityResult> SendUserNewPasswordEmailAsync(ApplicationServiceUser serviceUser, string clientCompanyName);
        Task<IdentityResult> UpdateUserAsync(ApplicationServiceUser user);
        Task<IdentityResult> UpdateUserContactAsync(ApplicationServiceUser user);
        Task<IdentityResult> UpdateMyAccountAsync(ApplicationServiceUser user);
        Task<IList<IdentityResult>> ApproveUsersAsync(ApproveUsersRequest approveUserRequests, ICollection<ClientCompaniesModel> clientCompanies);
        Task<IList<IdentityResult>> AuthoriseSignatoryAsync(AuthoriseSignatoryRequest authoriseSignatoryRequests, ICollection<ClientCompaniesModel> clientCompanies);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IdentityResult> DeleteUserContactAsync(int clientCompanyContactId);
        ApplicationServiceUser GetApplicationUserByAuthUserId(int authUserId);
        AuthUser GetAuthUserById(int authUserId);
        AppUser GetFXDBAppUserById(int appUserId);
        IList<UserModel> GetUserLoginDetails(IList<int> clientCompanyIDs);
        IList<UserModel> GetUserLoginDetails(int authUserId);
        AppUser GetAppUserById(int authUserId);
        IList<ClientCompanyContactModel> GetAuthorisedSignatories(int clientCompanyId);
        IList<PendingApprovalUserChangeRequest> GetPendingChangeRequest();
        Task<ApproveUserChangeResponse> ApproveUserChangeRequest(ApproveUserChangeRequest approveUserChangeRequest);
        RequestOrigin GetRequestOrigin(IIdentity userIdentity);
        string GenerateUniqueUsername(string initialValue);
    }
}
