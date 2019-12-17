using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogClientCompanyContactCategory
    {
        public long LogId { get; set; }
        public string LogAction { get; set; }
        public int ClientCompanyContactId { get; set; }
        public int ContactCategoryId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedByAuthUserId { get; set; }
    }
}
