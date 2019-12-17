using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftvalidationCurrencyMessageField
    {
        public int CurrencyId { get; set; }
        public int MessageId { get; set; }
        public int MessageFieldId { get; set; }

        public Currency Currency { get; set; }
        public SwiftvalidationMessage Message { get; set; }
        public SwiftvalidationMessageField MessageField { get; set; }
    }
}
