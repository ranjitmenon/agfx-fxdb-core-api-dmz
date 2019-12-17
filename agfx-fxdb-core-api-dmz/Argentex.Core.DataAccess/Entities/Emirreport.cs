using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Emirreport
    {
        public Emirreport()
        {
            EmirreportFxforwardTrade = new HashSet<EmirreportFxforwardTrade>();
            EmirreportIncomingFile = new HashSet<EmirreportIncomingFile>();
        }

        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? EmirreportOutgoingFileId { get; set; }

        public EmirreportOutgoingFile EmirreportOutgoingFile { get; set; }
        public ICollection<EmirreportFxforwardTrade> EmirreportFxforwardTrade { get; set; }
        public ICollection<EmirreportIncomingFile> EmirreportIncomingFile { get; set; }
    }
}
