using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EmanifestParser
{
    //public class BolParser
    //{
    //    public static BOLDetails Parse(string line)
    //    {
    //        line = line.Replace(Environment.NewLine, "");
    //        BOLDetails bol = new BOLDetails();
    //        var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList();
    //        var header = valuesArray[0];
    //        if (header != "BOL")
    //        {
    //            throw new Exception($"This line does not start with BOL , Line Header : {header}");
    //        }
    //        if (valuesArray.Count() != 61)
    //        {
    //            throw new Exception($"The number of values is different from the number of Bill of lading properties. Line : {line}");
    //        }
    //        CultureInfo provider = CultureInfo.InvariantCulture;
    //        bol.BillOfLadingNumber = valuesArray[1];
    //        bol.BoxPartneringLineCode = valuesArray[2];
    //        bol.BoxPartneringAgentCode = valuesArray[3];
    //        bol.PortCodeOfOrigin = valuesArray[4];
    //        bol.PortCodeOfLoading = valuesArray[5];
    //        bol.PortCodeOfDischarge = valuesArray[6];
    //        bol.PortCodeOfDestination = valuesArray[7];
    //        bol.DateOfLoading = valuesArray[8] != null ? (DateTime?)DateTime.ParseExact(valuesArray[8], "dd-MMM-yyyy", provider) : null; ;
    //        bol.ManifestRegisterationNumber = valuesArray[9];
    //        bol.TradeCode = valuesArray[10];
    //        bol.TransShipmentMode = valuesArray[11];
    //        bol.BillOfLadingOwnerName = valuesArray[12];
    //        bol.BillOfLadingOwnerAddress = valuesArray[13];
    //        bol.CargoCode = valuesArray[14];
    //        bol.ConsolidatedCargoIndicator = valuesArray[15];
    //        bol.StorageRequestCode = valuesArray[16];
    //        bol.ContainerServiceType = valuesArray[17];
    //        bol.CountryOfOrigin = valuesArray[18];
    //        bol.OriginalConsigneeName = valuesArray[19];
    //        bol.OriginalConsigneeAddress = valuesArray[20];
    //        bol.OriginalVesselName = valuesArray[21];
    //        bol.OriginalVoyageNumber = valuesArray[22];
    //        bol.OriginalBolNumber = valuesArray[23];
    //        bol.OriginalShipperName = valuesArray[24];
    //        bol.OriginalShipperAddress = valuesArray[25];
    //        bol.ShipperName = valuesArray[26];
    //        bol.ShipperAddress = valuesArray[27];
    //        bol.ShipperCountryCode = valuesArray[28];
    //        bol.ConsigneeCode = valuesArray[29];
    //        bol.ConsigneeName = valuesArray[30];
    //        bol.ConsigneeAddress = valuesArray[31];
    //        bol.Notify1Code = valuesArray[32];
    //        bol.Notify1Name = valuesArray[33];
    //        bol.Notify1Address = valuesArray[34];
    //        bol.Notify2Code = valuesArray[35];
    //        bol.Notify2Name = valuesArray[36];
    //        bol.Notify2Address = valuesArray[37];
    //        bol.Notify3Code = valuesArray[38];
    //        bol.Notify3Name = valuesArray[39];
    //        bol.Notify3Address = valuesArray[40];
    //        bol.MarksAndNumbers = valuesArray[41];
    //        bol.CommodityCode = valuesArray[42];
    //        bol.CommodityDescription = valuesArray[43];
    //        bol.Packages = valuesArray[44] != null ? (int?)Convert.ToInt32(valuesArray[44]) : null;
    //        bol.PackageType = valuesArray[45];
    //        bol.PackageTypeCode = valuesArray[46];
    //        bol.ContainerNumber = valuesArray[47];
    //        bol.CheckDigit = valuesArray[48];
    //        bol.NoOfContainers = valuesArray[49] != null ? (int?)Convert.ToInt32(valuesArray[49]) : null;
    //        bol.NoOfTeus = valuesArray[50] != null ? (int?)Convert.ToInt32(valuesArray[50]) : null;
    //        bol.TotalTareWeight = valuesArray[51] != null ? (double?)Convert.ToDouble(valuesArray[51]) : null;
    //        bol.CargoWeight = valuesArray[52] != null ? (double?)Convert.ToDouble(valuesArray[52]) : null;
    //        bol.GrossWeight = valuesArray[53] != null ? (double?)Convert.ToDouble(valuesArray[53]) : null;
    //        bol.CargoVolume = valuesArray[54] != null ? (double?)Convert.ToDouble(valuesArray[54]) : null;
    //        bol.TotalQuantity = valuesArray[55] != null ? (int?)Convert.ToInt32(valuesArray[55]) : null;
    //        bol.FreightTonne = valuesArray[56] != null ? (double?)Convert.ToDouble(valuesArray[56]) : null;
    //        bol.NoOfPallets = valuesArray[57] != null ? (int?)Convert.ToInt32(valuesArray[57]) : null;
    //        bol.SlacIndicator = valuesArray[58];
    //        bol.ContractCarriageCondition = valuesArray[59];
    //        bol.Remarks = valuesArray[60];



    //        //TODO properties validation
    //        //TODO business validation the object

    //        return bol;

    //    }
    //    private static string NullIfEmpty(string value)
    //    {
    //        if (value == "")
    //        {
    //            return null;
    //        }
    //        return value;
    //    }

    //}
    public class BolParser<BOL, CON, CTR, VEH> where BOL : IBOLDetails<CON, CTR, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
         where CTR : IContainerDetails<CON, VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        public static BOL Parse(string line)
        {
            line = line.Replace(Environment.NewLine, "");
            BOL bol = new BOL();
            var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList();
            var header = valuesArray[0];
            if (header != "BOL")
            {
                throw new Exception($"This line does not start with BOL , Line Header : {header}");
            }
            if (valuesArray.Count() != 61)
            {
                throw new Exception($"The number of values is different from the number of Bill of lading properties. Line : {line}");
            }
            CultureInfo provider = CultureInfo.InvariantCulture;
            bol.BillOfLadingNumber = valuesArray[1];
            bol.BoxPartneringLineCode = valuesArray[2];
            bol.BoxPartneringAgentCode = valuesArray[3];
            bol.PortCodeOfOrigin = valuesArray[4];
            bol.PortCodeOfLoading = valuesArray[5];
            bol.PortCodeOfDischarge = valuesArray[6];
            bol.PortCodeOfDestination = valuesArray[7];
            bol.DateOfLoading = valuesArray[8] != null ? (DateTime?)DateTime.ParseExact(valuesArray[8], "dd-MMM-yyyy", provider) : null; ;
            bol.ManifestRegisterationNumber = valuesArray[9];
            bol.TradeCode = valuesArray[10];
            bol.TransShipmentMode = valuesArray[11];
            bol.BillOfLadingOwnerName = valuesArray[12];
            bol.BillOfLadingOwnerAddress = valuesArray[13];
            bol.CargoCode = valuesArray[14];
            bol.ConsolidatedCargoIndicator = valuesArray[15];
            bol.StorageRequestCode = valuesArray[16];
            bol.ContainerServiceType = valuesArray[17];
            bol.CountryOfOrigin = valuesArray[18];
            bol.OriginalConsigneeName = valuesArray[19];
            bol.OriginalConsigneeAddress = valuesArray[20];
            bol.OriginalVesselName = valuesArray[21];
            bol.OriginalVoyageNumber = valuesArray[22];
            bol.OriginalBolNumber = valuesArray[23];
            bol.OriginalShipperName = valuesArray[24];
            bol.OriginalShipperAddress = valuesArray[25];
            bol.ShipperName = valuesArray[26];
            bol.ShipperAddress = valuesArray[27];
            bol.ShipperCountryCode = valuesArray[28];
            bol.ConsigneeCode = valuesArray[29];
            bol.ConsigneeName = valuesArray[30];
            bol.ConsigneeAddress = valuesArray[31];
            bol.Notify1Code = valuesArray[32];
            bol.Notify1Name = valuesArray[33];
            bol.Notify1Address = valuesArray[34];
            bol.Notify2Code = valuesArray[35];
            bol.Notify2Name = valuesArray[36];
            bol.Notify2Address = valuesArray[37];
            bol.Notify3Code = valuesArray[38];
            bol.Notify3Name = valuesArray[39];
            bol.Notify3Address = valuesArray[40];
            bol.MarksAndNumbers = valuesArray[41];
            bol.CommodityCode = valuesArray[42];
            bol.CommodityDescription = valuesArray[43];
            bol.Packages = valuesArray[44] != null ? (int?)Convert.ToInt32(valuesArray[44]) : null;
            bol.PackageType = valuesArray[45];
            bol.PackageTypeCode = valuesArray[46];
            bol.ContainerNumber = valuesArray[47];
            bol.CheckDigit = valuesArray[48];
            bol.NoOfContainers = valuesArray[49] != null ? (int?)Convert.ToInt32(valuesArray[49]) : null;
            bol.NoOfTeus = valuesArray[50] != null ? (int?)Convert.ToInt32(valuesArray[50]) : null;
            bol.TotalTareWeight = valuesArray[51] != null ? (double?)Convert.ToDouble(valuesArray[51]) : null;
            bol.CargoWeight = valuesArray[52] != null ? (double?)Convert.ToDouble(valuesArray[52]) : null;
            bol.GrossWeight = valuesArray[53] != null ? (double?)Convert.ToDouble(valuesArray[53]) : null;
            bol.CargoVolume = valuesArray[54] != null ? (double?)Convert.ToDouble(valuesArray[54]) : null;
            bol.TotalQuantity = valuesArray[55] != null ? (int?)Convert.ToInt32(valuesArray[55]) : null;
            bol.FreightTonne = valuesArray[56] != null ? (double?)Convert.ToDouble(valuesArray[56]) : null;
            bol.NoOfPallets = valuesArray[57] != null ? (int?)Convert.ToInt32(valuesArray[57]) : null;
            bol.SlacIndicator = valuesArray[58];
            bol.ContractCarriageCondition = valuesArray[59];
            bol.Remarks = valuesArray[60];



            //TODO properties validation
            //TODO business validation the object

            return bol;

        }
        private static string NullIfEmpty(string value)
        {
            if (value == "")
            {
                return null;
            }
            return value;
        }

    }
}