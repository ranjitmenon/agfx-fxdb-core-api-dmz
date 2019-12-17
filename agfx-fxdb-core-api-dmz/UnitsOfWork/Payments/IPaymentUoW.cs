using System.Collections.Generic;
using System.Data;
using System.Linq;
using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;

namespace Argentex.Core.UnitsOfWork.Payments
{
    public interface IPaymentUoW : IBaseUow
    {
        IQueryable<Payment> GetPayment(string paymentCode);
        IQueryable<ClientCompanyOpitransaction> GetClientCompanyOpiTransaction(string paymentCode);
        IEnumerable<PaymentType> GetPaymentType(string paymentType);
        DataTable MakePayment(Payment payment, ClientCompanyOpi clientCompanyOpi, BankAccount debitBankAccount, 
            ClientCompanyVirtualAccount debitVirtualAccount, int paymentSwiftOutgoingStatusId, bool paymentAuthorised, string authUserName);
        IQueryable<Payment> GetPaymentNotification(string paymentCode);
    }
}
