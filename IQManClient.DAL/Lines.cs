using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQManClient.DAL
{
    public class Lines : ILine
    {
        [Key]
        [Required(AllowEmptyStrings = false)]
        [StringLength(6)]
        public string LineCode { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(30)]
        public string LineName { get; set; }
        [Required]
        public int Serial { get; set; }
        public bool? IsActive { get; set; }

    }
}
