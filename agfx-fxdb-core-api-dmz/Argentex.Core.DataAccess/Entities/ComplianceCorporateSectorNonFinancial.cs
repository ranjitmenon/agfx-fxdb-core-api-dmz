using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ComplianceCorporateSectorNonFinancial
    {
        public ComplianceCorporateSectorNonFinancial()
        {
            ClientCompanyComplianceCorporateSector = new HashSet<ClientCompanyComplianceCorporateSector>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }

        public ICollection<ClientCompanyComplianceCorporateSector> ClientCompanyComplianceCorporateSector { get; set; }
    }
}
