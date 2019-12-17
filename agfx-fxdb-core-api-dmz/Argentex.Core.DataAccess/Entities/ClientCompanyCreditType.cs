using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyCreditType
    {
        public ClientCompanyCreditType()
        {
            ClientCompany = new HashSet<ClientCompany>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }

        public ICollection<ClientCompany> ClientCompany { get; set; }
    }
}
