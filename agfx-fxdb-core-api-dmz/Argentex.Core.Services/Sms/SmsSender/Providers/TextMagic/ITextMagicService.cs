using Argentex.Core.Service.Sms.Models;
using System;

namespace Argentex.Core.Service.Sms.SmsSender
{
    public interface ITextMagicService : IDisposable
    {
        bool SendMessage(SmsModel smsModel);
    }
}
