using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyComplianceCurrency
    {
        public int Id { get; set; }
        public int ClientCompanyComplianceId { get; set; }
        public int CurrencyId { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }

        public ClientCompanyCompliance ClientCompanyCompliance { get; set; }
        public Currency Currency { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
    }
}
