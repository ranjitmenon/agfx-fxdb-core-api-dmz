using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class PaymentType
    {
        public PaymentType()
        {
            Payment = new HashSet<Payment>();
            SwiftvalidationField = new HashSet<SwiftvalidationField>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool RequiresApproval { get; set; }
        public bool CanBeSwift { get; set; }
        public bool DefaultSendToSwift { get; set; }

        public ICollection<Payment> Payment { get; set; }
        public ICollection<SwiftvalidationField> SwiftvalidationField { get; set; }
    }
}
