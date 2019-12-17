using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FixApatradeCapture
    {
        public int Id { get; set; }
        public string TradeCode { get; set; }
        public string TradeReportId { get; set; }
        public int AuthUserId { get; set; }
        public string BloombergTradeId { get; set; }
        public short? AcknowledgeStatus { get; set; }
        public string RejectReason { get; set; }
        public string ErrorMessage { get; set; }
        public short? BloombergPublishIndicator { get; set; }
        public DateTime? PublishDateTime { get; set; }
        public int ApastatusId { get; set; }
        public DateTime ApastatusUpdatedDateTime { get; set; }

        public Emirstatus Apastatus { get; set; }
        public AuthUser AuthUser { get; set; }
        public FxforwardTrade TradeCodeNavigation { get; set; }
    }
}
