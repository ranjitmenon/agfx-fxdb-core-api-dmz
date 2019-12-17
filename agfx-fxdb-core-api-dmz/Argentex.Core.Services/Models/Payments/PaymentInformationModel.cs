using System;

namespace Argentex.Core.Service.Models.Payments
{
    public class PaymentInformationModel
    {
        public string PaymentCode { get; set; }
        public string PaymentType { get; set; }
        public DateTime ValueDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string Currency { get; set; }
    }
}
