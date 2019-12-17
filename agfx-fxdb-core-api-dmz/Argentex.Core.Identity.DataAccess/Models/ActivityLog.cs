using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Argentex.Core.Identity.DataAccess
{
    public class ActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ActivityLogId { get; set; }
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
        [Required]
        public DateTime LogDate { get; set; }
        [Required]
        public bool IsSuccess { get; set; }
        [MaxLength(128)]
        public string PrimaryIP { get; set; }
        [MaxLength(128)]
        public string SecondaryIP { get; set; }

        [Required]
        public int ActivityId { get; set; }
        [ForeignKey(nameof(ActivityId))]
        public Activity Activity { get; set; }
        
        public int? AuthUserId { get; set; }

        //Name should be UserId, but it must be set as Id to match the Primary Key column in the User table (ApplicationUser class)
        public long? Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
