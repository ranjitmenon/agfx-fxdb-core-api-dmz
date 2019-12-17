using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class CountryClearingCodePrefix
    {
        public int CountryId { get; set; }
        public int ClearingCodePrefixId { get; set; }
        public bool IsDefault { get; set; }
        public int Sequence { get; set; }

        public ClearingCodePrefix ClearingCodePrefix { get; set; }
        public Country Country { get; set; }
    }
}
