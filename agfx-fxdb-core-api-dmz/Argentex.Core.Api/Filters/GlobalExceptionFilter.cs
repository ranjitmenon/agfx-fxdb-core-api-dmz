using Argentex.Core.Api.Filters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SynetecLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Argentex.Core.Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogWrapper _logger;

        public GlobalExceptionFilter(ILogWrapper logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var response = new ErrorResponse()
            {
                Data = context.Exception.Message //temp
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                DeclaredType = typeof(ErrorResponse)
            };

            _logger.Error(context.Exception);
        }
    }
}
