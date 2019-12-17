using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Argentex.Core.Api.Models;
using Argentex.Core.Service;
using Argentex.Core.Service.Models.ClientCompany;
using Microsoft.AspNetCore.Mvc;
using SynetecLogger;

namespace Argentex.Core.Api.Controllers.Client
{
    [Produces("application/json")]
    [Route("api/client-company-contact")]
    public class ClientCompanyContactController : Controller
    {
        private readonly IClientCompanyService _clientCompanyContactService;
        private readonly ILogWrapper _logger;

        public ClientCompanyContactController(IClientCompanyService clientCompanyContactService, ILogWrapper logger)
        {
            _clientCompanyContactService = clientCompanyContactService;
            _logger = logger;
        }        

        [HttpGet]
        [Route("company-name/{clientCompanyId:int}")]
        public IActionResult GetCompanyName(int clientCompanyId)
        {
            return Ok(_clientCompanyContactService.GetClientCompanyName(clientCompanyId));

        }

        [HttpPost]
        [Route("categories")]
        public IActionResult AddContactCategory([FromBody] ContactCategoryModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel.ResponseFromInvalidModelState(ModelState));

            var succeeded = _clientCompanyContactService.AddContactCategory(model);

            if (succeeded)
                return Ok(ResponseModel.ResponseWithInfo($"Contact category {model.Description} created successfully"));

            return BadRequest(
                ResponseModel.ResponseWithErrors($"Contact category {model.Description} could not be added"));
        }

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetContactCategories()
        {
            try
            {
                return Ok(await _clientCompanyContactService.GetContactCategories());
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return BadRequest(ResponseModel.ResponseWithErrors("Contact Categories could not be retrieved"));
            }
        }

        [HttpGet]
        [Route("{clientCompanyContactId:int}/categories/")]
        public async Task<IActionResult> GetClientCompanyContactCategories(int clientCompanyContactId)
        {
            try
            {
                return Ok(await _clientCompanyContactService.GetClientCompanyContactCategories(clientCompanyContactId));
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return BadRequest(ResponseModel.ResponseWithErrors(
                    $"ClientCompanyContactCategories could not be retrieved for ClientCompanyContactId {clientCompanyContactId}"));
            }
        }

        [HttpGet]
        [Route("categories/{contactCategoryId:int}")]
        public IActionResult GetContactCategory(int contactCategoryId)
        {
            try
            {
                return Ok(_clientCompanyContactService.GetContactCategory(contactCategoryId));
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return BadRequest(
                    ResponseModel.ResponseWithErrors($"Contact Category {contactCategoryId} could not be retrieved"));
            }
        }

        [HttpGet]
        [Route("categories/{*contactCategoryDescription}")]
        public IActionResult GetContactCategory(string contactCategoryDescription)
        {
            try
            {
                return Ok(_clientCompanyContactService.GetContactCategory(contactCategoryDescription));
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return BadRequest(
                    ResponseModel.ResponseWithErrors(
                        $"Contact Category {contactCategoryDescription} could not be retrieved"));
            }
        }

        [HttpPut]
        [Route("categories")]
        public IActionResult ProcessClientCompanyContactCategories([FromBody] ClientCompanyContactBulkCategoryModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ResponseModel.ResponseFromInvalidModelState(ModelState));

            bool succeeded = _clientCompanyContactService.ProcessClientCompanyContactCategories(model);
            if (succeeded)
                return Ok(ResponseModel.ResponseWithInfo(
                    $"Client Company Contact Categories have been processed successfully for Company Contact Id {model.ClientCompanyContactId}"));

            return BadRequest(ResponseModel.ResponseWithErrors(
                $"Client Company Contact Categories could not be processed successfully for Company Contact Id {model.ClientCompanyContactId}"));
        }

        [HttpGet]
        [Route("contact")]
        public IActionResult GetClientCompanyContact([FromQuery] ClientCompanyContactSearchContext clientCompanyContactSearchContext)
        {
            try
            {
                var companyContact = _clientCompanyContactService.GetClientCompanyContact(clientCompanyContactSearchContext);

                if (companyContact.CompanyContactModel == null)
                    return NotFound(_clientCompanyContactService.GetErrorMessages(HttpStatusCode.NotFound, null, clientCompanyContactSearchContext));

                return Ok(companyContact);
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
                return BadRequest(_clientCompanyContactService.GetErrorMessages(HttpStatusCode.BadRequest, exception, clientCompanyContactSearchContext));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clientCompanyContactService.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}