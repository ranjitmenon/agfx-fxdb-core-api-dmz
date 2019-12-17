using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Argentex.Core.Identity.DataAccess
{
    public class CountryGroup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Description { get;set;}
        [Required]
        public int Sequence { get; set; }
    }
}
