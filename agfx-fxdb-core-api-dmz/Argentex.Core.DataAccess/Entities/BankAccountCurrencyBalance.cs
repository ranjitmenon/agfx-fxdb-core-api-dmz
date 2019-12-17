using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class BankAccountCurrencyBalance
    {
        public int BankAccountId { get; set; }
        public int CurrencyId { get; set; }
        public decimal? Balance { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public DateTime? BalanceDate { get; set; }
        public int TransactionCommitId { get; set; }

        public BankAccount BankAccount { get; set; }
        public Currency Currency { get; set; }
        public TransactionCommit TransactionCommit { get; set; }
    }
}
