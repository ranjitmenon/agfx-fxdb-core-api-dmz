using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClearingCodePrefix
    {
        public ClearingCodePrefix()
        {
            BankAccountCurrencyDetails = new HashSet<BankAccountCurrencyDetails>();
            ClientCompanyOpi = new HashSet<ClientCompanyOpi>();
            CountryClearingCodePrefix = new HashSet<CountryClearingCodePrefix>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public ICollection<BankAccountCurrencyDetails> BankAccountCurrencyDetails { get; set; }
        public ICollection<ClientCompanyOpi> ClientCompanyOpi { get; set; }
        public ICollection<CountryClearingCodePrefix> CountryClearingCodePrefix { get; set; }
    }
}
