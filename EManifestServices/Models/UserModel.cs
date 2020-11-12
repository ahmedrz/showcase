using EManifestServices.Attributes;
using EManifestServices.DAL;
using EManifestServices.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        [Required, StringLength(10)]
        public string UserName { get; set; }
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [RequiredIf("UserId", "00000000-0000-0000-0000-000000000000"), StringLength(10)]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        [RequiredIf("UserType", "2")]
        public Guid? CarrierId { get; set; }
        public string CarrierName { get; set; }
        [Required]
        public int? UserType { get; set; }
    }
}