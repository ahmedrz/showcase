using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class VehicleDetails
    {
        [Required]
        public char? VehicleEquipmentIndicator { get; set; }
        [Required]
        public char? UsedOrNew { get; set; }
        [StringLength(24)]
        public string ChasisNumber { get; set; }
        [StringLength(24)]
        public string CaseNumber { get; set; }
        [Required]
        [StringLength(20)]
        public string Make { get; set; }
        [Required]
        [StringLength(20)]
        public string Model { get; set; }
        [StringLength(30)]
        public string EngineNumber { get; set; }
        [StringLength(4)]
        public string YearBuilt { get; set; }
        [StringLength(16)]
        public string Color { get; set; }
        [Required]
        public char? RollingOrStatic { get; set; }
        [Required]
        [StringLength(200)]
        public string DescriptionOfGood { get; set; }
        [StringLength(100)]
        public string AdditionalAccesseries { get; set; }
        [Range(1, 999999999.999)]
        public double? WeightInKg { get; set; }
        [Range(1, 999999999.999)]
        public double? Volume { get; set; }
        [StringLength(200)]
        public string Remarks { get; set; }


    }
}