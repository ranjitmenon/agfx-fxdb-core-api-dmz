using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ComplianceClassification
    {
        public ComplianceClassification()
        {
            ClientCompanyCompliance = new HashSet<ClientCompanyCompliance>();
            ComplianceClassificationFile = new HashSet<ComplianceClassificationFile>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }

        public ICollection<ClientCompanyCompliance> ClientCompanyCompliance { get; set; }
        public ICollection<ComplianceClassificationFile> ComplianceClassificationFile { get; set; }
    }
}
