using Argentex.Core.Service.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SynetecLogger;
using System.Threading.Tasks;

namespace Argentex.Core.Api.Controllers.Paymets
{
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly ILogWrapper _logger;
        private readonly IConfiguration _config;
        private readonly IPaymentsService _paymentService;

        public PaymentsController(
            ILogWrapper logger,
            IConfiguration config,
            IPaymentsService paymentService)
        {
            _logger = logger;
            _config = config;
            _paymentService = paymentService;
        }

        [HttpPost]
        [Route("notify-contacts/{paymentCode}")]
        public async Task<IActionResult> NotifyContacts(string paymentCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _paymentService.NotifyContacts(paymentCode);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _paymentService.Dispose();
                base.Dispose(disposing);
            }
        }

    }
}
