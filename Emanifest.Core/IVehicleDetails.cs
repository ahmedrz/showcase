using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IVehicleDetails
    {
        Guid VehicleDetailsId { get; set; }

        int? SerialNumber { get; set; }

        string VehicleEquipmentIndicator { get; set; }

        string UsedOrNew { get; set; }

        string ChasisNumber { get; set; }

        string CaseNumber { get; set; }

        string Make { get; set; }

        string Model { get; set; }

        string EngineNumber { get; set; }

        string YearBuilt { get; set; }

        string Color { get; set; }

        string RollingOrStatic { get; set; }

        string DescriptionOfGood { get; set; }

        string AdditionalAccesseries { get; set; }

        double? WeightInKg { get; set; }

        double? Volume { get; set; }

        string Remarks { get; set; }

        Guid? ConsignmentDetailsId { get; set; }
    }
}
