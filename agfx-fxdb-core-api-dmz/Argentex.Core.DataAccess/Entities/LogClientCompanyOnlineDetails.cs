﻿using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogClientCompanyOnlineDetails
    {
        public long LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public bool AllowOnlineTrading { get; set; }
        public decimal? MaxTradeSize { get; set; }
        public decimal? MaxOpen { get; set; }
        public DateTime? MaxTenor { get; set; }
        public decimal? Collateral { get; set; }
        public int? SpotSpread { get; set; }
        public int? FwdSpread { get; set; }
        public bool? Kicked { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
