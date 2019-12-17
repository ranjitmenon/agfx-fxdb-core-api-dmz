using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class BankAccountCurrencyDetails
    {
        public int BankAccountId { get; set; }
        public int CurrencyId { get; set; }
        public int CountryId { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountSort { get; set; }
        public string BankAccountSwift { get; set; }
        public string BankAccountIban { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }
        public int? ClearingCodePrefixId { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public BankAccount BankAccount { get; set; }
        public ClearingCodePrefix ClearingCodePrefix { get; set; }
        public Country Country { get; set; }
        public AuthUser CreatedByAuthUser { get; set; }
        public Currency Currency { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
    }
}
