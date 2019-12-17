using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyLinkedGroup
    {
        public ClientCompanyLinkedGroup()
        {
            ClientCompany = new HashSet<ClientCompany>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }

        public AuthUser UpdatedByAuthUser { get; set; }
        public ICollection<ClientCompany> ClientCompany { get; set; }
    }
}
