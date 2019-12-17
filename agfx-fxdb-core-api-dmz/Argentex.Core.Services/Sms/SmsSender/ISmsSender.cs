using Argentex.Core.Service.Enums;
using Argentex.Core.Service.Sms.Models;
using System;

namespace Argentex.Core.Service.Sms.SmsSender
{
    public interface ISmsSender : IDisposable
    {
        bool SendMessage(SmsModel smsModel, SmsProviders provider = 0);
    }
}
