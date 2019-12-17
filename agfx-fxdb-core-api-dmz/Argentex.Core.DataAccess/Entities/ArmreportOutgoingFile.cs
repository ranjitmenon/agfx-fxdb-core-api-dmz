using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ArmreportOutgoingFile
    {
        public ArmreportOutgoingFile()
        {
            Armreport = new HashSet<Armreport>();
        }

        public int Id { get; set; }
        public string Csvfilename { get; set; }
        public string UploadedFilename { get; set; }
        public DateTime? UploadedDateTime { get; set; }
        public int? ArmreportOutgoingFileContentId { get; set; }

        public ArmreportOutgoingFileContent ArmreportOutgoingFileContent { get; set; }
        public ICollection<Armreport> Armreport { get; set; }
    }
}
