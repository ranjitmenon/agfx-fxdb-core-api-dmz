using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Emirstatus
    {
        public Emirstatus()
        {
            ArmfxForwardTradeStatusesHistory = new HashSet<ArmfxForwardTradeStatusesHistory>();
            ArmreportFxforwardTrade = new HashSet<ArmreportFxforwardTrade>();
            EmirreportFxforwardTrade = new HashSet<EmirreportFxforwardTrade>();
            FixApatradeCapture = new HashSet<FixApatradeCapture>();
            FxforwardTradeApastatus = new HashSet<FxforwardTrade>();
            FxforwardTradeArmstatus = new HashSet<FxforwardTrade>();
            FxforwardTradeEmirstatus = new HashSet<FxforwardTrade>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }

        public ICollection<ArmfxForwardTradeStatusesHistory> ArmfxForwardTradeStatusesHistory { get; set; }
        public ICollection<ArmreportFxforwardTrade> ArmreportFxforwardTrade { get; set; }
        public ICollection<EmirreportFxforwardTrade> EmirreportFxforwardTrade { get; set; }
        public ICollection<FixApatradeCapture> FixApatradeCapture { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeApastatus { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeArmstatus { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeEmirstatus { get; set; }
    }
}
