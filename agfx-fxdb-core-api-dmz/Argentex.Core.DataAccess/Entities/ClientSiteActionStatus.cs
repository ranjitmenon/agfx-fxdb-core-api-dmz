using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientSiteActionStatus
    {
        public ClientSiteActionStatus()
        {
            ClientSiteAction = new HashSet<ClientSiteAction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }

        public ICollection<ClientSiteAction> ClientSiteAction { get; set; }
    }
}
