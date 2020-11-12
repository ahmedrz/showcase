namespace EManifestServices.DAL
{
    using Emanifest.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public partial class BOLDetails : IBOLDetails<ConsignmentDetails, ContainerDetails, VehicleDetails>, IValidatableObject, INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOLDetails()
        {
            ConsignmentDetails = new ObservableCollection<ConsignmentDetails>();
            ContainerDetails = new ObservableCollection<ContainerDetails>();
            ConsolidatedCargoIndicator = "N";
            SlacIndicator = "N";
        }
        [Key]
        public Guid BOLDetailsId { get; set; }
        [Required]
        [StringLength(20)]
        public string BillOfLadingNumber { get; set; }
        [Required]
        [StringLength(30)]
        public string BoxPartneringLineCode { get; set; }
        [Required]
        [StringLength(30)]
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
        [StringLength(1)]
        [SpecificString(AllowedStrings = new string[] { "I", "T" }, Info = "I - IMPORT; T - TRANS-SHIPMENT")]
        public string TradeCode { get; set; }
        [StringLength(1)]
        public string TransShipmentMode { get; set; }
        [StringLength(30)]
        public string BillOfLadingOwnerName { get; set; }
        [StringLength(240)]
        public string BillOfLadingOwnerAddress { get; set; }
        [Required]
        [StringLength(1)]
        [SpecificString(Info = @"F - FCL CONTAINER
                            L - LCL CONTAINER
                            M - EMPTY CONTAINER
                            B - BULK SOLID
                            Q - BULK LIQUID
                            R - RO-RO UNIT
                            P - PASSENGER
                            A - LIVE STICK (ANIMALS)
                            G - GENERAL CARGO ( BREAK BULK )",
            AllowedStrings = new string[] { "F", "L", "M", "B", "Q", "R", "P", "G", "A" })]
        public string CargoCode { get; set; }
        [Required]
        [StringLength(1)]
        [SpecificString(Info = @"Y - YES FOR CONSOLIDATED CARGO ( GROUPAGE CARGO )
                            N - NO, OTHERWISE",
            AllowedStrings = new string[] { "Y", "N" })]
        public string ConsolidatedCargoIndicator { get; set; }
        [StringLength(1)]
        [SpecificString(Info = @"D - DIRECT DELIVEY
                            S - STORAGE IN SHEDS
                            Y - STORAGE IN YARDS",
        AllowedStrings = new string[] { "D", "S", "Y" })]
        public string StorageRequestCode { get; set; }
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
        [StringLength(10)]
        public string ContainerNumber { get; set; }
        [StringLength(1)]
        public string CheckDigit { get; set; }
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
        [StringLength(1)]
        public string SlacIndicator { get; set; }
        [StringLength(3)]
        public string ContractCarriageCondition { get; set; }
        [StringLength(200)]
        public string Remarks { get; set; }
        public Guid VoyageDetailsId { get; set; }

        public virtual VoyageDetails VoyageDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsignmentDetails> ConsignmentDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContainerDetails> ContainerDetails { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Transhipment validation
            if (TradeCode == "T")
            {
                //
                var transhipMode = new[] { "TransShipmentMode" };
                if (TransShipmentMode == null)
                {
                    yield return new ValidationResult("TransShipmentMode is required for TradeCode : T (TRANS-SHIPMENT)", transhipMode);
                }
                else
                {
                    var allowedTransshipModes = new[] { "S", "A", "R" };
                    if (!allowedTransshipModes.ToList().Contains(TransShipmentMode))
                    {
                        yield return new ValidationResult("Allowed TransShipmentMode : S - SEA; A - AIR ; R - ROAD", transhipMode);
                    }
                }
            }
            if (BillOfLadingOwnerName != null)
            {
                var consigneeAddress = new[] { "BillOfLadingOwnerAddress" };
                if (BillOfLadingOwnerAddress == null)
                {
                    yield return new ValidationResult("BillOfLadingOwnerAddress if BillOfLadingOwnerName is appearing", consigneeAddress);
                }
            }

            if (CargoCode == "F" || CargoCode == "L")
            {
                var containerServiceType = new[] { "ContainerServiceType" };
                if (ContainerServiceType == null)
                {
                    yield return new ValidationResult("ContainerServiceType is required for containerized cargo", containerServiceType);
                }
                else
                {
                    var allowedServiceTypes = new[] { "FCL/FCL", "FCL/LCL", "LCL/LCL", "LCL/FCL" };
                    if (!allowedServiceTypes.ToList().Contains(ContainerServiceType))
                    {
                        yield return new ValidationResult("Allowed ContaierServiceTypes : FCL/FCL (OR) FCL/LCL (OR) LCL/LCL (OR) LCL/FCL", containerServiceType);
                    }
                }
            }

            if (CargoCode == "L")
            {
                var containerNo = new[] { "ContainerNumber" };
                if (ContainerNumber == null)
                {
                    yield return new ValidationResult("ContainerNumber if required for LCL containers", containerNo);
                }

                var checkDigit = new[] { "CheckDigit" };
                if (CheckDigit == null)
                {
                    yield return new ValidationResult("CheckDigit if required for LCL containers", checkDigit);
                }
            }

            if (CargoCode == "F")
            {
                var noOfContainers = new[] { "NoOfContainers" };
                if (NoOfContainers == null)
                {
                    yield return new ValidationResult("NoOfContainers if required for FCL containers", noOfContainers);
                }

                var noOfTues = new[] { "NoOfTeus" };
                if (NoOfTeus == null)
                {
                    yield return new ValidationResult("NoOfTeus if required for FCL containers", noOfTues);
                }

                var totalTareWeight = new[] { "TotalTareWeight" };
                if (TotalTareWeight == null)
                {
                    yield return new ValidationResult("TotalTareWeight if required for FCL containers", totalTareWeight);
                }
            }

            if (CargoCode == "L" || CargoCode == "G")
            {
                var cargoVolume = new[] { "CargoVolume" };
                if (CargoVolume == null)
                {
                    yield return new ValidationResult("CargoVolume if required for LCL & GENERAL CARGO", cargoVolume);
                }

                var freightTonne = new[] { "FreightTonne" };
                if (FreightTonne == null)
                {
                    yield return new ValidationResult("FreightTonne if required for LCL & GENERAL CARGO", freightTonne);
                }

                var noOfPallets = new[] { "NoOfPallets" };
                if (NoOfPallets == null)
                {
                    yield return new ValidationResult("NoOfPallets if required for LCL & GENERAL CARGO", noOfPallets);
                }
            }
            var consignmentsAndContainers = new[] { "ConsignmentDetails", "ContainerDetails" };
            if (ConsignmentDetails.Count == 0 && !ContainerDetails.Any(c => c.ConsignmentDetails.Count != 0))
            {
                yield return new ValidationResult("There must be at least one consignment per BOL", consignmentsAndContainers);
            }

            if (SlacIndicator != null)
            {
                var slac = new[] { "SlacIndicator" };
                var allowedSlacIndicators = new[] { "Y", "N" };
                if (!allowedSlacIndicators.ToList().Contains(SlacIndicator))
                {
                    yield return new ValidationResult("Allowed SlacIndicator : Y - YES , N - NO", slac);
                }
            }

            //
            var contractCariage = new[] { "ContractCarriageCondition" };
            if (ContractCarriageCondition != null)
            {

                var allowedcontractCariages = new[] { "C&F", "CIF", "FOB", "D/D", "XXX" };
                if (!allowedcontractCariages.ToList().Contains(ContractCarriageCondition))
                {
                    yield return new ValidationResult("Allowed ContractCarriageCondition : C&F - COST & FREIGHT; CIF - COST, INSURANCE AND FREIGHT;FOB - FREE ON BOARD; D/D - DOOR TO DOOR; XXX - OTHERS", contractCariage);
                }
            }


        }

    }
}
