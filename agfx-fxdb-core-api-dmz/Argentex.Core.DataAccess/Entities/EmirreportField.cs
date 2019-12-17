using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class EmirreportField
    {
        public int Id { get; set; }
        public int EmirreportTypeId { get; set; }
        public string Description { get; set; }
        public string FieldCode { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string AppSettingKey { get; set; }
        public bool IsBlank { get; set; }
        public bool? IsActive { get; set; }
        public int Sequence { get; set; }

        public EmirreportType EmirreportType { get; set; }
    }
}
