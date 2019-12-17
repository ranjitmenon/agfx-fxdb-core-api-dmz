using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class CommissionType
    {
        public CommissionType()
        {
            AppUserType = new HashSet<AppUserType>();
            Commission = new HashSet<Commission>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public double DefaultCommissionRate { get; set; }

        public ICollection<AppUserType> AppUserType { get; set; }
        public ICollection<Commission> Commission { get; set; }
    }
}
