using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EManifestServices.DAL
{
    public class UNHazardousCodes
    {
        [Key]
        [Required(AllowEmptyStrings = false)]
        [StringLength(4)]
        public string Code { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(150)]
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(4)]
        public string Class { get; set; }
        [Required]
        public int Serial { get; set; }
    }
}
