using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class CountryGroup
    {
        public CountryGroup()
        {
            Country = new HashSet<Country>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? Sequence { get; set; }

        public ICollection<Country> Country { get; set; }
    }
}
