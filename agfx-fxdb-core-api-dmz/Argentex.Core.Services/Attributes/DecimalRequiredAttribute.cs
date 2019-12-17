using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Argentex.Core.Service.Attributes
{
    public class DecimalRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is decimal && (decimal)value >= 0m && (decimal) value <= decimal.MaxValue;
        }
    }
}
