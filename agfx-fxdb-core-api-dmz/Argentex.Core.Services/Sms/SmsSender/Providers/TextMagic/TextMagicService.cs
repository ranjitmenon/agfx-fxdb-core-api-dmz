using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Sms.Models;
using System;
using TextmagicRest;

namespace Argentex.Core.Service.Sms.SmsSender
{
    public class TextMagicService : ITextMagicService
    {
        #region Constructor & Dependencies

        private readonly IConfigWrapper _config;

        public TextMagicService(IConfigWrapper config)
        {
            _config = config;
        }

        #endregion

        #region Properties

        private bool _disposed;
        private TextMagicConfigModel _textMagicConfig
        {
            get
            {
                return new TextMagicConfigModel()
                {
                    UserName = _config.Get("Sms:TextMagic:UserName"),
                    Token = _config.Get("Sms:TextMagic:Token")
                };
            }
        }

        #endregion

        /// <summary>
        /// Sending Sms Message with TextMagic service provider
        /// </summary>
        /// <param name="smsModel">Phone Number and Message</param>
        /// <returns>If the message was sent successfuly</returns>
        public bool SendMessage(SmsModel smsModel)
        {
            var textMagicConfig = _textMagicConfig;
            var client = new Client(textMagicConfig.UserName, textMagicConfig.Token);
            var link = client.SendMessage(smsModel.Message, smsModel.PhoneNumber);

            return link.Success;
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

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
