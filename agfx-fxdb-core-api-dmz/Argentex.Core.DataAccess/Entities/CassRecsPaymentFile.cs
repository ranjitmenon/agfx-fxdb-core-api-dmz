using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class CassRecsPaymentFile
    {
        public int Id { get; set; }
        public DateTime CassRecsDate { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string DocumentId { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public int UploadedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }

        public AuthUser UpdatedByAuthUser { get; set; }
        public AuthUser UploadedByAuthUser { get; set; }
    }
}
