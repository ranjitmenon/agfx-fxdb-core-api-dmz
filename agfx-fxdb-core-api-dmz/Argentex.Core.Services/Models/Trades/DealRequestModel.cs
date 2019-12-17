using Argentex.Core.Service.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Service.Models.Trades
{
    public class DealRequestModel
    {
        [Required]
        public int? AuthUserId { get; set; }
        [Required]
        public int? ClientCompanyId { get; set; }
        [Required(ErrorMessage = "At least one deal must be supplied")]
        public ICollection<DealModel> DealModels { get; set; }
    }

    public class DealModel
    {
        [Required]
        public int TradeIndex { get; set; }
        [Required(ErrorMessage = "LHS currency cannot be empty")]
        [StringLength(3)]
        public string LhsCcy { get; set; }
        [Required(ErrorMessage = "RHS currency cannot be empty")]
        [StringLength(3)]
        public string RhsCcy { get; set; }
        [BoolRequired(ErrorMessage = "IsBuy must be boolean")]
        public bool IsBuy { get; set; }
        [DecimalRequired(ErrorMessage = "Amount must be 0 or greater")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Client Rate
        /// </summary>
        [DecimalRequired(ErrorMessage = "Amount must be 0 or greater")]
        public decimal Rate { get; set; }
        [DecimalRequired(ErrorMessage = "Amount must be 0 or greater")]
        public decimal BrokerRate { get; set; }
        [DateRequired(ErrorMessage = "Value date cannot be empty")]
        public DateTime ValueDate { get; set; }
        [DateRequired(ErrorMessage = "Expiration Date Time cannot be empty")]
        public DateTime ExpirationDateTime { get; set; }
        [BoolRequired(ErrorMessage = "IsRhsMajor must be boolean")]
        public bool IsRhsMajor { get; set; }
        [Required]
        public string QuoteId { get; set; }
        [Required]
        public string QuoteReqId { get; set; }
    }
}
