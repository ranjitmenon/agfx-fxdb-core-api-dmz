using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FixFxforwardTradeOrder
    {
        public int Id { get; set; }
        public string FxforwardCode { get; set; }
        public string BarclaysTradeId { get; set; }
        public string BarclaysAssignedId { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsFilled { get; set; }
        public string RejectReason { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }

        public FxforwardTrade FxforwardCodeNavigation { get; set; }
        public AuthUser User { get; set; }
    }
}
