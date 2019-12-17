using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class PaymentSwiftoutgoingStatus
    {
        public PaymentSwiftoutgoingStatus()
        {
            Payment = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Status { get; set; }
        public bool IsSwiftRejected { get; set; }

        public ICollection<Payment> Payment { get; set; }
    }
}
