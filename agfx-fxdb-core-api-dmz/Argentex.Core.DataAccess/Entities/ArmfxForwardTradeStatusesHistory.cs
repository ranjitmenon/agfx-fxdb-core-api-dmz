using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ArmfxForwardTradeStatusesHistory
    {
        public int Id { get; set; }
        public int? ArmreportId { get; set; }
        public string FxForwardTradeCode { get; set; }
        public DateTime ArmstatusUpdatedDateTime { get; set; }
        public string ErrorDescription { get; set; }
        public int ArmstatusId { get; set; }

        public Armreport Armreport { get; set; }
        public Emirstatus Armstatus { get; set; }
        public FxforwardTrade FxForwardTradeCodeNavigation { get; set; }
    }
}
