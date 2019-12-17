using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogFxforwardTrade
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public int ClientCompanyId { get; set; }
        public int? AuthorisedByClientCompanyContactId { get; set; }
        public int? TradeInstructionMethodId { get; set; }
        public int? BrokerId { get; set; }
        public bool Verified { get; set; }
        public int? VerifiedByAuthUserId { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool? IsOrder { get; set; }
        public string CurrencyPair { get; set; }
        public bool IsBuy { get; set; }
        public int? Lhsccyid { get; set; }
        public int? Rhsccyid { get; set; }
        public decimal? ClientRate { get; set; }
        public decimal? ClientLhsamt { get; set; }
        public decimal? ClientRhsamt { get; set; }
        public decimal? BrokerRate { get; set; }
        public decimal? BrokerLhsamt { get; set; }
        public decimal? BrokerRhsamt { get; set; }
        public decimal? CollateralPerc { get; set; }
        public int FxforwardTradeStatusId { get; set; }
        public bool? IsRhsmajor { get; set; }
        public decimal? Profit { get; set; }
        public bool? ProfitConsolidated { get; set; }
        public bool? Deleted { get; set; }
        public int? TransactionCommitId { get; set; }
        public int? ClientCompanyOpiid { get; set; }
        public decimal? ProfitConsolidatedValue { get; set; }
        public DateTime? ProfitConsolidatedDateTime { get; set; }
        public DateTime? SettledDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public string Notes { get; set; }
        public decimal? ProfitGbprate { get; set; }
        public decimal? PrevailingRate2 { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public DateTime? CommPaidOutDate { get; set; }
        public string EmirUti { get; set; }
        public bool? EmirReported { get; set; }
        public DateTime? EmirReportedDateTime { get; set; }
        public string Reference { get; set; }
        public decimal? RemainingClientLhsamt { get; set; }
        public decimal? RemainingClientRhsamt { get; set; }
        public decimal? MarkToMarketValue { get; set; }
        public decimal? BrokenDatePrice { get; set; }
        public DateTime? MarkToMarketValueUpdatedDateTime { get; set; }
        public bool? IsComplianceSupported { get; set; }
        public bool? IsComplianceRegulated { get; set; }
        public bool? ComplianceTradeReasonId { get; set; }
        public DateTime? FilledDateTime { get; set; }
        public DateTime? ContractNoteSentToClientDateTime { get; set; }
        public DateTime? ContractNoteSentToMyselfDateTime { get; set; }
        public string ComplianceIsin { get; set; }
        public string EmirsubmissionId { get; set; }
        public string EmirdelegatedSubmissionId { get; set; }
        public decimal? PrevDayMarktoMarket { get; set; }
        public DateTime? PrevDayMarktoMarketUpdatedDateTime { get; set; }
        public string MarketSideUti { get; set; }
        public decimal? BdpforwardPoints { get; set; }
        public int? FilledByAuthUserId { get; set; }
        public int? OpiupdatedByAuthUserId { get; set; }
        public DateTime? OpiupdatedDateTime { get; set; }
        public int? BrokeredByAuthUserId { get; set; }
        public DateTime? BrokeredDate { get; set; }
        public DateTime? OpenValueDate { get; set; }
    }
}
