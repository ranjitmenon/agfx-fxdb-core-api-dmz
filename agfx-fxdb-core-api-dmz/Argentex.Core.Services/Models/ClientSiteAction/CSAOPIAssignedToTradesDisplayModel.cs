using System;

namespace Argentex.Core.Service.Models.ClientSiteAction
{
    public class CSAOPIsAssignedToTradesDisplayModel
    {
        public string CompanyName { get; set; }
        public int CompanyID { get; set; }
        public string TradeCode { get; set; }
        public string OPIName { get; set; }
        public decimal Amount { get; set; }
        public string CreatedByClientName { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
