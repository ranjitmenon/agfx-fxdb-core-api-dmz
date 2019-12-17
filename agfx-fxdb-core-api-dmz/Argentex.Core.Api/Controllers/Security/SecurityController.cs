using Argentex.Core.Api.Exceptions;
using Argentex.Core.Api.Models.AccountViewModels;
using Argentex.Core.Api.Models.SecurityModels;
using Argentex.Core.Service;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Identity.Services;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.Service.Sms.Models;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SynetecLogger;
using System.Linq;
using System.Threading.Tasks;
using Argentex.Core.Api.Models;
using Argentex.Core.Service.Identity;

namespace Argentex.Core.Api.Controllers.Security
{
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly ILogWrapper _logger;
        private readonly IConfiguration _config;
        private readonly IIdentityService _identityService;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public SecurityController(
            IIdentityService identity,
            ILogWrapper logger,
            IConfiguration config,
            IEmailService emailService,
            ISmsService smsService
            )
        {
            _identityService = identity;
            _logger = logger;
            _config = config;
            _emailService = emailService;
            _smsService = smsService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateToken(OpenIdConnectRequest login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "Invalid request data"
                });
            }
            var loginService = new LoginServiceModel
            {
                Username = login.Username,
                Password = login.Password,
                Grant_Type = login.GrantType,
                ClientId = login.ClientId,
                RefreshToken = login.RefreshToken,
                ClientSecret = login.ClientSecret
            };
            var token = await _identityService.AuthenticateAsync(loginService);

            if (token == null)
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "Please check that your credentials are correct"
                });
            }
            else
            // sending 2FA Message
            {
                // sending message to the user
                if (loginService.Grant_Type == OpenIdConnectConstants.GrantTypes.Password)
                {
                    token.Validation_code = await _smsService.Send2FAMessage(login.Username);

                    if (string.IsNullOrEmpty(token.Validation_code))
                    {
                        return BadRequest(new { data = $"Error sending message, please try again." });
                    }
                }
            }

            return Ok(token);
        }


        [AllowAnonymous]
        [HttpPost("token/refresh")]
        [Produces("application/json")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "Invalid request data"
                });
            }

            var token = await _identityService.RefreshToken(model);

            if (token == null)
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "Please check that your credentials are correct"
                });
            }

            return Ok(token);
        }

        [HttpGet]
        [Route("resend-validation-code/{username}")]
        public async Task<IActionResult> ResendValidationCode(string username)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(username))
            {
                return BadRequest(new OpenIdConnectResponse
                {
                    Error = OpenIdConnectConstants.Errors.InvalidGrant,
                    ErrorDescription = "Invalid request data"
                });
            }

            // sending the message
            var validationCode = await _smsService.Send2FAMessage(username);

            if (string.IsNullOrEmpty(validationCode))
            {
                return BadRequest(new { data = $"Error sending message, please try again." });
            }

            return Ok(new { validationCode });
        }

        [HttpPut]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string message;
                var result = await _identityService.ChangePasswordAsync(model.UserId.ToString(), model.CurrentPassword, model.NewPassword, model.ConfirmPassword);

                if (result.Succeeded)
                {
                    message = $"Password of user {model.UserName} was updated successfully";
                    return Ok(new { data = message });
                }

                message = string.Join(";", result.Errors.Select(x => $"Code: {x.Code}. Description: {x.Description}"));
                _logger.Error(new IdentityException($"Error updating password user {model.UserName}. Message: {message}"));

                return BadRequest(new { data = result.Errors.Select(x => x.Description) });
            }
            catch (PasswordsDoNotMatchException e)
            {
                _logger.Error(e);
                return BadRequest(new { data = e.Message });
            }
            catch (ApplicationUserNotFoundException e)
            {
                _logger.Error(e);
                return BadRequest(new { data = e.Message });
            }
            catch (PasswordAlreadyUsedException e)
            {
                _logger.Error(e);
                return BadRequest(new { data = e.Message });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("pass-reset-link")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseModel.ResponseFromInvalidModelState(ModelState));
            }

            var result = await _emailService.SendResetPasswordEmailAsync(model.UserName);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                _logger.Info(errors);
                if (result.Errors.Any(e => e.Code != IdentityResultCodes.UserNotFound && e.Code != IdentityResultCodes.InvalidUserState))
                    return BadRequest(ResponseModel.ResponseFromIdentityModel(result));
            }

            return Ok(new {message = $"If user {model.UserName} exists, an e-mail with a password reset link has been sent."}); //Always return Ok, to prevent username fishing
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input data");
            }

            try
            {
                var result = await _identityService.ResetPasswordAsync(model.UserName, model.Code, model.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors);
                    _logger.Info(errors);
                    if (result.Errors.Any(e => e.Code != IdentityResultCodes.UserNotFound && e.Code != IdentityResultCodes.InvalidUserState))
                        return BadRequest(errors);
                }

                return Ok(); //Always return Ok, to prevent username fishing
            }
            catch (PasswordAlreadyUsedException e)
            {
                _logger.Error(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("set-user-admin/{username}")]
        public async Task<IActionResult> SetUserAsAdmin(string username)
        {
            await _identityService.SetUserAsAdmin(username);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("verify-token")]
        public async Task<IActionResult> VerifyUserToken([FromBody] ResetPasswordViewModel model)
        {
            var isTokenValid = await _identityService.VerifyUserToken(model.UserName, model.Code);

            return Ok(isTokenValid);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("logout-notification/{identityUserName}/{refreshToken}")]
        public async Task<IActionResult> LogoutNotification(string identityUserName, string refreshToken)
        {
            await _identityService.LogoutAsync(identityUserName, refreshToken);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _identityService.Dispose();
                //_logger.Dispose(); //TODO
                base.Dispose(disposing);
            }
        }

    }
}
