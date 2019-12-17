using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Commission
    {
        public int Id { get; set; }
        public int CommissionTypeId { get; set; }
        public int AppUserId { get; set; }
        public double Commission1 { get; set; }

        public AppUser AppUser { get; set; }
        public CommissionType CommissionType { get; set; }
    }
}
