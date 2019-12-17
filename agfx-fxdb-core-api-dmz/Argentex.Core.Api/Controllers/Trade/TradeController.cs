using Argentex.Core.Service.Trade;
using Argentex.Core.Service.Models.Trades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SynetecLogger;
using System.Threading.Tasks;
using System;

namespace Argentex.Core.Api.Controllers.Trade
{
    [Produces("application/json")]
    [Route("api/trade")]
    public class TradeController : Controller
    {
        private readonly ITradeService _tradeService;
        private readonly ILogWrapper _logger;

        public TradeController(ITradeService tradeService, ILogWrapper logger)
        {
            _tradeService = tradeService;
            _logger = logger;
        }

        [HttpGet]
        [Route("unsettled-trades/{clientCompanyId:int}")]
        public IActionResult GetUnsettledTrades(int clientCompanyId)
        {
            return Ok(_tradeService.GetUnsettledTrades(clientCompanyId));
        }

        [HttpGet]
        [Route("currency-codes")]
        public IActionResult GetCurrencyCodes()
        {
            return Ok(_tradeService.GetCurrencyCodes());
        }

        [HttpGet]
        [Route("allowed-currency-pairs")]
        public IActionResult GetAllowedCurrencyPairs()
        {
            return Ok(_tradeService.GetAllowedCurrencyPairs());
        }

        [HttpGet("trade-note")]
        public async Task<IActionResult> GetTradeNote(string tradeCode)
        {
            return Ok(await _tradeService.GetTradeNote(tradeCode));
        }

        [HttpGet("trade-information")]
        public IActionResult GetTradeInformation(string tradeCode)
        {
            return Ok(_tradeService.GetTradeInformation(tradeCode));
        }

        [HttpPost("trade-default-opi-set")]
        public IActionResult SetTradeDefaultOPI([FromQuery]string tradeCode, int clientCompanyId, bool setAsDefault)
        {
            return Ok(_tradeService.SetTradeDefaultOPI(tradeCode, clientCompanyId, setAsDefault));            
        }

        [HttpPost]
        [Route("quote")] 
        public async Task<IActionResult> Quote([FromBody] QuoteRequestModel quoteRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var quoteResponse = await _tradeService.GetQuotesAsync(quoteRequest);

            return Ok(quoteResponse);
        }

        [HttpPost]
        [Route("deal")]
        public async Task<IActionResult> Deal([FromBody] DealRequestModel dealRequest)
        {
            if (!ModelState.IsValid) return BadRequest();

            var dealResponse = await _tradeService.Deal(dealRequest);

            return Ok(dealResponse);
        }

        [HttpGet]
        [Route("closed-trades/{clientCompanyId:int}")]
        public IActionResult GetClosedTrades(int clientCompanyId)
        {
            return Ok(_tradeService.GetClosedTrades(clientCompanyId));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tradeService.Dispose();
            }
        }
    }
}