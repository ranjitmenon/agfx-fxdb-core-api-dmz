using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class EmirreportIncomingFileContent
    {
        public EmirreportIncomingFileContent()
        {
            EmirreportIncomingFile = new HashSet<EmirreportIncomingFile>();
        }

        public int Id { get; set; }
        public string FileContent { get; set; }

        public ICollection<EmirreportIncomingFile> EmirreportIncomingFile { get; set; }
    }
}
