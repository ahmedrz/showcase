using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class VehicleParser
    {
        public static VehicleDetails Parse(string line)
        {
            VehicleDetails vehicle = new VehicleDetails();
            var valuesArray = line.Split(',');
            var header = valuesArray[0];
            if (header != "VEH")
            {
                throw new Exception($"This line does not start with VEH , Line Header : {header}");
            }
            if (valuesArray.Count() != 11)
            {
                throw new Exception($"The number of values is different from the number of vehicle properties. Line : {line}");
            }

            vehicle.VehicleEquipmentIndicator = valuesArray[1] != "" ? (char?)valuesArray[1].First() : null;
            vehicle.UsedOrNew = valuesArray[2] != "" ? (char?)valuesArray[2].First() : null;
            vehicle.ChasisNumber = valuesArray[3];
            vehicle.CaseNumber = valuesArray[4];
            vehicle.Make = valuesArray[5];
            vehicle.Model = valuesArray[6];
            vehicle.EngineNumber = valuesArray[7];
            vehicle.YearBuilt = valuesArray[8];
            vehicle.Color = valuesArray[9];
            vehicle.RollingOrStatic = valuesArray[10] != "" ? (char?)valuesArray[10].First() : null;
            vehicle.DescriptionOfGood = valuesArray[11];
            vehicle.AdditionalAccesseries = valuesArray[12];
            vehicle.WeightInKg = valuesArray[13] != ""  ? (double?)Convert.ToDouble(valuesArray[13]) : null;
            vehicle.Volume = valuesArray[14] != ""  ? (double?)Convert.ToDouble(valuesArray[14]) : null;
            vehicle.Remarks = valuesArray[15];



            //TODO properties validation
            //TODO business validation the object

            return vehicle;

        }

    }
}