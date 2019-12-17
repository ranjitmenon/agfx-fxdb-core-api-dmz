using System;
using System.Linq;
using Argentex.Core.Api.Models.ClientCompanies;
using Argentex.Core.Service;
using Argentex.Core.Service.ClientCompanies;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Models.ClientCompany;
using Argentex.Core.Service.Settlements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SynetecLogger;

namespace Argentex.Core.Api.Controllers.ClientCompanies
{
    [Produces("application/json")]
    [Route("api/client-company-accounts")]
    public class ClientCompanyAccountsController : Controller
    {
        private readonly IClientCompanyAccountsService _clientCompanyAccountsService;
        private readonly ISettlementService _settlementService;
        private readonly ILogWrapper _logger;

        public ClientCompanyAccountsController(IClientCompanyAccountsService clientCompanyAccountsService,
            ISettlementService settlementService,
            ILogWrapper logger)
        {
            _clientCompanyAccountsService = clientCompanyAccountsService;
            _settlementService = settlementService;
            _logger = logger;
        }

        [HttpGet("{clientCompanyId:int}")]
        public IActionResult GetClientCompanyAccounts(int clientCompanyId)
        {
            try
            {
                var accounts = _clientCompanyAccountsService.GetClientCompanyAccounts(clientCompanyId);

                if (!accounts.Any())
                    return NoContent();

                var mappedAccounts = accounts.Select(MapClientCompanyAccount);

                return Ok(mappedAccounts);
            }
            catch (ClientCompanyNotFoundException e)
            {
                _logger.Error(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("client-company-account/{clientCompanyOpiId:int}")]
        public IActionResult GetClientCompanyAccount(int clientCompanyOpiId)
        {
            return Ok(_clientCompanyAccountsService.GetClientCompanyAccount(clientCompanyOpiId));
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddClientCompanyAccount([FromBody] SettlementAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (String.IsNullOrEmpty(model.AccountNumber.ToString()) && String.IsNullOrEmpty(model.Iban))
            {
                return BadRequest("Both Account name and IBAN are empty.");
            }

            _clientCompanyAccountsService.AddSettlementAccount(model);

            return Ok();
        }

        [HttpPost("edit")]
        public IActionResult EditClientCompanyAccount([FromBody] SettlementAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(model.AccountNumber.ToString()) && string.IsNullOrEmpty(model.Iban))
            {
                return BadRequest("Both Account name and IBAN are empty.");
            }

            _clientCompanyAccountsService.EditSettlementAccount(model);

            return Ok();
        }

        [HttpGet]
        [Route("clearing-code-prefixes")]
        public IActionResult GetClearingCodePrefixes()
        {
            return Ok(_clientCompanyAccountsService.GetClearingCodePrefixes());
        }

        [HttpPost("set-as-default")]
        public IActionResult SetAccountAsDefault([FromBody] SetDefaultAccountModel model)
        {
            _clientCompanyAccountsService.SetAccountAsDefault(model);
            return Ok();
        }
        
        [HttpGet("trade-opis/{*tradeCode}")]
        public IActionResult GetTradeOPIs(string tradeCode)
        {
            return Ok(_clientCompanyAccountsService.GetTradeOPIs(tradeCode));
        }

        [HttpPost("trade-opis/add")]
        public IActionResult AddTradeOPI([FromBody] FXForwardTrade2OPIModel model)
        {
            _clientCompanyAccountsService.AddTradeOPI(model);
            return Ok();
        }

        [HttpDelete("trade-opis/delete/{opiTradeAllocationID:long}")]
        public IActionResult DeleteTradeOPIAllocation(long opiTradeAllocationID)
        {
            _settlementService.DeleteAssignedSettlements(opiTradeAllocationID);

            return Ok();
        }

        private static ClientCompanyAccountDto MapClientCompanyAccount(ClientCompanyAccountModel account)
        {
            return new ClientCompanyAccountDto
            {
                ClientCompanyOpiId = account.ClientCompanyOpiId,
                ClientCompanyId = account.ClientCompanyId,
                AccountName = account.AccountName,
                AccountNumber = account.AccountNumber,
                Currency = account.Currency
            };
        }

        [HttpDelete("assigned-settlements/{clientCompanyOpiId:int}")]
        public IActionResult DeleteSettlementAccount(int clientCompanyOpiId, [FromQuery] int authUserId)
        {
            if (clientCompanyOpiId.Equals(null) || clientCompanyOpiId.Equals(0))
                return BadRequest("There is no Client Company OPI related to the provided ID.");

            _clientCompanyAccountsService.DeleteSettlementAccount(clientCompanyOpiId, authUserId);

            return Ok();
        }

        [HttpGet("assigned-settlements/count/{*clientCompanyOpiId}")]
        public IActionResult GetNumberOfAssignedSettlements(int clientCompanyOpiId)
        {
            if (clientCompanyOpiId.Equals(null) || clientCompanyOpiId.Equals(0))
                return BadRequest("There is no Client Company OPI related to the provided ID.");

            int numberOfAssignedSettlements = _clientCompanyAccountsService.GetNumberOfAssociatedTrades(clientCompanyOpiId);

            return Ok(numberOfAssignedSettlements);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clientCompanyAccountsService.Dispose();
            }
        }
    }
}
