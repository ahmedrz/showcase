using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class BolParser
    {
        public static BOLDetails Parse(string line)
        {
            BOLDetails bol = new BOLDetails();
            var valuesArray = line.Split(',');
            var header = valuesArray[0];
            if (header != "BOL")
            {
                throw new Exception($"This line does not start with BOL , Line Header : {header}");
            }
            if (valuesArray.Count() != 11)
            {
                throw new Exception($"The number of values is different from the number of vehicle properties. Line : {line}");
            }
            CultureInfo provider = CultureInfo.InvariantCulture;
            bol.BillOfLadingNumber = valuesArray[1];
            bol.BoxPartneringLineCode = valuesArray[2];
            bol.BoxPartneringAgentCode = valuesArray[3];
            bol.PortCodeOfOrigin = valuesArray[4];
            bol.PortCodeOfLoading = valuesArray[5];
            bol.PortCodeOfDischarge = valuesArray[6];
            bol.PortCodeOfDestination = valuesArray[7];
            bol.DateOfLoading = valuesArray[8] != "" ? (DateTime?)DateTime.ParseExact(valuesArray[8], "DD-MMM-YYYY", provider) : null; ;
            bol.ManifestRegisterationNumber = valuesArray[9];
            bol.TradeCode = valuesArray[10] != "" ? (char?)valuesArray[10].First() : null;
            bol.TransShipmentMode = valuesArray[11] != "" ? (char?)valuesArray[11].First() : null;
            bol.BillOfLadingOwnerName = valuesArray[12];
            bol.BillOfLadingOwnerAddress = valuesArray[13];
            bol.CargoCode = valuesArray[14] != "" ? (char?)valuesArray[14].First() : null;
            bol.ConsolidatedCargoIndicator = valuesArray[15] != "" ? (char?)valuesArray[15].First() : null;
            bol.StorageRequestCode = valuesArray[16] != "" ? (char?)valuesArray[16].First() : null;
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
            bol.Packages = valuesArray[44] != ""  ? (int?)Convert.ToInt32(valuesArray[44]) : null;
            bol.PackageType = valuesArray[45];
            bol.PackageTypeCode = valuesArray[46];
            bol.ContainerNumber = valuesArray[47];
            bol.CheckDigit = valuesArray[48] != "" ? (char?)valuesArray[48].First() : null;
            bol.NoOfContainers = valuesArray[49] != ""  ? (int?)Convert.ToInt32(valuesArray[49]) : null;
            bol.NoOfTeus = valuesArray[50] != ""  ? (int?)Convert.ToInt32(valuesArray[50]) : null;
            bol.TotalTareWeight = valuesArray[51] != ""  ? (double?)Convert.ToDouble(valuesArray[51]) : null;
            bol.CargoWeight = valuesArray[52] != ""  ? (double?)Convert.ToDouble(valuesArray[52]) : null;
            bol.GrossWeight = valuesArray[53] != ""  ? (double?)Convert.ToDouble(valuesArray[53]) : null;
            bol.CargoVolume = valuesArray[54] != ""  ? (double?)Convert.ToDouble(valuesArray[54]) : null;
            bol.TotalQuantity = valuesArray[55] != ""  ? (int?)Convert.ToInt32(valuesArray[55]) : null;
            bol.FreightTonne = valuesArray[56] != ""  ? (double?)Convert.ToDouble(valuesArray[56]) : null;
            bol.NoOfPallets = valuesArray[57] != ""  ? (int?)Convert.ToInt32(valuesArray[57]) : null;
            bol.SlacIndicator = valuesArray[58] != "" ? (char?)valuesArray[58].First() : null;
            bol.ContractCarriageCondition = valuesArray[59];
            bol.Remarks = valuesArray[60];



            //TODO properties validation
            //TODO business validation the object

            return bol;

        }

    }
}