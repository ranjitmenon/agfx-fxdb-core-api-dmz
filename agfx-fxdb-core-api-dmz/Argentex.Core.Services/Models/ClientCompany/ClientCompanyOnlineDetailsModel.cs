using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service
{
    public class ClientCompanyOnlineDetailsModel
    {
        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public bool AllowOnlineTrading { get; set; }
        public decimal? MaxTradeSize { get; set; }
        public decimal? MaxOpen { get; set; }
        public DateTime? MaxTenor { get; set; }
        public decimal? Collateral { get; set; }
        public int? SpotSpread { get; set; }
        public int? FwdSpread { get; set; }
        public bool? Kicked { get; set; }

        public string DealerFullName { get; set; }
        public string DealerPhoneNumber { get; set; }
        
    }
}
