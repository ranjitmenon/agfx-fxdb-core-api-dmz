using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogSwiftincomingMatchedAccount
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public string MatchingContent { get; set; }
        public int ClientCompanyId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int? UpdatedByAuthUserId { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int? ChecksumMatchingContent { get; set; }
    }
}
