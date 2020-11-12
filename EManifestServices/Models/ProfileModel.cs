using EManifestServices.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class ProfileModel
    {
        public Guid UserId { get; set; }
        [Required, StringLength(10)]
        public string UserName { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(10)]
        public string OldPassword { get; set; }
        [StringLength(10)]
        public string NewPassword { get; set; }

    }
}