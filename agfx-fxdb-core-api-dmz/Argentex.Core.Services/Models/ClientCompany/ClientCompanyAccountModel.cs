using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service
{
    public class ClientCompanyAccountModel
    {
        public int ClientCompanyOpiId { get; set; }
        public int ClientCompanyId { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public int ClearingCodePrefixId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }
        public string SwiftCode { get; set; }
        public string Iban { get; set; }
        public bool IsDefault { get; set; }
        public bool Approved { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public string Reference { get; set; }
    }
}
