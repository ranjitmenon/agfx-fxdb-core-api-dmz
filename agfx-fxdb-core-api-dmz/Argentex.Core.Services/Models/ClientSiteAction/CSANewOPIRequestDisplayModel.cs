using System;

namespace Argentex.Core.Service.Models.ClientSiteAction
{
    [Serializable]
    public class CSANewOPIRequestDisplayModel
    {
        public string CompanyName { get; set; }
        public int CompanyID { get; set; }
        public string CurrencyCode { get; set; }
        public string OPIName { get; set; }
        public string Status { get; set; }
        public string CreatedByClientName { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
