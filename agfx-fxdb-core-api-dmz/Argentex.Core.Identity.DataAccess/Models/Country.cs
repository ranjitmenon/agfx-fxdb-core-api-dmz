using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Argentex.Core.Identity.DataAccess
{
    public class Country
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(256)]
        public string FormalName { get; set; }
        [Required]
        [StringLength(2)]
        public string CodeISO2 { get; set; }
        [Required]
        [StringLength(3)]
        public string CodeISO3 { get; set; }
        [MaxLength(25)]
        public string PhoneCode { get; set; }
        public int CodeISO3Numeric { get; set; }
        [Required]
        public int Sequence { get; set; }
        public int CountryGroupId { get; set; }
        [ForeignKey(nameof(CountryGroupId))]
        public CountryGroup ContryGroup { get; set; }
        public int? LengthIBAN { get; set; }
        [MaxLength(256)]
        public string RegexBBAN { get; set; }
    }
}
