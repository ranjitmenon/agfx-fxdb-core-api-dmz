using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyVirtualAccountCurrencyBalance
    {
        public int ClientCompanyVirtualAccountId { get; set; }
        public int CurrencyId { get; set; }
        public decimal? Balance { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public DateTime BalanceDate { get; set; }
        public int TransactionCommitId { get; set; }

        public ClientCompanyVirtualAccount ClientCompanyVirtualAccount { get; set; }
        public Currency Currency { get; set; }
    }
}
