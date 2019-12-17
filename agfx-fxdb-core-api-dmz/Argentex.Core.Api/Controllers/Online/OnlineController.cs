using Argentex.Core.Service.ClientSiteAction;
using Microsoft.AspNetCore.Mvc;
using SynetecLogger;
using Microsoft.AspNetCore.Authorization;
using Argentex.Core.Service.Models.ClientSiteAction;

namespace Argentex.Core.Api.Controllers.ClientSiteAction
{
    [Produces("application/json")]
    [Route("api/online")]
    public class OnlineController : Controller
    {
        private readonly ILogWrapper _logger;

        public OnlineController()
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetSignal()
        {
            return Ok("Core API connection: Successful");
        }
    
    }
}
