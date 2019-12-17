using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class EmirreportIncomingFile
    {
        public int Id { get; set; }
        public int EmirreportId { get; set; }
        public string Zipfilename { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Xmlfilename { get; set; }
        public int? EmirreportIncomingFileContentId { get; set; }

        public Emirreport Emirreport { get; set; }
        public EmirreportIncomingFileContent EmirreportIncomingFileContent { get; set; }
    }
}
