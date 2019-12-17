using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AppUserEmailAlternative
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string AlternativeEmailAddress { get; set; }

        public AppUser AppUser { get; set; }
    }
}
