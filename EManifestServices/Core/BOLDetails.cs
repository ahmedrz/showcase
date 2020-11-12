using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class BOLDetails
    {
        [Required]
        [StringLength(20)]
        public string BillOfLadingNumber { get; set; }
        [Required]
        [StringLength(6)]
        public string BoxPartneringLineCode { get; set; }
        [Required]
        [StringLength(6)]
        public string BoxPartneringAgentCode { get; set; }
        [Required]
        [StringLength(5)]
        public string PortCodeOfOrigin { get; set; }
        [Required]
        [StringLength(5)]
        public string PortCodeOfLoading { get; set; }
        [Required]
        [StringLength(5)]
        public string PortCodeOfDischarge { get; set; }
        [Required]
        [StringLength(5)]
        public string PortCodeOfDestination { get; set; }
        public DateTime? DateOfLoading { get; set; }
        [StringLength(8)]
        public string ManifestRegisterationNumber { get; set; }
        [Required]
        public char? TradeCode { get; set; }
        public char? TransShipmentMode { get; set; }
        [StringLength(30)]
        public string BillOfLadingOwnerName { get; set; }
        [StringLength(240)]
        public string BillOfLadingOwnerAddress { get; set; }
        [Required]
        public char? CargoCode { get; set; }
        [Required]
        public char? ConsolidatedCargoIndicator { get; set; }
        public char? StorageRequestCode { get; set; }
        [StringLength(7)]
        public string ContainerServiceType { get; set; }
        [Required]
        [StringLength(2)]
        public string CountryOfOrigin { get; set; }
        [StringLength(30)]
        public string OriginalConsigneeName { get; set; }
        [StringLength(240)]
        public string OriginalConsigneeAddress { get; set; }
        [StringLength(30)]
        public string OriginalVesselName { get; set; }
        [StringLength(10)]
        public string OriginalVoyageNumber { get; set; }
        [StringLength(20)]
        public string OriginalBolNumber { get; set; }
        [StringLength(30)]
        public string OriginalShipperName { get; set; }
        [StringLength(240)]
        public string OriginalShipperAddress { get; set; }
        [Required]
        [StringLength(30)]
        public string ShipperName { get; set; }
        [Required]
        [StringLength(240)]
        public string ShipperAddress { get; set; }
        [Required]
        [StringLength(2)]
        public string ShipperCountryCode { get; set; }
        [Required]
        [StringLength(5)]
        public string ConsigneeCode { get; set; }
        [Required]
        [StringLength(48)]
        public string ConsigneeName { get; set; }
        [Required]
        [StringLength(240)]
        public string ConsigneeAddress { get; set; }

        [StringLength(6)]
        public string Notify1Code { get; set; }
        [Required]
        [StringLength(48)]
        public string Notify1Name { get; set; }
        [Required]
        [StringLength(240)]
        public string Notify1Address { get; set; }
        [StringLength(6)]
        public string Notify2Code { get; set; }
        [StringLength(48)]
        public string Notify2Name { get; set; }
        [StringLength(240)]
        public string Notify2Address { get; set; }
        [StringLength(6)]
        public string Notify3Code { get; set; }
        [StringLength(48)]
        public string Notify3Name { get; set; }
        [StringLength(240)]
        public string Notify3Address { get; set; }
        [Required]
        [StringLength(200)]
        public string MarksAndNumbers { get; set; }
        [Required]
        [StringLength(10)]
        public string CommodityCode { get; set; }
        [Required]
        [StringLength(100)]
        public string CommodityDescription { get; set; }
        [Required]
        [Range(1, 999999999)]
        public int? Packages { get; set; }
        [Required]
        [StringLength(30)]
        public string PackageType { get; set; }
        [Required]
        [StringLength(3)]
        public string PackageTypeCode { get; set; }
        [StringLength(11)]
        public string ContainerNumber { get; set; }
        public char? CheckDigit { get; set; }
        [Range(1, 999)]
        public int? NoOfContainers { get; set; }
        [Range(1, 999)]
        public int? NoOfTeus { get; set; }
        [Range(1, 9999.9)]
        public double? TotalTareWeight { get; set; }
        [Required]
        [Range(1, 999999999.999)]
        public double? CargoWeight { get; set; }
        [Required]
        [Range(1, 999999999.999)]
        public double? GrossWeight { get; set; }
        [Range(1, 999999999.999)]
        public double? CargoVolume { get; set; }
        [Range(1, 999999999)]
        public int? TotalQuantity { get; set; }
        [Range(1, 999999999.999)]
        public double? FreightTonne { get; set; }
        [Range(1, 9999)]
        public int? NoOfPallets { get; set; }
        public char? SlacIndicator { get; set; }
        [StringLength(3)]
        public string ContractCarriageCondition { get; set; }
        [StringLength(200)]
        public string Remarks { get; set; }

        public List<ConsignmentDetails> Consignments { get; set; }
        public List<ContainerDetails> Containers { get; set; }
        public List<VehicleDetails> Vehicles { get; set; }
        public BOLDetails()
        {
            Consignments = new List<ConsignmentDetails>();
            Containers = new List<ContainerDetails>();
            Vehicles = new List<VehicleDetails>();
        }

    }
}