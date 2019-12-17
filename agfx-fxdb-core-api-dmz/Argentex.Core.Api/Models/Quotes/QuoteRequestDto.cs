using Argentex.Core.Service.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Api.Models.Quotes
{
    public class QuoteRequestDto
    {
        [Range(1, int.MaxValue)]
        public int ClientCompanyId { get; set; }
        [Required]
        [StringLength(3)]
        public string LeftCurrency { get; set; }
        [Required]
        [StringLength(3)]
        public string RightCurrency { get; set; }
        public bool IsBuy { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [DateRequired]
        public DateTime ValueDate { get; set; }
        [BoolRequired]
        public bool IsRhsMajor { get; set; }
    }
}
