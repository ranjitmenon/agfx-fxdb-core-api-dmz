using Argentex.Core.Service.Email.EmailSender;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Helpers;
using EQService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Argentex.Core.Service
{
    public class EmailSender : IEmailSender
    {
        
        private List<EmailAttachment> _emailAttachments;
        private IServiceEmail _eqsService;
        private IConfigWrapper _config;

        public EmailSender(IServiceEmail emailService, IConfigWrapper config)
        {
            _eqsService = emailService;
            _config = config;
        }

        public async Task SendAsync(
            string appAccounLogin, string appAccountPass, string emailUserName,
            string recipient, string subject, string body, bool IsHtml,
            string sender, string fromName, int priority = 0, string bccEmail = null, string ccEmail = null)
        {
            List<EmailAttachment> emailAttachments = new List<EmailAttachment>();
            EmailAttachment[] arrayOfAttachments = emailAttachments.ToArray();

            string defaultEmail = _config.Get("Emails:DefaultEmail");

            if (!string.IsNullOrWhiteSpace(defaultEmail))
            {
                recipient = defaultEmail;

                if (!string.IsNullOrWhiteSpace(bccEmail))
                {
                    bccEmail = defaultEmail;
                }

                if (!string.IsNullOrWhiteSpace(ccEmail))
                {
                    ccEmail = defaultEmail;                    
                }
            }

            AddRequest request = CreateRequest
                (appAccounLogin, appAccountPass, emailUserName, recipient, 
                subject, body, IsHtml, sender, fromName, priority, bccEmail, 
                ccEmail, arrayOfAttachments);

            var emailId = await _eqsService.AddAsync(request);
        }

        private static AddRequest CreateRequest(string appAccounLogin, string appAccountPass, string emailUserName, string recipient, string subject, string body, bool IsHtml, string sender, string fromName, int priority, string bccEmail, string ccEmail, EmailAttachment[] arrayOfAttachments)
        {
            return new AddRequest
            {
                appAccountLogin = appAccounLogin,
                appAccountPassword = appAccountPass,
                emailUserName = emailUserName,
                toAddress = recipient,
                subject = subject,
                body = body,
                isHTML = IsHtml,
                attachments = arrayOfAttachments,
                fromAddress = sender,
                fromName = fromName,
                bccAddress = bccEmail,
                ccAddress = ccEmail,
                priority = priority,
                sendDateTime = DateTime.UtcNow
            };
        }

        public async Task SendWithAttachmentsAsync(
            string appAccounLogin, string appAccountPass, string emailUserName,
            string recipient, string subject, string body, bool IsHtml,
            string sender, string fromName, int priority = 0, string bccEmail = null, string ccEmail = null)
        {
            //TODO implement as property
            List<EmailAttachment> emailAttachments = new List<EmailAttachment>();
            EmailAttachment[] arrayOfAttachments = emailAttachments.ToArray();

            string defaultEmail = _config.Get("Emails:DefaultEmail");

            if (!string.IsNullOrWhiteSpace(defaultEmail))
            {
                recipient = defaultEmail;

                if (!string.IsNullOrWhiteSpace(bccEmail))
                {
                    bccEmail = defaultEmail;
                }

                if (!string.IsNullOrWhiteSpace(ccEmail))
                {
                    ccEmail = defaultEmail;
                }
            }

            var emailId = await _eqsService.AddAsync(CreateRequest
                (appAccounLogin, appAccountPass, emailUserName, recipient,
                subject, body, IsHtml, sender, fromName, priority, bccEmail,
                ccEmail, arrayOfAttachments));
        }

        public string CreateBody(EmailType type)
        {
            string url = AppDomain.CurrentDomain.BaseDirectory;
            switch(type)
            {
                case EmailType.NewUser:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/setNewPassword.html");
                    break;
                case EmailType.ResetPassword:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/resetPassword.html");
                    break;
                case EmailType.PasswordChanged:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/passwordChanged.html");
                    break;
                case EmailType.TradeNote:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/tradeNote.html");
                    break;

                case EmailType.BrokerTradeNote:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/brokerTradeNote.html");
                    break;
                case EmailType.FailedFIXTrades:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/failedFIXTrades.html");
                    break;


                case EmailType.OrderNote:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/orderNote.html");
                    break;
                case EmailType.DealerOrderNote:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/dealerOrderNote.html");
                    break;
                case EmailType.CancelOrder:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/cancelOrder.html");
                    break;
                case EmailType.SettlementAssigned:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/settlementAssigned.html");
                    break;

                // Payment Emails
                case EmailType.InwardPayment:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/inwardPayment.html");
                    break;
                case EmailType.OutwardPayment:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/outwardPayment.html");
                    break;

                // User Change Request Email
                case EmailType.UserChangeRequestAlert:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/UserChangeRequiresApprovalAlert.html");
                    break;

                case EmailType.MobileChangeEmailAlert:
                    url += Path.Combine(url, "/Email/EmailSender/Templates/phoneChanged.html");
                    break;

                default:
                    throw new NoSuchEmailTemplate("Please specify EmailType and define email body template");
            }

            using (StreamReader sourceReader = File.OpenText(url))
            {
                return sourceReader.ReadToEnd();
            }
        }
    }
}
