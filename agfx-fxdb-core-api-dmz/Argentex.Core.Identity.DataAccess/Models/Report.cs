using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Argentex.Core.Identity.DataAccess
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ReportId { get; set; }
        [MaxLength(128)]
        public string Description { get; set; }

        //[ForeignKey(nameof(UserReport.ReportId))]
        public virtual ICollection<UserReport> UserReports { get; set; }
    }
}
