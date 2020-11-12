using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanifestDownloader.Models
{
    public class VoyageResult
    {
        public Guid VoyageDetailsId { get; set; }
        public string VesselName { get; set; }
        public string VoyageNo { get; set; }
        public int Installment { get; set; }
        public string AgentName { get; set; }
        public string PortCode { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public bool Downloaded { get; set; }

    }
}
