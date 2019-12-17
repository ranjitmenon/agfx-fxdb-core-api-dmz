using Argentex.Core.Service.Models.Email;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts;
using Argentex.Core.UnitsOfWork.Payments;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Payments
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IPaymentUoW _paymentUoW;
        private readonly IEmailService _emailService;
        private readonly IClientCompanyUow _clientCompanyUow;

        private bool _disposed;

        public PaymentsService(IPaymentUoW paymentUoW,
            IEmailService emailService, IClientCompanyUow clientCompanyUow)
        {
            _paymentUoW = paymentUoW;            
            _emailService = emailService;
            _clientCompanyUow = clientCompanyUow;
        }

        public async Task<bool> NotifyContacts(string paymentCode)
        {
            var payment = GetPaymentNotificationModel(paymentCode);

            var emailList = GetPaymentNotificationEmails(payment.ClientCompany.Id).ToList();

            if(emailList.Count > 0)
            {
                switch (payment.PaymentTypeDescription)
                {
                    case "OUT":
                        await _emailService.SendOutwardPaymentEmailAsync(payment, emailList);
                        return true;
                    case "IN":
                        await _emailService.SendInwardPaymentEmailAsync(payment, emailList);
                        return true;
                    case "Inter-Virtual-Account":
                        // some email
                        return true;
                }
            }

            return false;
        }

        private IEnumerable<string> GetPaymentNotificationEmails(int clientCompanyID)
        {
            var contacts = _clientCompanyUow.GetClientCompanyContact(clientCompanyID).Where(e=> !e.IsDeleted && e.Authorized && e.RecNotifications);
            return contacts.Select(e => e.Email);
        }

        private PaymentNotificationModel GetPaymentNotificationModel(string paymentCode)
        {
            return _paymentUoW.GetPaymentNotification(paymentCode)
                .Select(payment => new PaymentNotificationModel
                {
                    PaymentCode = payment.Code,
                    PaymentAmount = (decimal)payment.Amount,
                    ValueDate = payment.ValueDate,
                    Reference = payment.Reference,
                    PaymentTypeDescription = payment.PaymentType.Description,
                    ClientCompany = payment.ClientCompany,
                    Currency = payment.Currency,
                    ClientCompanyOpi = payment.ClientCompanyOpitransaction.FirstOrDefault().ClientCompanyOpi
                }).SingleOrDefault();
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    //_paymentUoW?.Dispose();
                    //_tradeUow?.Dispose();
                    //_currencyUoW?.Dispose();
                    //_clientCompanyUow?.Dispose();
                    //_clientCompanyAccountsUoW?.Dispose();
                    //_settlementUow?.Dispose();
                    //_userService?.Dispose();
                    //_currencyService?.Dispose();
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
