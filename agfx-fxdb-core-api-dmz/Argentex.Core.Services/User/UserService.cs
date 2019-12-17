using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Identity.Services;
using Argentex.Core.Service.Models.ClientCompany;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.Service.Sms.Models;
using Argentex.Core.Service.Sms.SmsSender;
using Argentex.Core.UnitsOfWork.Users;
using Argentex.Core.UnitsOfWork.Users.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Argentex.Core.Service.Enums;
using Argentex.Core.Service.Extensions;
using ApproveUserChangeRequest = Argentex.Core.UnitsOfWork.Users.Model.ApproveUserChangeRequest;

namespace Argentex.Core.Service.User
{
    public class UserService : IUserService
    {
        private readonly IConfigWrapper _config;
        private readonly IUserUow _userUow;
        private readonly IEmailSender _emailSender;
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;
        private readonly IAppSettingService _appSettingService;
        private readonly ISmsSender _smsSender;

        private bool _disposed;

        public UserService(IConfigWrapper config,
            IUserUow userUow,
            IEmailSender emailSender,
            IIdentityService identityService,
            IEmailService emailService,
            IAppSettingService appSettingService,
            ISmsSender smsSender)
        {
            _config = config;
            _userUow = userUow;
            _emailSender = emailSender;
            _identityService = identityService;
            _emailService = emailService;
            _appSettingService = appSettingService;
            _smsSender = smsSender;
        }

        public IEnumerable<ApplicationServiceUser> GetUnapprovedApplicationUsers()
        {
            var applicationServiceUserList = _userUow.GetUnapprovedUsers()
                .Select(x => new ApplicationServiceUser
                {
                    Id = x.Id,
                    Title = x.Title,
                    Forename = x.Forename,
                    Surname = x.Surname,
                    Username = x.UserName,
                    Email = x.Email,
                    ClientCompanyId = x.ClientCompanyId,
                    UpdatedByAuthUserId = x.UpdatedByAuthUserId,
                    Position = x.Position,
                    PhoneNumberDirect = x.PhoneNumber,
                    PhoneNumberMobile = x.PhoneNumberMobile,
                    Birthday = x.Birthday.ToString(),
                    IsApproved = x.IsApproved,
                    ApprovedByAuthUserId = x.ApprovedByAuthUserId,
                    IsAdmin = x.IsAdmin,
                    IsSignatory = x.IsSignatory,
                    IsAuthorisedSignatory = x.IsAuthorisedSignatory
                });

            return applicationServiceUserList;
        }



        public IEnumerable<ApplicationServiceUser> GetApplicationUsersOfCompany(int clientCompanyId)
        {
            var applicationServiceUserList = _userUow.GetUsersByCompanyId(clientCompanyId)
                .Select(x => new ApplicationServiceUser
                {
                    Id = x.Id,
                    Title = x.Title,
                    Forename = x.Forename,
                    Surname = x.Surname,
                    Username = x.UserName,
                    Email = x.Email,
                    ClientCompanyId = x.ClientCompanyId,
                    UpdatedByAuthUserId = x.UpdatedByAuthUserId,
                    Position = x.Position,
                    PhoneNumberDirect = x.PhoneNumber,
                    PhoneNumberMobile = x.PhoneNumberMobile,
                    Birthday = x.Birthday.ToString(),
                    IsApproved = x.IsApproved,
                    IsAdmin = x.IsAdmin,
                    IsSignatory = x.IsSignatory,
                    IsAuthorisedSignatory = x.IsAuthorisedSignatory
                });

            return applicationServiceUserList;
        }

        public async Task<ApplicationServiceUser> GetApplicationUserAsync(string userId)
        {
            var applicationUser = await _userUow.GetUserByIdAsync(userId);
            if (applicationUser == null) return null;

            var applicationServiceUser = MapAppServiceUser(new ApplicationServiceUser(), applicationUser);

            return applicationServiceUser;
        }

        public async Task<IdentityResult> AddUnapprovedUserAsync(ApplicationServiceUser serviceUser)
        {
            var clientUser = MapClientUser(serviceUser);

            var validationResult = _userUow.ValidateUserDetails(new UserValidationModel
            {
                Email = clientUser.Email,
                Username = clientUser.Username,
                ClientCompanyId = clientUser.ClientCompanyId,
                ClientCompanyContactId = clientUser.ClientCompanyContactId,
                ValidateUserDetails = serviceUser.ValidateUserDetails
            });

            if (!validationResult.Succeeded)
                return validationResult;

            clientUser.EmailConfirmed = false;
            clientUser.LastPasswordChangeDate = clientUser.UpdatedDateTime;

            var result = await _userUow.AddUserAsync(clientUser, _config.Get("GeneratedPassword"));

            return result;
        }

        public async Task<IdentityResult> SendUserNewPasswordEmailAsync(ApplicationServiceUser serviceUser, string clientCompanyName)
        {
            var user = MapAppUser(new ApplicationUser(), serviceUser);
            user.EmailConfirmed = false;
            user.LastPasswordChange = DateTime.Now;

            var result = await _emailService.SendUserNewPasswordEmailAsync(user.UserName, clientCompanyName);
            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationServiceUser serviceUser)
        {
            ApplicationUser originalUser;

            if (serviceUser.FindUserByUsername.HasValue && serviceUser.FindUserByUsername.Value)
            {
                originalUser = await _userUow.GetUserByNameAsync(serviceUser.Username);
            }
            else
            {
                if (serviceUser.FindUserByEmail.HasValue && serviceUser.FindUserByEmail.Value)
                {
                    originalUser = await _userUow.GetUserByEmailAsync(serviceUser.Email);
                }
                else
                {
                    originalUser = await _userUow.GetUserByIdAsync(serviceUser.Id.ToString());
                }
            }

            if (originalUser == null) return IdentityResult.Failed(new IdentityError { Description = "Model is missing a valid Id" });

            ApplicationUser userToUpdate = MapAppUserForUpdate(originalUser, serviceUser);

            var validationResult = _userUow.ValidateUserDetails(new UserValidationModel
            {
                Email = userToUpdate.Email,
                Username = userToUpdate.UserName,
                ClientCompanyId = userToUpdate.ClientCompanyId,
                ClientCompanyContactId = userToUpdate.ClientCompanyContactId,
                ValidateUserDetails = serviceUser.ValidateUserDetails
            });

            if (!validationResult.Succeeded)
                return validationResult;

            var result = await _userUow.UpdateUserAsync(userToUpdate, originalUser);

            return result;
        }

        public async Task<IdentityResult> UpdateUserContactAsync(ApplicationServiceUser serviceUser)
        {
            var updatedClientUser = MapClientUser(serviceUser);
            var originalClientUser = _userUow.GetClientUserModelByContactId(updatedClientUser.ClientCompanyContactId);

            if (originalClientUser == null)
                return IdentityResult.Failed(new IdentityError { Description = $"User is not tied to a valid ClientCompanyContactId {updatedClientUser.ClientCompanyContactId}. Database out of sync.", Code = "ContactNotFound" });

            var updatedByAuthUser = _userUow.GetAuthUserByAuthUserId(serviceUser.UpdatedByAuthUserId);
            if (updatedByAuthUser == null)
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"AuthUserId attempting update is not tied to a valid AuthUser {serviceUser.UpdatedByAuthUserId}. Aborting update.",
                    Code = "InvalidAuthUser"
                });

            var validationResult = _userUow.ValidateUserDetails(new UserValidationModel
            {
                Email = updatedClientUser.Email,
                Username = updatedClientUser.Username,
                ClientCompanyContactId = updatedClientUser.ClientCompanyContactId,
                ClientCompanyId = updatedClientUser.ClientCompanyId,
                ValidateUserDetails = serviceUser.ValidateUserDetails
            });

            if (!validationResult.Succeeded)
                return validationResult;

            string description = string.Empty;
            var daysPeriod = _appSettingService.GetUserChangeDaysRequiredForApproval();

            //Validate if mobile phone and email is being updated
            var mobileChangeResponse = _userUow.ValidateUserMobileChangeRequest(updatedClientUser, originalClientUser, daysPeriod);
            var emailChangeResponse = _userUow.ValidateUserEmailChangeRequest(updatedClientUser, originalClientUser, daysPeriod);

            if (mobileChangeResponse.InsertOrUpdateUserChangeRequest)
            {
                UserChangeRequest newMobileChangeRequest = new UserChangeRequest()
                {
                    AuthUserId = updatedClientUser.AuthUserId,
                    CurrentValue = originalClientUser.PhoneNumberMobile,
                    ProposedValue = updatedClientUser.PhoneNumberMobile,
                    ChangeValueType = "Telephone",
                    ChangeDateTime = updatedClientUser.UpdatedDateTime,
                    ChangedByAuthUserId = updatedClientUser.UpdatedByAuthUserId,
                    ChangeStatus = "Pending"
                };

                await _userUow.ProcessUserChangeRequest(newMobileChangeRequest);

                //retain previous values until approval
                updatedClientUser.PhoneNumberMobile = originalClientUser.PhoneNumberMobile;
                updatedClientUser.LastPhoneNumberMobileChangeDate = originalClientUser.LastPhoneNumberMobileChangeDate;
            }
            else
            {
                if (updatedClientUser.PhoneNumberMobile != originalClientUser.PhoneNumberMobile)
                    updatedClientUser.LastPhoneNumberMobileChangeDate = updatedClientUser.UpdatedDateTime;
            }

            if (!string.IsNullOrEmpty(mobileChangeResponse.WarningMessage))
                description += mobileChangeResponse.WarningMessage;

            if (emailChangeResponse.InsertOrUpdateUserChangeRequest)
            {
                UserChangeRequest newEmailChangeRequest = new UserChangeRequest()
                {
                    AuthUserId = updatedClientUser.AuthUserId,
                    CurrentValue = originalClientUser.Email,
                    ProposedValue = updatedClientUser.Email,
                    ChangeValueType = "Email",
                    ChangeDateTime = updatedClientUser.UpdatedDateTime,
                    ChangedByAuthUserId = updatedClientUser.UpdatedByAuthUserId,
                    ChangeStatus = "Pending"
                };

                await _userUow.ProcessUserChangeRequest(newEmailChangeRequest);

                //retain previous values until approval
                updatedClientUser.Email = originalClientUser.Email;
                updatedClientUser.LastEmailChangeDate = originalClientUser.LastEmailChangeDate;
            }
            else
            {
                if (updatedClientUser.Email != originalClientUser.Email)
                    updatedClientUser.LastEmailChangeDate = updatedClientUser.UpdatedDateTime;
            }

            if (!string.IsNullOrEmpty(emailChangeResponse.WarningMessage))
            {
                if (!string.IsNullOrEmpty(description))
                {
                    description += ",";
                }
                description += emailChangeResponse.WarningMessage;
            }

            IdentityResult result = await _userUow.UpdateUserAsync(updatedClientUser);

            if (mobileChangeResponse.SendUserChangeAlerts || emailChangeResponse.SendUserChangeAlerts)
            {
                await _emailService.SendEmailToDirectorsForApproval();
            }

            return result;
        }

        public async Task<IdentityResult> UpdateMyAccountAsync(ApplicationServiceUser serviceUser)
        {
            ApplicationUser originalUser = await _userUow.GetUserByIdAsync(serviceUser.Id.ToString());
            if (originalUser == null) return IdentityResult.Failed(new IdentityError { Description = $"Model is missing a valid Id: {serviceUser.Id}" });

            ApplicationUser userToUpdate = MapAppUserFromMyAccount(originalUser, serviceUser);

            var result = await _userUow.UpdateUserAsync(userToUpdate, originalUser);

            return result;
        }

        public async Task<IList<IdentityResult>> ApproveUsersAsync(ApproveUsersRequest approveUserRequests, ICollection<ClientCompaniesModel> clientCompanies)
        {
            List<IdentityResult> resultList = new List<IdentityResult>();

            foreach (var userIdToApprove in approveUserRequests.UserIdsToApprove)
            {
                IdentityResult result;
                ApplicationUser userToApprove = await _userUow.GetUserByIdAsync(userIdToApprove.ToString());

                if (userToApprove != null)
                {
                    userToApprove.IsApproved = true;
                    userToApprove.ApprovedByAuthUserId = approveUserRequests.ApproverAuthUserId;

                    result = await _userUow.ApproveUserAsync(userToApprove);
                    if (result.Succeeded)
                    {
                        await _emailService.SendUserNewPasswordEmailAsync(userToApprove.UserName, GetClientCompanyName(userToApprove, clientCompanies));
                    }

                    resultList.Add(result);
                }
                else
                {
                    resultList.Add(IdentityResult.Failed(new IdentityError { Description = $"Invalid Id :{userIdToApprove}" }));
                }
            }

            return resultList;
        }

        public async Task<IList<IdentityResult>> AuthoriseSignatoryAsync(AuthoriseSignatoryRequest authoriseSignatoryRequests, ICollection<ClientCompaniesModel> clientCompanies)
        {
            List<IdentityResult> resultList = new List<IdentityResult>();

            foreach (var userIdToAuthorise in authoriseSignatoryRequests.UserIdsToAuthorise)
            {
                IdentityResult result;
                ApplicationUser userToAuthorise = await _userUow.GetUserByIdAsync(userIdToAuthorise.ToString());

                if (userToAuthorise != null)
                {
                    userToAuthorise.IsAuthorisedSignatory = true;
                    userToAuthorise.ApprovedByAuthUserId = authoriseSignatoryRequests.ApproverAuthUserId;

                    result = await _userUow.AuthoriseSignatoryAsync(userToAuthorise);
                    if (result.Succeeded)
                    {
                        await _emailService.SendUserNewPasswordEmailAsync(userToAuthorise.UserName, GetClientCompanyName(userToAuthorise, clientCompanies));
                    }

                    resultList.Add(result);
                }
                else
                {
                    resultList.Add(IdentityResult.Failed(new IdentityError { Description = $"Invalid Id :{userIdToAuthorise}" }));
                }
            }

            return resultList;
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            ApplicationUser userToDelete = await _userUow.GetUserByIdAsync(userId);
            if (userToDelete == null) return IdentityResult.Failed(new IdentityError { Description = $"Invalid userId: {userId}" });

            var result = await _userUow.DeleteUserAsync(userToDelete);

            return result;
        }

        public async Task<IdentityResult> DeleteUserContactAsync(int clientCompanyContactId)
        {
            var userToDelete = _userUow.GetUserByClientCompanyContactId(clientCompanyContactId);

            if (userToDelete == null)
                return IdentityResult.Failed(new IdentityError { Description = $"User is not tied to a valid ClientCompanyContactId: {clientCompanyContactId}. Database out of sync.", Code = "ContactNotFound" });

            var result = await _userUow.DeleteUserAsync(userToDelete);

            return result;
        }

        public ApplicationServiceUser GetApplicationUserByAuthUserId(int authUserId)
        {
            return _userUow.GetApplicationUserByAuthUserId(authUserId)
                    .Select(x => new ApplicationServiceUser
                    {
                        AuthUserId = x.AuthUserId,
                        Email = x.Email,
                        Forename = x.Forename,
                        Surname = x.Surname,
                        ClientCompanyContactId = x.ClientCompanyContactId
                    }).SingleOrDefault();
        }

        public AuthUser GetAuthUserById(int authUserId)
        {
            return _userUow.GetAuthUserByAuthUserId(authUserId);
        }

        public IList<UserModel> GetUserLoginDetails(IList<int> clientCompanyIDs)
        {
            IList<ActivityLogModel> list = GetActivityLogModel(clientCompanyIDs, "login");

            IList<UserModel> userModelList =
                list.Select(x => new UserModel
                {
                    AuthUserId = x.AuthUserId ?? 0,
                    UserId = x.Id ?? 0,
                    ClientCompanyId = x.ApplicationUser.ClientCompanyId,
                    LastLoginDate = x.LogDate,
                    IsSuccesfullLogin = x.IsSuccess,
                    IsOnline = x.IsOnline
                }).ToList();

            return userModelList;
        }

        public IList<UserModel> GetUserLoginDetails(int authUserId)
        {
            var query = _userUow.GetUserActivityLog(authUserId);

            IList<UserModel> userModelList =
                query.Select(x => new UserModel
                {
                    AuthUserId = x.AuthUserId ?? 0,
                    UserId = x.Id ?? 0,
                    ClientCompanyId = x.ApplicationUser.ClientCompanyId,
                    LastLoginDate = x.LogDate,
                    IsSuccesfullLogin = x.IsSuccess
                }).ToList();

            return userModelList;
        }

        public AppUser GetAppUserById(int appUserId)
        {
            return _userUow.GetAppUserById(appUserId);
        }

        public AppUser GetFXDBAppUserById(int appUserId)
        {
            return _userUow.AppUserRepository.GetByPrimaryKey(appUserId);
        }

        public IList<ClientCompanyContactModel> GetAuthorisedSignatories(int clientCompanyId)
        {
            return _userUow
                .ApplicationUserRepo
                .GetQueryable(x => x.ClientCompanyId == clientCompanyId
                                    && !x.IsDeleted
                                    && x.IsAuthorisedSignatory)
                .Select(x => new ClientCompanyContactModel
                {
                    ContactTitle = x.Title,
                    ContactForename = x.Forename,
                    ContactSurname = x.Surname,
                    ContactEmail = x.Email,
                    ContactTelephone = x.PhoneNumber
                })
                .ToList();
        }

        public async Task<ApproveUserChangeResponse> ApproveUserChangeRequest(ApproveUserChangeRequest approveUserChangeRequest)
        {
            var approveUserChangeResponse = await _userUow.ApproveUserChangeRequest(approveUserChangeRequest);

            if (approveUserChangeResponse.Result.Succeeded)
            {
                if (approveUserChangeResponse.UserChangeRequest != null && approveUserChangeResponse.SendNotification)
                {
                    if (approveUserChangeResponse.UserChangeRequest.ChangeValueType == "Email")
                    {
                        string userPhoneNumber = _config.Get("Sms:DefaultPhoneNumber");

                        if (string.IsNullOrEmpty(userPhoneNumber))
                        {
                            userPhoneNumber = _userUow.GetSendersPhoneNumber(approveUserChangeResponse.UserChangeRequest.AuthUserId);
                        }

                        if (!string.IsNullOrEmpty(userPhoneNumber))
                        {
                            // creating the model
                            var smsModel = new SmsModel()
                            {
                                PhoneNumber = userPhoneNumber,
                                Message = string.Format("Security message: This confirms your email address has been updated to {0}. Please contact Argentex if you didn't request this change.",
                                                            approveUserChangeResponse.UserChangeRequest.ProposedValue)
                            };

                            // send the message to client
                            _smsSender.SendMessage(smsModel);
                        }
                    }
                    else if (approveUserChangeResponse.UserChangeRequest.ChangeValueType == "Telephone")
                    {
                        string userEmail = _config.Get("Emails:DefaultEmail");

                        if (string.IsNullOrEmpty(userEmail))
                        {
                            userEmail = _userUow.GetSendersEmailAddress(approveUserChangeResponse.UserChangeRequest.AuthUserId);
                        }
                        await _emailService.SendMobileChangeEmailAsync(approveUserChangeResponse.UserChangeRequest.ProposedValue, userEmail);
                    }
                }
            }

            return approveUserChangeResponse;
        }


        public UserChangeRequest GetUserChangeRequest(int userChangeRequestId)
        {
            return _userUow.GetUserChangeRequest(userChangeRequestId);
        }

        public IList<PendingApprovalUserChangeRequest> GetPendingChangeRequest()
        {
            return _userUow.GetPendingChangeRequest().ToList();
        }

        public RequestOrigin GetRequestOrigin(IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;

            return claimsIdentity.GetRequestOrigin();
        }

        public string GenerateUniqueUsername(string initialValue)
            => _userUow.GenerateUniqueUsername(initialValue);

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
            appUser.Notes = serviceUser.Notes;
            appUser.IsApproved = serviceUser.IsApproved;
            appUser.IsSignatory = serviceUser.IsSignatory;
            appUser.IsAuthorisedSignatory = serviceUser.IsAuthorisedSignatory;
            appUser.PrimaryContact = serviceUser.PrimaryContact;
            appUser.IsAdmin = serviceUser.IsAdmin;
            appUser.ClientCompanyContactId = serviceUser.ClientCompanyContactId;
            appUser.LockoutEnabled = serviceUser.IsLockedOut ?? false;

            return appUser;
        }

        private static ApplicationUser MapAppUserForUpdate(ApplicationUser appUser, ApplicationServiceUser serviceUser)
        {
            //the id should not be set as it is the key and the update will fail with an error
            //The property 'Id' on entity type 'ApplicationUser' is part of a key and so cannot be modified or marked as modified.
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
            appUser.IsSignatory = serviceUser.IsSignatory;
            appUser.IsAuthorisedSignatory = serviceUser.IsAuthorisedSignatory;
            appUser.PrimaryContact = serviceUser.PrimaryContact;
            appUser.IsAdmin = serviceUser.IsAdmin;
            appUser.ClientCompanyContactId = serviceUser.ClientCompanyContactId;
            appUser.LockoutEnabled = serviceUser.IsLockedOut ?? false;

            return appUser;
        }

        public ClientUserModel MapClientUser(ApplicationServiceUser serviceUser)
        {
            return new ClientUserModel()
            {
                Title = serviceUser.Title,
                Forename = serviceUser.Forename,
                Surname = serviceUser.Surname,
                Username = serviceUser.Username,
                Email = serviceUser.Email,
                ClientCompanyId = serviceUser.ClientCompanyId,
                ClientCompanyContactId = serviceUser.ClientCompanyContactId,
                AuthUserId = serviceUser.AuthUserId,
                UpdatedByAuthUserId = serviceUser.UpdatedByAuthUserId,
                Position = serviceUser.Position,
                PhoneNumberDirect = serviceUser.PhoneNumberDirect,
                PhoneNumberMobile = serviceUser.PhoneNumberMobile,
                PhoneNumberOther = serviceUser.PhoneNumberOther,
                Birthday = DateTime.TryParse(serviceUser.Birthday, out var date) ? (DateTime?)date : null,
                IsApproved = serviceUser.IsApproved,
                Notes = serviceUser.Notes,
                PrimaryContact = serviceUser.PrimaryContact,
                ASPNumber = serviceUser.ASPNumber,
                ASPCreationDate = serviceUser.ASPCreationDate,
                Authorized = serviceUser.Authorized,
                RecNotification = serviceUser.RecNotification,
                RecAmReport = serviceUser.RecAmReport,
                RecActivityReport = serviceUser.RecActivityReport,
                NiNumber = serviceUser.NiNumber,
                BloombergGpi = serviceUser.BloombergGpi,
                IsDeleted = serviceUser.IsDeleted,
                AssignedCategoryIds = serviceUser.AssignedCategoryIds,
                IsLockedOut = serviceUser.IsLockedOut,
                Comment = serviceUser.Comment,
                IsAdmin = serviceUser.IsAdmin,
                IsSignatory = serviceUser.IsSignatory,
                IsAuthorisedSignatory = serviceUser.IsAuthorisedSignatory,
                ApprovedByAuthUserId = serviceUser.ApprovedByAuthUserId,
                UpdatedDateTime = serviceUser.UpdatedDateTime
            };
        }

        private static ApplicationServiceUser MapAppServiceUser(ApplicationServiceUser serviceUser, ApplicationUser appUser)
        {
            serviceUser.Id = appUser.Id;
            serviceUser.Title = appUser.Title;
            serviceUser.Forename = appUser.Forename;
            serviceUser.Surname = appUser.Surname;
            serviceUser.Username = appUser.UserName;
            serviceUser.Email = appUser.Email;
            serviceUser.ClientCompanyId = appUser.ClientCompanyId;
            serviceUser.ClientCompanyContactId = appUser.ClientCompanyContactId;
            serviceUser.UpdatedByAuthUserId = appUser.UpdatedByAuthUserId;
            serviceUser.Position = appUser.Position;
            serviceUser.PhoneNumberDirect = appUser.PhoneNumber;
            serviceUser.PhoneNumberMobile = appUser.PhoneNumberMobile;
            serviceUser.PhoneNumberOther = appUser.PhoneNumberOther;
            serviceUser.Birthday = appUser.Birthday.HasValue ? appUser.Birthday.Value.ToString("dd/MM/yyyy") : "";
            serviceUser.IsApproved = appUser.IsApproved;
            serviceUser.PrimaryContact = appUser.PrimaryContact ?? false;
            serviceUser.IsAdmin = appUser.IsAdmin;
            serviceUser.IsSignatory = appUser.IsSignatory;
            serviceUser.IsAuthorisedSignatory = appUser.IsAuthorisedSignatory;

            return serviceUser;
        }

        private static ApplicationUser MapAppUserFromMyAccount(ApplicationUser appUser, ApplicationServiceUser serviceUser)
        {
            appUser.Id = serviceUser.Id;
            appUser.Forename = serviceUser.Forename;
            appUser.Surname = serviceUser.Surname;
            appUser.UserName = serviceUser.Username;
            appUser.Email = serviceUser.Email;
            appUser.ClientCompanyId = serviceUser.ClientCompanyId;
            appUser.UpdatedByAuthUserId = serviceUser.UpdatedByAuthUserId;

            return appUser;
        }

        private IList<ActivityLogModel> GetActivityLogModel(IList<int> clientCompanyIDs, string activityType)
        {
            List<ActivityLogModel> activityLogModelList = new List<ActivityLogModel>();

            //get the login and logout groups for the companies            
            List<IGrouping<int, ActivityLog>> loginGroupsCompanyList = _userUow.GetActivityLog(clientCompanyIDs, "login").ToList();
            List<IGrouping<int, ActivityLog>> logoutGroupsCompanyList = _userUow.GetActivityLog(clientCompanyIDs, "logout").ToList();

            foreach (var loginGroupCompany in loginGroupsCompanyList)
            {
                //get the corresponding logout group per company                        
                var logoutGroupCompany = logoutGroupsCompanyList.FirstOrDefault(x => x.Key == loginGroupCompany.Key);

                if (logoutGroupCompany == null)
                {
                    //no corresponding logout group, then this login group is the one we need
                    activityLogModelList.Add(CreateActivityLogModel(loginGroupCompany.ToList()[0], true));
                    //move to the next company login group 
                    continue;
                }

                List<ActivityLog> loginActivityLog = loginGroupCompany.ToList();
                List<ActivityLog> logoutActivityLog = logoutGroupCompany.ToList();

                List<ActivityLogModel> tempList = null;
                foreach (var login in loginActivityLog)
                {
                    tempList = new List<ActivityLogModel>();
                    var logout = logoutActivityLog.FirstOrDefault(x =>
                        x.AuthUserId == login.AuthUserId &&
                        x.Id == login.Id &&
                        login.LogDate < x.LogDate);

                    if (logout == null)
                    {
                        //found the login entity without a corresponding logout
                        tempList.Add(CreateActivityLogModel(login, true));
                        break;
                    }
                    else
                    {
                        //remove the logout entity from the list
                        logoutActivityLog.Remove(logout);
                    }
                }

                if (tempList == null || tempList.Count == 0)
                {
                    //add the login entity even if it's not active
                    //the user has logged in, then logged out
                    if (loginActivityLog.Count > 0)
                    {
                        //add the activity log, set to offline
                        activityLogModelList.Add(CreateActivityLogModel(loginActivityLog[0], false));
                    }
                }
                else
                {
                    activityLogModelList.AddRange(tempList);
                }
            }

            return activityLogModelList;
        }


        private string GetClientCompanyName(ApplicationUser user, ICollection<ClientCompaniesModel> clientCompanies)
        {
            return clientCompanies.Where(x => x.ClientCompanyId == user.ClientCompanyId).Select(x => x.ClientCompanyName).FirstOrDefault();
        }

        private ActivityLogModel CreateActivityLogModel(ActivityLog activityLog, bool isOnline)
        {
            if (activityLog == null) return null;

            return new ActivityLogModel()
            {
                Id = activityLog.Id,
                ActivityLogId = activityLog.ActivityLogId,
                UserName = activityLog.UserName,
                LogDate = activityLog.LogDate,
                IsSuccess = activityLog.IsSuccess,
                PrimaryIP = activityLog.PrimaryIP,
                SecondaryIP = activityLog.SecondaryIP,
                ActivityId = activityLog.ActivityId,
                Activity = activityLog.Activity,
                AuthUserId = activityLog.AuthUserId,
                ApplicationUser = activityLog.ApplicationUser,
                IsOnline = isOnline
            };
        }
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
    }
}