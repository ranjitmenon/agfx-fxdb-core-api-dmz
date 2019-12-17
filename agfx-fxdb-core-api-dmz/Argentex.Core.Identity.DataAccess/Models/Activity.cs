using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Argentex.Core.Identity.DataAccess
{
    public class Activity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Type { get; set; }
        public ICollection<ActivityLog> ActivityLogs { get; set; }
    }
}
