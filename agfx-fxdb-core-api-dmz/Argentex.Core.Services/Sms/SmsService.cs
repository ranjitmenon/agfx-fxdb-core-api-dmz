using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Identity.Services;
using Argentex.Core.Service.Sms.Models;
using Argentex.Core.Service.Sms.SmsSender;
using System.Threading.Tasks;

namespace Argentex.Core.Service
{
    public class SmsService : ISmsService
    {
        #region Constructor & Dependencies

        private readonly IConfigWrapper _config;
        private readonly ISmsSender _smsSender;
        private readonly IIdentityService _identityService;

        public SmsService(IConfigWrapper config, ISmsSender smsSender, IIdentityService identityService)
        {
            _config = config;
            _smsSender = smsSender;
            _identityService = identityService;
        }

        #endregion

        #region Properties

        private bool _disposed;

        #endregion

        public async Task<string> Send2FAMessage(string username)
        {
            var validationCode = SmsHelpers.GenerateValidationCodeFor2FA();
            var messageTemplate = _config.Get("Sms:2FAMessageTemplate");

            // if a default phone number is set for dev enviroment we use that
            var userPhoneNumber = _config.Get("Sms:DefaultPhoneNumber");
            if (string.IsNullOrEmpty(userPhoneNumber))
            {
                userPhoneNumber = await _identityService.GetUserPhoneNumber(username);
            }

            if (!string.IsNullOrEmpty(userPhoneNumber))
            {
                // creating the model
                var smsModel = new SmsModel()
                {
                    PhoneNumber = userPhoneNumber,
                    Message = string.Format(messageTemplate, validationCode)
                };

                // send the message to client
                var isMessageSent = _smsSender.SendMessage(smsModel);
                // Encrypt validation code
                var encryptedValidationCode = SmsHelpers.GetHash(validationCode);

                return isMessageSent ? encryptedValidationCode : string.Empty;
            }

            return string.Empty;
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _smsSender?.Dispose();
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
