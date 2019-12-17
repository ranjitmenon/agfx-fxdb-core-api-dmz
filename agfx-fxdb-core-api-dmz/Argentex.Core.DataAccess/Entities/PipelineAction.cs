using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class PipelineAction
    {
        public PipelineAction()
        {
            ClientCompanyPipeline = new HashSet<ClientCompanyPipeline>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int PipelineActionTypeId { get; set; }
        public int DisplayOrder { get; set; }

        public PipelineActionType PipelineActionType { get; set; }
        public ICollection<ClientCompanyPipeline> ClientCompanyPipeline { get; set; }
    }
}
