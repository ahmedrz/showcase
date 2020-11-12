using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IContainerDetails<CON, VEH> where CON : IConsignmentDetails<VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        Guid ContainerDetailsId { get; set; }

        string ContainerNo { get; set; }

        string CheckDigit { get; set; }

        string ISOCode { get; set; }

        double? TareWeight { get; set; }

        string SealNumber { get; set; }
        string ContainerOwnerType { get; set; }

        Guid? BOLDetailsId { get; set; }

        ICollection<CON> ConsignmentDetails { get; set; }
    }
}
