using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftvalidationOptionField
    {
        public int OptionId { get; set; }
        public int FieldId { get; set; }
        public int Sequence { get; set; }

        public SwiftvalidationField Field { get; set; }
        public SwiftvalidationOption Option { get; set; }
    }
}
