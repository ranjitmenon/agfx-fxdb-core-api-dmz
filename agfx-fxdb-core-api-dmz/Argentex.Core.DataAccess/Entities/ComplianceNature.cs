using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ComplianceNature
    {
        public ComplianceNature()
        {
            ClientCompanyCompliance = new HashSet<ClientCompanyCompliance>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string EmirValue { get; set; }
        public int Sequence { get; set; }

        public ICollection<ClientCompanyCompliance> ClientCompanyCompliance { get; set; }
    }
}
