using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyNote
    {
        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public string Title { get; set; }
        public string NoteText { get; set; }
        public int? AuthUserId { get; set; }
        public DateTime CreateDateTime { get; set; }

        public AuthUser AuthUser { get; set; }
        public ClientCompany ClientCompany { get; set; }
    }
}
