using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftvalidationField
    {
        public SwiftvalidationField()
        {
            SwiftvalidationFieldFieldComponent = new HashSet<SwiftvalidationFieldFieldComponent>();
            SwiftvalidationOptionField = new HashSet<SwiftvalidationOptionField>();
        }

        public int Id { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }
        public int? PaymentTypeId { get; set; }

        public PaymentType PaymentType { get; set; }
        public ICollection<SwiftvalidationFieldFieldComponent> SwiftvalidationFieldFieldComponent { get; set; }
        public ICollection<SwiftvalidationOptionField> SwiftvalidationOptionField { get; set; }
    }
}
