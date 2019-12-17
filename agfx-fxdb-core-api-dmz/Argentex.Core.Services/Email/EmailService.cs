using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using Argentex.Core.Service.Email.EmailSender;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Identity;
using Argentex.Core.Service.Identity.Services;
using Argentex.Core.Service.Models.Email;
using Argentex.Core.Service.Models.Trades;
using Argentex.Core.UnitsOfWork.Trades;
using Argentex.Core.UnitsOfWork.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argentex.Core.Service
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IConfigWrapper _config;
        private readonly IUserUow _userUow;
        private readonly ITradeUow _tradeUow;
      

        public EmailService(IEmailSender emailSender,
            IConfigWrapper config, 
            IUserUow userUow,
            ITradeUow tradeUow
            )
        {
            _emailSender = emailSender;
            _config = config;
            _userUow = userUow;
            _tradeUow = tradeUow;
            
        }

        #region User Emails

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email">New user email address</param>
        /// <returns></returns>
        public async Task<IdentityResult> SendUserNewPasswordEmailAsync(string userName, string  clientCompanyName)
        {
            ApplicationUser user = await _userUow.GetUserByNameAsync(userName);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = $"Invalid User {userName}" });

            try
            {
                var sanitizedToken = await GetSanitizedToken(user);
                var clientSiteUrl = _config.Get("Urls:ClientSiteUrl");
                var newPasswordURL = clientSiteUrl + string.Format(
                    _config.Get("Urls:NewPasswordUrl"), sanitizedToken, user.UserName);

                var emailImages = _config.Get("Urls:ImagesUrl");
                var emailSubject = "Argentex Client Site New Password";
                var emailBody = _emailSender.CreateBody(EmailType.NewUser);

                var emailMessage = emailBody.Replace("<!--*[UserName]*-->", user.UserName)
                                        .Replace("<!--*[ClientCompanyName]*-->", clientCompanyName)
                                        .Replace("<!--*[MailImagesBaseURL]*-->", emailImages)
                                        .Replace("<!--*[NewPasswordURL]*-->", newPasswordURL);
                                        
                const bool isHtml = true;
                await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                            _config.Get("EQS:EQSAppAccountPassword"),
                                            _config.Get("EQS:EQSEmailUsername"),
                                            user.Email, emailSubject, emailMessage, isHtml,
                                            _config.Get("EQS:EQSSenderEmail"),
                                            _config.Get("EQS:EQSSenderName"));
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Error sending email : {ex}" });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email">User email address</param>
        /// <returns></returns>
        public async Task<IdentityResult> SendResetPasswordEmailAsync(string userName)
        {
            ApplicationUser user = await _userUow.GetUserByNameAsync(userName);
            if (user == null || user.IsDeleted)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = IdentityResultCodes.UserNotFound,
                    Description = $"User with username {userName} does not exist"
                });

            if (!user.IsApproved || user.LockoutEnabled)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = IdentityResultCodes.InvalidUserState,
                    Description = $"{userName} is in an invalid state"
                });
            try
            {
                var sanitizedToken = await GetSanitizedToken(user);
                var clientSiteUrl = _config.Get("Urls:ClientSiteUrl");
                var resetPasswordURL = clientSiteUrl + string.Format(
                    _config.Get("Urls:ResetPasswordUrl"), sanitizedToken, user.UserName);

                var emailImages = _config.Get("Urls:ImagesUrl");
                var emailSubject = "Argentex Client Site Forgot Password";
                var emailBody = _emailSender.CreateBody(EmailType.ResetPassword);

                var emailMessage = emailBody.Replace("<!--*[UserName]*-->", user.UserName)
                                        .Replace("<!--*[MailImagesBaseURL]*-->", emailImages)
                                        .Replace("<!--*[ResetPasswordURL]*-->", resetPasswordURL);

                const bool isHtml = true;
                await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                            _config.Get("EQS:EQSAppAccountPassword"),
                                            _config.Get("EQS:EQSEmailUsername"),
                                            user.Email, emailSubject, emailMessage, isHtml,
                                            _config.Get("EQS:EQSSenderEmail"),
                                            _config.Get("EQS:EQSSenderName"));
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Error sending email : {ex}" });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email">User email address</param>
        /// <returns></returns>
        public async Task<IdentityResult> SendPasswordChangedEmailAsync(string userName)
        {
            try
            {
                ApplicationUser user = await _userUow.GetUserByNameAsync(userName);

                if (user == null) return IdentityResult.Failed(new IdentityError { Description = $"Invalid User {userName}" });

                var emailImages = _config.Get("Urls:ImagesUrl");
                var emailSubject = "Password successfully changed";
                var emailBody = _emailSender.CreateBody(EmailType.PasswordChanged);

                var clientSiteUrl = _config.Get("Urls:ClientSiteUrl");
                var emailMessage = emailBody.Replace("<!--*[MailImagesBaseURL]*-->", emailImages)
                                            .Replace("<!--*[ClientSiteUrl]*-->", clientSiteUrl)
                                            .Replace("<!--*[UserName]*-->", user.UserName);

                const bool isHtml = true;
                await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                            _config.Get("EQS:EQSAppAccountPassword"),
                                            _config.Get("EQS:EQSEmailUsername"),
                                            user.Email, emailSubject, emailMessage, isHtml,
                                            _config.Get("EQS:EQSSenderEmail"),
                                            _config.Get("EQS:EQSSenderName"));
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Error sending email : {ex}" });
            }
        }

        private async Task<string> GetSanitizedToken(ApplicationUser user)
        {
            var confirmationToken = await _userUow.GeneratePasswordResetTokenAsync(user);
            var sanitizedToken = confirmationToken
                .Replace(SanitizeForwardSlash.Old, SanitizeForwardSlash.New)
                .Replace(SanitizeDoubleEqual.Old, SanitizeDoubleEqual.New)
                .Replace(SanitizePlus.Old, SanitizePlus.New);
            return System.Web.HttpUtility.UrlEncode(sanitizedToken);
        }

        public async Task<IdentityResult> SendEmailToDirectorsForApproval()
        {
            try
            {
                IEnumerable<AppUser> directors = _userUow.GetAllDirectorsAsList();

                foreach (AppUser user in directors)
                {
                    SendNewUserChangeRequestEmailAlert(user.AuthUser.Email);
                }

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Error sending email : {ex}" });
            }
        }

        public async void SendNewUserChangeRequestEmailAlert(string email)
        {
            var emailImages = _config.Get("Urls:ImagesUrl");
            var emailSubject = "New User Change Request";
            var emailBody = _emailSender.CreateBody(EmailType.UserChangeRequestAlert);

            var emailMessage = emailBody.Replace("<!--*[MailImagesBaseURL]*-->", emailImages);

            const bool isHtml = true;
            await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                        _config.Get("EQS:EQSAppAccountPassword"),
                                        _config.Get("EQS:EQSEmailUsername"),
                                        email, emailSubject, emailMessage, isHtml,
                                        _config.Get("EQS:EQSSenderEmail"),
                                        _config.Get("EQS:EQSSenderName"));
        }


        public async Task SendMobileChangeEmailAsync( string proposedValue, string email)
        {
            var emailImages = _config.Get("Urls:ImagesUrl");
            var emailSubject = "Mobile Phone Number Updated";
            var emailBody = _emailSender.CreateBody(EmailType.MobileChangeEmailAlert);
            var clientSiteUrl = _config.Get("Urls:ClientSiteUrl");

            var emailMessage = emailBody.Replace("<!--*[MailImagesBaseURL]*-->", emailImages)
                                        .Replace("<!--*[PhoneNumber]*-->", proposedValue)
                                        .Replace("<!--*[ClientSiteUrl]*-->", clientSiteUrl);
            const bool isHtml = true;
            await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                        _config.Get("EQS:EQSAppAccountPassword"),
                                        _config.Get("EQS:EQSEmailUsername"),
                                        email, emailSubject, emailMessage, isHtml,
                                        _config.Get("EQS:EQSSenderEmail"),
                                        _config.Get("EQS:EQSSenderName"));
        }



        #endregion

        #region Order Emails

        /// <summary>
        /// Sending email to client to notify that an order was made
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SendOrderNoteEmailAsync(OrderNoteModel model)
        {
            var body = _emailSender.CreateBody(EmailType.OrderNote);
            var subject = $"Order confirmation ({model.TradeRef})";

            var emailBody = body;
            var emailSubject = subject;
            var emailImages = _config.Get("Urls:ImagesUrl");
            var clientSiteUrl = _config.Get("Urls:ClientSiteUrl");
            var emailMessage = emailBody.Replace("<!--*[ClientCompanyName]*-->", model.ClientCompany.Name)
                            .Replace("<!--*[CRN]*-->", model.ClientCompany.Crn)
                            .Replace("<!--*[InstructedBy]*-->", model.InstructedBy)
                            .Replace("<!--*[InstructedTime]*-->", model.InstructedDateTime.ToString("dd/MM/yyyy HH:mm"))
                            .Replace("<!--*[InstructionMethod]*-->", model.Method)
                            .Replace("<!--*[TradeCode]*-->", model.TradeRef)
                            .Replace("<!--*[SellCCYAndAmount]*-->", string.Format("{0:0.00####}", model.SellAmount) + " " + model.SellCcy)
                            .Replace("<!--*[BuyCCYAndAmount]*-->", string.Format("{0:0.00####}", model.BuyAmount) + " " + model.BuyCcy)
                            .Replace("<!--*[CurrencyPair]*-->", model.CurrencyPair)
                            .Replace("<!--*[ClientRate]*-->", string.Format("{0:0.0000####}", model.Rate))
                            .Replace("<!--*[ValueDate]*-->", model.ValueDate.ToString("dd/MM/yyyy"))

                            .Replace("<!--*[CreatedDate]*-->", model.CreatedDate.ToString("dd/MM/yyyy"))
                            .Replace("<!--*[Validity]*-->", model.ValidityDate != null ? ((DateTime)model.ValidityDate).ToString("dd/MM/yyyy") : "-")
                            //Images paths
                            .Replace("<!--*[MailImagesBaseURL]*-->", emailImages)
                            .Replace("<!--*[ClientSiteUrl]*-->", clientSiteUrl);

            var sendTo = model.ClientEmail;
            const bool isHtml = true;
            await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                        _config.Get("EQS:EQSAppAccountPassword"),
                                        _config.Get("EQS:EQSEmailUsername"),
                                        sendTo, emailSubject, emailMessage, isHtml,
                                        _config.Get("EQS:EQSSenderEmail"),
                                        _config.Get("EQS:EQSSenderName"));

        }

        /// <summary>
        /// Sending email to broker to notify that an order was made
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SendDealerOrderNoteEmailAsync(OrderNoteModel model)
        {
            var body = _emailSender.CreateBody(EmailType.DealerOrderNote);
            var subject = $"Order confirmation ({model.TradeRef})";

            var emailBody = body;
            var emailSubject = subject;
            var emailImages = _config.Get("Urls:ImagesUrl");
            var emailMessage = emailBody.Replace("<!--*[ClientCompanyName]*-->", model.ClientCompany.Name)
                            .Replace("<!--*[CRN]*-->", model.ClientCompany.Crn)
                            .Replace("<!--*[InstructedBy]*-->", model.InstructedBy)
                            .Replace("<!--*[InstructedTime]*-->", model.InstructedDateTime.ToString("dd/MM/yyyy HH:mm"))
                            .Replace("<!--*[InstructionMethod]*-->", model.Method)
                            .Replace("<!--*[TradeCode]*-->", model.TradeRef)
                            .Replace("<!--*[SellCCYAndAmount]*-->", string.Format("{0:0.00####}", model.SellAmount) + " " + model.SellCcy)
                            .Replace("<!--*[BuyCCYAndAmount]*-->", string.Format("{0:0.00####}", model.BuyAmount) + " " + model.BuyCcy)
                            .Replace("<!--*[CurrencyPair]*-->", model.CurrencyPair)
                            .Replace("<!--*[ClientRate]*-->", string.Format("{0:0.0000####}", model.Rate))
                            .Replace("<!--*[ValueDate]*-->", model.ValueDate.ToString("dd/MM/yyyy"))

                            .Replace("<!--*[CreatedDate]*-->", model.CreatedDate.ToString("dd/MM/yyyy"))
                            .Replace("<!--*[Validity]*-->", model.ValidityDate != null ? ((DateTime)model.ValidityDate).ToString("dd/MM/yyyy") : "-")
                            //Images paths
                            .Replace("<!--*[MailImagesBaseURL]*-->", emailImages);

            var sendTo = model.DealerAuthUser != null ? model.DealerAuthUser.Email : null;
            const bool isHtml = true;
            await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                        _config.Get("EQS:EQSAppAccountPassword"),
                                        _config.Get("EQS:EQSEmailUsername"),
                                        sendTo, emailSubject, emailMessage, isHtml,
                                        _config.Get("EQS:EQSSenderEmail"),
                                        _config.Get("EQS:EQSSenderName"));

        }

        /// <summary>
        /// Sending email to client to notify that an order was cancelled by the client or on validity date overdued
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SendCancelOrderEmailAsync(CancelOrderModel model)
        {
            var body = _emailSender.CreateBody(EmailType.CancelOrder);
            var subject = "Order cancellation";

            var emailBody = body;
            var emailSubject = subject;
            var emailImages = _config.Get("Urls:ImagesUrl");
            var clientSiteUrl = _config.Get("Urls:ClientSiteUrl");
            var emailMessage = emailBody.Replace("<!--*[ClientCompanyName]*-->", model.ClientCompany.Name)
                            .Replace("<!--*[CRN]*-->", model.ClientCompany.Crn)
                            .Replace("<!--*[InstructedBy]*-->", model.InstructedBy)
                            .Replace("<!--*[InstructedTime]*-->", model.InstructedDateTime.ToString("dd/MM/yyyy HH:mm"))
                            .Replace("<!--*[InstructionMethod]*-->", model.Method)
                            .Replace("<!--*[TradeCode]*-->", model.TradeRef)
                            .Replace("<!--*[SellCCYAndAmount]*-->", string.Format("{0:0.00####}", model.SellAmount) + " " + model.SellCcy)
                            .Replace("<!--*[BuyCCYAndAmount]*-->", string.Format("{0:0.00####}", model.BuyAmount) + " " + model.BuyCcy)
                            .Replace("<!--*[CurrencyPair]*-->", model.CurrencyPair)
                            .Replace("<!--*[ClientRate]*-->", string.Format("{0:0.0000####}", model.Rate))
                            .Replace("<!--*[ValueDate]*-->", model.ValueDate.ToString("dd/MM/yyyy"))
                            .Replace("<!--*[CollateralCCYAndAmount]*-->", string.Format("{0:0.00####}", model.Collateral) + " " + model.CollateralCcy)

                            .Replace("<!--*[CreatedDate]*-->", model.CreatedDate.ToString("dd/MM/yyyy"))
                            .Replace("<!--*[Validity]*-->", model.ValidityDate != null ? ((DateTime)model.ValidityDate).ToString("dd/MM/yyyy") : "-")
                            //Images paths
                            .Replace("<!--*[MailImagesBaseURL]*-->", emailImages)
                            .Replace("<!--*[ClientSiteUrl]*-->", clientSiteUrl);

            var sendTo = model.ClientEmail;
            const bool isHtml = true;
            await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                        _config.Get("EQS:EQSAppAccountPassword"),
                                        _config.Get("EQS:EQSEmailUsername"),
                                        sendTo, emailSubject, emailMessage, isHtml,
                                        _config.Get("EQS:EQSSenderEmail"),
                                        _config.Get("EQS:EQSSenderName"));

        }
        
        #endregion

        #region Trade Emails

        /// <summary>
        /// Sending email to client to notify about the newly created trade
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public async Task SendTradeReceiptEmailAsync(FxForwardTradeInformationModel trade)
        {
            //implement universal email handling
            var emailImages = _config.Get("Urls:ImagesUrl");
            var emailSubject = "Trade Contract Note";
            var emailBody = _emailSender.CreateBody(EmailType.TradeNote);
            var clientSiteUrl = _config.Get("Urls:ClientSiteUrl");
            var settlementAccountsUrl = clientSiteUrl + _config.Get("Urls:SettlementAccountsUrl");
            //Trade information
            var emailMessage = emailBody.Replace("<!--*[ClientCompanyName]*-->", trade.ClientCompany.Name)
                                        .Replace("<!--*[CRN]*-->", trade.ClientCompany.Crn)
                                        .Replace("<!--*[InstructedBy]*-->", trade.InstructedBy)
                                        .Replace("<!--*[InstructedTime]*-->", trade.InstructedDateTime.ToString("dd/MM/yyyy HH:mm"))
                                        .Replace("<!--*[InstructionMethod]*-->", trade.Method)
                                        .Replace("<!--*[TradeCode]*-->", trade.TradeRef)
                                        .Replace("<!--*[SellCCYAndAmount]*-->", trade.SellAmount.ToString("N2") + " " + trade.SellCcy)
                                        .Replace("<!--*[BuyCCYAndAmount]*-->", trade.BuyAmount.ToString("N2") + " " + trade.BuyCcy)
                                        .Replace("<!--*[CurrencyPair]*-->", trade.CurrencyPair)
                                        .Replace("<!--*[ClientRate]*-->", trade.Rate.ToString("N4"))
                                        .Replace("<!--*[ValueDate]*-->", trade.ValueDate.ToString("dd/MM/yyyy"))
                                        .Replace("<!--*[CollateralCCYAndAmount]*-->", trade.Collateral.ToString("N2") + " " + trade.CollateralCcy)
                                        //Images paths
                                        .Replace("<!--*[MailImagesBaseURL]*-->", emailImages)
                                        .Replace("<!--*[ClientSiteUrl]*-->", clientSiteUrl)
                                        .Replace("<!--*[SettlementAccountsUrl]*-->", settlementAccountsUrl);

            if (trade.SettlementAccountDetails != null)
            {
                //Account information
                emailMessage = emailMessage.Replace("<!--*[SettlementBankName]*-->", trade.SettlementAccountDetails.BankName)
                         .Replace("<!--*[SettlementAccountName]*-->", trade.SettlementAccountDetails.AccountName)
                         .Replace("<!--*[SettlementAccountSort]*-->", trade.SettlementAccountDetails.SortCode)
                         .Replace("<!--*[SettlementAccountNumber]*-->", trade.SettlementAccountDetails.AccountNumber)
                         .Replace("<!--*[SettlementAccountSwiftCode]*-->", trade.SettlementAccountDetails.SwiftCode)
                         .Replace("<!--*[SettlementAccountIBAN]*-->", trade.SettlementAccountDetails.Iban);
            }
            var isHtml = true;

            await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                            _config.Get("EQS:EQSAppAccountPassword"),
                            _config.Get("EQS:EQSEmailUsername"),
                            trade.ClientEmail, emailSubject, emailMessage, isHtml,
                            _config.Get("EQS:EQSSenderEmail"),
                            _config.Get("EQS:EQSSenderName"));
        }

        /// <summary>
        /// Sending email to broker and dealer assigned
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public async Task SendBrokerTradeNoteEmailAsync(BrokerTradeNoteModel trade)
        {
            //implement universal email handling
            var emailImages = _config.Get("Urls:ImagesUrl");
            var emailSubject = $"{trade.TradeCode} Trade Contract Note";
            var emailBody = _emailSender.CreateBody(EmailType.BrokerTradeNote);

            //Trade information
            var emailMessage = emailBody.Replace("<!--*[ClientCompanyName]*-->", trade.ClientCompany.Name)
                                        .Replace("<!--*[CRN]*-->", trade.ClientCompany.Crn)
                                        .Replace("<!--*[InstructedBy]*-->", trade.InstructedBy)
                                        .Replace("<!--*[InstructedTime]*-->", trade.InstructedDateTime.ToString("dd/MM/yyyy HH:mm"))
                                        .Replace("<!--*[InstructionMethod]*-->", trade.Method)
                                        .Replace("<!--*[TradeCode]*-->", trade.TradeCode)
                                        .Replace("<!--*[SellCCYAndAmount]*-->", trade.SellAmount.ToString("N2") + " " + trade.SellCcy)
                                        .Replace("<!--*[BuyCCYAndAmount]*-->", trade.BuyAmount.ToString("N2") + " " + trade.BuyCcy)
                                        .Replace("<!--*[CurrencyPair]*-->", trade.CurrencyPair)                                        
                                        .Replace("<!--*[ClientRate]*-->", trade.Rate.ToString("N4"))
                                        .Replace("<!--*[ValueDate]*-->", trade.ValueDate.ToString("dd/MM/yyyy"))
                                        .Replace("<!--*[CollateralCCYAndAmount]*-->", trade.Collateral.ToString("N2") + " " + trade.CollateralCcy)                                        
                                        //Images paths
                                        .Replace("<!--*[MailImagesBaseURL]*-->", emailImages);

            if (trade.SettlementAccountDetails != null)
            {
                //Account information
                emailMessage = emailMessage.Replace("<!--*[SettlementBankName]*-->", trade.SettlementAccountDetails.BankName)
                         .Replace("<!--*[SettlementAccountName]*-->", trade.SettlementAccountDetails.AccountName)
                         .Replace("<!--*[SettlementAccountSort]*-->", trade.SettlementAccountDetails.SortCode)
                         .Replace("<!--*[SettlementAccountNumber]*-->", trade.SettlementAccountDetails.AccountNumber)
                         .Replace("<!--*[SettlementAccountSwiftCode]*-->", trade.SettlementAccountDetails.SwiftCode)
                         .Replace("<!--*[SettlementAccountIBAN]*-->", trade.SettlementAccountDetails.Iban);
            }
            var isHtml = true;

            var emailTo = trade.Broker.BrokerNoteEmailAddress;
            var emailToCC = trade.DealerAuthUser != null ? trade.DealerAuthUser.Email : null;
            await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                            _config.Get("EQS:EQSAppAccountPassword"),
                            _config.Get("EQS:EQSEmailUsername"), emailTo
                            , emailSubject, emailMessage, isHtml,
                            _config.Get("EQS:EQSSenderEmail"),
                            _config.Get("EQS:EQSSenderName"), 0, null, emailToCC);
        }

        /// <summary>
        /// Sending email to Argentex Settlements department when a deal does not receive confirmation from FIX
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SendFailedFIXTradeEmailAsync(FailedFIXTradeModel model)
        {
            var body = _emailSender.CreateBody(EmailType.FailedFIXTrades);
            var subject = $"{model.TradeCode} confirmation not received from FIX";

            var emailBody = body;
            var emailSubject = subject;
            var emailImages = _config.Get("Urls:ImagesUrl");
            // Replacing $0 with trade code in the url from appsettings
            var tradeDetailsPath = _config.Get("Urls:FXDBTraderUrl") + _config.Get("Urls:FXDBTradeDetailsPath");
            var tradeDetailsUrl = String.Format(tradeDetailsPath, model.TradeCode);

            var emailMessage = emailBody.Replace("<!--*[ClientCompanyName]*-->", model.ClientCompany.Name)
                            .Replace("<!--*[TradeCode]*-->", model.TradeCode)
                            .Replace("<!--*[CurrencyPair]*-->", model.CurrencyPair)
                            .Replace("<!--*[ValueDate]*-->", model.ValueDate.ToString("dd/MM/yyyy"))
                            .Replace("<!--*[SellCCY]*-->", model.SellCcy)
                            .Replace("<!--*[BuyCCY]*-->", model.BuyCcy)
                            .Replace("<!--*[ClientRate]*-->", model.Rate.ToString("N4"))
                            // FXDB Trade Details Link
                            .Replace("<!--*[TradeDetailsUrl]*-->", tradeDetailsUrl)
                            // Images paths
                            .Replace("<!--*[MailImagesBaseURL]*-->", emailImages);

            var sendTo = _config.Get("Emails:SettlementsDepartment");
            const bool isHtml = true;
            await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                        _config.Get("EQS:EQSAppAccountPassword"),
                                        _config.Get("EQS:EQSEmailUsername"),
                                        sendTo, emailSubject, emailMessage, isHtml,
                                        _config.Get("EQS:EQSSenderEmail"),
                                        _config.Get("EQS:EQSSenderName"));
        }

        #endregion

        #region Settlement Emails

        /// <summary>
        /// Sending email to client to notify that a settlement was made
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SendSettlementEmailsAsync(IList<SettlementNoteModel> modelList, List<string> emailList)
        {
            var body = _emailSender.CreateBody(EmailType.SettlementAssigned);
            var subject = $"Settlements Request for Trade: ({modelList[0].ParentTradeCode})";

            var emailBody = body;
            var emailSubject = subject;
            var emailImages = _config.Get("Urls:ImagesUrl");
            var clientSiteUrl = _config.Get("Urls:ClientSiteUrl");
            var settlementInformationMessages = "You have requested: <br /><br />";
            var dueForSettlementMessages = "";
            int count = 0;

            foreach (var model in modelList)
            {
                count++;
                settlementInformationMessages += 
                    $"{string.Format("{0:n}", model.SettlementAmount)} {model.AccountCurrency} from trade {model.ParentTradeCode} be paid to {model.AccountName} On {model.ValueDate.ToString("dd/MM/yyyy")} <br /><br />";

                if (count != modelList.Count)
                {
                    settlementInformationMessages += "And <br /><br />";
                }

                dueForSettlementMessages += $"On {model.ValueDate.ToString("dd/MM/yyyy")} Settlement due from you is therefore {string.Format("{0:n}", model.Amount)} {model.TradedCurrency}<br />";
            }

            emailBody = emailBody.Replace("<!--*[SettlementInformationMessages]*-->", settlementInformationMessages)
                .Replace("<!--*[DueForSettlementMessages]*-->", dueForSettlementMessages)
                .Replace("<!--*[InstructedTime]*-->", modelList[0].InstructedDateTime.ToString("dd/MM/yyyy HH:mm"))
                .Replace("<!--*[InstructedBy]*-->", modelList[0].InstructedBy)
                //Images paths
                .Replace("<!--*[MailImagesBaseURL]*-->", emailImages)
                .Replace("<!--*[ClientSiteUrl]*-->", clientSiteUrl);

            foreach (var email in emailList)
            {
                var sendTo = email;
                const bool isHtml = true;
                await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                            _config.Get("EQS:EQSAppAccountPassword"),
                                            _config.Get("EQS:EQSEmailUsername"),
                                            sendTo, emailSubject, emailBody, isHtml,
                                            _config.Get("EQS:EQSSenderEmail"),
                                            _config.Get("EQS:EQSSenderName"));
            }
        }

        #endregion

        #region Payments Emails

        public async Task SendInwardPaymentEmailAsync(PaymentNotificationModel model, IEnumerable<string> emailList)
        {
            var body = _emailSender.CreateBody(EmailType.InwardPayment);
            var subject = $"Payment Notification ({model.PaymentCode})";

            var emailBody = body;
            var emailSubject = subject;
            var emailImages = _config.Get("Urls:ImagesUrl");
            var emailMessage = emailBody.Replace("<!--*[ClientCompanyName]*-->", model.ClientCompany.Name)
                            .Replace("<!--*[CRN]*-->", model.ClientCompany.Crn)
                            .Replace("<!--*[PaymentCode]*-->", model.PaymentCode)
                            .Replace("<!--*[PaymentAmount]*-->", string.Format("{0:n}", model.PaymentAmount))
                            .Replace("<!--*[PaymentCCYCode]*-->", model.Currency.Code)
                            .Replace("<!--*[ValueDate]*-->", model.ValueDate.ToString("dd/MM/yyyy"))
                            .Replace("<!--*[Reference]*-->", model.Reference)
                            //Images paths
                            .Replace("<!--*[MailImagesBaseURL]*-->", emailImages);

            foreach (var email in emailList)
            {
                var sendTo = email;
                const bool isHtml = true;
                await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                            _config.Get("EQS:EQSAppAccountPassword"),
                                            _config.Get("EQS:EQSEmailUsername"),
                                            sendTo, emailSubject, emailMessage, isHtml,
                                            _config.Get("EQS:EQSSenderEmail"),
                                            _config.Get("EQS:EQSSenderName"));
            }
        }

        public async Task SendOutwardPaymentEmailAsync(PaymentNotificationModel model, IEnumerable<string> emailList)
        {
            var body = _emailSender.CreateBody(EmailType.OutwardPayment);
            var subject = $"Payment Notification ({model.PaymentCode})";

            var emailBody = body;
            var emailSubject = subject;
            var emailImages = _config.Get("Urls:ImagesUrl");
            var emailMessage = emailBody.Replace("<!--*[ClientCompanyName]*-->", model.ClientCompany.Name)
                            .Replace("<!--*[CRN]*-->", model.ClientCompany.Crn)
                            .Replace("<!--*[PaymentCode]*-->", model.PaymentCode)
                            .Replace("<!--*[PaymentAmount]*-->", string.Format("{0:n}", model.PaymentAmount))
                            .Replace("<!--*[PaymentCCYCode]*-->", model.Currency.Code)
                            .Replace("<!--*[ValueDate]*-->", model.ValueDate.ToString("dd/MM/yyyy"))
                            .Replace("<!--*[Reference]*-->", model.Reference)
                            // OPI
                            .Replace("<!--*[AccountName]*-->", model.ClientCompanyOpi.AccountName)
                            .Replace("<!--*[AccountNumber]*-->", model.ClientCompanyOpi.AccountNumber)
                            .Replace("<!--*[SortCode]*-->", model.ClientCompanyOpi.SortCode)
                            .Replace("<!--*[BankName]*-->", model.ClientCompanyOpi.BankName)
                            .Replace("<!--*[SwiftCode]*-->", model.ClientCompanyOpi.SwiftCode)
                            .Replace("<!--*[IBAN]*-->", model.ClientCompanyOpi.Iban)
                            // Images paths
                            .Replace("<!--*[MailImagesBaseURL]*-->", emailImages);

            foreach(var email in emailList)
            {
                var sendTo = email;
                const bool isHtml = true;
                await _emailSender.SendAsync(_config.Get("EQS:EQSAppAccountLogin"),
                                            _config.Get("EQS:EQSAppAccountPassword"),
                                            _config.Get("EQS:EQSEmailUsername"),
                                            sendTo, emailSubject, emailMessage, isHtml,
                                            _config.Get("EQS:EQSSenderEmail"),
                                            _config.Get("EQS:EQSSenderName"));
            }

        }

        #endregion

    }
}
