using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ComplianceQuestionnaire
    {
        public int Id { get; set; }
        public int ClientCompanyComplianceId { get; set; }
        public int ComplianceQuestionnaireQuestionId { get; set; }
        public int ComplianceQuestionnaireAnswerId { get; set; }
        public bool? IsFirstTimeSelect { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }

        public ClientCompanyCompliance ClientCompanyCompliance { get; set; }
        public ComplianceQuestionnaireAnswer ComplianceQuestionnaireAnswer { get; set; }
        public ComplianceQuestionnaireQuestion ComplianceQuestionnaireQuestion { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
    }
}
