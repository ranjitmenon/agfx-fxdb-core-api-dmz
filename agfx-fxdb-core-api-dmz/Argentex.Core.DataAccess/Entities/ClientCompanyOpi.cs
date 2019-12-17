using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyOpi
    {
        public ClientCompanyOpi()
        {
            Breach = new HashSet<Breach>();
            ClientCompanyCurrencyDefaultOpi = new HashSet<ClientCompanyCurrencyDefaultOpi>();
            ClientCompanyOpiduplicateDuplicateClientCompanyOpi = new HashSet<ClientCompanyOpiduplicate>();
            ClientCompanyOpiduplicateOriginalClientCompanyOpi = new HashSet<ClientCompanyOpiduplicate>();
            ClientCompanyOpitransaction = new HashSet<ClientCompanyOpitransaction>();
            ClientSiteAction2ClientCompanyOpi = new HashSet<ClientSiteAction2ClientCompanyOpi>();
            FxforwardTrade = new HashSet<FxforwardTrade>();
            FxforwardTrade2Opi = new HashSet<FxforwardTrade2Opi>();
            Fxoption = new HashSet<Fxoption>();
        }

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
        public bool IsCompanyAccount { get; set; }
        public string BeneficiaryAddress { get; set; }
        public int? CountryId { get; set; }
        public string BeneficiaryName { get; set; }
        public string BankAddress { get; set; }
        public int? ClearingCodePrefixId { get; set; }
        public bool Rejected { get; set; }
        public int? RejectedByAuthUserId { get; set; }
        public DateTime? RejectedDateTime { get; set; }
        public bool IsOwnAccount { get; set; }
        public bool IsDeleted { get; set; }

        public ClearingCodePrefix ClearingCodePrefix { get; set; }
        public ClientCompany ClientCompany { get; set; }
        public Country Country { get; set; }
        public Currency Currency { get; set; }
        public AuthUser RejectedByAuthUser { get; set; }
        public ICollection<Breach> Breach { get; set; }
        public ICollection<ClientCompanyCurrencyDefaultOpi> ClientCompanyCurrencyDefaultOpi { get; set; }
        public ICollection<ClientCompanyOpiduplicate> ClientCompanyOpiduplicateDuplicateClientCompanyOpi { get; set; }
        public ICollection<ClientCompanyOpiduplicate> ClientCompanyOpiduplicateOriginalClientCompanyOpi { get; set; }
        public ICollection<ClientCompanyOpitransaction> ClientCompanyOpitransaction { get; set; }
        public ICollection<ClientSiteAction2ClientCompanyOpi> ClientSiteAction2ClientCompanyOpi { get; set; }
        public ICollection<FxforwardTrade> FxforwardTrade { get; set; }
        public ICollection<FxforwardTrade2Opi> FxforwardTrade2Opi { get; set; }
        public ICollection<Fxoption> Fxoption { get; set; }
    }
}
