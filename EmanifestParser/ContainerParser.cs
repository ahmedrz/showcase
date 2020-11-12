using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmanifestParser
{
    //public class ContainerParser
    //{
    //    public static ContainerDetails Parse(string line)
    //    {
    //        line = line.Replace(Environment.NewLine, "");
    //        ContainerDetails container = new ContainerDetails();
    //        var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList(); ;
    //        var header = valuesArray[0];
    //        if (header != "CTR")
    //        {
    //            throw new Exception($"This line does not start with CTR , Line Header : {header}");
    //        }
    //        if (valuesArray.Count() != 6)
    //        {
    //            throw new Exception($"The number of values is different from the number of container properties. Line : {line}");
    //        }
    //        container.ContainerNo = valuesArray[1];
    //        container.CheckDigit = valuesArray[2];
    //        container.ISOCode = valuesArray[3];
    //        container.TareWeight = valuesArray[4] != null ? (double?)Convert.ToDouble(valuesArray[4]) : null;
    //        container.SealNumber = valuesArray[5];
    //        //TODO properties validation
    //        //TODO business validation the object
    //        return container;

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

    public class ContainerParser<CTR, CON, VEH> where CTR : IContainerDetails<CON, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        public static CTR Parse(string line)
        {
            line = line.Replace(Environment.NewLine, "");
            CTR container = new CTR();
            var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList(); ;
            var header = valuesArray[0];
            if (header != "CTR")
            {
                throw new Exception($"This line does not start with CTR , Line Header : {header}");
            }
            if (valuesArray.Count() != 7)
            {
                throw new Exception($"The number of values is different from the number of container properties. Line : {line}");
            }
            container.ContainerNo = valuesArray[1];
            container.CheckDigit = valuesArray[2];
            container.ISOCode = valuesArray[3];
            container.TareWeight = valuesArray[4] != null ? (double?)Convert.ToDouble(valuesArray[4]) : null;
            container.SealNumber = valuesArray[5];
            container.ContainerOwnerType = valuesArray[6];
            //TODO properties validation
            //TODO business validation the object
            return container;

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