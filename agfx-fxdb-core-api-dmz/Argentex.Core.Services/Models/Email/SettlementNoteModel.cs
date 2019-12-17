using System;

namespace Argentex.Core.Service.Models.Email
{
    public class SettlementNoteModel
    {
        public string ParentTradeCode { get; set; }
        public string TradedCurrency { get; set; }
        public decimal Amount { get; set; }
        public string InstructedBy { get; set; }
        public DateTime InstructedDateTime { get; set; }
        public DateTime ValueDate { get; set; }
        public string AccountName { get; set; }
        public decimal? SettlementAmount { get; set; }
        public string AccountCurrency { get; set; }
    }
}
