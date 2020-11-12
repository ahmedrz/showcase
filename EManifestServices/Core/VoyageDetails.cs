using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class VoyageDetails
    {
        [Required]
        [StringLength(6)]
        public string LineCode { get; set; }
        [Required]
        [StringLength(6)]
        public string VoyageAgentCode { get; set; }
        [Required]
        [StringLength(30)]
        public string VesselName { get; set; }
        [Required]
        [StringLength(10)]
        public string AgentVoyageNumber { get; set; }
        [Required]
        [StringLength(5)]
        public string PortCodeOfDischarge { get; set; }
        [Required]
        public DateTime? ExpectedToArriveDate { get; set; }
        [StringLength(6)]
        public string RotationNumber { get; set; }
        [Required]
        [StringLength(3)]
        public string MessageType { get; set; }
        [Required]
        [Range(minimum: 1, maximum: 999)]
        public int? NumberOfInstalment { get; set; }
        [Required]
        [Range(minimum: 1, maximum: 99999)]
        public int? AgentManifestSequenceNumber { get; set; }

        public List<BOLDetails> BillOfLadings { get; set; }
        public VoyageDetails()
        {
            BillOfLadings = new List<BOLDetails>();
        }


    }
}