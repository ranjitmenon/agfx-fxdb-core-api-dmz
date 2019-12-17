using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class PaymentRecReason
    {
        public PaymentRecReason()
        {
            Payment = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDebit { get; set; }

        public ICollection<Payment> Payment { get; set; }
    }
}
