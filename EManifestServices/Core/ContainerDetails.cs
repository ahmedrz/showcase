using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class ContainerDetails
    {
        [Required]
        [StringLength(11)]
        public string ContainerNo { get; set; }
        [Required]
        [StringLength(1)]
        public char? CheckDigit { get; set; }
        [Required]
        [Range(1, 9999.9)]
        public double? TareWeight { get; set; }
        [Required]
        [StringLength(10)]
        public string SealNumber { get; set; }

        public List<ConsignmentDetails> Consignments { get; set; }
        public List<VehicleDetails> Vehicles { get; set; }


        public ContainerDetails()
        {
            Consignments = new List<ConsignmentDetails>();
            Vehicles = new List<VehicleDetails>();
        }
    }
}