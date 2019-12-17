using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogClientCompanyOnlineDetailsSkew
    {
        public long LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public int ClientCompanyOnlineDetailsId { get; set; }
        public int Currency1Id { get; set; }
        public int Currency2Id { get; set; }
        public bool IsBuy { get; set; }
        public int Spread { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
