using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientSiteAction2FxforwardTrade2Opi
    {
        public long Id { get; set; }
        public long ClientSiteActionId { get; set; }
        public long FxforwardTrade2Opiid { get; set; }

        public ClientSiteAction ClientSiteAction { get; set; }
        public FxforwardTrade2Opi FxforwardTrade2Opi { get; set; }
    }
}
