using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SystemEmailSenderAddress
    {
        public int Id { get; set; }
        public string EmailKeyName { get; set; }
        public string EmailAddressValue { get; set; }
    }
}
