using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Argentex.Core.Identity.DataAccess
{
    public class Token
    {
        #region Properties
        [Key]
        [Required]
        public int Id { get; set; }

        public string ClientId { get; set; }

        public int? Type { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }
        #endregion

        #region Lazy-Load Properties
        /// <summary>
        /// The user related to this token
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        #endregion
    }
}
