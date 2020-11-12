using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class StatusModel
    {
        public Guid StatusId { get; set; }
        public string StatusDesc { get; set; }
        public bool? AllowModify { get; set; }
    }
}