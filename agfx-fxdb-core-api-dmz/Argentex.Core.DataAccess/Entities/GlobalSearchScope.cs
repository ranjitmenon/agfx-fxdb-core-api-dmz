using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class GlobalSearchScope
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string StoredProcName { get; set; }
    }
}
