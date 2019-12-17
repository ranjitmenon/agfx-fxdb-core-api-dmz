using System;
using Argentex.Core.Api.Models.Quotes;
using Argentex.Core.Service.Models.Fix;

namespace Argentex.Core.Api.Helpers
{
    public static class QuoteHelpers
    {
        public static FixQuoteRequestModel CreateFixQuoteRequestModel(QuoteRequestDto quoteRequest, string tradeCode)
        {
            return new FixQuoteRequestModel
            {
                TradeCode = tradeCode,
                LHSCCY = quoteRequest.LeftCurrency,
                RHSCCY = quoteRequest.RightCurrency,
                //MajorCurrency = quoteRequest.IsBuy ? quoteRequest.RightCurrency : quoteRequest.LeftCurrency,
                MajorCurrency = quoteRequest.IsRhsMajor ? quoteRequest.RightCurrency : quoteRequest.LeftCurrency,
                Side = quoteRequest.IsBuy ? 1 : 2,
                BrokerMajorAmount = quoteRequest.Amount,
                ValueDate = quoteRequest.ValueDate.ToString("yyyy-MM-dd"),
                TimeOut = 10000,
                Duration = 35
            };
        }
    }
}
