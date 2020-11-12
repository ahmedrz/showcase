using System;
using System.Collections;
using System.Collections.Generic;

namespace Emanifest.Core
{
    public interface IBOLDetails<CON, CTR, VEH>
        where CON : IConsignmentDetails<VEH>, new()
        where CTR : IContainerDetails<CON, VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        Guid BOLDetailsId { get; set; }

        string BillOfLadingNumber { get; set; }

        string BoxPartneringLineCode { get; set; }

        string BoxPartneringAgentCode { get; set; }

        string PortCodeOfOrigin { get; set; }

        string PortCodeOfLoading { get; set; }

        string PortCodeOfDischarge { get; set; }

        string PortCodeOfDestination { get; set; }
        DateTime? DateOfLoading { get; set; }

        string ManifestRegisterationNumber { get; set; }

        string TradeCode { get; set; }

        string TransShipmentMode { get; set; }

        string BillOfLadingOwnerName { get; set; }

        string BillOfLadingOwnerAddress { get; set; }

        string CargoCode { get; set; }

        string ConsolidatedCargoIndicator { get; set; }

        string StorageRequestCode { get; set; }

        string ContainerServiceType { get; set; }

        string CountryOfOrigin { get; set; }

        string OriginalConsigneeName { get; set; }

        string OriginalConsigneeAddress { get; set; }

        string OriginalVesselName { get; set; }

        string OriginalVoyageNumber { get; set; }

        string OriginalBolNumber { get; set; }

        string OriginalShipperName { get; set; }

        string OriginalShipperAddress { get; set; }

        string ShipperName { get; set; }

        string ShipperAddress { get; set; }

        string ShipperCountryCode { get; set; }


        string ConsigneeCode { get; set; }

        string ConsigneeName { get; set; }

        string ConsigneeAddress { get; set; }


        string Notify1Code { get; set; }

        string Notify1Name { get; set; }

        string Notify1Address { get; set; }

        string Notify2Code { get; set; }

        string Notify2Name { get; set; }

        string Notify2Address { get; set; }

        string Notify3Code { get; set; }

        string Notify3Name { get; set; }

        string Notify3Address { get; set; }

        string MarksAndNumbers { get; set; }

        string CommodityCode { get; set; }

        string CommodityDescription { get; set; }

        int? Packages { get; set; }

        string PackageType { get; set; }

        string PackageTypeCode { get; set; }

        string ContainerNumber { get; set; }

        string CheckDigit { get; set; }

        int? NoOfContainers { get; set; }

        int? NoOfTeus { get; set; }

        double? TotalTareWeight { get; set; }

        double? CargoWeight { get; set; }

        double? GrossWeight { get; set; }

        double? CargoVolume { get; set; }

        int? TotalQuantity { get; set; }

        double? FreightTonne { get; set; }

        int? NoOfPallets { get; set; }

        string SlacIndicator { get; set; }

        string ContractCarriageCondition { get; set; }

        string Remarks { get; set; }
        Guid VoyageDetailsId { get; set; }
        ICollection<CON> ConsignmentDetails { get; set; }
        ICollection<CTR> ContainerDetails { get; set; }
    }
}
