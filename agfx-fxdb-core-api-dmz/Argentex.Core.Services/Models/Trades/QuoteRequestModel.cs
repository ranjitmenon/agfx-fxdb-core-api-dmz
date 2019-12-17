using Argentex.Core.Service.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Service.Models.Trades
{
    public class QuoteRequestModel
    {
        [Required]
        public int? AuthUserId { get; set; }
        [Required]
        public int? ClientCompanyId { get; set; }
        [Required(ErrorMessage = "At least one order must be supplied")]
        public ICollection<QuoteModel> QuoteModels { get; set; }
    }

    public class QuoteModel
    {
        [Required]
        public int QuoteIndex { get; set; }
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
        [DateRequired(ErrorMessage = "Value date cannot be empty")]
        public DateTime ValueDate { get; set; }
        [BoolRequired(ErrorMessage = "IsRhsMajor must be boolean")]
        public bool IsRhsMajor { get; set; }
        [DateRequired(ErrorMessage = "Contract date cannot be empty")]
        public DateTime ContractDate { get; set; }
    }
}
