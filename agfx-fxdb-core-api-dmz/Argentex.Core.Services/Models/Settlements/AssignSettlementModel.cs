using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Service.Models.Settlements
{
    public class AssignSettlementModel
    {
        public long SettlementId { get; set; }
        public string TradedCurrency { get; set; }
        [Required]
        public AccountModel Account { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string ValueDate { get; set; }
        //[Required]
        //public string TradeCode { get; set; }
        public string Reference { get; set; }
        public bool IsPayTotal { get; set; }
        public int Status { get; set; }
        public bool IsWarning { get; set; }
        public string WarningMessage { get; set; }

    }

    public class AccountModel
    {
        public int clientCompanyOpiId { get; set; }
        public int clientCompanyId { get; set; }
        public string currency { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
    }

}
