using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ArmreportField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrokerValue { get; set; }
        public string ClientValue { get; set; }
        public string AppSettingKey { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsBlank { get; set; }
        public bool? IsActive { get; set; }
    }
}
