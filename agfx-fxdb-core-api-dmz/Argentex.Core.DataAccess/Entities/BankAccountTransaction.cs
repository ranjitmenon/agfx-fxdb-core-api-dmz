using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class BankAccountTransaction
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public int CurrencyId { get; set; }
        public decimal? Amount { get; set; }
        public bool IsDebit { get; set; }
        public int? PaymentId { get; set; }
        public string FxforwardTradeCode { get; set; }
        public byte[] UpdateTimeStamp { get; set; }

        public BankAccount BankAccount { get; set; }
        public Currency Currency { get; set; }
        public FxforwardTrade FxforwardTradeCodeNavigation { get; set; }
        public Payment Payment { get; set; }
    }
}
