using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emanifest.Core
{
    public interface IConsignmentDetails<VEH> where VEH : IVehicleDetails, new()
    {
        Guid ConsignmentDetailsId { get; set; }

        int? SerialNumber { get; set; }

        string MarksAndNumbers { get; set; }

        string CargoDescription { get; set; }

        string UsedOrNew { get; set; }

        string CommodityCode { get; set; }

        int? ConsignmentPackages { get; set; }

        string PackageType { get; set; }

        string PackageTypeCode { get; set; }

        int? NumberOfPallets { get; set; }

        double? ConsignmentWeight { get; set; }

        double? ConsignmentValume { get; set; }

        string DangerousGoodsIndicator { get; set; }

        string IMOClassNumber { get; set; }

        string UnNumberOfDangerousGoods { get; set; }

        double? FlashPoint { get; set; }

        string UnitOfTemperatureForFlashPoint { get; set; }

        string StorageRequestedForDangerousGoods { get; set; }

        string RefrigerationRequired { get; set; }

        double? MinimumTemperatureOfRefrigeration { get; set; }

        double? MaximumTemperatureOfRefrigeration { get; set; }

        string UnitOfTemperatureForRegrigeration { get; set; }

        Guid? ContainerDetailsId { get; set; }

        Guid? BOLDetailsId { get; set; }
        ICollection<VEH> VehicleDetails { get; set; }
    }
}
