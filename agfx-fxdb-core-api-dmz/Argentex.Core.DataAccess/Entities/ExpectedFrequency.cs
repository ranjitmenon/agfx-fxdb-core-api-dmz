using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ExpectedFrequency
    {
        public ExpectedFrequency()
        {
            ClientCompanyCompliance = new HashSet<ClientCompanyCompliance>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? Sequence { get; set; }
        public int? Value { get; set; }

        public ICollection<ClientCompanyCompliance> ClientCompanyCompliance { get; set; }
    }
}
