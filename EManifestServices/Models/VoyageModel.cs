using EManifestServices.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EManifestServices.Models
{
    public class VoyageModel
    {
        public VoyageModel()
        {
        }
        [Required]
        public Guid? VoyageDetailsId { get; set; }
        [Required]
        public string LineCode { get; set; }
        [Required]
        public string VoyageAgentCode { get; set; }
        [Required]
        public string VesselName { get; set; }
        [Required]
        public string AgentVoyageNumber { get; set; }
        [Required]
        public string PortCodeOfDischarge { get; set; }
        [Required]
        public DateTime? ExpectedToArriveDate { get; set; }
        [Required]
        public string RotationNumber { get; set; }
        [Required]
        public string MessageType { get; set; }
        [Required]
        public int? NumberOfInstalment { get; set; }
        [Required]
        public int? AgentManifestSequenceNumber { get; set; }
        [Required]
        public DateTime? UploadDate { get; set; }
        [Required]
        public string FileName { get; set; }
        public Guid? CarrierId { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? UserId { get; set; }
        public StatusModel Status { get; set; }
    }
}