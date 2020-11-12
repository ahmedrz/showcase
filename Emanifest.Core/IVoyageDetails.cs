using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IVoyageDetails<BOL, CON, CTR, VEH>
        where BOL : IBOLDetails<CON, CTR, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
        where CTR : IContainerDetails<CON, VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        Guid VoyageDetailsId { get; set; }
        string LineCode { get; set; }
        string VoyageAgentCode { get; set; }
        string VesselName { get; set; }
        string AgentVoyageNumber { get; set; }
        string PortCodeOfDischarge { get; set; }
        DateTime? ExpectedToArriveDate { get; set; }
        string RotationNumber { get; set; }
        string MessageType { get; set; }
        int? NumberOfInstalment { get; set; }
        int? AgentManifestSequenceNumber { get; set; }
        string ManifestType { get; set; }

        //extra fields
        Guid? CarrierId { get; set; }

        Guid? StatusId { get; set; }

        Guid? UserId { get; set; }

        DateTime? UploadDate { get; set; }

        string FileName { get; set; }

        Guid? VesselId { get; set; }

        ICollection<BOL> BOLDetails { get; set; }
    }
}
