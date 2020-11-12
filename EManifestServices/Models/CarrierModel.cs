using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class CarrierModel
    {
        public Guid CarrierId { get; set; }
        [Required, StringLength(100)]
        public string CarrierName { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(15)]
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        [StringLength(300)]
        public string Info { get; set; }
    }
}