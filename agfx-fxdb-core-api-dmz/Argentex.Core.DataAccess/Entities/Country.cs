using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Country
    {
        public Country()
        {
            BankAccountCurrencyDetails = new HashSet<BankAccountCurrencyDetails>();
            ClientCompanyCompliance = new HashSet<ClientCompanyCompliance>();
            ClientCompanyOpi = new HashSet<ClientCompanyOpi>();
            ClientCompanyOpitransaction = new HashSet<ClientCompanyOpitransaction>();
            CountryClearingCodePrefix = new HashSet<CountryClearingCodePrefix>();
            SwiftvalidationCurrencyCountry = new HashSet<SwiftvalidationCurrencyCountry>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FormalName { get; set; }
        public string CodeIso2 { get; set; }
        public string CodeIso3 { get; set; }
        public string PhoneCode { get; set; }
        public int? CodeIso3numeric { get; set; }
        public int Sequence { get; set; }
        public int? CountryGroupId { get; set; }
        public int? LengthIban { get; set; }
        public string RegexBban { get; set; }
        public bool IsEea { get; set; }

        public CountryGroup CountryGroup { get; set; }
        public ICollection<BankAccountCurrencyDetails> BankAccountCurrencyDetails { get; set; }
        public ICollection<ClientCompanyCompliance> ClientCompanyCompliance { get; set; }
        public ICollection<ClientCompanyOpi> ClientCompanyOpi { get; set; }
        public ICollection<ClientCompanyOpitransaction> ClientCompanyOpitransaction { get; set; }
        public ICollection<CountryClearingCodePrefix> CountryClearingCodePrefix { get; set; }
        public ICollection<SwiftvalidationCurrencyCountry> SwiftvalidationCurrencyCountry { get; set; }
    }
}
