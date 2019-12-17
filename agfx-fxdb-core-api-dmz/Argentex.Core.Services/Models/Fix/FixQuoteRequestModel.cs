using Argentex.Core.Service.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Service.Models.Fix
{
    public class FixQuoteRequestModel
    {
        [Required]
        public string TradeCode { get; set; }
        [Required]
        [StringLength(3)]
        public string LHSCCY { get; set; }
        [Required]
        [StringLength(3)]
        public string RHSCCY { get; set; }
        [Required]
        [StringLength(3)]
        public string MajorCurrency { get; set; }
        [Required]
        [Range(1, 2)]
        public int Side { get; set; }
        [Required]
        public decimal BrokerMajorAmount { get; set; }
        [Required]
        public string ValueDate { get; set; }
        [Required]
        [Range(1000, 60000)]
        public int TimeOut { get; set; }
        [Required]
        [Range(1, 1440)]
        public int Duration { get; set; }
    }
}
