using Argentex.Core.Service.Models.Email;
using Argentex.Core.Service.Models.Trades;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argentex.Core.Service
{
    public interface IEmailService
    {
        Task<IdentityResult> SendUserNewPasswordEmailAsync(string userName, string clientCompanyName);
        Task<IdentityResult> SendResetPasswordEmailAsync(string email);
        Task<IdentityResult> SendPasswordChangedEmailAsync(string email);

        Task<IdentityResult> SendEmailToDirectorsForApproval();

        Task SendTradeReceiptEmailAsync(FxForwardTradeInformationModel trade);
        Task SendBrokerTradeNoteEmailAsync(BrokerTradeNoteModel trade);
        Task SendFailedFIXTradeEmailAsync(FailedFIXTradeModel model);

        Task SendOrderNoteEmailAsync(OrderNoteModel model);
        Task SendDealerOrderNoteEmailAsync(OrderNoteModel model);
        Task SendCancelOrderEmailAsync(CancelOrderModel model);

        Task SendSettlementEmailsAsync(IList<SettlementNoteModel> modelList, List<string> emailList);

        Task SendInwardPaymentEmailAsync(PaymentNotificationModel model, IEnumerable<string> emailList);
        Task SendOutwardPaymentEmailAsync(PaymentNotificationModel model, IEnumerable<string> emailList);

        Task SendMobileChangeEmailAsync(string email, string proposedValue);
      
    }
}
