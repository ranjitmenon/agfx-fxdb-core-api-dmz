using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftvalidationCurrencyCountry
    {
        public int CurrencyId { get; set; }
        public int CountryId { get; set; }
        public int OptionId { get; set; }

        public Country Country { get; set; }
        public Currency Currency { get; set; }
        public SwiftvalidationOption Option { get; set; }
    }
}
