using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class PipelineActionType
    {
        public PipelineActionType()
        {
            PipelineAction = new HashSet<PipelineAction>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<PipelineAction> PipelineAction { get; set; }
    }
}
