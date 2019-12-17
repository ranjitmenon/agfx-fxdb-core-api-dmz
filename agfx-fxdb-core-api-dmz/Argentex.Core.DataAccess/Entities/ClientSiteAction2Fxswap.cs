using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientSiteAction2Fxswap
    {
        public long Id { get; set; }
        public long ClientSiteActionId { get; set; }
        public int FxswapId { get; set; }

        public ClientSiteAction ClientSiteAction { get; set; }
        public Fxswap Fxswap { get; set; }
    }
}
