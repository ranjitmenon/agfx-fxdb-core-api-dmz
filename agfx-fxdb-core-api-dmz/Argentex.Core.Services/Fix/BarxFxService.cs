using Argentex.ClientSite.Service.Http;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.Models.Fix;
using SynetecLogger;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Fix
{
    public class BarxFxService : IBarxFxService
    {
        private readonly IHttpService _httpService;
        private readonly IAppSettingService _appSettingService;
        private readonly ILogWrapper _logger;

        private bool _disposed;

        public BarxFxService(IHttpService httpService, IAppSettingService appSetting, ILogWrapper logger)
        {
            _httpService = httpService;
            _appSettingService = appSetting;
            _logger = logger;
        }

        public async Task<FixQuoteResponseModel> GetQuoteAsync(FixQuoteRequestModel quoteRequest)
        {
            var baseUri = _appSettingService.GetBarxFXFixQuoteUrl();
            var uri = _httpService.GenerateUri(baseUri, quoteRequest);
            _httpService.AddRequestUri(uri);
            _httpService.AddMethod(HttpMethod.Get);

            HttpResponseMessage result = null;
            try
            {
                result = await _httpService.SendAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Synetec FIX API is unreachable", ex.InnerException);
            }

            FixQuoteResponseModel quoteResponse = null;

            //Not valid request model
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                var responseObj = _httpService.GetResponseAsString(result);
                throw new HttpRequestException($"FIX API is unreachable. Reason: {responseObj.Result}");
            }

            //Not valid request model
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                var responseObj = _httpService.GetResponseAsString(result);
                throw new HttpRequestException($"Invalid http request to Synetec FIX API. Reason: {responseObj.Result}");
            }
            //BarxFX is not available
            if (result.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                quoteResponse = await _httpService.GetResponseObject<FixQuoteResponseModel>(result);
                throw new HttpRequestException($"BarxFX is not available. Reason: {quoteResponse?.ErrorMessage}");
            }
            //successfull http call
            if (result.StatusCode == HttpStatusCode.OK)
            {
                quoteResponse = await _httpService.GetResponseObject<FixQuoteResponseModel>(result);
                //unsuccessfull call from Synetec FIX API to BarxFX
                if(quoteResponse != null && quoteResponse.ErrorMessage != null)
                {
                    throw new HttpRequestException($"Error getting quote from BarxFX. Reason: {quoteResponse?.ErrorMessage}");
                }
            }

            return quoteResponse;
        }

        public async Task<FixNewOrderResponseModel> NewOrderSingleAsync(FixNewOrderRequestModel dealRequest)
        {
            var baseUri = _appSettingService.GetBarxFXFixNewOrderUrl();
            var uri = _httpService.GenerateUri(baseUri, dealRequest);
            _httpService.AddRequestUri(uri);
            _httpService.AddMethod(HttpMethod.Get);

            HttpResponseMessage result = null;
            try
            {
                result = await _httpService.SendAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.Error(ex);
                throw new HttpRequestException("Deal not done due to an unexpected error, please try again", ex.InnerException);
            }

            FixNewOrderResponseModel dealResponse = null;

            //Not valid request model
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                var responseObj = _httpService.GetResponseAsString(result);
                //dealResponse.ErrorMessage = $"Invalid http request to Synetec FIX API. Reason: {responseObj.Result}";
            }
            //BarxFX is not available
            if (result.StatusCode == HttpStatusCode.ServiceUnavailable || result.StatusCode == HttpStatusCode.NotFound)
            {
                dealResponse = await _httpService.GetResponseObject<FixNewOrderResponseModel>(result);
                //dealResponse.ErrorMessage = $"BarxFX is not available. Reason: {dealResponse?.ErrorMessage}";
                _logger.Error(new Exception($"BarxFX is not available. Reason: {dealResponse?.ErrorMessage}"));
                dealResponse.ErrorMessage = "Deal not done due to a communication failure, please try again";
            }
            //successfull http call
            if (result.StatusCode == HttpStatusCode.OK)
            {
                dealResponse = await _httpService.GetResponseObject<FixNewOrderResponseModel>(result);
                //unsuccessfull call from Synetec FIX API to BarxFX
                if (dealResponse != null && dealResponse.ErrorMessage != null)
                {
                    dealResponse.ErrorMessage = "Deal not done due to market movements causing variance in rates, please re-quote and try again";
                    _logger.Error(new Exception($"Error creating order from BarxFX. Reason: {dealResponse?.ErrorMessage}"));
                    //dealResponse.ErrorMessage = $"Error creating order from BarxFX. Reason: {dealResponse?.ErrorMessage}";
                }
            }

            return dealResponse;
        }

        public void SetHttpTimeout(TimeSpan timeout)
        {
            _httpService.AddTimeout(timeout);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
