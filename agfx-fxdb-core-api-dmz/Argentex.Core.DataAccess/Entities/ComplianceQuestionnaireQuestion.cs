using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ComplianceQuestionnaireQuestion
    {
        public ComplianceQuestionnaireQuestion()
        {
            ComplianceQuestionnaire = new HashSet<ComplianceQuestionnaire>();
            ComplianceQuestionnaireAnswer = new HashSet<ComplianceQuestionnaireAnswer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public ICollection<ComplianceQuestionnaire> ComplianceQuestionnaire { get; set; }
        public ICollection<ComplianceQuestionnaireAnswer> ComplianceQuestionnaireAnswer { get; set; }
    }
}
