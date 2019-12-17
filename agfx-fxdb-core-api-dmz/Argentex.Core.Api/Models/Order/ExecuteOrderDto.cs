using System;

namespace Argentex.Core.Api.Models
{
    public class ExecuteOrderDto
    {
        public string SellCcy { get; set; }
        public string BuyCcy { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? ValueDate { get; set; }
        public decimal ReciprocalValue { get; set; }
        public string AuthUserName { get; set; }
        public int ClientCompanyId { get; set; }
        public decimal? ClientSellAmount { get; set; }
        public decimal? ClientRate { get; set; }
    }
}
