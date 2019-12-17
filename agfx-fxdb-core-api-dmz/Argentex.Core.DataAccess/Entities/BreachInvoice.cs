using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class BreachInvoice
    {
        public int Id { get; set; }
        public int BreachId { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string Comment { get; set; }
        public string DocumentId { get; set; }
        public DateTime? UploadedDateTime { get; set; }
        public int UploadedByAuthUserId { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }

        public Breach Breach { get; set; }
        public BreachInvoice IdNavigation { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
        public AuthUser UploadedByAuthUser { get; set; }
        public BreachInvoice InverseIdNavigation { get; set; }
    }
}
