using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyCompliance
    {
        public ClientCompanyCompliance()
        {
            ClientCompanyComplianceCorporateSector = new HashSet<ClientCompanyComplianceCorporateSector>();
            ClientCompanyComplianceCurrency = new HashSet<ClientCompanyComplianceCurrency>();
            ComplianceClassificationFile = new HashSet<ComplianceClassificationFile>();
            ComplianceQuestionnaire = new HashSet<ComplianceQuestionnaire>();
        }

        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public int? AmlriskId { get; set; }
        public int? RegisteredDomicileCountryId { get; set; }
        public DateTime? RefreshDueDateTime { get; set; }
        public decimal? ExpectedTotalVolume { get; set; }
        public int? ExpectedFrequencyId { get; set; }
        public decimal? ExpectedMaxTradeSize { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public decimal? TurnoverGbp { get; set; }
        public decimal? BalanceSheetGbp { get; set; }
        public decimal? OwnFundsGbp { get; set; }
        public bool Regulated { get; set; }
        public int? ClassificationId { get; set; }
        public int? ReasonId { get; set; }
        public bool Ttca { get; set; }
        public int? NatureId { get; set; }
        public bool RequestInvoices { get; set; }
        public bool ThirdPartyPayments { get; set; }
        public bool DelegatedReporting { get; set; }
        public bool IsMiFid { get; set; }

        public Amlrisk Amlrisk { get; set; }
        public ComplianceClassification Classification { get; set; }
        public ClientCompany ClientCompany { get; set; }
        public ExpectedFrequency ExpectedFrequency { get; set; }
        public ComplianceNature Nature { get; set; }
        public ComplianceReason Reason { get; set; }
        public Country RegisteredDomicileCountry { get; set; }
        public ICollection<ClientCompanyComplianceCorporateSector> ClientCompanyComplianceCorporateSector { get; set; }
        public ICollection<ClientCompanyComplianceCurrency> ClientCompanyComplianceCurrency { get; set; }
        public ICollection<ComplianceClassificationFile> ComplianceClassificationFile { get; set; }
        public ICollection<ComplianceQuestionnaire> ComplianceQuestionnaire { get; set; }
    }
}
