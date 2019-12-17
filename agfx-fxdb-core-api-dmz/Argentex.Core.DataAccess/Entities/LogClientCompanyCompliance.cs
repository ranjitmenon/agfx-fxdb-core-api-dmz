using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogClientCompanyCompliance
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
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
        public bool? Regulated { get; set; }
        public int? ClassificationId { get; set; }
        public int? ReasonId { get; set; }
        public bool? Ttca { get; set; }
        public int? NatureId { get; set; }
        public bool? RequestInvoices { get; set; }
        public bool? ThirdPartyPayments { get; set; }
        public bool? DelegatedReporting { get; set; }
        public bool? IsMiFid { get; set; }
    }
}
