using Argentex.Core.Service.ClientSiteAction;
using Microsoft.AspNetCore.Mvc;
using SynetecLogger;
using Microsoft.AspNetCore.Authorization;
using Argentex.Core.Service.Models.ClientSiteAction;

namespace Argentex.Core.Api.Controllers.ClientSiteAction
{
    [Produces("application/json")]
    [Route("api/csa")]
    public class ClientSiteActionController : Controller
    {
        private readonly IClientSiteActionService _clientSiteActionService;
        private readonly ILogWrapper _logger;

        public ClientSiteActionController(IClientSiteActionService clientSiteActionService, ILogWrapper logger)
        {
            _clientSiteActionService = clientSiteActionService;
            _logger = logger;
        }

        [HttpGet]
        [Route("{clientSiteActionID:long}")]
        public IActionResult GetClientSiteAction(long clientSiteActionID)
        {
            return Ok(_clientSiteActionService.GetClientSiteAction(clientSiteActionID));
        }

        [HttpGet]
        [Route("trades-no-fix-confirmation")]
        public IActionResult GetTradesWithoutFIXConfirmation()
        {
            return Ok(_clientSiteActionService.GetTradesWithoutFIXConfirmation());
        }

        [HttpPut]
        public IActionResult UpdateClientSiteAction([FromBody] ClientSiteActionModel model)
        {
            _clientSiteActionService.UpdateClientSiteAction(model);
            return Ok();
        }

        [HttpGet]
        [Route("action-status/{actionStatusName}")]
        public IActionResult GetClientSiteActionStatus(string actionStatusName)
        {
            return Ok(_clientSiteActionService.GetClientSiteActionStatus(actionStatusName));
        }

        [HttpGet("opis-assigned-trade")]
        public IActionResult GetOPIsAssignedToTrades()
        {
            return Ok(_clientSiteActionService.GetOPIsAssignedToTrades());
        }

        [HttpGet("new-opi-requested")]
        public IActionResult GetNewOPIRequested()
        {
            return Ok(_clientSiteActionService.GetNewOPIRequested());
        }

        [HttpGet]
        [Route("csa-opi/{clientCompanyOPIID:int}")]
        public IActionResult GetClientSiteActionByOPIID(int clientCompanyOPIID)
        {
            return Ok(_clientSiteActionService.GetClientSiteActionByOPIID(clientCompanyOPIID));
        }

        [HttpGet("swaps")]
        public IActionResult GetSwaps()
        {
            return Ok(_clientSiteActionService.GetSwaps());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clientSiteActionService?.Dispose();
            }
        }
    }
}
