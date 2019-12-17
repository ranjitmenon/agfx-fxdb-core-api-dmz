using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftincomingFileProcessingStatus
    {
        public SwiftincomingFileProcessingStatus()
        {
            SwiftincomingFile = new HashSet<SwiftincomingFile>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public ICollection<SwiftincomingFile> SwiftincomingFile { get; set; }
    }
}
