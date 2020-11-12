using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortSystem.DAL
{
    public class CountryCodes : ICountryCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryCodeId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(2)]
        public string CountryCode { get; set; }
        [Required]
        [StringLength(255)]
        public string CountryName { get; set; }
        [Required]
        public int Serial { get; set; }

    }
}
