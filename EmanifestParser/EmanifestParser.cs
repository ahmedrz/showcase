using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EmanifestParser
{
    //public class EmanifestParser
    //{
    //    Stream fileStream;
    //    public EmanifestParser(Stream fileStream)
    //    {
    //        this.fileStream = fileStream;
    //    }
    //    private VoyageDetails ParseReader(TextReader reader)
    //    {
    //        if (reader == null)
    //        {
    //            throw new Exception("reader is null");
    //        }
    //        object lastObject = null;
    //        VoyageDetails voyage = null;
    //        BOLDetails lastBol = null;
    //        Dictionary<BOLDetails, List<ConsignmentDetails>> allConsignments = new Dictionary<BOLDetails, List<ConsignmentDetails>>();
    //        //List<ContainerDetails> allContainers = new List<ContainerDetails>();
    //        ContainerDetails lastContainer = null;
    //        string line;
    //        while ((line = reader.ReadLine()) != null)
    //        {
    //            if (string.IsNullOrEmpty(line.Trim()))
    //            {
    //                continue;
    //            }
    //            if (line.StartsWith("\"VOY\""))
    //            {
    //                voyage = VoyageParser.Parse(line);
    //                voyage.VoyageDetailsId = Guid.NewGuid();
    //                lastObject = voyage;
    //            }
    //            else if (line.StartsWith("\"BOL\""))
    //            {
    //                if (voyage == null)
    //                {
    //                    throw new Exception("the ordering of the content is not correct.");
    //                }
    //                var bol = BolParser.Parse(line);
    //                bol.BOLDetailsId = Guid.NewGuid();
    //                voyage.BOLDetails.Add(bol);
    //                lastObject = lastBol = bol;
    //            }
    //            else if (line.StartsWith("\"CTR\""))
    //            {
    //                if (lastBol == null)
    //                {
    //                    throw new Exception("the ordering of the content is not correct.");
    //                }
    //                var container = ContainerParser.Parse(line);
    //                //var checkContainer = allContainers.FirstOrDefault(c => c.ContainerNo == container.ContainerNo);
    //                //if (checkContainer != null)
    //                //{
    //                //    container = checkContainer;
    //                //}
    //                //else
    //                //{
    //                container.ContainerDetailsId = Guid.NewGuid();
    //                //allContainers.Add(container);
    //                //}
    //                lastBol.ContainerDetails.Add(container);
    //                lastObject = lastContainer = container;
    //            }
    //            else if (line.StartsWith("\"CON\""))
    //            {
    //                if (lastBol == null && lastContainer == null)
    //                {
    //                    throw new Exception("the ordering of the content is not correct.");
    //                }
    //                var consignment = ConsignmentParser.Parse(line);
    //                consignment.ConsignmentDetailsId = Guid.NewGuid();
    //                if (lastObject is BOLDetails)
    //                {
    //                    lastBol.ConsignmentDetails.Add(consignment);
    //                }
    //                else if (lastObject is ContainerDetails)
    //                {
    //                    lastContainer.ConsignmentDetails.Add(consignment);
    //                }
    //                var checkDictionary = allConsignments.ContainsKey(lastBol);
    //                if (checkDictionary)
    //                {
    //                    allConsignments[lastBol].Add(consignment);
    //                }
    //                else
    //                {
    //                    var consignmentList = new List<ConsignmentDetails>();
    //                    consignmentList.Add(consignment);
    //                    allConsignments.Add(lastBol, consignmentList);
    //                }
    //            }
    //            else if (line.StartsWith("\"VEH\""))
    //            {
    //                if (lastBol == null && lastContainer == null)
    //                {
    //                    throw new Exception("the ordering of the content is not correct.");
    //                }
    //                var vehicle = VehicleParser.Parse(line);
    //                vehicle.VehicleDetailsId = Guid.NewGuid();
    //                if (!allConsignments.ContainsKey(lastBol))
    //                {
    //                    throw new Exception("the ordering of the content is not correct last BOLDetails not found (VEH tag).");
    //                }
    //                var consignment = allConsignments[lastBol].FirstOrDefault(c => c.SerialNumber == vehicle.SerialNumber);
    //                if (consignment == null)
    //                {
    //                    throw new Exception("Vehicle to consignment relation is not correct (VEH tag).");
    //                }
    //                consignment.VehicleDetails.Add(vehicle);

    //            }
    //            else if (line.StartsWith("\"END\""))
    //            {
    //                //End of the file
    //            }
    //            else
    //            {
    //                throw new Exception("content is not valid");
    //            }
    //        }
    //        return voyage;
    //    }
    //    public VoyageDetails ParseStream()
    //    {
    //        if (fileStream == null)
    //        {
    //            throw new Exception("File stream is null");
    //        }
    //        using (StreamReader reader = new StreamReader(fileStream))
    //        {
    //            return ParseReader(reader);
    //        }
    //    }
    //    public VoyageDetails ParseString(string content)
    //    {
    //        if (string.IsNullOrEmpty(content.Trim()))
    //        {
    //            throw new Exception("content is null");
    //        }
    //        if (!content.StartsWith("\"VOY\""))
    //        {
    //            throw new Exception("content is not recognized");
    //        }
    //        using (StringReader reader = new StringReader(content))
    //        {
    //            return ParseReader(reader);
    //        }

    //    }

    //}

    public class EmanifestParser<VOY, BOL, CON, CTR, VEH>
        where VOY : IVoyageDetails<BOL, CON, CTR, VEH>, new()
        where BOL : IBOLDetails<CON, CTR, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
        where CTR : IContainerDetails<CON, VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        Stream fileStream;
        public EmanifestParser(Stream fileStream)
        {
            this.fileStream = fileStream;
        }
        private VOY ParseReader(TextReader reader)
        {
            if (reader == null)
            {
                throw new Exception("reader is null");
            }
            object lastObject = null;
            VOY voyage = default(VOY);
            BOL lastBol = default(BOL);
            Dictionary<BOL, List<CON>> allConsignments = new Dictionary<BOL, List<CON>>();
            //List<ContainerDetails> allContainers = new List<ContainerDetails>();
            CTR lastContainer = default(CTR);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line.Trim()))
                {
                    continue;
                }
                if (line.StartsWith("\"VOY\""))
                {
                    voyage = VoyageParser<VOY, BOL, CON, CTR, VEH>.Parse(line);
                    voyage.VoyageDetailsId = Guid.NewGuid();
                    lastObject = voyage;
                }
                else if (line.StartsWith("\"BOL\""))
                {
                    if (voyage == null)
                    {
                        throw new Exception("the ordering of the content is not correct.");
                    }
                    var bol = BolParser<BOL, CON, CTR, VEH>.Parse(line);
                    bol.BOLDetailsId = Guid.NewGuid();
                    voyage.BOLDetails.Add(bol);
                    lastObject = lastBol = bol;
                }
                else if (line.StartsWith("\"CTR\""))
                {
                    if (lastBol == null)
                    {
                        throw new Exception("the ordering of the content is not correct.");
                    }
                    var container = ContainerParser<CTR, CON, VEH>.Parse(line);
                    //var checkContainer = allContainers.FirstOrDefault(c => c.ContainerNo == container.ContainerNo);
                    //if (checkContainer != null)
                    //{
                    //    container = checkContainer;
                    //}
                    //else
                    //{
                    container.ContainerDetailsId = Guid.NewGuid();
                    //allContainers.Add(container);
                    //}
                    lastBol.ContainerDetails.Add(container);
                    lastObject = lastContainer = container;
                }
                else if (line.StartsWith("\"CON\""))
                {
                    if (lastBol == null && lastContainer == null)
                    {
                        throw new Exception("the ordering of the content is not correct.");
                    }
                    var consignment = ConsignmentParser<CON, VEH>.Parse(line);
                    consignment.ConsignmentDetailsId = Guid.NewGuid();
                    if (lastObject is BOL)
                    {
                        lastBol.ConsignmentDetails.Add(consignment);
                    }
                    else if (lastObject is CTR)
                    {
                        lastContainer.ConsignmentDetails.Add(consignment);
                    }
                    var checkDictionary = allConsignments.ContainsKey(lastBol);
                    if (checkDictionary)
                    {
                        allConsignments[lastBol].Add(consignment);
                    }
                    else
                    {
                        var consignmentList = new List<CON>();
                        consignmentList.Add(consignment);
                        allConsignments.Add(lastBol, consignmentList);
                    }
                }
                else if (line.StartsWith("\"VEH\""))
                {
                    if (lastBol == null && lastContainer == null)
                    {
                        throw new Exception("the ordering of the content is not correct.");
                    }
                    var vehicle = VehicleParser<VEH>.Parse(line);
                    vehicle.VehicleDetailsId = Guid.NewGuid();
                    if (!allConsignments.ContainsKey(lastBol))
                    {
                        throw new Exception("the ordering of the content is not correct last BOLDetails not found (VEH tag).");
                    }
                    var consignment = allConsignments[lastBol].FirstOrDefault(c => c.SerialNumber == vehicle.SerialNumber);
                    if (consignment == null)
                    {
                        throw new Exception("Vehicle to consignment relation is not correct (VEH tag).");
                    }
                    consignment.VehicleDetails.Add(vehicle);

                }
                else if (line.StartsWith("\"END\""))
                {
                    //End of the file
                }
                else
                {
                    throw new Exception("content is not valid");
                }
            }
            return voyage;
        }
        public VOY ParseStream()
        {
            if (fileStream == null)
            {
                throw new Exception("File stream is null");
            }
            using (StreamReader reader = new StreamReader(fileStream))
            {
                return ParseReader(reader);
            }
        }
        public VOY ParseString(string content)
        {
            if (string.IsNullOrEmpty(content.Trim()))
            {
                throw new Exception("content is null");
            }
            if (!content.StartsWith("\"VOY\""))
            {
                throw new Exception("content is not recognized");
            }
            using (StringReader reader = new StringReader(content))
            {
                return ParseReader(reader);
            }

        }

    }
}