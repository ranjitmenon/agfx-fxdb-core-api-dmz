﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Argentex.Core.Service.Attributes
{
    public class BoolRequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool;
        }
    }
}
