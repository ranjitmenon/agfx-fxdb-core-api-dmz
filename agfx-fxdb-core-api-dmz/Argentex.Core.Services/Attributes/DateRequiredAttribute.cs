using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Argentex.Core.Service.Attributes
{
    public class DateRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is DateTime 
                && ((DateTime)value).Date >= DateTime.UtcNow.Date;
        }
    }
}
