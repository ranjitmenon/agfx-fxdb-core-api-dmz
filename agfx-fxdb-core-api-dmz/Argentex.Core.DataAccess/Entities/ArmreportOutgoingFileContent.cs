using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ArmreportOutgoingFileContent
    {
        public ArmreportOutgoingFileContent()
        {
            ArmreportOutgoingFile = new HashSet<ArmreportOutgoingFile>();
        }

        public int Id { get; set; }
        public string FileContent { get; set; }

        public ICollection<ArmreportOutgoingFile> ArmreportOutgoingFile { get; set; }
    }
}
