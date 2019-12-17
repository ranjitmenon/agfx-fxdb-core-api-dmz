using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanySalesAppUser
    {
        public int ClientCompanyId { get; set; }
        public int SalesPersonAppUserId { get; set; }
        public int SalesOrder { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }

        public ClientCompany ClientCompany { get; set; }
        public AppUser SalesPersonAppUser { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
    }
}
