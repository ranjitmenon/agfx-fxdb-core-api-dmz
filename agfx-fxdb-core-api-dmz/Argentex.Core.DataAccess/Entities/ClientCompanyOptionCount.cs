using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyOptionCount
    {
        public int ClientCompanyId { get; set; }
        public int OptionCount { get; set; }

        public ClientCompany ClientCompany { get; set; }
    }
}
