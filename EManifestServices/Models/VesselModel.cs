using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class VesselModel
    {
        public Guid VesselId { get; set; }
        [Required]
        [StringLength(30)]
        public string VesselName { get; set; }
        [Required]
        public double GRT { get; set; }
        [Required]
        [StringLength(30)]
        public string VesselCountry { get; set; }
        public string IMONo { get; set; }
        public string CallSign { get; set; }
        public double? LOA { get; set; }
        public double? LWL { get; set; }
        public double? DraftAFT { get; set; }
        public double? DraftFWD { get; set; }
        [Required]
        public Guid? CarrierId { get; set; }
    }
}