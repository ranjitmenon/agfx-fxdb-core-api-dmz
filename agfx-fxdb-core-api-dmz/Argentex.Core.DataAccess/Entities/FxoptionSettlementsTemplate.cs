using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxoptionSettlementsTemplate
    {
        public int Id { get; set; }
        public int? FxoptionTypeId { get; set; }
        public string Template { get; set; }
        public bool? IsBuy { get; set; }
        public string TradeCodeSuffix { get; set; }
        public string Notional { get; set; }
        public string ClientRate { get; set; }
        public int? GroupNum { get; set; }
    }
}
