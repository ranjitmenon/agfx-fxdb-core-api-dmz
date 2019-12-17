using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyVirtualAccountCurrencyBalanceHistory
    {
        public int ClientCompanyVirtualAccountId { get; set; }
        public int CurrencyId { get; set; }
        public int TransactionCommitId { get; set; }
        public DateTime BalanceDate { get; set; }
        public decimal? Balance { get; set; }
        public int? UpdatedByAuthUserId { get; set; }

        public ClientCompanyVirtualAccount ClientCompanyVirtualAccount { get; set; }
        public Currency Currency { get; set; }
        public TransactionCommit TransactionCommit { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
    }
}
