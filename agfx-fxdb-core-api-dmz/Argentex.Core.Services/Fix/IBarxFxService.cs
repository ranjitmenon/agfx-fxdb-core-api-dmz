using Argentex.Core.Service.Models.Fix;
using System;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Fix
{
    public interface IBarxFxService : IDisposable
    {
        Task<FixQuoteResponseModel> GetQuoteAsync(FixQuoteRequestModel quoteRequest);
        Task<FixNewOrderResponseModel> NewOrderSingleAsync(FixNewOrderRequestModel dealRequest);
        void SetHttpTimeout(TimeSpan timeout);
    }
}
