using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyOpitransaction
    {
        public int Id { get; set; }
        public int ClientCompanyOpiid { get; set; }
        public int CurrencyId { get; set; }
        public decimal? Amount { get; set; }
        public int PaymentId { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public string Opireference { get; set; }
        public string Opidescription { get; set; }
        public string OpiaccountName { get; set; }
        public string OpiaccountNumber { get; set; }
        public string OpibankName { get; set; }
        public string OpibankAddress { get; set; }
        public string OpisortCode { get; set; }
        public string OpiswiftCode { get; set; }
        public string Opiiban { get; set; }
        public string OpibeneficiaryName { get; set; }
        public string OpibeneficiaryAddress { get; set; }
        public bool OpidetailsUpdated { get; set; }
        public int? OpicountryId { get; set; }

        public ClientCompanyOpi ClientCompanyOpi { get; set; }
        public Currency Currency { get; set; }
        public Country Opicountry { get; set; }
        public Payment Payment { get; set; }
    }
}
