using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.Api.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string SiteUrl { get; set; }
    }
}
