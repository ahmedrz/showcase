using Emanifest.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace EManifestServices.DAL
{
    public class ApiClients : IApiClient
    {
        [Key]
        public Guid ApiClientId { get; set; }
        [Required]
        [StringLength(20)]
        public string ApiClientName { get; set; }
        [Required]
        [StringLength(20)]
        public string ApiClientPassword { get; set; }
        [Required]
        [StringLength(40)]
        public string Role { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CarrierId { get; set; }
        public Carriers Carriers { get; set; }
    }
}
