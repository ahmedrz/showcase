using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class ConsignmentDetails
    {
        [Required]
        [Range(1, 999999)]
        public int? SerialNumber { get; set; }
        [Required]
        [StringLength(200)]
        public string MarksAndNumbers { get; set; }
        [Required]
        [StringLength(100)]
        public string CargoDescription { get; set; }
        public char? UsedOrNew { get; set; }
        [Required]
        [StringLength(10)]
        public string CommodityCode { get; set; }
        [Required]
        [Range(1, 999999999)]
        public int? ConsignmentPackages { get; set; }
        [Required]
        [StringLength(30)]
        public string PackageType { get; set; }
        [Required]
        [StringLength(3)]
        public string PackageTypeCode { get; set; }
        [Required]
        [Range(1, 9999)]
        public int? NumberOfPallets { get; set; }
        [Required]
        [Range(1, 999999999.999)]
        public double? ConsignmentWeight { get; set; }
        [Required]
        [Range(1, 999999999.999)]
        public double? ConsignmentValume { get; set; }
        [Required]
        public char? DangerousGoodsIndicator { get; set; }
        [StringLength(3)]
        public string IMOClassNumber { get; set; }
        [StringLength(5)]
        public string UnNumberOfDangerousGoods { get; set; }
        [Range(1, 9999.9)]
        public double? FlashPoint { get; set; }
        public char? UnitOfTemperatureForFlashPoint { get; set; }
        public char? StorageRequestedForDangerousGoods { get; set; }
        [Required]
        public char? RefrigerationRequired { get; set; }
        [Range(1, 9999.9)]
        public double? MinimumTemperatureOfRefrigeration { get; set; }
        [Range(1, 9999.9)]
        public double? MaximumTemperatureOfRefrigeration { get; set; }
        public char? UnitOfTemperatureForRegrigeration { get; set; }



    }
}