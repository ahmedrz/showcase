using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class ContainerParser
    {
        public static ContainerDetails Parse(string line)
        {
            ContainerDetails container = new ContainerDetails();
            var valuesArray = line.Split(',');
            var header = valuesArray[0];
            if (header != "CTR")
            {
                throw new Exception($"This line does not start with CTR , Line Header : {header}");
            }
            if (valuesArray.Count() != 5)
            {
                throw new Exception($"The number of values is different from the number of container properties. Line : {line}");
            }
            container.ContainerNo = valuesArray[1];
            container.CheckDigit = valuesArray[2] != "" ? (char?)valuesArray[2].First() : null;
            container.TareWeight = valuesArray[3] != "" ? (double?)Convert.ToDouble(valuesArray[3]) : null;
            container.SealNumber = valuesArray[4];
            //TODO properties validation
            //TODO business validation the object
            return container;

        }

    }
}