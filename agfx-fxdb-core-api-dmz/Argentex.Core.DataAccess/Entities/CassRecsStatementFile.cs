using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class CassRecsStatementFile
    {
        public CassRecsStatementFile()
        {
            CassRecs = new HashSet<CassRecs>();
        }

        public int Id { get; set; }
        public DateTime CassRecsDate { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string DocumentId { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public int UploadedByAuthUserId { get; set; }

        public ICollection<CassRecs> CassRecs { get; set; }
    }
}
