namespace IQManClient.DAL
{
    using Emanifest.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public partial class VoyageDetails : IVoyageDetails<BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>, INotifyPropertyChanged //: IValidatableObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VoyageDetails()
        {
            BOLDetails = new ObservableCollection<BOLDetails>();
            MessageType = "MFI";
        }
        [Key]
        public Guid VoyageDetailsId { get; set; }
        [Required]
        [StringLength(30)]
        [NoCommaString]
        public string LineCode { get; set; }
        [Required]
        [StringLength(30)]
        [NoCommaString]
        public string VoyageAgentCode { get; set; }
        [Required]
        [StringLength(30)]
        [NoCommaString]
        public string VesselName { get; set; }
        [Required]
        [StringLength(10)]
        [NoCommaString]
        public string AgentVoyageNumber { get; set; }
        [Required]
        [StringLength(5)]
        [NoCommaString]
        public string PortCodeOfDischarge { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? ExpectedToArriveDate { get; set; }
        [StringLength(6)]
        public string RotationNumber { get; set; }
        [Required]
        [StringLength(3)]
        [SpecificString(AllowedStrings = new string[] { "MFI" }, Info = "MessageType should be MFI for manifest upload")]
        public string MessageType { get; set; }
        [Required]
        [Range(minimum: 1, maximum: 999)]
        public int? NumberOfInstalment { get; set; }
        [Range(minimum: 1, maximum: 99999)]
        public int? AgentManifestSequenceNumber { get; set; }
        public string ManifestType { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOLDetails> BOLDetails { get; set; }




        //extra fields
        public Guid? CarrierId { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? UploadDate { get; set; }

        public string FileName { get; set; }

        public Guid? VesselId { get; set; }
        public virtual Vessels Vessels { get; set; }

        public virtual Carriers Carriers { get; set; }

        public virtual Status Status { get; set; }

        public virtual Users Users { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var bolDetails = new[] { "BOLDetails" };
        //    if (BOLDetails.Count == 0)
        //    {
        //        yield return new ValidationResult("There must be at least one BOL per Voyage", bolDetails);
        //    }
        //}
    }
}
