using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service
{
    public class ClientCompanyModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Crn { get; set; }
        public int? DealerAppUserID { get; set; }
        public string Description { get; set; }
        public string TradingName { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string WebsiteURL { get; set; }
        public string Address { get; set; }
        public int? ClientCompanyTypeID { get; set; }
        public int ClientCompanyStatusID { get; set; }
        public int UpdatedByAuthUserID { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string ImportantNote { get; set; }
        public int? ClientCompanyCategoryID { get; set; }
        public bool IsHouseAccount { get; set; }
        public string PostCode { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public bool? IsKYC { get; set; }
        public bool? IsTandCs { get; set; }
        public bool? IsRiskWarning { get; set; }
        public int? ClientCompanyOptionStatusID { get; set; }
        public DateTime? ApprovedOptionDateTime { get; set; }
        public bool IsPitched { get; set; }
        public int? PitchedByAppUserID { get; set; }
        public DateTime? PitchedDateTime { get; set; }
        public DateTime? AccountFormsSentDateTime { get; set; }
        public bool IsInternalAccount { get; set; }
        public string QualifiedNewTradeCode { get; set; }
        public string TradingAddress { get; set; }
        public int? MaxOpenGBP { get; set; }
        public int? MaxTradeSizeGBP { get; set; }
        public int? MaxTenorMonths { get; set; }
        public decimal? MaxCreditLimit { get; set; }
        public string TradingPostCode { get; set; }
        public string EMIR_LEI { get; set; }
        public bool? EMIR_EEA { get; set; }
        public bool? AssignNewTrades { get; set; }
        public int? ClientCompanyIndustrySectorID { get; set; }
        public int ClientCompanySalesRegionID { get; set; }
        public string SpreadsNote { get; set; }
        public int? ClientCompanyLinkedGroupID { get; set; }
        public bool IsExcludedFromEMoney { get; set; }
        public DateTime? FirstTradeDate { get; set; }
        public int ClientCompanyCreditTypeID { get; set; }
    }
}
