using Argentex.Core.Service.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Service.Models.Trade
{
    public class OrderRequestModel
    {
        [Required]
        public int? AuthUserId { get; set; }
        [Required]
        public int? ClientCompanyId { get; set; }
        [Required(ErrorMessage = "At least one order must be supplied")]
        public ICollection<OrderModel> OrderModels { get; set; }
    }

    public class OrderModel
    {
        [DateRequired(ErrorMessage = "Value date cannot be empty")]
        public DateTime ValueDate { get; set; }
        public DateTime? ValidityDate { get; set; }
        [DecimalRequiredAttribute(ErrorMessage = "Client rate cannot be empty")]
        public decimal ClientRate { get; set; }
        [Required(ErrorMessage = "LHS currency cannot be empty")]
        public string LhsCcy { get; set; }
        [Required(ErrorMessage = "RHS currency cannot be empty")]
        public string RhsCcy { get; set; }
        [DecimalRequired(ErrorMessage ="Client amount must be 0 or greater")]
        public decimal ClientAmount { get; set; }
        [BoolRequired(ErrorMessage = "IsBuy must be boolean")]
        public bool IsBuy { get; set; }
        [BoolRequired(ErrorMessage="IsRhsMajor must be boolean")]
        public bool IsRhsMajor { get; set; }
    }
}
