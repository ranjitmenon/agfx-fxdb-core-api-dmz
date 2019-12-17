using Argentex.Core.Service.Email.EmailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.Service
{
    public interface IEmailSender
    {
        Task SendAsync(string appAccounLogin, string appAccountPass, string emailUserName, string recipient, string subject, string body, bool IsHtml, string sender, string fromName, int priority = 0, string bccEmail = null, string ccEmail = null);
        string CreateBody(EmailType type);
    }
}
