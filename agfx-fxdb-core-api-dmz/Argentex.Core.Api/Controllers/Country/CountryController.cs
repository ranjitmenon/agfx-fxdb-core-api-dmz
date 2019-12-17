using Argentex.Core.Service.Country;
using Microsoft.AspNetCore.Mvc;

namespace Argentex.Core.Api.Controllers.Country
{
    [Produces("application/json")]
    [Route("api/country")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [Route("countries")]
        public IActionResult GetCountries()
        {
            return Ok(_countryService.GetCountries());
        }
    }
}