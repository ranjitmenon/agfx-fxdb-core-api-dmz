using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class EmirreportTradeResponseError
    {
        public int Id { get; set; }
        public int EmirreportFxforwardTradeId { get; set; }
        public string Source { get; set; }
        public int? EmirreportResponseCodeId { get; set; }
        public int? ResponseCode { get; set; }
        public string ResponseMessage { get; set; }

        public EmirreportFxforwardTrade EmirreportFxforwardTrade { get; set; }
        public EmirreportResponseCode EmirreportResponseCode { get; set; }
    }
}
