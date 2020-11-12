namespace EManifestServices.DAL
{
    using Emanifest.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContainerDetails : IContainerDetails<ConsignmentDetails, VehicleDetails>, INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ContainerDetails()
        {
            ConsignmentDetails = new ObservableCollection<ConsignmentDetails>();

        }
        [Key]
        public Guid ContainerDetailsId { get; set; }
        [Required]
        [StringLength(10)]
        public string ContainerNo { get; set; }
        [Required]
        [StringLength(1)]
        public string CheckDigit { get; set; }
        [Required]
        [StringLength(4)]
        public string ISOCode { get; set; }
        [Required]
        [Range(1, 9999.9)]
        public double? TareWeight { get; set; }
        [Required]
        [StringLength(10)]
        public string SealNumber { get; set; }
        [StringLength(3)]
        [SpecificString(Info = @"COC - CARRIER OWNED CONTAINER
                            SOC - SHIPPER OWNED CONTAINER",
                        AllowedStrings = new string[] { "COC", "SOC" })]
        public string ContainerOwnerType { get; set; }

        public Guid? BOLDetailsId { get; set; }

        public virtual BOLDetails BOLDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConsignmentDetails> ConsignmentDetails { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
