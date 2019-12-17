using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftvalidationFieldComponent
    {
        public SwiftvalidationFieldComponent()
        {
            SwiftvalidationFieldFieldComponent = new HashSet<SwiftvalidationFieldFieldComponent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<SwiftvalidationFieldFieldComponent> SwiftvalidationFieldFieldComponent { get; set; }
    }
}
