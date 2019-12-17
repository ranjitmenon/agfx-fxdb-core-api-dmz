using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogComplianceQuestionnaire
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public int ClientCompanyComplianceId { get; set; }
        public int ComplianceQuestionnaireQuestionId { get; set; }
        public int ComplianceQuestionnaireAnswerId { get; set; }
        public bool IsFirstTimeSelect { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
