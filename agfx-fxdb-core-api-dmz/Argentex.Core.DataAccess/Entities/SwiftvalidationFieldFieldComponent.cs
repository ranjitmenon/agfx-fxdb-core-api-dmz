using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftvalidationFieldFieldComponent
    {
        public int FieldId { get; set; }
        public int FieldComponentId { get; set; }
        public int LineNumber { get; set; }
        public int Sequence { get; set; }

        public SwiftvalidationField Field { get; set; }
        public SwiftvalidationFieldComponent FieldComponent { get; set; }
    }
}
