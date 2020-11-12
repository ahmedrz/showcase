using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class VoyageParser
    {
        public static VoyageDetails Parse(string line)
        {
            VoyageDetails voyage = new VoyageDetails();
            var valuesArray = line.Split(',');
            var header = valuesArray[0];
            if (header != "VOY")
            {
                throw new Exception($"This line does not start with VOY , Line Header : {header}");
            }
            if (valuesArray.Count() != 11)
            {
                throw new Exception($"The number of values is different from the number of Voyage properties. Line : {line}");
            }
            CultureInfo provider = CultureInfo.InvariantCulture;
            voyage.LineCode = valuesArray[1];
            voyage.VoyageAgentCode = valuesArray[2];
            voyage.VesselName = valuesArray[3];
            voyage.AgentVoyageNumber = valuesArray[4];
            voyage.PortCodeOfDischarge = valuesArray[5];
            voyage.ExpectedToArriveDate = valuesArray[6] != "" ? (DateTime?)DateTime.ParseExact(valuesArray[6], "DD-MMM-YYYY", provider) : null;
            voyage.RotationNumber = valuesArray[7];
            voyage.MessageType = valuesArray[8];
            voyage.NumberOfInstalment = valuesArray[9] != ""  ? (int?)Convert.ToInt32(valuesArray[9]) : null;
            voyage.AgentManifestSequenceNumber = valuesArray[10] != ""  ? (int?)Convert.ToInt32(valuesArray[10]) : null;
            //TODO properties validation
            //TODO business validation the object
            return voyage;

        }
    }
}