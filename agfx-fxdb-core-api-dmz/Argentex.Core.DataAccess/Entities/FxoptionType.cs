using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxoptionType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string TermSheetImg { get; set; }
        public string VisibleInputs { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsPrimary { get; set; }
        public int? ExtOptionTypeId { get; set; }
        public int? LevOptionTypeId { get; set; }
    }
}
