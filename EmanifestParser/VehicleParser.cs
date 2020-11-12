using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmanifestParser
{
    //public class VehicleParser
    //{
    //    public static VehicleDetails Parse(string line)
    //    {
    //        line = line.Replace(Environment.NewLine, "");
    //        VehicleDetails vehicle = new VehicleDetails();
    //        var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList(); ;
    //        var header = valuesArray[0];
    //        if (header != "VEH")
    //        {
    //            throw new Exception($"This line does not start with VEH , Line Header : {header}");
    //        }
    //        if (valuesArray.Count() != 17)
    //        {
    //            throw new Exception($"The number of values is different from the number of vehicle properties. Line : {line}");
    //        }
    //        vehicle.SerialNumber = valuesArray[1] != null ? (int?)Convert.ToInt32(valuesArray[1]) : null;
    //        vehicle.VehicleEquipmentIndicator = valuesArray[2];
    //        vehicle.UsedOrNew = valuesArray[3];
    //        vehicle.ChasisNumber = valuesArray[4];
    //        vehicle.CaseNumber = valuesArray[5];
    //        vehicle.Make = valuesArray[6];
    //        vehicle.Model = valuesArray[7];
    //        vehicle.EngineNumber = valuesArray[8];
    //        vehicle.YearBuilt = valuesArray[9];
    //        vehicle.Color = valuesArray[10];
    //        vehicle.RollingOrStatic = valuesArray[11];
    //        vehicle.DescriptionOfGood = valuesArray[12];
    //        vehicle.AdditionalAccesseries = valuesArray[13];
    //        vehicle.WeightInKg = valuesArray[14] != null ? (double?)Convert.ToDouble(valuesArray[14]) : null;
    //        vehicle.Volume = valuesArray[15] != null ? (double?)Convert.ToDouble(valuesArray[15]) : null;
    //        vehicle.Remarks = valuesArray[16];



    //        //TODO properties validation
    //        //TODO business validation the object

    //        return vehicle;

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

    public class VehicleParser<VEH> where VEH : IVehicleDetails, new()
    {
        public static VEH Parse(string line)
        {
            line = line.Replace(Environment.NewLine, "");
            VEH vehicle = new VEH();
            var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList(); ;
            var header = valuesArray[0];
            if (header != "VEH")
            {
                throw new Exception($"This line does not start with VEH , Line Header : {header}");
            }
            if (valuesArray.Count() != 17)
            {
                throw new Exception($"The number of values is different from the number of vehicle properties. Line : {line}");
            }
            vehicle.SerialNumber = valuesArray[1] != null ? (int?)Convert.ToInt32(valuesArray[1]) : null;
            vehicle.VehicleEquipmentIndicator = valuesArray[2];
            vehicle.UsedOrNew = valuesArray[3];
            vehicle.ChasisNumber = valuesArray[4];
            vehicle.CaseNumber = valuesArray[5];
            vehicle.Make = valuesArray[6];
            vehicle.Model = valuesArray[7];
            vehicle.EngineNumber = valuesArray[8];
            vehicle.YearBuilt = valuesArray[9];
            vehicle.Color = valuesArray[10];
            vehicle.RollingOrStatic = valuesArray[11];
            vehicle.DescriptionOfGood = valuesArray[12];
            vehicle.AdditionalAccesseries = valuesArray[13];
            vehicle.WeightInKg = valuesArray[14] != null ? (double?)Convert.ToDouble(valuesArray[14]) : null;
            vehicle.Volume = valuesArray[15] != null ? (double?)Convert.ToDouble(valuesArray[15]) : null;
            vehicle.Remarks = valuesArray[16];



            //TODO properties validation
            //TODO business validation the object

            return vehicle;

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