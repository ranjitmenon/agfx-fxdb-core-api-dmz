using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class EmirreportOutgoingFileContent
    {
        public EmirreportOutgoingFileContent()
        {
            EmirreportOutgoingFile = new HashSet<EmirreportOutgoingFile>();
        }

        public int Id { get; set; }
        public string FileContent { get; set; }

        public ICollection<EmirreportOutgoingFile> EmirreportOutgoingFile { get; set; }
    }
}
