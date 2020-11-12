using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EmanifestParser
{
    //public class VoyageParser
    //{
    //    public static VoyageDetails Parse(string line)
    //    {
    //        line = line.Replace(Environment.NewLine, "");
    //        VoyageDetails voyage = new VoyageDetails();
    //        var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList();
    //        var header = valuesArray[0];
    //        if (header != "VOY")
    //        {
    //            throw new Exception($"This line does not start with VOY , Line Header : {header}");
    //        }
    //        if (valuesArray.Count() != 12)
    //        {
    //            throw new Exception($"The number of values is different from the number of Voyage properties. Line : {line}");
    //        }
    //        CultureInfo provider = CultureInfo.InvariantCulture;
    //        voyage.LineCode = valuesArray[1];
    //        voyage.VoyageAgentCode = valuesArray[2];
    //        voyage.VesselName = valuesArray[3];
    //        voyage.AgentVoyageNumber = valuesArray[4];
    //        voyage.PortCodeOfDischarge = valuesArray[5];
    //        voyage.ExpectedToArriveDate = valuesArray[6] != null ? (DateTime?)DateTime.ParseExact(valuesArray[6], "dd-MMM-yyyy", provider) : null;
    //        voyage.RotationNumber = valuesArray[7];
    //        voyage.MessageType = valuesArray[8];
    //        voyage.NumberOfInstalment = valuesArray[9] != null ? (int?)Convert.ToInt32(valuesArray[9]) : null;
    //        voyage.AgentManifestSequenceNumber = valuesArray[10] != null ? (int?)Convert.ToInt32(valuesArray[10]) : null;
    //        voyage.ManifestType = valuesArray[11];
    //        //TODO properties validation
    //        //TODO business validation the object
    //        return voyage;

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

    public class VoyageParser<VOY, BOL, CON, CTR, VEH> where VOY : IVoyageDetails<BOL, CON, CTR, VEH>, new()
        where BOL : IBOLDetails<CON, CTR, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
        where CTR : IContainerDetails<CON, VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        public static VOY Parse(string line)
        {
            line = line.Replace(Environment.NewLine, "");
            VOY voyage = new VOY();
            var valuesArray = line.Split(',').ToList().Select(item => NullIfEmpty(item.Trim('"').Trim())).ToList();
            var header = valuesArray[0];
            if (header != "VOY")
            {
                throw new Exception($"This line does not start with VOY , Line Header : {header}");
            }
            if (valuesArray.Count() != 12)
            {
                throw new Exception($"The number of values is different from the number of Voyage properties. Line : {line}");
            }
            CultureInfo provider = CultureInfo.InvariantCulture;
            voyage.LineCode = valuesArray[1];
            voyage.VoyageAgentCode = valuesArray[2];
            voyage.VesselName = valuesArray[3];
            voyage.AgentVoyageNumber = valuesArray[4];
            voyage.PortCodeOfDischarge = valuesArray[5];
            voyage.ExpectedToArriveDate = valuesArray[6] != null ? (DateTime?)DateTime.ParseExact(valuesArray[6], "dd-MMM-yyyy", provider) : null;
            voyage.RotationNumber = valuesArray[7];
            voyage.MessageType = valuesArray[8];
            voyage.NumberOfInstalment = valuesArray[9] != null ? (int?)Convert.ToInt32(valuesArray[9]) : null;
            voyage.AgentManifestSequenceNumber = valuesArray[10] != null ? (int?)Convert.ToInt32(valuesArray[10]) : null;
            voyage.ManifestType = valuesArray[11];
            //TODO properties validation
            //TODO business validation the object
            return voyage;

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