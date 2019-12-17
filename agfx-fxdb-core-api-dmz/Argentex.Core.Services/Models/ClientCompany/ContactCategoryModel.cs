using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RestSharp.Validation;

namespace Argentex.Core.Service.Models.ClientCompany
{
    public class ContactCategoryModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int?  Sequence { get; set; }
    }
}
