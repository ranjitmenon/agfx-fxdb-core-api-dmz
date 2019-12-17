using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyOpiduplicate
    {
        public int Id { get; set; }
        public int DuplicateClientCompanyOpiid { get; set; }
        public int OriginalClientCompanyOpiid { get; set; }
        public bool IsOk { get; set; }
        public string Note { get; set; }
        public int? IsOkupdatedByAuthUserId { get; set; }
        public DateTime? IsOkupdatedDateTime { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }

        public AuthUser CreatedByAuthUser { get; set; }
        public ClientCompanyOpi DuplicateClientCompanyOpi { get; set; }
        public AuthUser IsOkupdatedByAuthUser { get; set; }
        public ClientCompanyOpi OriginalClientCompanyOpi { get; set; }
    }
}
