using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Argentex.Core.Api.Models
{
    public class ResponseModel
    {
        private ResponseModel() { }
        public IDictionary<string, string[]> ResponseMessages { get; set; }

        public static ResponseModel ResponseWithErrors(params string[] errors) =>
            new ResponseModel
            {
                ResponseMessages = new Dictionary<string, string[]>
                {
                    { "Errors", errors}
                }
            };

        public static ResponseModel ResponseWithInfo(params string[] messages) =>
            new ResponseModel
            {
                ResponseMessages = new Dictionary<string, string[]>
                {
                    { "Info", messages}
                }
            };

        public static ResponseModel ResponseFromIdentityModel(IdentityResult result)
        {
            if (result.Succeeded)
                throw new ArgumentOutOfRangeException(nameof(result), "Unable to generate Response model for a successful Identity result");
            return ResponseWithErrors(result.Errors.Select(e => e.Description).ToArray());
        }

        public static ResponseModel ResponseFromInvalidModelState(ModelStateDictionary modelState)
            => ResponseWithErrors(modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray());
    }
}
