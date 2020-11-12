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
    public class LocationCodes : ILocationCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LocationCodeId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(2)]
        public string CountryCode { get; set; }
        [StringLength(5)]
        public string LocationCode { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullCode
        {
            get { return CountryCode + LocationCode; }
            private set { /* needed for EF */ }
        }
        [Required]
        [StringLength(255)]
        public string LocationName { get; set; }
        [StringLength(16)]
        public string Function { get; set; }
        [Required]
        public int Serial { get; set; }

    }
}
