using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class EmirreportFxforwardTrade
    {
        public EmirreportFxforwardTrade()
        {
            EmirreportTradeResponseError = new HashSet<EmirreportTradeResponseError>();
        }

        public int Id { get; set; }
        public int EmirreportId { get; set; }
        public string FxforwardTradeCode { get; set; }
        public int EmirreportTypeId { get; set; }
        public int EmirstatusId { get; set; }
        public DateTime EmirstatusUpdatedDateTime { get; set; }

        public Emirreport Emirreport { get; set; }
        public EmirreportType EmirreportType { get; set; }
        public Emirstatus Emirstatus { get; set; }
        public FxforwardTrade FxforwardTradeCodeNavigation { get; set; }
        public ICollection<EmirreportTradeResponseError> EmirreportTradeResponseError { get; set; }
    }
}
