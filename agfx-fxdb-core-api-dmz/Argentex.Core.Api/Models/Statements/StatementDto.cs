using System;

namespace Argentex.Core.Api.Models.Statements
{
    public class StatementDto
    {
        public string TradeCode { get; set; }
        public string PaymentCode { get; set; }
        public int BankAccountId { get; set; }
        public DateTime ValueDate { get; set; }
        public string Event { get; set; }
        public bool IsDebit { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
