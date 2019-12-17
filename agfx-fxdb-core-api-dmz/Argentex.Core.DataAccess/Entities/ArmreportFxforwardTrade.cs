using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ArmreportFxforwardTrade
    {
        public int Id { get; set; }
        public int ArmreportId { get; set; }
        public string FxforwardTradeCode { get; set; }
        public int ArmstatusId { get; set; }
        public bool IsResubmited { get; set; }
        public DateTime ReportedDateTime { get; set; }

        public Armreport Armreport { get; set; }
        public Emirstatus Armstatus { get; set; }
        public FxforwardTrade FxforwardTradeCodeNavigation { get; set; }
    }
}
