using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxoptionSettlements
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FxoptionCode { get; set; }
        public string Description { get; set; }
        public string CurrencyPair { get; set; }
        public decimal? ClientRate { get; set; }
        public decimal? ClientLhsamt { get; set; }
        public decimal? ClientRhsamt { get; set; }
        public int? Lhsccyid { get; set; }
        public int? Rhsccyid { get; set; }
        public int? ClientCompanyId { get; set; }
        public int? AuthorisedByClientCompanyContactId { get; set; }
        public int? TradeInstructionMethodId { get; set; }
        public int? CreatedByAuthUserId { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ValueDate { get; set; }
        public bool? IsBuy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal? Notional { get; set; }
        public int? GroupNum { get; set; }
        public int? IsRhsmajour { get; set; }
        public bool? IsSettled { get; set; }
        public int? FxoptionSettlementsTemplateId { get; set; }
        public int? BrokerId { get; set; }
    }
}
