using System;
using Argentex.Core;
using Argentex.Core.DataAccess.Entities;


namespace Argentex.Core.Service.Models.Email
{
    public class PaymentNotificationModel
    {
        public string PaymentTypeDescription { get; set; }
        public string PaymentCode { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime ValueDate { get; set; }
        public string Reference { get; set; }

        public Currency Currency { get; set; }
        public Argentex.Core.DataAccess.Entities.ClientCompany ClientCompany { get; set; }
        public ClientCompanyOpi ClientCompanyOpi { get; set; }

    }
}
