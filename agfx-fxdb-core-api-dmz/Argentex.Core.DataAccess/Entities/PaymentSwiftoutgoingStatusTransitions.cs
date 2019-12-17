using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class PaymentSwiftoutgoingStatusTransitions
    {
        public int FromStatusId { get; set; }
        public int ToStatusId { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
