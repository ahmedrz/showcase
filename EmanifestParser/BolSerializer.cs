using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanifestParser
{
    //public class BolSerializer
    //{
    //    public string Serialize(BOLDetails Bol)
    //    {
    //        string result = "\"BOL\",";
    //        result += $"\"{EmptyIfNull(Bol.BillOfLadingNumber)}\",";
    //        result += $"\"{EmptyIfNull(Bol.BoxPartneringLineCode)}\",";
    //        result += $"\"{EmptyIfNull(Bol.BoxPartneringAgentCode)}\",";
    //        result += $"\"{EmptyIfNull(Bol.PortCodeOfOrigin)}\",";
    //        result += $"\"{EmptyIfNull(Bol.PortCodeOfLoading)}\",";
    //        result += $"\"{EmptyIfNull(Bol.PortCodeOfDischarge)}\",";
    //        result += $"\"{EmptyIfNull(Bol.PortCodeOfDestination)}\",";
    //        result += $"\"{EmptyIfNull(Bol.DateOfLoading)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ManifestRegisterationNumber)}\",";
    //        result += $"\"{EmptyIfNull(Bol.TradeCode, "0.000")}\",";
    //        result += $"\"{EmptyIfNull(Bol.TransShipmentMode, "0.000")}\",";
    //        result += $"\"{EmptyIfNull(Bol.BillOfLadingOwnerName)}\",";
    //        result += $"\"{EmptyIfNull(Bol.BillOfLadingOwnerAddress)}\",";
    //        result += $"\"{EmptyIfNull(Bol.CargoCode)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ConsolidatedCargoIndicator)}\",";
    //        result += $"\"{EmptyIfNull(Bol.StorageRequestCode)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ContainerServiceType)}\",";
    //        result += $"\"{EmptyIfNull(Bol.CountryOfOrigin)}\",";
    //        result += $"\"{EmptyIfNull(Bol.OriginalConsigneeName)}\",";
    //        result += $"\"{EmptyIfNull(Bol.OriginalConsigneeAddress)}\",";
    //        result += $"\"{EmptyIfNull(Bol.OriginalVesselName)}\",";
    //        result += $"\"{EmptyIfNull(Bol.OriginalVoyageNumber)}\",";
    //        result += $"\"{EmptyIfNull(Bol.OriginalBolNumber)}\",";
    //        result += $"\"{EmptyIfNull(Bol.OriginalShipperName)}\",";
    //        result += $"\"{EmptyIfNull(Bol.OriginalShipperAddress)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ShipperName)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ShipperAddress)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ShipperCountryCode)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ConsigneeCode)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ConsigneeName)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ConsigneeAddress)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Notify1Code)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Notify1Name)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Notify1Address)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Notify2Code)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Notify2Name)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Notify2Address)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Notify3Code)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Notify3Name)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Notify3Address)}\",";
    //        result += $"\"{EmptyIfNull(Bol.MarksAndNumbers)}\",";
    //        result += $"\"{EmptyIfNull(Bol.CommodityCode)}\",";
    //        result += $"\"{EmptyIfNull(Bol.CommodityDescription)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Packages)}\",";
    //        result += $"\"{EmptyIfNull(Bol.PackageType)}\",";
    //        result += $"\"{EmptyIfNull(Bol.PackageTypeCode)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ContainerNumber)}\",";
    //        result += $"\"{EmptyIfNull(Bol.CheckDigit)}\",";
    //        result += $"\"{EmptyIfNull(Bol.NoOfContainers)}\",";
    //        result += $"\"{EmptyIfNull(Bol.NoOfTeus)}\",";
    //        result += $"\"{EmptyIfNull(Bol.TotalTareWeight, "0.0")}\",";
    //        result += $"\"{EmptyIfNull(Bol.CargoWeight)}\",";
    //        result += $"\"{EmptyIfNull(Bol.GrossWeight)}\",";
    //        result += $"\"{EmptyIfNull(Bol.CargoVolume)}\",";
    //        result += $"\"{EmptyIfNull(Bol.TotalQuantity)}\",";
    //        result += $"\"{EmptyIfNull(Bol.FreightTonne)}\",";
    //        result += $"\"{EmptyIfNull(Bol.NoOfPallets)}\",";
    //        result += $"\"{EmptyIfNull(Bol.SlacIndicator)}\",";
    //        result += $"\"{EmptyIfNull(Bol.ContractCarriageCondition)}\",";
    //        result += $"\"{EmptyIfNull(Bol.Remarks)}\"";
    //        result += Environment.NewLine;
    //        return result;
    //    }

    //    private string EmptyIfNull(object value, string doubleFormat = "0.000")
    //    {
    //        if (value == null)
    //        {
    //            return "";
    //        }
    //        if (value is DateTime)
    //        {
    //            return ((DateTime)value).ToString("dd-MMM-yyyy");
    //        }
    //        if (value is double)
    //        {
    //            return ((double)value).ToString(doubleFormat);
    //        }
    //        return value.ToString();
    //    }
    //}

    public class BolSerializer<BOL, CON, CTR, VEH> where BOL : IBOLDetails<CON, CTR, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
         where CTR : IContainerDetails<CON, VEH>, new()
        where VEH : IVehicleDetails, new()

    {
        public string Serialize(BOL Bol)
        {
            string result = "\"BOL\",";
            result += $"\"{EmptyIfNull(Bol.BillOfLadingNumber)}\",";
            result += $"\"{EmptyIfNull(Bol.BoxPartneringLineCode)}\",";
            result += $"\"{EmptyIfNull(Bol.BoxPartneringAgentCode)}\",";
            result += $"\"{EmptyIfNull(Bol.PortCodeOfOrigin)}\",";
            result += $"\"{EmptyIfNull(Bol.PortCodeOfLoading)}\",";
            result += $"\"{EmptyIfNull(Bol.PortCodeOfDischarge)}\",";
            result += $"\"{EmptyIfNull(Bol.PortCodeOfDestination)}\",";
            result += $"\"{EmptyIfNull(Bol.DateOfLoading)}\",";
            result += $"\"{EmptyIfNull(Bol.ManifestRegisterationNumber)}\",";
            result += $"\"{EmptyIfNull(Bol.TradeCode, "0.000")}\",";
            result += $"\"{EmptyIfNull(Bol.TransShipmentMode, "0.000")}\",";
            result += $"\"{EmptyIfNull(Bol.BillOfLadingOwnerName)}\",";
            result += $"\"{EmptyIfNull(Bol.BillOfLadingOwnerAddress)}\",";
            result += $"\"{EmptyIfNull(Bol.CargoCode)}\",";
            result += $"\"{EmptyIfNull(Bol.ConsolidatedCargoIndicator)}\",";
            result += $"\"{EmptyIfNull(Bol.StorageRequestCode)}\",";
            result += $"\"{EmptyIfNull(Bol.ContainerServiceType)}\",";
            result += $"\"{EmptyIfNull(Bol.CountryOfOrigin)}\",";
            result += $"\"{EmptyIfNull(Bol.OriginalConsigneeName)}\",";
            result += $"\"{EmptyIfNull(Bol.OriginalConsigneeAddress)}\",";
            result += $"\"{EmptyIfNull(Bol.OriginalVesselName)}\",";
            result += $"\"{EmptyIfNull(Bol.OriginalVoyageNumber)}\",";
            result += $"\"{EmptyIfNull(Bol.OriginalBolNumber)}\",";
            result += $"\"{EmptyIfNull(Bol.OriginalShipperName)}\",";
            result += $"\"{EmptyIfNull(Bol.OriginalShipperAddress)}\",";
            result += $"\"{EmptyIfNull(Bol.ShipperName)}\",";
            result += $"\"{EmptyIfNull(Bol.ShipperAddress)}\",";
            result += $"\"{EmptyIfNull(Bol.ShipperCountryCode)}\",";
            result += $"\"{EmptyIfNull(Bol.ConsigneeCode)}\",";
            result += $"\"{EmptyIfNull(Bol.ConsigneeName)}\",";
            result += $"\"{EmptyIfNull(Bol.ConsigneeAddress)}\",";
            result += $"\"{EmptyIfNull(Bol.Notify1Code)}\",";
            result += $"\"{EmptyIfNull(Bol.Notify1Name)}\",";
            result += $"\"{EmptyIfNull(Bol.Notify1Address)}\",";
            result += $"\"{EmptyIfNull(Bol.Notify2Code)}\",";
            result += $"\"{EmptyIfNull(Bol.Notify2Name)}\",";
            result += $"\"{EmptyIfNull(Bol.Notify2Address)}\",";
            result += $"\"{EmptyIfNull(Bol.Notify3Code)}\",";
            result += $"\"{EmptyIfNull(Bol.Notify3Name)}\",";
            result += $"\"{EmptyIfNull(Bol.Notify3Address)}\",";
            result += $"\"{EmptyIfNull(Bol.MarksAndNumbers)}\",";
            result += $"\"{EmptyIfNull(Bol.CommodityCode)}\",";
            result += $"\"{EmptyIfNull(Bol.CommodityDescription)}\",";
            result += $"\"{EmptyIfNull(Bol.Packages)}\",";
            result += $"\"{EmptyIfNull(Bol.PackageType)}\",";
            result += $"\"{EmptyIfNull(Bol.PackageTypeCode)}\",";
            result += $"\"{EmptyIfNull(Bol.ContainerNumber)}\",";
            result += $"\"{EmptyIfNull(Bol.CheckDigit)}\",";
            result += $"\"{EmptyIfNull(Bol.NoOfContainers)}\",";
            result += $"\"{EmptyIfNull(Bol.NoOfTeus)}\",";
            result += $"\"{EmptyIfNull(Bol.TotalTareWeight, "0.0")}\",";
            result += $"\"{EmptyIfNull(Bol.CargoWeight)}\",";
            result += $"\"{EmptyIfNull(Bol.GrossWeight)}\",";
            result += $"\"{EmptyIfNull(Bol.CargoVolume)}\",";
            result += $"\"{EmptyIfNull(Bol.TotalQuantity)}\",";
            result += $"\"{EmptyIfNull(Bol.FreightTonne)}\",";
            result += $"\"{EmptyIfNull(Bol.NoOfPallets)}\",";
            result += $"\"{EmptyIfNull(Bol.SlacIndicator)}\",";
            result += $"\"{EmptyIfNull(Bol.ContractCarriageCondition)}\",";
            result += $"\"{EmptyIfNull(Bol.Remarks)}\"";
            result += Environment.NewLine;
            return result;
        }

        private string EmptyIfNull(object value, string doubleFormat = "0.000")
        {
            if (value == null)
            {
                return "";
            }
            if (value is DateTime || value is DateTime?)
            {
                CultureInfo ci = new CultureInfo("en-US");
                return ((DateTime)value).ToString("dd-MMM-yyyy", ci);
            }
            if (value is double)
            {
                return ((double)value).ToString(doubleFormat);
            }
            return value.ToString();
        }
    }
}
