using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogClientCompanyComplianceCorporateSector
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public int ClientCompanyComplianceId { get; set; }
        public int? ComplianceCorporateSectorFinancialId { get; set; }
        public int? ComplianceCorporateSectorNonFinancialId { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
