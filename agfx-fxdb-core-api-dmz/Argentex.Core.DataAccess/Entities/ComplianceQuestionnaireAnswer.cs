using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ComplianceQuestionnaireAnswer
    {
        public ComplianceQuestionnaireAnswer()
        {
            ComplianceQuestionnaire = new HashSet<ComplianceQuestionnaire>();
        }

        public int Id { get; set; }
        public int ComplianceQuestionnaireQuestionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public ComplianceQuestionnaireQuestion ComplianceQuestionnaireQuestion { get; set; }
        public ICollection<ComplianceQuestionnaire> ComplianceQuestionnaire { get; set; }
    }
}
