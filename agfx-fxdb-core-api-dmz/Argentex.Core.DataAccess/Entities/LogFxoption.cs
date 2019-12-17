using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogFxoption
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
        public int? VerifiedByAuthUserId { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CurrencyPair { get; set; }
        public int? FxoptionTypeId { get; set; }
        public int? FxoptionSettlementId { get; set; }
        public int? FxoptionStatusId { get; set; }
        public int? Lhsccyid { get; set; }
        public int? Rhsccyid { get; set; }
        public decimal? ClientLhsamt { get; set; }
        public decimal? ClientRhsamt { get; set; }
        public decimal? ProtectedLevel { get; set; }
        public decimal? PercentagePart { get; set; }
        public decimal? Premium { get; set; }
        public int? SettlementTradeId { get; set; }
        public DateTime? SettledDate { get; set; }
        public decimal? ExtBarrier { get; set; }
        public DateTime? ExtBarrierDate { get; set; }
        public DateTime? ExtValueDate { get; set; }
        public decimal? ExtStrike { get; set; }
        public decimal? LevBarrier { get; set; }
        public DateTime? LevBarrierDate { get; set; }
        public DateTime? LevValueDate { get; set; }
        public decimal? LevStrike { get; set; }
        public decimal? Profit { get; set; }
        public int? TransactionCommitId { get; set; }
        public int? ClientCompanyOpiid { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? CommPaidOutDate { get; set; }
        public bool? Verified { get; set; }
        public bool? IsBuy { get; set; }
        public bool? IsExtended { get; set; }
        public bool? IsLeveraged { get; set; }
        public bool? Deleted { get; set; }
        public bool? IsRhsmajour { get; set; }
        public decimal? OptionTrigger { get; set; }
        public decimal? OptionTriggerProtecLvl { get; set; }
        public decimal? BestCaseRate { get; set; }
        public decimal? WorstCaseRate { get; set; }
        public decimal? KnockOutRate { get; set; }
        public decimal? KnockInRate { get; set; }
        public decimal? LevNotional { get; set; }
        public bool? IsExpired { get; set; }
        public string ParentCode { get; set; }
        public bool? IsGenerated { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public DateTime? ValueDate { get; set; }
        public decimal? Barrier { get; set; }
        public decimal? ForwardRate { get; set; }
        public decimal? ClientLhsamtNotional { get; set; }
        public decimal? ClientRhsamtNotional { get; set; }
        public string GraphImgTemplateFile { get; set; }
        public bool? IsKnockedIn { get; set; }
        public bool? IsKnockedOut { get; set; }
    }
}
