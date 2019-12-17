using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Argentex.Core.Api.Models.Statements;
using Argentex.Core.Service.Models.Statements;
using Argentex.Core.Service.Statements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SynetecLogger;

namespace Argentex.Core.Api.Controllers.Statements
{
    [Produces("application/json")]
    [Route("api/statements")]
    [Authorize]
    public class StatementsController : Controller
    {
        private readonly IStatementService _statementService;
        private readonly ILogWrapper _logger;

        public StatementsController(IStatementService statementService, ILogWrapper logger)
        {
            _statementService = statementService;
            _logger = logger;
        }

        [HttpGet("company-statements/{clientCompanyId:int}")]
        public IActionResult GetStatements(int clientCompanyId, DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                return BadRequest("Start date cannot be posterior to end date");

            return Ok(_statementService.GetStatements(clientCompanyId, startDate, endDate));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _statementService.Dispose();
            }
        }
    }
}