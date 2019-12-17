using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftvalidationMessageField
    {
        public SwiftvalidationMessageField()
        {
            SwiftvalidationCurrencyMessageField = new HashSet<SwiftvalidationCurrencyMessageField>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<SwiftvalidationCurrencyMessageField> SwiftvalidationCurrencyMessageField { get; set; }
    }
}
