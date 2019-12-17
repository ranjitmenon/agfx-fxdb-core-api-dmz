using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service.Models.ClientCompany
{
    public class SpreadAdjustmentModel
    {
        public int ClientCompanyID { get; set; }
        public string BuyCcy { get; set; }
        public string SellCcy { get; set; }
        public bool? IsBuy { get; set; }
        public int SpreadAdjustment { get; set; }
        public int UpdatedByAuthUserId { get; set; }
    }
}
