using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class TelephoneCountryCode
    {
        public TelephoneCountryCode()
        {
            AppUser = new HashSet<AppUser>();
        }

        public int Id { get; set; }
        public string Iso { get; set; }
        public string Name { get; set; }
        public string Nicename { get; set; }
        public string Iso3 { get; set; }
        public int? Numcode { get; set; }
        public int Phonecode { get; set; }

        public ICollection<AppUser> AppUser { get; set; }
    }
}
