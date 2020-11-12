using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EManifestServices.DAL
{
    public class PackageCodes : IPackageCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PackageCodeId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(3)]
        public string PackageCode { get; set; }
        [Required]
        [StringLength(20)]
        public string PackageDescription { get; set; }
        [Required]
        public int Serial { get; set; }
    }
}
