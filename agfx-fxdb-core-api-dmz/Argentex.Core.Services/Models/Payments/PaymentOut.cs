using Argentex.Core.DataAccess.Entities;


namespace Argentex.Core.Service.Models.Payments
{
    public class PaymentOut : Payment
    {
        public ClientCompanyOpi CreditOPI { get; set; }
        public BankAccount CreditAccount { get; set; }
        public BankAccount DebitAccount { get; set; }
        public ClientCompanyVirtualAccount CreditVirtualAccount { get; set; }
        public ClientCompanyVirtualAccount DebitVirtualAccount { get; set; }

        public string AuthUserName { get; set; }
    }
}
