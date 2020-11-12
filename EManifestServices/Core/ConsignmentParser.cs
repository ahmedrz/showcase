using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class ConsignmentParser
    {
        public static ConsignmentDetails Parse(string line)
        {
            ConsignmentDetails consignment = new ConsignmentDetails();
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
            consignment.SerialNumber = valuesArray[1] != ""  ? (int?)Convert.ToInt32(valuesArray[1]) : null;
            consignment.MarksAndNumbers = valuesArray[2];
            consignment.CargoDescription = valuesArray[3];
            consignment.UsedOrNew = valuesArray[4] != "" ? (char?)valuesArray[4].First() : null;
            consignment.CommodityCode = valuesArray[5];
            consignment.ConsignmentPackages = valuesArray[6] != null ? (int?)Convert.ToInt32(valuesArray[6]) : null;
            consignment.PackageType = valuesArray[7];
            consignment.PackageTypeCode = valuesArray[8];
            consignment.NumberOfPallets = valuesArray[9] != ""  ? (int?)Convert.ToInt32(valuesArray[9]) : null;
            consignment.ConsignmentWeight = valuesArray[10] != ""  ? (double?)Convert.ToDouble(valuesArray[10]) : null;
            consignment.ConsignmentValume = valuesArray[11] != ""  ? (double?)Convert.ToDouble(valuesArray[11]) : null;
            consignment.DangerousGoodsIndicator = valuesArray[12] != "" ? (char?)valuesArray[12].First() : null;
            consignment.IMOClassNumber = valuesArray[13];
            consignment.UnNumberOfDangerousGoods = valuesArray[14];
            consignment.FlashPoint = valuesArray[15] != ""  ? (double?)Convert.ToDouble(valuesArray[15]) : null;
            consignment.UnitOfTemperatureForFlashPoint = valuesArray[16] != "" ? (char?)valuesArray[16].First() : null;
            consignment.StorageRequestedForDangerousGoods = valuesArray[17] != "" ? (char?)valuesArray[17].First() : null;
            consignment.RefrigerationRequired = valuesArray[18] != "" ? (char?)valuesArray[18].First() : null;
            consignment.MinimumTemperatureOfRefrigeration = valuesArray[19] != ""  ? (double?)Convert.ToDouble(valuesArray[19]) : null;
            consignment.MaximumTemperatureOfRefrigeration = valuesArray[20] != ""  ? (double?)Convert.ToDouble(valuesArray[20]) : null;
            consignment.UnitOfTemperatureForRegrigeration = valuesArray[21] != "" ? (char?)valuesArray[21].First() : null;



            //TODO properties validation
            //TODO business validation the object

            return consignment;

        }

    }
}