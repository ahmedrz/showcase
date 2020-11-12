using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanifestParser
{
    //public class VehicleSerializer
    //{
    //    public string Serialize(VehicleDetails vehicle)
    //    {
    //        string result = "\"VEH\",";
    //        result += $"\"{EmptyIfNull(vehicle.SerialNumber)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.VehicleEquipmentIndicator)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.UsedOrNew)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.ChasisNumber)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.CaseNumber)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.Make)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.Model)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.EngineNumber)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.YearBuilt)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.Color)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.RollingOrStatic)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.DescriptionOfGood)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.AdditionalAccesseries)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.WeightInKg)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.Volume)}\",";
    //        result += $"\"{EmptyIfNull(vehicle.Remarks)}\"";
    //        result += Environment.NewLine;
    //        return result;
    //    }

    //    private string EmptyIfNull(object value)
    //    {
    //        if (value == null)
    //        {
    //            return "";
    //        }
    //        if (value is double)
    //        {
    //            return ((double)value).ToString("0.000");
    //        }
    //        return value.ToString();
    //    }
    //}

    public class VehicleSerializer<VEH> where VEH : IVehicleDetails, new()
    {
        public string Serialize(VEH vehicle)
        {
            string result = "\"VEH\",";
            result += $"\"{EmptyIfNull(vehicle.SerialNumber)}\",";
            result += $"\"{EmptyIfNull(vehicle.VehicleEquipmentIndicator)}\",";
            result += $"\"{EmptyIfNull(vehicle.UsedOrNew)}\",";
            result += $"\"{EmptyIfNull(vehicle.ChasisNumber)}\",";
            result += $"\"{EmptyIfNull(vehicle.CaseNumber)}\",";
            result += $"\"{EmptyIfNull(vehicle.Make)}\",";
            result += $"\"{EmptyIfNull(vehicle.Model)}\",";
            result += $"\"{EmptyIfNull(vehicle.EngineNumber)}\",";
            result += $"\"{EmptyIfNull(vehicle.YearBuilt)}\",";
            result += $"\"{EmptyIfNull(vehicle.Color)}\",";
            result += $"\"{EmptyIfNull(vehicle.RollingOrStatic)}\",";
            result += $"\"{EmptyIfNull(vehicle.DescriptionOfGood)}\",";
            result += $"\"{EmptyIfNull(vehicle.AdditionalAccesseries)}\",";
            result += $"\"{EmptyIfNull(vehicle.WeightInKg)}\",";
            result += $"\"{EmptyIfNull(vehicle.Volume)}\",";
            result += $"\"{EmptyIfNull(vehicle.Remarks)}\"";
            result += Environment.NewLine;
            return result;
        }

        private string EmptyIfNull(object value)
        {
            if (value == null)
            {
                return "";
            }
            if (value is double)
            {
                return ((double)value).ToString("0.000");
            }
            return value.ToString();
        }
    }
}
