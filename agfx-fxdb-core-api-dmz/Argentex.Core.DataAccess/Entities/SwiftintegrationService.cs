using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class SwiftintegrationService
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastStatusChangeDateTime { get; set; }
        public int LastStatusChangeByAuthUserId { get; set; }
        public byte[] UpdateTimeStamp { get; set; }

        public AuthUser LastStatusChangeByAuthUser { get; set; }
    }
}
