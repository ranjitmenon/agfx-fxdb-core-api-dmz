using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class BreachLevel
    {
        public BreachLevel()
        {
            Breach = new HashSet<Breach>();
            BreachType = new HashSet<BreachType>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int Sequence { get; set; }

        public ICollection<Breach> Breach { get; set; }
        public ICollection<BreachType> BreachType { get; set; }
    }
}
