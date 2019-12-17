using Argentex.Core.Api.Exceptions;
using Argentex.Core.Api.Models;
using Argentex.Core.Api.Models.SecurityModels;
using Argentex.Core.Service;
using Argentex.Core.Service.Models.ClientCompany;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.Users.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SynetecLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Argentex.Core.Service.Enums;
using Argentex.Core.Service.Extensions;

namespace Argentex.Core.Api.Controllers.User
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogWrapper _logger;
        private readonly IConfiguration _config;
        private readonly IClientCompanyService _clientCompanyService;

        public UserController(IUserService userService, ILogWrapper logger, IConfiguration config, IClientCompanyService clientCompanyService)
        {
            _userService = userService;
            _logger = logger;
            _config = config;
            _clientCompanyService = clientCompanyService;
        }

        [HttpGet]
        [Route("get-all-unapproved-users")]
        public IActionResult GetUnapprovedApplicationUsers()
        {
            var appUserList = _userService.GetUnapprovedApplicationUsers();

            if (!appUserList.Any()) return NoContent();

            return Ok(appUserList);
        }

        [HttpGet]
        [Route("get-users-of-company/{clientCompanyId:int}")]
        public IActionResult GetApplicationUsersOfCompany(int clientCompanyId)
        {
            var appUserList = _userService.GetApplicationUsersOfCompany(clientCompanyId);

            if (!appUserList.Any()) return NoContent();

            return Ok(appUserList);
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<IActionResult> GetApplicationUser(int userId)
        {
            var appUser = await _userService.GetApplicationUserAsync(userId.ToString());

            if (appUser == null) return BadRequest();

            return Ok(appUser);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddUser([FromBody] AddUserModel model)
        {
            if (model == null)
                return BadRequest(ResponseModel.ResponseWithErrors("Model must be supplied in the body of the request"));

            if (!ModelState.IsValid)
                return BadRequest(ResponseModel.ResponseFromInvalidModelState(ModelState));

            ApplicationServiceUser user = MapApplicationServiceUserFrom(model);

            var result = await _userService.AddUnapprovedUserAsync(user);

            if (result.Succeeded)
                return Ok(ResponseModel.ResponseWithInfo($"User {model.Username} created successfully"));

            string message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
            _logger.Error(new IdentityException($"Error creating new user {model.Username}. Message: {message}"));
            return BadRequest(ResponseModel.ResponseFromIdentityModel(result));
        }

        [HttpPost]
        [Route("sendActivationEmail")]
        public async Task<IActionResult> SendActivationEmail([FromBody] AddUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel.ResponseFromInvalidModelState(ModelState));

            ApplicationServiceUser user = MapApplicationServiceUserFrom(model);

            var result = await _userService.SendUserNewPasswordEmailAsync(user, _clientCompanyService.GetClientCompanyName(user.ClientCompanyId));

            if (result.Succeeded)
                return Ok(ResponseModel.ResponseWithInfo(
                    $"New Password Email sent to User: {model.Username} with Email: {model.Email}"));

            string message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
            _logger.Error(new IdentityException($"Error creating new user {model.Username}. Message: {message}"));
            return BadRequest(ResponseModel.ResponseFromIdentityModel(result));
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] AddUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel.ResponseFromInvalidModelState(ModelState));

            ApplicationServiceUser user = MapApplicationServiceUserFrom(model);

            var result = await _userService.UpdateUserAsync(user);

            if (result.Succeeded)
                return Ok(ResponseModel.ResponseWithInfo($"User {model.Username} has been updated successfully"));

            var message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
            _logger.Error(new IdentityException($"Error updating user {model.Username}. Message: {message}"));
            return BadRequest(ResponseModel.ResponseWithErrors(result.Errors.Select(x => x.Description).ToArray()));
        }

        [HttpPut]
        [Route("updateContact")]
        public async Task<IActionResult> UpdateContact([FromBody] AddUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel.ResponseFromInvalidModelState(ModelState));

            ApplicationServiceUser user = MapApplicationServiceUserFrom(model);

            var result = await _userService.UpdateUserContactAsync(user);

            if (result.Succeeded)
            {
                return Ok(ResponseModel.ResponseWithInfo($"User {model.Username} has been updated successfully"));
            }
            if (result.Errors.Any(e => e.Code == "404"))
                return NotFound(ResponseModel.ResponseWithErrors($"User {user.ClientCompanyContactId} not found."));

            var message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
            _logger.Error(new IdentityException($"Error updating user {model.Username}. Message: {message}"));
            return BadRequest(ResponseModel.ResponseWithErrors(result.Errors.Select(x => x.Description).ToArray()));
        }

        [HttpPut]
        [Route("update-my-account")]
        public async Task<IActionResult> UpdateMyAccount([FromBody] UpdateUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationServiceUser user = MapApplicationServiceUserFrom(model);

            var message = string.Empty;
            var result = await _userService.UpdateMyAccountAsync(user);

            if (result.Succeeded)
            {
                message = $"User {model.Username} updated successfully";

                return Ok(new { data = message });
            }

            message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
            _logger.Error(new IdentityException($"Error updating user {model.Username}. Message: {message}"));
            return BadRequest(new { data = result.Errors?.FirstOrDefault()?.Description });
        }

        [HttpPost]
        [Route("approve-users")]
        public async Task<IActionResult> ApproveUsers([FromBody] ApproveUsersRequest approvalsRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results = await _userService.ApproveUsersAsync(approvalsRequest, GetCompaniesList());

            var message = string.Empty;
            bool aFailure = false;

            foreach (var result in results)
            {
                if (!result.Succeeded)
                {
                    aFailure = true;
                    message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
                    _logger.Error(new IdentityException($"Error approving user. Message: {message}"));
                }
            }

            if (aFailure) return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("authorise-signatories")]
        public async Task<IActionResult> AuthoriseSignatories([FromBody] AuthoriseSignatoryRequest authorisationsRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var results = await _userService.AuthoriseSignatoryAsync(authorisationsRequest, GetCompaniesList());

            var message = string.Empty;
            bool aFailure = false;

            foreach (var result in results)
            {
                if (!result.Succeeded)
                {
                    aFailure = true;
                    message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
                    _logger.Error(new IdentityException($"Error authorising signatory. Message: {message}"));
                }
            }

            if (aFailure) return BadRequest();

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{userId:int}")]
        public async Task<IActionResult> Delete(int userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = string.Empty;
            var result = await _userService.DeleteUserAsync(userId.ToString());

            if (result.Succeeded)
            {
                message = $"User with ID {userId.ToString()} , deleted successfully";

                return Ok(new { data = message });
            }

            message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
            _logger.Error(new IdentityException($"Error deleting user {userId.ToString()}. Message: {message}"));
            return BadRequest(new { data = result.Errors?.FirstOrDefault()?.Description });
        }

        [HttpDelete]
        [Route("deleteContact/{clientCompanyContactId:int}")]
        public async Task<IActionResult> DeleteContact(int clientCompanyContactId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel.ResponseFromInvalidModelState(ModelState));

            var result = await _userService.DeleteUserContactAsync(clientCompanyContactId);

            if (result.Succeeded)
                return Ok(ResponseModel.ResponseWithInfo($"User with ID {clientCompanyContactId} , deleted successfully"));

            string message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
            _logger.Error(new IdentityException($"Error deleting user {clientCompanyContactId.ToString()}. Message: {message}"));
            return BadRequest(ResponseModel.ResponseFromIdentityModel(result));
        }

        [HttpPost]
        [Route("login-details")]
        public IActionResult GetUserLoginDetails([FromBody] IList<int> clientCompanyIDs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = string.Empty;
            var results = _userService.GetUserLoginDetails(clientCompanyIDs);

            return Ok(results);
        }

        [HttpGet]
        [Route("authorised-signatories/{clientCompanyId:int}")]
        public IActionResult GetAuthorisedSignatories(int clientCompanyId)
        {
            return Ok(_userService.GetAuthorisedSignatories(clientCompanyId));
        }

        [HttpPost]
        [Route("approve-change-request")]
        public async Task<IActionResult> ApproveUserChangeRequest([FromBody] ApproveUserChangeRequest approveUserChangeRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel.ResponseFromInvalidModelState(ModelState));


            var approveUserChangeResponse = await _userService.ApproveUserChangeRequest(approveUserChangeRequest);
            if (approveUserChangeResponse.Result.Succeeded)
            {
                return Ok(ResponseModel.ResponseWithInfo("True"));
            }
            var message = string.Join(";", approveUserChangeResponse.Result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
            _logger.Error(new IdentityException($"Error approving user change requests. Message: {message}"));
            return BadRequest(ResponseModel.ResponseFromIdentityModel(approveUserChangeResponse.Result));
        }

        [HttpGet]
        [Route("pending-change-request")]
        public IActionResult GetPendingChangeRequests()
        {
            try
            {
                var pendingChangeRequest = _userService.GetPendingChangeRequest();

                if (!pendingChangeRequest.Any()) return NoContent();

                return Ok(pendingChangeRequest);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        private ICollection<ClientCompaniesModel> GetCompaniesList()
        {
            return _clientCompanyService.GetClientCompanies();
        }

        private static ApplicationServiceUser MapApplicationServiceUserFrom(RegisterModel model)
        {
            return new ApplicationServiceUser
            {
                Title = model.Title,
                Forename = model.Forename,
                Surname = model.Surname,
                Username = !string.IsNullOrWhiteSpace(model.Username) ? model.Username : model.Email,
                Email = model.Email,
                Password = model.Password,
                ClientCompanyId = model.ClientCompanyId,
                UpdatedByAuthUserId = model.UpdatedByAuthUserId
            };
        }

        private ApplicationServiceUser MapApplicationServiceUserFrom(UpdateUserModel model)
        {
            return new ApplicationServiceUser
            {
                Id = model.Id,
                Forename = model.Forename,
                Surname = model.Surname,
                Username = model.Username,
                Email = model.Email,
                ClientCompanyId = model.ClientCompanyId,
                UpdatedByAuthUserId = model.UpdatedByAuthUserId,
            };
        }

        private ApplicationServiceUser MapApplicationServiceUserFrom(AddUserModel model)
        {
            return new ApplicationServiceUser
            {
                Id = model.Id,
                Title = model.Title,
                Forename = model.Forename,
                Surname = model.Surname,
                Username = !string.IsNullOrWhiteSpace(model.Username) ? model.Username : GenerateUniqueUsername(model.Email),
                Email = model.Email,
                ClientCompanyId = model.ClientCompanyId,
                ClientCompanyContactId = model.ClientCompanyContactId.GetValueOrDefault(),
                AuthUserId = model.AuthUserId.GetValueOrDefault(),
                UpdatedByAuthUserId = model.UpdatedByAuthUserId,
                UpdatedDateTime = model.UpdatedDateTime ?? DateTime.Now,
                Birthday = model.Birthday,
                Position = model.Position,
                PhoneNumberDirect = model.PhoneNumberDirect,
                PhoneNumberMobile = model.PhoneNumberMobile,
                PhoneNumberOther = model.PhoneNumberOther,
                ASPNumber = model.ASPNumber,
                ASPCreationDate = model.ASPCreationDate,
                PrimaryContact = model.PrimaryContact,
                Notes = model.Notes,
                Authorized = model.Authorized,
                RecNotification = model.RecNotification,
                RecAmReport = model.RecAmReport,
                RecActivityReport = model.RecActivityReport,
                NiNumber = model.NiNumber,
                BloombergGpi = model.BloombergGpi,
                AssignedCategoryIds = model.AssignedCategoryIds,
                IsLockedOut = model.IsLockedOut,
                Comment = model.Comment,
                IsApproved = model.IsApproved,
                ApprovedByAuthUserId = model.ApprovedByAuthUserId,
                IsAdmin = model.IsAdmin,
                IsSignatory = model.IsSignatory,
                IsAuthorisedSignatory = model.IsAuthorisedSignatory,
                AppClientUrl = model.AppClientUrl,
                FindUserByUsername = model.FindUserByUsername,
                FindUserByEmail = model.FindUserByEmail,
                ValidateUserDetails = ValidateUserDetails(model.IsApproved)
            };
        }

        /// <summary>
        /// Determine if the user details should be validated, based on the origin of the request
        /// </summary>
        /// <returns></returns>
        private bool? ValidateUserDetails(bool isApproved)
        {
            RequestOrigin requestOrigin = _userService.GetRequestOrigin(this.User.Identity);

            switch (requestOrigin)
            {
                case RequestOrigin.ArgentexTrader:
                    return isApproved;
                case RequestOrigin.ClientSite:
                    return true;
                case RequestOrigin.Unknown:
                default:
                    throw new AuthenticationException("Request origin is unknown");
            }
        }

        /// <summary>
        /// Get an unique username using an initialValue (e.g. email) and a GUID
        /// Max length restriction due to AuthUser.Username length
        /// </summary>
        /// <param name="initialValue"></param>
        /// <returns></returns>
        private string GenerateUniqueUsername(string initialValue)
            => _userService.GenerateUniqueUsername(initialValue);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userService.Dispose();
                //_logger.Dispose(); //TODO
                base.Dispose(disposing);
            }
        }

    }
}