using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompany
    {
        public ClientCompany()
        {
            ClientCompanyCompliance = new HashSet<ClientCompanyCompliance>();
            ClientCompanyComplianceNote = new HashSet<ClientCompanyComplianceNote>();
            ClientCompanyContact = new HashSet<ClientCompanyContact>();
            ClientCompanyCurrencyDefaultOpi = new HashSet<ClientCompanyCurrencyDefaultOpi>();
            ClientCompanyIbrelationship = new HashSet<ClientCompanyIbrelationship>();
            ClientCompanyNote = new HashSet<ClientCompanyNote>();
            ClientCompanyOnlineDetails = new HashSet<ClientCompanyOnlineDetails>();
            ClientCompanyOpi = new HashSet<ClientCompanyOpi>();
            ClientCompanySalesAppUser = new HashSet<ClientCompanySalesAppUser>();
            ClientCompanyVirtualAccount = new HashSet<ClientCompanyVirtualAccount>();
            FxforwardTrade = new HashSet<FxforwardTrade>();
            Fxoption = new HashSet<Fxoption>();
            Payment = new HashSet<Payment>();
            SwiftincomingMatchedAccount = new HashSet<SwiftincomingMatchedAccount>();
        }

        public int Id { get; set; }
        public string Crn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TradingName { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address { get; set; }
        public int? ClientCompanyTypeId { get; set; }
        public int ClientCompanyStatusId { get; set; }
        public int? DealerAppUserId { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string ImportantNote { get; set; }
        public int? ClientCompanyCategoryId { get; set; }
        public bool IsHouseAccount { get; set; }
        public string PostCode { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public bool? IsKyc { get; set; }
        public bool? IsTandCs { get; set; }
        public bool? IsRiskWarning { get; set; }
        public int? ClientCompanyOptionStatusId { get; set; }
        public DateTime? ApprovedOptionDateTime { get; set; }
        public bool IsPitched { get; set; }
        public int? PitchedByAppUserId { get; set; }
        public DateTime? PitchedDateTime { get; set; }
        public DateTime? AccountFormsSentDateTime { get; set; }
        public bool IsInternalAccount { get; set; }
        public string QualifiedNewTradeCode { get; set; }
        public string TradingAddress { get; set; }
        public int? MaxOpenGbp { get; set; }
        public int? MaxTradeSizeGbp { get; set; }
        public int? MaxTenorMonths { get; set; }
        public decimal? MaxCreditLimit { get; set; }
        public string TradingPostCode { get; set; }
        public string EmirLei { get; set; }
        public bool? EmirEea { get; set; }
        public bool? AssignNewTrades { get; set; }
        public int? ClientCompanyIndustrySectorId { get; set; }
        public int ClientCompanySalesRegionId { get; set; }
        public string SpreadsNote { get; set; }
        public int? ClientCompanyLinkedGroupId { get; set; }
        public bool IsExcludedFromEmoney { get; set; }
        public DateTime? FirstTradeDate { get; set; }
        public int ClientCompanyCreditTypeId { get; set; }
        public DateTime? LastContractDate { get; set; }

        public ClientCompanyCategory ClientCompanyCategory { get; set; }
        public ClientCompanyCreditType ClientCompanyCreditType { get; set; }
        public ClientCompanyIndustrySector ClientCompanyIndustrySector { get; set; }
        public ClientCompanyLinkedGroup ClientCompanyLinkedGroup { get; set; }
        public ClientCompanySalesRegion ClientCompanySalesRegion { get; set; }
        public ClientCompanyStatus ClientCompanyStatus { get; set; }
        public ClientCompanyType ClientCompanyType { get; set; }
        public FxforwardTrade QualifiedNewTradeCodeNavigation { get; set; }
        public ClientCompanyOptionCount ClientCompanyOptionCount { get; set; }
        public ClientCompanyPipeline ClientCompanyPipeline { get; set; }
        public ClientCompanyTradeCount ClientCompanyTradeCount { get; set; }
        public ICollection<ClientCompanyCompliance> ClientCompanyCompliance { get; set; }
        public ICollection<ClientCompanyComplianceNote> ClientCompanyComplianceNote { get; set; }
        public ICollection<ClientCompanyContact> ClientCompanyContact { get; set; }
        public ICollection<ClientCompanyCurrencyDefaultOpi> ClientCompanyCurrencyDefaultOpi { get; set; }
        public ICollection<ClientCompanyIbrelationship> ClientCompanyIbrelationship { get; set; }
        public ICollection<ClientCompanyNote> ClientCompanyNote { get; set; }
        public ICollection<ClientCompanyOnlineDetails> ClientCompanyOnlineDetails { get; set; }
        public ICollection<ClientCompanyOpi> ClientCompanyOpi { get; set; }
        public ICollection<ClientCompanySalesAppUser> ClientCompanySalesAppUser { get; set; }
        public ICollection<ClientCompanyVirtualAccount> ClientCompanyVirtualAccount { get; set; }
        public ICollection<FxforwardTrade> FxforwardTrade { get; set; }
        public ICollection<Fxoption> Fxoption { get; set; }
        public ICollection<Payment> Payment { get; set; }
        public ICollection<SwiftincomingMatchedAccount> SwiftincomingMatchedAccount { get; set; }
    }
}
