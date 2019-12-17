using System;

namespace Argentex.Core.Service.Models.ClientSiteAction
{
    public class ClientSiteActionModel
    {
        public long ID { get; set; }
        public string ActionType { get; set; }
        public int ActionStatusID { get; set; }
        public string ActionStatus { get; set; }
        public string Details { get; set; }
        public string CreatedByUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UpdatedByUser { get; set; }
        public int UpdatedByUserID { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
