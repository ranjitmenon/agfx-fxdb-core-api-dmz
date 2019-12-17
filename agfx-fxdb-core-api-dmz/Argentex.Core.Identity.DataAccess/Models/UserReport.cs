using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Argentex.Core.Identity.DataAccess
{
    public class UserReport
    {
        //[Key, Column(Order = 1)]
        public long UserId { get; set; }
        //[Key, Column(Order = 2)]
        public long ReportId { get; set; }
        //[ForeignKey(nameof(ReportId))]
        public virtual Report Report { get; set; }
        //[ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
    }
}
