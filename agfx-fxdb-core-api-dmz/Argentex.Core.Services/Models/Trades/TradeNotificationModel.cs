using System;

namespace Argentex.Core.Service.Models.Trades
{
    public class TradeNotificationModel
    {
        public int ClientCompanyID { get; set; }
        public string ClientCompanyName { get; set; }
        public int AuthUserID { get; set; }
        public string AuthUserName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public decimal ClientRate { get; set; }        
        public string SellCcy { get; set; }
        public string BuyCcy { get; set; }
        public decimal ClientSellAmount { get; set; }
        public decimal ClientBuyAmount { get; set; } 
        public bool IsBuy { get; set; }
        public DateTime ValueDate { get; set; }
        public DateTime ContractDate { get; set; }        
        public decimal Spread { get; set; } 
        /// <summary>        
        /// Dealer ID for the specific company
        /// </summary>
        public int? DealerAppUserID { get; set; }

        public int TraderNotificationCounter { get; set; }

        public bool SendNotification { get; set; }
    }
}
