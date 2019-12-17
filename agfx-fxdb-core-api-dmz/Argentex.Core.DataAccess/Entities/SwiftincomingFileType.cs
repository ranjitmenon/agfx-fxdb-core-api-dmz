using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftincomingFileType
    {
        public SwiftincomingFileType()
        {
            SwiftincomingFile = new HashSet<SwiftincomingFile>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<SwiftincomingFile> SwiftincomingFile { get; set; }
    }
}
