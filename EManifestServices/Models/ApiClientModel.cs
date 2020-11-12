using EManifestServices.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class ApiClientModel
    {
        public Guid ApiClientId { get; set; }
        [Required]
        [StringLength(20)]
        public string ApiClientName { get; set; }
        [RequiredIf("ApiClientId", "00000000-0000-0000-0000-000000000000")]
        [StringLength(20)]
        [DataType(DataType.Password)]
        public string ApiClientPassword { get; set; }
        [Required]
        [StringLength(40)]
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public Guid? CarrierId { get; set; }

    }
}