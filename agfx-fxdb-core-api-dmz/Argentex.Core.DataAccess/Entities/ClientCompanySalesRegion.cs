using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanySalesRegion
    {
        public ClientCompanySalesRegion()
        {
            AppUserType = new HashSet<AppUserType>();
            ClientCompany = new HashSet<ClientCompany>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? PermissionId { get; set; }

        public ICollection<AppUserType> AppUserType { get; set; }
        public ICollection<ClientCompany> ClientCompany { get; set; }
    }
}
