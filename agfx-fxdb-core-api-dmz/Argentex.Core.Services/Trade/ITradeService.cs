using Argentex.Core.Service.Models.Fix;
using Argentex.Core.Service.Models.Trade;
using Argentex.Core.Service.Models.Trades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Trade
{
    public interface ITradeService : IDisposable
    {
        IList<TradeModel> GetUnsettledTrades(int clientCompanyId);
        IEnumerable<string> GetCurrencyCodes();
        IEnumerable<string> GetAllowedCurrencyPairs();
        Task<FxForwardTradeInformationModel> GetTradeNote(string tradeCode);
        FxForwardTradeInformationModel GetTradeInformation(string tradeCode);
        Task<IList<FixQuoteResponseModel>> GetQuotesAsync(QuoteRequestModel quoteRequest);
        Task<IList<DealResponseModel>> Deal(DealRequestModel dealRequest);
        bool SetTradeDefaultOPI(string tradeCode, int clientCompanyId, bool setAsDefault);
        IList<TradeModel> GetClosedTrades(int clientCompanyId);
        decimal GetTradeBalance(int clientCompanyId, string tradeCode);
    }
}
