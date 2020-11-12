using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanifestParser
{
    //public class ConsignmentSerializer
    //{
    //    public string Serialize(ConsignmentDetails consignment)
    //    {
    //        string result = "\"CON\",";
    //        result += $"\"{EmptyIfNull(consignment.SerialNumber)}\",";
    //        result += $"\"{EmptyIfNull(consignment.MarksAndNumbers)}\",";
    //        result += $"\"{EmptyIfNull(consignment.CargoDescription)}\",";
    //        result += $"\"{EmptyIfNull(consignment.UsedOrNew)}\",";
    //        result += $"\"{EmptyIfNull(consignment.CommodityCode)}\",";
    //        result += $"\"{EmptyIfNull(consignment.ConsignmentPackages)}\",";
    //        result += $"\"{EmptyIfNull(consignment.PackageType)}\",";
    //        result += $"\"{EmptyIfNull(consignment.PackageTypeCode)}\",";
    //        result += $"\"{EmptyIfNull(consignment.NumberOfPallets)}\",";
    //        result += $"\"{EmptyIfNull(consignment.ConsignmentWeight, "0.000")}\",";
    //        result += $"\"{EmptyIfNull(consignment.ConsignmentValume, "0.000")}\",";
    //        result += $"\"{EmptyIfNull(consignment.DangerousGoodsIndicator)}\",";
    //        result += $"\"{EmptyIfNull(consignment.IMOClassNumber)}\",";
    //        result += $"\"{EmptyIfNull(consignment.UnNumberOfDangerousGoods)}\",";
    //        result += $"\"{EmptyIfNull(consignment.FlashPoint)}\",";
    //        result += $"\"{EmptyIfNull(consignment.UnitOfTemperatureForFlashPoint)}\",";
    //        result += $"\"{EmptyIfNull(consignment.StorageRequestedForDangerousGoods)}\",";
    //        result += $"\"{EmptyIfNull(consignment.RefrigerationRequired)}\",";
    //        result += $"\"{EmptyIfNull(consignment.MinimumTemperatureOfRefrigeration)}\",";
    //        result += $"\"{EmptyIfNull(consignment.MaximumTemperatureOfRefrigeration)}\",";
    //        result += $"\"{EmptyIfNull(consignment.UnitOfTemperatureForFlashPoint)}\"";
    //        result += Environment.NewLine;
    //        return result;
    //    }

    //    private string EmptyIfNull(object value, string doubleFormat = "0.0")
    //    {
    //        if (value == null)
    //        {
    //            return "";
    //        }
    //        if (value is double)
    //        {
    //            return ((double)value).ToString(doubleFormat);
    //        }
    //        return value.ToString();
    //    }
    //}

    public class ConsignmentSerializer<CON, VEH> where CON : IConsignmentDetails<VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        public string Serialize(CON consignment)
        {
            string result = "\"CON\",";
            result += $"\"{EmptyIfNull(consignment.SerialNumber)}\",";
            result += $"\"{EmptyIfNull(consignment.MarksAndNumbers)}\",";
            result += $"\"{EmptyIfNull(consignment.CargoDescription)}\",";
            result += $"\"{EmptyIfNull(consignment.UsedOrNew)}\",";
            result += $"\"{EmptyIfNull(consignment.CommodityCode)}\",";
            result += $"\"{EmptyIfNull(consignment.ConsignmentPackages)}\",";
            result += $"\"{EmptyIfNull(consignment.PackageType)}\",";
            result += $"\"{EmptyIfNull(consignment.PackageTypeCode)}\",";
            result += $"\"{EmptyIfNull(consignment.NumberOfPallets)}\",";
            result += $"\"{EmptyIfNull(consignment.ConsignmentWeight, "0.000")}\",";
            result += $"\"{EmptyIfNull(consignment.ConsignmentValume, "0.000")}\",";
            result += $"\"{EmptyIfNull(consignment.DangerousGoodsIndicator)}\",";
            result += $"\"{EmptyIfNull(consignment.IMOClassNumber)}\",";
            result += $"\"{EmptyIfNull(consignment.UnNumberOfDangerousGoods)}\",";
            result += $"\"{EmptyIfNull(consignment.FlashPoint)}\",";
            result += $"\"{EmptyIfNull(consignment.UnitOfTemperatureForFlashPoint)}\",";
            result += $"\"{EmptyIfNull(consignment.StorageRequestedForDangerousGoods)}\",";
            result += $"\"{EmptyIfNull(consignment.RefrigerationRequired)}\",";
            result += $"\"{EmptyIfNull(consignment.MinimumTemperatureOfRefrigeration)}\",";
            result += $"\"{EmptyIfNull(consignment.MaximumTemperatureOfRefrigeration)}\",";
            result += $"\"{EmptyIfNull(consignment.UnitOfTemperatureForFlashPoint)}\"";
            result += Environment.NewLine;
            return result;
        }

        private string EmptyIfNull(object value, string doubleFormat = "0.0")
        {
            if (value == null)
            {
                return "";
            }
            if (value is double)
            {
                return ((double)value).ToString(doubleFormat);
            }
            return value.ToString();
        }
    }
}
