using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service.Models.Payments
{
    public class PaymentOutResponseModel
    {
        public string Code { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
