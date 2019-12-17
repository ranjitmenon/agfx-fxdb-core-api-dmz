using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyStatus
    {
        public ClientCompanyStatus()
        {
            ClientCompany = new HashSet<ClientCompany>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsLeadStatus { get; set; }

        public ICollection<ClientCompany> ClientCompany { get; set; }
    }
}
