using Argentex.Core.Service;
using Argentex.Core.Service.Models.ClientCompany;
using Microsoft.AspNetCore.Mvc;
using SynetecLogger;
using System;
using System.Net;

namespace Argentex.Core.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/client-company")]
    public class ClientCompanyController : Controller
    {
        private readonly IClientCompanyService _clientCompanyService;
        private readonly ILogWrapper _logger;

        public ClientCompanyController(IClientCompanyService clientCompanyService, ILogWrapper logger)
        {
            _clientCompanyService = clientCompanyService;
            _logger = logger;
        }
        [HttpGet]
        [Route("{clientCompanyId:int}")]
        public IActionResult GetCompanyName(int clientCompanyId)
        {
            return Ok(_clientCompanyService.GetClientCompany(clientCompanyId));

        }

        [HttpGet]
        [Route("accounts/{clientCompanyId:int}")]
        public IActionResult GetClientCompanyAccounts(int clientCompanyId)
        {
            return Ok(_clientCompanyService.GetClientCompanyAccounts(clientCompanyId));
        }

        [HttpGet]
        [Route("online-details/{clientCompanyId:int}")]
        public IActionResult GetClientCompanyOnlineDetails(int clientCompanyId)
        {
            return Ok(_clientCompanyService.GetClientCompanyOnlineDetailsModel(clientCompanyId));
        }

        [HttpPost]
        [Route("spread-adjustment/add")]
        public IActionResult AddClientCompanyAccount([FromBody] SpreadAdjustmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _clientCompanyService.AddSpredAdjustment(model);

            return Ok();
        }

        [HttpPost]
        [Route("set-kicked/{clientCompanyId:int}")]
        public IActionResult SetKicked(int clientCompanyId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _clientCompanyService.SetKicked(clientCompanyId);

            return Ok();
        }

        [HttpGet]
        [Route("contacts/{clientCompanyId:int}")]
        public IActionResult GetCompanyContactList(int clientCompanyId)
        {
            try
            {
                var contactList = _clientCompanyService.GetCompanyContactList(clientCompanyId);

                return Ok(contactList);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return BadRequest(_clientCompanyService.GetErrorMessagesForContactList(HttpStatusCode.BadRequest, exception, clientCompanyId));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clientCompanyService.Dispose();
                base.Dispose(disposing);
            }
        }

    }
}
