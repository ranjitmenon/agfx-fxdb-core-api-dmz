using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxoptionOutputs
    {
        public int Id { get; set; }
        public string FxoptionCode { get; set; }
        public string Outputs { get; set; }
        public string ExternalTradeCode { get; set; }
        public bool? IsDeleted { get; set; }
        public int? AuthUserId { get; set; }
        public int? FxoptionOutputsTemplateId { get; set; }
    }
}
