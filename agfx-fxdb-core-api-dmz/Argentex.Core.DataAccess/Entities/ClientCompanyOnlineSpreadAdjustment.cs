using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyOnlineSpreadAdjustment
    {
        public long Id { get; set; }
        public int ClientCompanyOnlineDetailsId { get; set; }
        public int? Currency1Id { get; set; }
        public int? Currency2Id { get; set; }
        public bool? IsBuy { get; set; }
        public int Spread { get; set; }
        public DateTime ExpirationDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }

        public ClientCompanyOnlineDetails ClientCompanyOnlineDetails { get; set; }
        public Currency Currency1 { get; set; }
        public Currency Currency2 { get; set; }
    }
}
