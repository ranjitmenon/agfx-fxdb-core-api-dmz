namespace Argentex.Core.Service.Models.Trades
{
    public class DealResponseModel
    {
        public int TradeIndex { get; set; }
        public string Code { get; set; }
        public string BarclaysAssignedId { get; set; }
        public string BarclaysTradeId { get; set; }
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
}