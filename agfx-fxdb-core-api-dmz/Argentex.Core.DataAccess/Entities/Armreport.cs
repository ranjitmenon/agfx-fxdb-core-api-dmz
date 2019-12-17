using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Armreport
    {
        public Armreport()
        {
            ArmfxForwardTradeStatusesHistory = new HashSet<ArmfxForwardTradeStatusesHistory>();
            ArmreportFxforwardTrade = new HashSet<ArmreportFxforwardTrade>();
        }

        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? ArmreportOutgoingFileId { get; set; }

        public ArmreportOutgoingFile ArmreportOutgoingFile { get; set; }
        public ICollection<ArmfxForwardTradeStatusesHistory> ArmfxForwardTradeStatusesHistory { get; set; }
        public ICollection<ArmreportFxforwardTrade> ArmreportFxforwardTrade { get; set; }
    }
}
