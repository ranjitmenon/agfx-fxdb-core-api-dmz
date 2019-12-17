namespace Argentex.Core.Service.Models.Payments
{
    public class PaymentOutInformationModel : PaymentInformationModel
    {
        public string OpiDescription { get; set; }
        public string OpiAccountName { get; set; }
        public string OpiSortCode { get; set; }
        public string OpiAccountNumber { get; set; }
        public string OpiBankName { get; set; }
        public string OpiSwiftCode { get; set; }
        public string OpiReference { get; set; }
        public string OpiIban { get; set; }
    }
}
