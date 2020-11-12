using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanifestParser
{
    //public class ContainerSerializer
    //{
    //    public string Serialize(ContainerDetails container)
    //    {
    //        string result = "\"CTR\",";
    //        result += $"\"{EmptyIfNull(container.ContainerNo)}\",";
    //        result += $"\"{EmptyIfNull(container.CheckDigit)}\",";
    //        result += $"\"{EmptyIfNull(container.ISOCode)}\",";
    //        result += $"\"{EmptyIfNull(container.TareWeight)}\",";
    //        result += $"\"{EmptyIfNull(container.SealNumber)}\"";
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
    //            return ((double)value).ToString("0.0");
    //        }
    //        return value.ToString();
    //    }
    //}

    public class ContainerSerializer<CTR, CON, VEH> where CTR : IContainerDetails<CON, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        public string Serialize(CTR container)
        {
            string result = "\"CTR\",";
            result += $"\"{EmptyIfNull(container.ContainerNo)}\",";
            result += $"\"{EmptyIfNull(container.CheckDigit)}\",";
            result += $"\"{EmptyIfNull(container.ISOCode)}\",";
            result += $"\"{EmptyIfNull(container.TareWeight)}\",";
            result += $"\"{EmptyIfNull(container.SealNumber)}\",";
            result += $"\"{EmptyIfNull(container.ContainerOwnerType)}\"";
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
                return ((double)value).ToString("0.0");
            }
            return value.ToString();
        }
    }
}
