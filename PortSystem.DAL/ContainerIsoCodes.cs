using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortSystem.DAL
{
    public class ContainerIsoCodes
    {
        [Key]
        [Required(AllowEmptyStrings = false)]
        [StringLength(4)]
        public string IsoTypeCode { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(60)]
        public string IsoTypeDescription { get; set; }
        [Required]
        public int Serial { get; set; }
    }
}
