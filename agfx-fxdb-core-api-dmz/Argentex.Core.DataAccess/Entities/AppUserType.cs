using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AppUserType
    {
        public AppUserType()
        {
            AppUser = new HashSet<AppUser>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string HomePage { get; set; }
        public int? CommissionTypeId { get; set; }
        public int ClientCompanySalesRegionId { get; set; }

        public ClientCompanySalesRegion ClientCompanySalesRegion { get; set; }
        public CommissionType CommissionType { get; set; }
        public ICollection<AppUser> AppUser { get; set; }
    }
}
