namespace IQManClient.DAL
{
    using Emanifest.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public partial class ConsignmentDetails : IConsignmentDetails<VehicleDetails>, IValidatableObject, INotifyPropertyChanged
    {
        public ConsignmentDetails()
        {
            VehicleDetails = new ObservableCollection<VehicleDetails>();
            DangerousGoodsIndicator = "N";
            RefrigerationRequired = "N";
        }
        [Key]
        public Guid ConsignmentDetailsId { get; set; }
        [Required]
        [Range(1, 999999)]
        public int? SerialNumber { get; set; }
        [Required]
        [StringLength(200)]
        [NoCommaString]
        public string MarksAndNumbers { get; set; }
        [Required]
        [StringLength(100)]
        [NoCommaString]
        public string CargoDescription { get; set; }
        [StringLength(1)]
        [SpecificString(Info = @"U - USED COMMODITY ; N - NEW COMMODITY", AllowedStrings = new string[] { "U", "N" })]
        public string UsedOrNew { get; set; }
        [Required]
        [StringLength(10)]
        [NoCommaString]
        public string CommodityCode { get; set; }
        [Required]
        [Range(1, 999999999)]
        public int? ConsignmentPackages { get; set; }
        [Required]
        [StringLength(30)]
        [NoCommaString]
        public string PackageType { get; set; }
        [Required]
        [StringLength(3)]
        [NoCommaString]
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
        [StringLength(1)]
        [SpecificString(Info = @"Y - YES; N - NO", AllowedStrings = new string[] { "Y", "N" })]
        public string DangerousGoodsIndicator { get; set; }
        [StringLength(3)]
        [NoCommaString]
        public string IMOClassNumber { get; set; }
        [StringLength(5)]
        public string UnNumberOfDangerousGoods { get; set; }
        [Range(-9999.9, 9999.9)]
        public double? FlashPoint { get; set; }
        [StringLength(1)]
        [SpecificString(Info = @"C - CENTIGRADE; F - FARENHEIT", AllowedStrings = new string[] { "C", "F" })]
        public string UnitOfTemperatureForFlashPoint { get; set; }
        [StringLength(1)]
        public string StorageRequestedForDangerousGoods { get; set; }
        [Required]
        [StringLength(1)]
        [SpecificString(Info = @"Y - YES; N - NO", AllowedStrings = new string[] { "Y", "N" })]
        public string RefrigerationRequired { get; set; }
        [Range(-9999.9, 9999.9)]
        public double? MinimumTemperatureOfRefrigeration { get; set; }
        [Range(-9999.9, 9999.9)]
        public double? MaximumTemperatureOfRefrigeration { get; set; }
        [StringLength(1)]
        [SpecificString(Info = @"C - CENTIGRADE; F - FARENHEIT", AllowedStrings = new string[] { "C", "F" })]
        public string UnitOfTemperatureForRegrigeration { get; set; }

        public Guid? ContainerDetailsId { get; set; }

        public Guid? BOLDetailsId { get; set; }

        public virtual BOLDetails BOLDetails { get; set; }

        public virtual ContainerDetails ContainerDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleDetails> VehicleDetails { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DangerousGoodsIndicator == "Y")
            {
                var imoClass = new[] { "IMOClassNumber" };
                if (IMOClassNumber == null)
                {
                    yield return new ValidationResult("IMOClassNumber is required if DANGEROUS INDICATOR IS = 'Y'", imoClass);
                }

                var unNumber = new[] { "UnNumberOfDangerousGoods" };
                if (UnNumberOfDangerousGoods == null)
                {
                    yield return new ValidationResult("UnNumberOfDangerousGoods is required if DANGEROUS INDICATOR IS = 'Y'", unNumber);
                }

                var storageRequested = new[] { "StorageRequestedForDangerousGoods" };
                if (StorageRequestedForDangerousGoods == null)
                {
                    yield return new ValidationResult("StorageRequestedForDangerousGoods is required if DANGEROUS INDICATOR IS = 'Y'", storageRequested);
                }
                else
                {
                    var allowedStorageRequests = new[] { "D", "S", "Y" };
                    if (!allowedStorageRequests.ToList().Contains(StorageRequestedForDangerousGoods))
                    {
                        yield return new ValidationResult("Allowed StorageRequests :D - DIRECT DELIVEY; S - STORAGE IN SHEDS; Y - STORAGE IN YARDS", storageRequested);
                    }
                }
            }

            if (FlashPoint != null)
            {
                var uniOfTemperature = new[] { "UnitOfTemperatureForFlashPoint" };
                if (UnitOfTemperatureForFlashPoint == null)
                {
                    yield return new ValidationResult("UnitOfTemperatureForFlashPoint is MANDATORY IF FLASH POINT IS GIVEN'", uniOfTemperature);
                }
            }

            if (RefrigerationRequired == "Y")
            {
                var minimumTemperature = new[] { "MinimumTemperatureOfRefrigeration" };
                if (MinimumTemperatureOfRefrigeration == null)
                {
                    yield return new ValidationResult("MinimumTemperatureOfRefrigeration is MANDATOTY IF REFRIGERATION REQUIRED='Y'", minimumTemperature);
                }

                var maximumTemperature = new[] { "MaximumTemperatureOfRefrigeration" };
                if (MaximumTemperatureOfRefrigeration == null)
                {
                    yield return new ValidationResult("MaximumTemperatureOfRefrigeration is MANDATOTY IF REFRIGERATION REQUIRED='Y'", maximumTemperature);
                }

                var uniOfTemperature = new[] { "UnitOfTemperatureForRegrigeration" };
                if (UnitOfTemperatureForRegrigeration == null)
                {
                    yield return new ValidationResult("UnitOfTemperatureForRegrigeration is MANDATOTY IF REFRIGERATION REQUIRED='Y'", uniOfTemperature);
                }
            }
        }

    }
}
