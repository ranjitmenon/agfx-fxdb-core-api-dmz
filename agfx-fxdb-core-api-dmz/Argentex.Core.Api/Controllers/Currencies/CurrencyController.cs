using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using SynetecLogger;

namespace Argentex.Core.Api.Controllers.Currencies
{
    [Produces("application/json")]
    [Route("api/currencies")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;
        private readonly ILogWrapper _logger;

        public CurrencyController(ICurrencyService currencyService, ILogWrapper logger)
        {
            _currencyService = currencyService;
            _logger = logger;
        }

        [HttpGet("currency-pair-rate")]
        public IActionResult GetCurrencyPairRate(string currencyPair)
        {
            try
            {
                var rate = _currencyService.GetCurrencyPairRate(currencyPair);
                return Ok(rate);
            }
            catch (CurrencyPairPricingNotFoundException e)
            {
                _logger.Error(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("")]
        public IActionResult GetCurrencies()
        {
            return Ok(_currencyService.GetCurrencies());
        }

        /// <summary>
        /// This is currently in trade and should be updated in client site to use this one
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //[Route("currency-codes")]
        //public IActionResult GetCurrencyCodes()
        //{
        //    return Ok(_tradeService.GetCurrencyCodes());
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _currencyService.Dispose();
            }
        }
    }
}
