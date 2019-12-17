using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class CurrencyPairValidation
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; }
        public int? UpdatedByAuthUserId { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
