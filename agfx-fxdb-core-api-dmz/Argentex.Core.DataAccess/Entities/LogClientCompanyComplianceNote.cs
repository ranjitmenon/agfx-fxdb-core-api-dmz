using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogClientCompanyComplianceNote
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public string Title { get; set; }
        public string NoteText { get; set; }
        public int? AuthUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
