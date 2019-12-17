using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FixApareportField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Tag { get; set; }
        public int? GroupTag { get; set; }
        public int? GroupNo { get; set; }
        public string Value { get; set; }
        public string AppSettingKey { get; set; }
        public bool IsBlank { get; set; }
        public bool? IsActive { get; set; }
    }
}
