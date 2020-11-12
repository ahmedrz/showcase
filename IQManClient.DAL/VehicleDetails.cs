namespace IQManClient.DAL
{
    using Emanifest.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class VehicleDetails : IVehicleDetails, IValidatableObject, INotifyPropertyChanged
    {
        [Key]
        public Guid VehicleDetailsId { get; set; }
        [Required]
        [Range(1, 999999)]
        public int? SerialNumber { get; set; }
        [Required]
        [StringLength(1)]
        [SpecificString(Info = @"V - VEHICLE; E - EQUIPMENT; X - OTHERS", AllowedStrings = new string[] { "V", "E", "X" })]
        public string VehicleEquipmentIndicator { get; set; }
        [Required]
        [StringLength(1)]
        [SpecificString(Info = @"U - USED ; N - NEW", AllowedStrings = new string[] { "U", "N" })]
        public string UsedOrNew { get; set; }
        [StringLength(24)]
        [NoCommaString]
        public string ChasisNumber { get; set; }
        [StringLength(24)]
        [NoCommaString]
        public string CaseNumber { get; set; }
        [Required]
        [StringLength(20)]
        [NoCommaString]
        public string Make { get; set; }
        [Required]
        [StringLength(20)]
        [NoCommaString]
        public string Model { get; set; }
        [StringLength(30)]
        [NoCommaString]
        public string EngineNumber { get; set; }
        [StringLength(4)]
        [NoCommaString]
        public string YearBuilt { get; set; }
        [StringLength(16)]
        [NoCommaString]
        public string Color { get; set; }
        [Required]
        [StringLength(1)]
        [SpecificString(Info = @"R - ROLLING; S - STATIC", AllowedStrings = new string[] { "R", "S" })]
        public string RollingOrStatic { get; set; }
        [Required]
        [StringLength(200)]
        [NoCommaString]
        public string DescriptionOfGood { get; set; }
        [StringLength(100)]
        [NoCommaString]
        public string AdditionalAccesseries { get; set; }
        [Range(1, 999999999.999)]
        public double? WeightInKg { get; set; }
        [Range(1, 999999999.999)]
        public double? Volume { get; set; }
        [NoCommaString]
        [StringLength(200)]
        public string Remarks { get; set; }

        public Guid? ConsignmentDetailsId { get; set; }

        public virtual ConsignmentDetails ConsignmentDetails { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ChasisNumber == null)
            {
                var caseNumber = new[] { "CaseNumber" };
                if (CaseNumber == null)
                {
                    yield return new ValidationResult("CaseNumber is required if ChasisNumber not mentioned.", caseNumber);
                }
            }
        }
    }
}
