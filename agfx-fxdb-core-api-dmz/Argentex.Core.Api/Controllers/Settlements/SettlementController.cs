using Argentex.Core.Service.Models.Settlements;
using Argentex.Core.Service.Settlements;
using Microsoft.AspNetCore.Mvc;
using SynetecLogger;
using System.Threading.Tasks;

namespace Argentex.Core.Api.Controllers.Settlements
{
    [Produces("application/json")]
    [Route("api/settlement")]
    public class SettlementController : Controller
    {
        private readonly ISettlementService _settlementService;
        private readonly ILogWrapper _logger;

        public SettlementController(ISettlementService settlementService, ILogWrapper logger)
        {
            _settlementService = settlementService;
            _logger = logger;
        }

        [HttpGet]
        [Route("payment-information")]
        public IActionResult GetPaymentOutInformation(string paymentCode, bool isPaymentOut = false)
        {
            var paymentInformation = _settlementService.GetPaymentInformation(paymentCode, isPaymentOut);

            return Ok(paymentInformation);
        }

        [HttpPost]
        [Route("assign")]
        public async  Task<IActionResult> Assign([FromBody] AssignSettlementRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = await _settlementService.AssignAsync(model);

            return Ok(response);
        }

        [HttpGet]
        [Route("assigned-settlements/{*tradeCode}")]
        public IActionResult GetAssignedSettlements(string tradeCode)
        {
            var assignedSettlements = _settlementService.GetAssignedSettlements(tradeCode);

            return Ok(assignedSettlements);
        }

        [HttpDelete]
        [Route("delete/{settlementId:int}")]
        public IActionResult DeleteAssignedSettlements(int settlementId)
        {
            if (!ModelState.IsValid) return BadRequest();

            _settlementService.DeleteAssignedSettlements(settlementId);

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _settlementService.Dispose();
            }
        }
    }
}
