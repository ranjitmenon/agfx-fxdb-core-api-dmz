using Argentex.Core.Service.Enums;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Sms.Models;

namespace Argentex.Core.Service.Sms.SmsSender
{

    public class SmsSender : ISmsSender
    {
        #region Constructor & Dependencies

        private readonly ITextMagicService _textMagicService;

        public SmsSender(ITextMagicService textMagicService)
        {
            _textMagicService = textMagicService;
        }

        #endregion

        #region Properties

        private bool _disposed;

        #endregion

        /// <summary>
        /// Sending Sms Message with the selected service provider
        /// </summary>
        /// <param name="smsModel">Phone Number and Message</param>
        /// <param name="provider">Service provider that sends the sms
        /// currently only TextMagic is setup</param>
        /// <returns>If the message was sent successfuly</returns>
        public bool SendMessage(SmsModel smsModel, SmsProviders provider = 0)
        {
            switch (provider)
            {
                case SmsProviders.TextMagic:
                   return _textMagicService.SendMessage(smsModel);
                default:
                    return false;
            }
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _textMagicService?.Dispose();
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
