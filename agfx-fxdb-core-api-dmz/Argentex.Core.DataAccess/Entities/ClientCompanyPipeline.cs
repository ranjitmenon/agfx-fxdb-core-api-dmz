using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyPipeline
    {
        public int ClientCompanyId { get; set; }
        public int TotalCalls { get; set; }
        public DateTime? LastCall { get; set; }
        public DateTime? LastLongCall { get; set; }
        public DateTime? LastEmail { get; set; }
        public string LastEmailFrom { get; set; }
        public string LastEmailTo { get; set; }
        public int? NextPipelineActionId { get; set; }
        public DateTime? NextActionDueDate { get; set; }
        public DateTime? NextActionUpdated { get; set; }
        public int? Rating { get; set; }
        public int? Confidence { get; set; }
        public int? Progress { get; set; }
        public DateTime? NextTradeDate { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int? UpdateAuthUserId { get; set; }
        public int? CallsToBrochure { get; set; }
        public int? CallsToAccFormSent { get; set; }
        public int? CallsToAccOpened { get; set; }

        public ClientCompany ClientCompany { get; set; }
        public PipelineAction NextPipelineAction { get; set; }
        public AuthUser UpdateAuthUser { get; set; }
    }
}
