using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientSiteAction
    {
        public ClientSiteAction()
        {
            ClientSiteAction2ClientCompanyOpi = new HashSet<ClientSiteAction2ClientCompanyOpi>();
            ClientSiteAction2FixFxforwardTrade = new HashSet<ClientSiteAction2FixFxforwardTrade>();
            ClientSiteAction2FxforwardTrade2Opi = new HashSet<ClientSiteAction2FxforwardTrade2Opi>();
            ClientSiteAction2Fxswap = new HashSet<ClientSiteAction2Fxswap>();
        }

        public long Id { get; set; }
        public int ClientSiteActionTypeId { get; set; }
        public int ClientSiteActionStatusId { get; set; }
        public string Details { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public byte[] UpdatedTimestamp { get; set; }

        public ClientSiteActionStatus ClientSiteActionStatus { get; set; }
        public ClientSiteActionType ClientSiteActionType { get; set; }
        public AuthUser CreatedByAuthUser { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
        public ICollection<ClientSiteAction2ClientCompanyOpi> ClientSiteAction2ClientCompanyOpi { get; set; }
        public ICollection<ClientSiteAction2FixFxforwardTrade> ClientSiteAction2FixFxforwardTrade { get; set; }
        public ICollection<ClientSiteAction2FxforwardTrade2Opi> ClientSiteAction2FxforwardTrade2Opi { get; set; }
        public ICollection<ClientSiteAction2Fxswap> ClientSiteAction2Fxswap { get; set; }
    }
}
