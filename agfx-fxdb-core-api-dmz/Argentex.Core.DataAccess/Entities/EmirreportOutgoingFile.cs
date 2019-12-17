using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class EmirreportOutgoingFile
    {
        public EmirreportOutgoingFile()
        {
            Emirreport = new HashSet<Emirreport>();
        }

        public int Id { get; set; }
        public string Xmlfilename { get; set; }
        public string UploadedFilename { get; set; }
        public DateTime? UploadedDateTime { get; set; }
        public int? EmirreportOutgoingFileContentId { get; set; }

        public EmirreportOutgoingFileContent EmirreportOutgoingFileContent { get; set; }
        public ICollection<Emirreport> Emirreport { get; set; }
    }
}
