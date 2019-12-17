using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class EmirreportResponseCode
    {
        public EmirreportResponseCode()
        {
            EmirreportTradeResponseError = new HashSet<EmirreportTradeResponseError>();
        }

        public int Id { get; set; }
        public int ResponseCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Description { get; set; }

        public ICollection<EmirreportTradeResponseError> EmirreportTradeResponseError { get; set; }
    }
}
