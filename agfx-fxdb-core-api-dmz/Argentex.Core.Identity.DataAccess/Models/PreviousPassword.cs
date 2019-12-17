using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Argentex.Core.Identity.DataAccess
{
    public class PreviousPassword
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PasswordHash { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
