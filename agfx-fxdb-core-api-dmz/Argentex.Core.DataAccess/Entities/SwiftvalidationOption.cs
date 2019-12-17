using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftvalidationOption
    {
        public SwiftvalidationOption()
        {
            SwiftvalidationCurrencyCountry = new HashSet<SwiftvalidationCurrencyCountry>();
            SwiftvalidationOptionField = new HashSet<SwiftvalidationOptionField>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<SwiftvalidationCurrencyCountry> SwiftvalidationCurrencyCountry { get; set; }
        public ICollection<SwiftvalidationOptionField> SwiftvalidationOptionField { get; set; }
    }
}
