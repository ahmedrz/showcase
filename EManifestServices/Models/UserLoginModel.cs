using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class UserLoginModel
    {
        [Required, StringLength(10)]
        public string UserName { get; set; }
        [Required, StringLength(10)]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string url { get; set; }
    }
}