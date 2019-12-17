using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientSiteAction2ClientCompanyOpi
    {
        public long Id { get; set; }
        public long ClientSiteActionId { get; set; }
        public int ClientCompanyOpiid { get; set; }

        public ClientCompanyOpi ClientCompanyOpi { get; set; }
        public ClientSiteAction ClientSiteAction { get; set; }
    }
}
