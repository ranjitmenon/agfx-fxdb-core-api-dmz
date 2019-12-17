using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyCurrencyDefaultOpi
    {
        public int ClientCompanyId { get; set; }
        public int CurrencyId { get; set; }
        public int ClientCompanyOpiid { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int UpdateAuthUserId { get; set; }

        public ClientCompany ClientCompany { get; set; }
        public ClientCompanyOpi ClientCompanyOpi { get; set; }
        public Currency Currency { get; set; }
        public AuthUser UpdateAuthUser { get; set; }
    }
}
