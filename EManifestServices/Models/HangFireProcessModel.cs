using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class HangFireProcessModel
    {
        public string TempFileName { get; set; }
        public Guid VoyageDetailsId { get; set; }
        public string VesselName { get; set; }
        public string VoyageNumber { get; set; }
        public Guid? UserId { get; set; }
        public Guid? CarrierId { get; set; }
        public string Email { get; set; }

    }
}