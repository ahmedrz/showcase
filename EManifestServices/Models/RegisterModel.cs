using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class RegisterModel
    {
        public Guid? RequestId { get; set; }
        [Required, StringLength(50)]
        public string CompanyName { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(20)]
        public string PhoneNo { get; set; }
        public string CompanyInfo { get; set; }
        [Required, StringLength(10)]
        public string UserName { get; set; }
        [Required, StringLength(50)]
        public string UserEmail { get; set; }
        [Required, StringLength(10)]
        public string Password { get; set; }

        public DateTime? RequestDate { get; set; }
        public bool? Approved { get; set; }

        public Guid? CarrierId { get; set; }

    }
}