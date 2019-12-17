using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxoptionOutputsTemplate
    {
        public int Id { get; set; }
        public int? FxoptionTypeId { get; set; }
        public string Template { get; set; }
        public bool? IsBuy { get; set; }
    }
}
