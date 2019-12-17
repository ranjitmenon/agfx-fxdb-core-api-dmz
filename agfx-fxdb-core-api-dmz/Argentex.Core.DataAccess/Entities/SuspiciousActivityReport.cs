using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SuspiciousActivityReport
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string TradeCode { get; set; }
        public string PaymentCode { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public byte[] UpdateTimestamp { get; set; }
        public bool IsSendNotification { get; set; }
        public int? CreatedByAuthUserId { get; set; }
        public DateTime? DateTimeReceivedByMlro { get; set; }
        public string ResearchUnderTakenDescription { get; set; }
        public string CustomerInformation { get; set; }
        public string DocumentsInvestigatedInformation { get; set; }
        public string Conlusions { get; set; }
        public bool IsReportMadeToNca { get; set; }
        public DateTime? NcareportDateTime { get; set; }
        public bool IsAcknowledgementReceived { get; set; }
        public DateTime? AcknowledgementReceivedDateTime { get; set; }
        public string ConsentNcareceivedDescription { get; set; }
        public string ReasonNcareportNotMade { get; set; }
        public bool IsIssueClosed { get; set; }
        public DateTime? IssueClosedDateTime { get; set; }
        public int? IssueClosedByAuthUserId { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public int? UpdatedByAuthUserId { get; set; }

        public AuthUser CreatedByAuthUser { get; set; }
        public AuthUser IssueClosedByAuthUser { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
    }
}
