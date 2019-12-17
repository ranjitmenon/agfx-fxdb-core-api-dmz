using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxforwardTrade
    {
        public FxforwardTrade()
        {
            ArmfxForwardTradeStatusesHistory = new HashSet<ArmfxForwardTradeStatusesHistory>();
            ArmreportFxforwardTrade = new HashSet<ArmreportFxforwardTrade>();
            BankAccountTransaction = new HashSet<BankAccountTransaction>();
            Breach = new HashSet<Breach>();
            ClientCompany = new HashSet<ClientCompany>();
            ClientSiteAction2FixFxforwardTrade = new HashSet<ClientSiteAction2FixFxforwardTrade>();
            EmirreportFxforwardTrade = new HashSet<EmirreportFxforwardTrade>();
            FixApatradeCapture = new HashSet<FixApatradeCapture>();
            FixApatradeMessage = new HashSet<FixApatradeMessage>();
            FixFxforwardTradeOrder = new HashSet<FixFxforwardTradeOrder>();
            FxforwardTrade2Opi = new HashSet<FxforwardTrade2Opi>();
            FxforwardTradeInvoice = new HashSet<FxforwardTradeInvoice>();
            FxswapDeliveryLegTradeCodeNavigation = new HashSet<Fxswap>();
            FxswapParentTradeCodeNavigation = new HashSet<Fxswap>();
            FxswapReversalLegTradeCodeNavigation = new HashSet<Fxswap>();
            Payment = new HashSet<Payment>();
            VirtualAccountTransaction = new HashSet<VirtualAccountTransaction>();
        }

        public string Code { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
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
        public bool ProfitConsolidated { get; set; }
        public bool Deleted { get; set; }
        public int? TransactionCommitId { get; set; }
        public int? ClientCompanyOpiid { get; set; }
        public decimal? ProfitConsolidatedValue { get; set; }
        public DateTime? ProfitConsolidatedDateTime { get; set; }
        public DateTime? SettledDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime? CommPaidOutDate { get; set; }
        public string Notes { get; set; }
        public decimal? ProfitGbprate { get; set; }
        public decimal? PrevailingRate2 { get; set; }
        public string EmirUti { get; set; }
        public bool EmirReported { get; set; }
        public DateTime? EmirReportedDateTime { get; set; }
        public string Reference { get; set; }
        public decimal? RemainingClientLhsamt { get; set; }
        public decimal? RemainingClientRhsamt { get; set; }
        public decimal? MarkToMarketValue { get; set; }
        public decimal? BrokenDatePrice { get; set; }
        public DateTime? MarkToMarketValueUpdatedDateTime { get; set; }
        public bool? IsComplianceSupported { get; set; }
        public bool? IsComplianceRegulated { get; set; }
        public int? ComplianceTradeReasonId { get; set; }
        public bool? IsEmirreportable { get; set; }
        public int? EmirstatusId { get; set; }
        public DateTime? EmirstatusUpdatedDateTime { get; set; }
        public bool EmirDelegatedReported { get; set; }
        public DateTime? FilledDateTime { get; set; }
        public DateTime? ContractNoteSentToClientDateTime { get; set; }
        public DateTime? ContractNoteSentToMyselfDateTime { get; set; }
        public string ComplianceIsin { get; set; }
        public string EmirsubmissionId { get; set; }
        public string EmirdelegatedSubmissionId { get; set; }
        public decimal? PrevDayMarktoMarket { get; set; }
        public DateTime? PrevDayMarktoMarketUpdatedDateTime { get; set; }
        public bool? IsApareportable { get; set; }
        public int? ApastatusId { get; set; }
        public DateTime? ApastatusUpdatedDateTime { get; set; }
        public bool? IsArmreportable { get; set; }
        public bool Armreported { get; set; }
        public DateTime? ArmstatusUpdatedDateTime { get; set; }
        public DateTime? BrokeredDate { get; set; }
        public int? ArmstatusId { get; set; }
        public string MarketSideUti { get; set; }
        public decimal? BdpforwardPoints { get; set; }
        public int? FilledByAuthUserId { get; set; }
        public bool FixNewOrder { get; set; }
        public int? OpiupdatedByAuthUserId { get; set; }
        public DateTime? OpiupdatedDateTime { get; set; }
        public int? BrokeredByAuthUserId { get; set; }
        public DateTime? OpenValueDate { get; set; }

        public Emirstatus Apastatus { get; set; }
        public Emirstatus Armstatus { get; set; }
        public ClientCompanyContact AuthorisedByClientCompanyContact { get; set; }
        public Broker Broker { get; set; }
        public AuthUser BrokeredByAuthUser { get; set; }
        public ClientCompany ClientCompanyNavigation { get; set; }
        public ClientCompanyOpi ClientCompanyOpi { get; set; }
        public ComplianceTradeReason ComplianceTradeReason { get; set; }
        public AuthUser CreatedByAuthUser { get; set; }
        public Emirstatus Emirstatus { get; set; }
        public AuthUser FilledByAuthUser { get; set; }
        public FxforwardTradeStatus FxforwardTradeStatus { get; set; }
        public Currency Lhsccy { get; set; }
        public AuthUser OpiupdatedByAuthUser { get; set; }
        public Currency Rhsccy { get; set; }
        public TradeInstructionMethod TradeInstructionMethod { get; set; }
        public TransactionCommit TransactionCommit { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
        public AuthUser VerifiedByAuthUser { get; set; }
        public ICollection<ArmfxForwardTradeStatusesHistory> ArmfxForwardTradeStatusesHistory { get; set; }
        public ICollection<ArmreportFxforwardTrade> ArmreportFxforwardTrade { get; set; }
        public ICollection<BankAccountTransaction> BankAccountTransaction { get; set; }
        public ICollection<Breach> Breach { get; set; }
        public ICollection<ClientCompany> ClientCompany { get; set; }
        public ICollection<ClientSiteAction2FixFxforwardTrade> ClientSiteAction2FixFxforwardTrade { get; set; }
        public ICollection<EmirreportFxforwardTrade> EmirreportFxforwardTrade { get; set; }
        public ICollection<FixApatradeCapture> FixApatradeCapture { get; set; }
        public ICollection<FixApatradeMessage> FixApatradeMessage { get; set; }
        public ICollection<FixFxforwardTradeOrder> FixFxforwardTradeOrder { get; set; }
        public ICollection<FxforwardTrade2Opi> FxforwardTrade2Opi { get; set; }
        public ICollection<FxforwardTradeInvoice> FxforwardTradeInvoice { get; set; }
        public ICollection<Fxswap> FxswapDeliveryLegTradeCodeNavigation { get; set; }
        public ICollection<Fxswap> FxswapParentTradeCodeNavigation { get; set; }
        public ICollection<Fxswap> FxswapReversalLegTradeCodeNavigation { get; set; }
        public ICollection<Payment> Payment { get; set; }
        public ICollection<VirtualAccountTransaction> VirtualAccountTransaction { get; set; }
    }
}
