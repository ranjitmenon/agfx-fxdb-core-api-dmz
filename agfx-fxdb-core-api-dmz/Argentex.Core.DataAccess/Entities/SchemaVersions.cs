using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SchemaVersions
    {
        public int Id { get; set; }
        public string ScriptName { get; set; }
        public DateTime Applied { get; set; }
    }
}
