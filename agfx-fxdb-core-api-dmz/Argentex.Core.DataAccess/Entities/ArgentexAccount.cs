using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ArgentexAccount
    {
        public int Id { get; set; }
        public string MatchingContent { get; set; }
        public int? ChecksumMatchingContent { get; set; }
    }
}
