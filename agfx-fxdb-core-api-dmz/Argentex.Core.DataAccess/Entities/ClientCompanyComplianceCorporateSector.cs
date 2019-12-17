using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyComplianceCorporateSector
    {
        public int Id { get; set; }
        public int ClientCompanyComplianceId { get; set; }
        public int? ComplianceCorporateSectorFinancialId { get; set; }
        public int? ComplianceCorporateSectorNonFinancialId { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }

        public ClientCompanyCompliance ClientCompanyCompliance { get; set; }
        public ComplianceCorporateSectorFinancial ComplianceCorporateSectorFinancial { get; set; }
        public ComplianceCorporateSectorNonFinancial ComplianceCorporateSectorNonFinancial { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
    }
}
