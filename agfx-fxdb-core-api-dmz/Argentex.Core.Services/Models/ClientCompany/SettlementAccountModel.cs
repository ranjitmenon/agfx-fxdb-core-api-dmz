using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Service.Models.ClientCompany
{
    public class SettlementAccountModel
    {
        public int ClientCompanyOpiId { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CurrencyId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }

        public string BankName { get; set; }

        public string BankAddress { get; set; }

        [Required]
        public string AccountName { get; set; }

        public int? AccountNumber { get; set; }

        public int? ClearingCodePrefixId { get; set; }

        public string SortCode { get; set; }

        public string Reference { get; set; }

        public string SwiftCode { get; set; }

        public string Iban { get; set; }

        public string BeneficiaryName { get; set; }

        public string BeneficiaryAddress { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int UpdatedByAuthUserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int ClientCompanyId { get; set; }

    }
}
