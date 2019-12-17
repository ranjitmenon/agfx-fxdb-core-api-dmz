using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Breach
    {
        public Breach()
        {
            BreachInvoice = new HashSet<BreachInvoice>();
        }

        public int Id { get; set; }
        public int BreachTypeId { get; set; }
        public int BreachLevelId { get; set; }
        public string TradeCode { get; set; }
        public int? ClientCompanyOpiid { get; set; }
        public string OriginalLimit { get; set; }
        public string OverrideValue { get; set; }
        public string Notes { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int? PaymentId { get; set; }

        public BreachLevel BreachLevel { get; set; }
        public BreachType BreachType { get; set; }
        public ClientCompanyOpi ClientCompanyOpi { get; set; }
        public AuthUser CreatedByAuthUser { get; set; }
        public Payment Payment { get; set; }
        public FxforwardTrade TradeCodeNavigation { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
        public ICollection<BreachInvoice> BreachInvoice { get; set; }
    }
}
