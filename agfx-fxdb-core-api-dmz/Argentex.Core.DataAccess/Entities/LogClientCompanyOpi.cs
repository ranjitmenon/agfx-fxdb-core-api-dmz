using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogClientCompanyOpi
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public string Description { get; set; }
        public string AccountName { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }
        public string Reference { get; set; }
        public string SwiftCode { get; set; }
        public string Iban { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Authorised { get; set; }
        public int? AuthorisedByAuthUserId { get; set; }
        public DateTime? AuthorisedDateTime { get; set; }
        public int CurrencyId { get; set; }
        public bool? IsCompanyAccount { get; set; }
        public string BeneficiaryAddress { get; set; }
        public int? CountryId { get; set; }
        public string BeneficiaryName { get; set; }
        public string BankAddress { get; set; }
        public int? ClearingCodePrefixId { get; set; }
        public bool Rejected { get; set; }
        public int? RejectedByAuthUserId { get; set; }
        public DateTime? RejectedDateTime { get; set; }
        public bool? IsOwnAccount { get; set; }
    }
}
