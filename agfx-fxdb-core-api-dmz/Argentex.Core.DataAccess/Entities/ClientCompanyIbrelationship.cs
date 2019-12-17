using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyIbrelationship
    {
        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public int IntroducingBrokerId { get; set; }
        public decimal? Percentage { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public ClientCompany ClientCompany { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
    }
}
