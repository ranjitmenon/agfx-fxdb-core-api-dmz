using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class NavMenuSection
    {
        public NavMenuSection()
        {
            NavMenuItem = new HashSet<NavMenuItem>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }

        public ICollection<NavMenuItem> NavMenuItem { get; set; }
    }
}
