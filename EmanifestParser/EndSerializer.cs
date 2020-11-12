using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmanifestParser
{
    //public class EndSerializer
    //{
    //    public string Serialize(VoyageDetails voyage)
    //    {
    //        string result = "\"END\",";
    //        result += $"\"{voyage.BOLDetails.Where(b => b.CargoCode == "F" || b.CargoCode == "L").Count()}\",";
    //        result += $"\"{voyage.BOLDetails.Where(b => b.CargoCode != "F" && b.CargoCode != "L").Count()}\",";
    //        result += $"\"Version No : {Assembly.GetEntryAssembly().GetName().Version},Product sr. no : IQMAN/108\"";
    //        return result;
    //    }
    //}

    public class EndSerializer<VOY, BOL, CON, CTR, VEH> where VOY : IVoyageDetails<BOL, CON, CTR, VEH>, new()
        where BOL : IBOLDetails<CON, CTR, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
        where CTR : IContainerDetails<CON, VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        public string Serialize(VOY voyage)
        {
            string result = "\"END\",";
            result += $"\"{voyage.BOLDetails.Where(b => b.CargoCode == "F" || b.CargoCode == "L").Count()}\",";
            result += $"\"{voyage.BOLDetails.Where(b => b.CargoCode != "F" && b.CargoCode != "L").Count()}\",";
            result += $"\"Version No : {Assembly.GetEntryAssembly().GetName().Version},Product sr. no : IQMAN/108\"";
            return result;
        }
    }
}
