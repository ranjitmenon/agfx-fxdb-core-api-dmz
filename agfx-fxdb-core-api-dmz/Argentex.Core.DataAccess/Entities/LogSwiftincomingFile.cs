using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogSwiftincomingFile
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public string Filename { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? SwiftincomingFileTypeId { get; set; }
        public string Content { get; set; }
        public string ContentDecoded { get; set; }
        public string Laufilename { get; set; }
        public string LaufileContent { get; set; }
        public int SwiftincomingFileProcessingStatusId { get; set; }
        public string DisplayError { get; set; }
        public string ProcessingError { get; set; }
    }
}
