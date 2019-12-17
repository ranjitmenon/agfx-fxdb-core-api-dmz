using System;

namespace Argentex.Core.Service.Models.Statements
{
    public class StatementModel
    {
        public string TradeCode { get; set; }
        public string PaymentCode { get; set; }
        public int BankAccountId { get; set; }
        public DateTime ValueDate { get; set; }
        public string Event { get; set; }
        public bool IsDebit { get; set; }
        public decimal Amount { get; set; }
    }
}
