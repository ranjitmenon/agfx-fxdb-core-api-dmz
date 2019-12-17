using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class BankAccountCurrencyBalanceHistory
    {
        public int BankAccountId { get; set; }
        public int CurrencyId { get; set; }
        public int TransactionCommitId { get; set; }
        public DateTime BalanceDate { get; set; }
        public decimal? Balance { get; set; }

        public BankAccount BankAccount { get; set; }
        public Currency Currency { get; set; }
        public TransactionCommit TransactionCommit { get; set; }
    }
}
