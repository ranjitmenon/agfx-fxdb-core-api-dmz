using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ComplianceIsincurrencyValueDate
    {
        public int Id { get; set; }
        public string Isin { get; set; }
        public string CurrencyPair { get; set; }
        public DateTime ValueDate { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }

        public AuthUser UpdatedByAuthUser { get; set; }
    }
}
