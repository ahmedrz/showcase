using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IVessels
    {
        Guid VesselId { get; set; }

        string VesselName { get; set; }

        double GRT { get; set; }

        string VesselCountry { get; set; }
        string IMONo { get; set; }
        string CallSign { get; set; }
        double? LOA { get; set; }
        double? LWL { get; set; }
        double? DraftAFT { get; set; }
        double? DraftFWD { get; set; }

        Guid CarrierId { get; set; }
    }
}
