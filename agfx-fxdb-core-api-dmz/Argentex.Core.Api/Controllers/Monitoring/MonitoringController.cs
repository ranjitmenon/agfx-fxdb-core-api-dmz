using Argentex.Core.Service.Models.Trades;
using Argentex.Core.Service.Monitoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argentex.Core.Api.Controllers.Monitoring
{
    [Route("api/monitoring")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private readonly IMonitoringService _monitoringService;

        public MonitoringController(IMonitoringService monitoringService)
        {
            _monitoringService = monitoringService;
        }

        [HttpGet]
        [Route("notify-trade-started/{authUserId:int}")]
        public async Task<IActionResult> NotifyTradeStarted(int authUserId)
        {
            var success = await _monitoringService.NotifyTradeStarted(authUserId);

            if (!success) return BadRequest();

            return Ok();
        }

        [HttpPost]
        [Route("check-execute-trade")]
        public async Task<IActionResult> CheckExecuteTrade([FromBody] TradeNotificationModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _monitoringService.CheckExecuteTrade(model));
        }

        [HttpGet]
        [Route("refresh-client-details")]
        public async Task<IActionResult> RefreshClientDetails()
        {
            if (!ModelState.IsValid) return BadRequest();
            await _monitoringService.RefreshClientDetails();
            return Ok();
        }
    }
}