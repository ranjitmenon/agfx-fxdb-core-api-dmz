using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class BreachType
    {
        public BreachType()
        {
            Breach = new HashSet<Breach>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }
        public int DefaultBreachLevelId { get; set; }

        public BreachLevel DefaultBreachLevel { get; set; }
        public ICollection<Breach> Breach { get; set; }
    }
}
