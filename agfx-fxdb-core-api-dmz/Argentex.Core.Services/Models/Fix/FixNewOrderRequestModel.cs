using System;
using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Service.Models.Fix
{
    public class FixNewOrderRequestModel
    {
        [Required]
        public string TradeCode { get; set; }
        [Required]
        public string QuoteId { get; set; }
        [Required]
        public string QuoteReqId { get; set; }
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
        [Range(1, 2, ErrorMessage = "Side must be 1 for Buy and 2 for Sale")]
        public int Side { get; set; }
        [Required]
        public decimal BrokerMajorAmount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string ValueDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal ClientPrice { get; set; }
        [Required]
        [Range(1000, 60000)]
        public int TimeOut { get; set; }
        [Required]
        [Range(1, 1440)]
        public int Duration { get; set; }
    }
}
