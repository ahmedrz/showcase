using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanifestParser
{
    //public class VoyageSerializer
    //{
    //    public string Serialize(VoyageDetails voyage)
    //    {
    //        string result = "\"VOY\",";
    //        result += $"\"{EmptyIfNull(voyage.LineCode)}\",";
    //        result += $"\"{EmptyIfNull(voyage.VoyageAgentCode)}\",";
    //        result += $"\"{EmptyIfNull(voyage.VesselName)}\",";
    //        result += $"\"{EmptyIfNull(voyage.AgentVoyageNumber)}\",";
    //        result += $"\"{EmptyIfNull(voyage.PortCodeOfDischarge)}\",";
    //        result += $"\"{EmptyIfNull(voyage.ExpectedToArriveDate)}\",";
    //        result += $"\"{EmptyIfNull(voyage.RotationNumber)}\",";
    //        result += $"\"{EmptyIfNull(voyage.MessageType)}\",";
    //        result += $"\"{EmptyIfNull(voyage.NumberOfInstalment)}\",";
    //        result += $"\"{EmptyIfNull(voyage.AgentManifestSequenceNumber)}\",";
    //        result += $"\"{voyage.ManifestType}\"";
    //        result += Environment.NewLine;
    //        return result;
    //    }

    //    private string EmptyIfNull(object value)
    //    {
    //        if (value == null)
    //        {
    //            return "";
    //        }
    //        if (value is DateTime)
    //        {
    //            return ((DateTime)value).ToString("dd-MMM-yyyy");
    //        }
    //        return value.ToString();
    //    }
    //}

    public class VoyageSerializer<VOY, BOL, CON, CTR, VEH> where VOY : IVoyageDetails<BOL, CON, CTR, VEH>, new()
        where BOL : IBOLDetails<CON, CTR, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
        where CTR : IContainerDetails<CON, VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        public string Serialize(VOY voyage)
        {
            string result = "\"VOY\",";
            result += $"\"{EmptyIfNull(voyage.LineCode)}\",";
            result += $"\"{EmptyIfNull(voyage.VoyageAgentCode)}\",";
            result += $"\"{EmptyIfNull(voyage.VesselName)}\",";
            result += $"\"{EmptyIfNull(voyage.AgentVoyageNumber)}\",";
            result += $"\"{EmptyIfNull(voyage.PortCodeOfDischarge)}\",";
            result += $"\"{EmptyIfNull(voyage.ExpectedToArriveDate)}\",";
            result += $"\"{EmptyIfNull(voyage.RotationNumber)}\",";
            result += $"\"{EmptyIfNull(voyage.MessageType)}\",";
            result += $"\"{EmptyIfNull(voyage.NumberOfInstalment)}\",";
            result += $"\"{EmptyIfNull(voyage.AgentManifestSequenceNumber)}\",";
            result += $"\"{voyage.ManifestType}\"";
            result += Environment.NewLine;
            return result;
        }

        private string EmptyIfNull(object value)
        {
            if (value == null)
            {
                return "";
            }
            if (value is DateTime || value is DateTime?)
            {
                CultureInfo ci = new CultureInfo("en-US");
                return ((DateTime)value).ToString("dd-MMM-yyyy", ci);
            }
            return value.ToString();
        }
    }
}
