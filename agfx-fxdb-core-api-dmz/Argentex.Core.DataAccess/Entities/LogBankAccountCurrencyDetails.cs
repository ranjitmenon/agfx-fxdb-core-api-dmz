using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogBankAccountCurrencyDetails
    {
        public long LogId { get; set; }
        public string LogAction { get; set; }
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
        public int CreatedByAuthUserId { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
    }
}
