﻿using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogSwiftincomingFileStatement
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int SwiftincomingFileId { get; set; }
        public int FilePartNumber { get; set; }
        public int? PaymentId { get; set; }
        public string RawContentLine61 { get; set; }
        public string RawContentLine86 { get; set; }
        public string MatchingContent { get; set; }
        public string DisplayError { get; set; }
        public string ProcessingError { get; set; }
        public bool MatchedProvisionally { get; set; }
    }
}
