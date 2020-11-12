using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortSystem.DAL
{
    public class Vessels : IVessels
    {
        public Vessels()
        {
            VoyageDetails = new ObservableCollection<VoyageDetails>();
        }
        [Key]
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

        public Guid CarrierId { get; set; }
        public Carriers Carriers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoyageDetails> VoyageDetails { get; set; }
    }
}
