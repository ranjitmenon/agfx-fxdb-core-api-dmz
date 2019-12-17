using System;

namespace Argentex.Core.Service.Models.ClientCompany
{
    public class FXForwardTrade2OPIModel
    {
        public long ID { get; set; }
        public string FXForwardTradeCode { get; set; }
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByAuthUserID { get; set; }
        public string CreatedByAuthUserName { get; set; }
        public bool IsClient { get; set; }
        public DateTime ValueDate { get; set; }
    }
}
