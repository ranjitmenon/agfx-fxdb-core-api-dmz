using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxforwardTradeInvoice
    {
        public int Id { get; set; }
        public string TradeCode { get; set; }
        public string FileName { get; set; }
        public int? FileSize { get; set; }
        public string Comment { get; set; }
        public int? DocumentId { get; set; }
        public DateTime? UploadedDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }

        public FxforwardTradeInvoice IdNavigation { get; set; }
        public FxforwardTrade TradeCodeNavigation { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
        public FxforwardTradeInvoice InverseIdNavigation { get; set; }
    }
}
