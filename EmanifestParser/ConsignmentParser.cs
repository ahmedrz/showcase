using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmanifestParser
{
    //public class ConsignmentParser
    //{
    //    public static ConsignmentDetails Parse(string line)
    //    {
    //        line = line.Replace(Environment.NewLine, "");
    //        ConsignmentDetails consignment = new ConsignmentDetails();
    //        var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList();
    //        var header = valuesArray[0];
    //        if (header != "CON")
    //        {
    //            throw new Exception($"This line does not start with CON , Line Header : {header}");
    //        }
    //        if (valuesArray.Count() != 22)
    //        {
    //            throw new Exception($"The number of values is different from the number of consignment properties. Line : {line}");
    //        }
    //        consignment.SerialNumber = valuesArray[1] != null ? (int?)Convert.ToInt32(valuesArray[1]) : null;
    //        consignment.MarksAndNumbers = valuesArray[2];
    //        consignment.CargoDescription = valuesArray[3];
    //        consignment.UsedOrNew = valuesArray[4];
    //        consignment.CommodityCode = valuesArray[5];
    //        consignment.ConsignmentPackages = valuesArray[6] != null ? (int?)Convert.ToInt32(valuesArray[6]) : null;
    //        consignment.PackageType = valuesArray[7];
    //        consignment.PackageTypeCode = valuesArray[8];
    //        consignment.NumberOfPallets = valuesArray[9] != null ? (int?)Convert.ToInt32(valuesArray[9]) : null;
    //        consignment.ConsignmentWeight = valuesArray[10] != null ? (double?)Convert.ToDouble(valuesArray[10]) : null;
    //        consignment.ConsignmentValume = valuesArray[11] != null ? (double?)Convert.ToDouble(valuesArray[11]) : null;
    //        consignment.DangerousGoodsIndicator = valuesArray[12];
    //        consignment.IMOClassNumber = valuesArray[13];
    //        consignment.UnNumberOfDangerousGoods = valuesArray[14];
    //        consignment.FlashPoint = valuesArray[15] != null ? (double?)Convert.ToDouble(valuesArray[15]) : null;
    //        consignment.UnitOfTemperatureForFlashPoint = valuesArray[16];
    //        consignment.StorageRequestedForDangerousGoods = valuesArray[17];
    //        consignment.RefrigerationRequired = valuesArray[18];
    //        consignment.MinimumTemperatureOfRefrigeration = valuesArray[19] != null ? (double?)Convert.ToDouble(valuesArray[19]) : null;
    //        consignment.MaximumTemperatureOfRefrigeration = valuesArray[20] != null ? (double?)Convert.ToDouble(valuesArray[20]) : null;
    //        consignment.UnitOfTemperatureForRegrigeration = valuesArray[21];



    //        //TODO properties validation
    //        //TODO business validation the object

    //        return consignment;

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

    public class ConsignmentParser<CON, VEH> where CON : IConsignmentDetails<VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        public static CON Parse(string line)
        {
            line = line.Replace(Environment.NewLine, "");
            CON consignment = new CON();
            var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList();
            var header = valuesArray[0];
            if (header != "CON")
            {
                throw new Exception($"This line does not start with CON , Line Header : {header}");
            }
            if (valuesArray.Count() != 22)
            {
                throw new Exception($"The number of values is different from the number of consignment properties. Line : {line}");
            }
            consignment.SerialNumber = valuesArray[1] != null ? (int?)Convert.ToInt32(valuesArray[1]) : null;
            consignment.MarksAndNumbers = valuesArray[2];
            consignment.CargoDescription = valuesArray[3];
            consignment.UsedOrNew = valuesArray[4];
            consignment.CommodityCode = valuesArray[5];
            consignment.ConsignmentPackages = valuesArray[6] != null ? (int?)Convert.ToInt32(valuesArray[6]) : null;
            consignment.PackageType = valuesArray[7];
            consignment.PackageTypeCode = valuesArray[8];
            consignment.NumberOfPallets = valuesArray[9] != null ? (int?)Convert.ToInt32(valuesArray[9]) : null;
            consignment.ConsignmentWeight = valuesArray[10] != null ? (double?)Convert.ToDouble(valuesArray[10]) : null;
            consignment.ConsignmentValume = valuesArray[11] != null ? (double?)Convert.ToDouble(valuesArray[11]) : null;
            consignment.DangerousGoodsIndicator = valuesArray[12];
            consignment.IMOClassNumber = valuesArray[13];
            consignment.UnNumberOfDangerousGoods = valuesArray[14];
            consignment.FlashPoint = valuesArray[15] != null ? (double?)Convert.ToDouble(valuesArray[15]) : null;
            consignment.UnitOfTemperatureForFlashPoint = valuesArray[16];
            consignment.StorageRequestedForDangerousGoods = valuesArray[17];
            consignment.RefrigerationRequired = valuesArray[18];
            consignment.MinimumTemperatureOfRefrigeration = valuesArray[19] != null ? (double?)Convert.ToDouble(valuesArray[19]) : null;
            consignment.MaximumTemperatureOfRefrigeration = valuesArray[20] != null ? (double?)Convert.ToDouble(valuesArray[20]) : null;
            consignment.UnitOfTemperatureForRegrigeration = valuesArray[21];



            //TODO properties validation
            //TODO business validation the object

            return consignment;

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