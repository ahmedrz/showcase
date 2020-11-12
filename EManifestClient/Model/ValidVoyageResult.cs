using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EManifestClient.Model
{
    public class ValidVoyageResult
    {
        public string VesselName { get; set; }
        public string PortCode { get; set; }
        public string Voyage { get; set; }
        public DateTime ExpectedArrivalDate { get; set; }
        public string Status { get; set; }
    }
}
