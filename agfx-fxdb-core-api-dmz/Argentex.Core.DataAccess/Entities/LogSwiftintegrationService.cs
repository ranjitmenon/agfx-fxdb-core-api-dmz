using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogSwiftintegrationService
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastStatusChangeDateTime { get; set; }
        public int LastStatusChangeByAuthUserId { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
    }
}
