using Emanifest.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmanifestParser
{
    public enum ManifestFileType
    {
        FCLContainerRelationNotknown = 0,
        FCLContainer,
        LCLContainer,
        GeneralCargo
    }
    //public class EmanifestSerializer
    //{
    //    ManifestFileType fileType;
    //    VoyageSerializer voyageSerializer;
    //    BolSerializer bolSerializer;
    //    ConsignmentSerializer consignmentSerializer;
    //    ContainerSerializer containerSerializer;
    //    VehicleSerializer vehicleSerializer;
    //    EndSerializer endSerializer;
    //    public EmanifestSerializer(ManifestFileType fileType)
    //    {
    //        this.fileType = fileType;
    //        voyageSerializer = new VoyageSerializer();
    //        bolSerializer = new BolSerializer();
    //        consignmentSerializer = new ConsignmentSerializer();
    //        containerSerializer = new ContainerSerializer();
    //        vehicleSerializer = new VehicleSerializer();
    //        endSerializer = new EndSerializer();
    //    }

    //    public string SerializeToString(VoyageDetails voyage)
    //    {
    //        string result = "";
    //        if (fileType == ManifestFileType.GeneralCargo)
    //        {
    //            result += voyageSerializer.Serialize(voyage);
    //            foreach (var bol in voyage.BOLDetails)
    //            {
    //                result += bolSerializer.Serialize(bol);
    //                foreach (var consignment in bol.ConsignmentDetails)
    //                {
    //                    result += consignmentSerializer.Serialize(consignment);
    //                }
    //                foreach (var vehicle in bol.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
    //                {
    //                    result += vehicleSerializer.Serialize(vehicle);
    //                }
    //            }
    //        }
    //        else if (fileType == ManifestFileType.LCLContainer)
    //        {
    //            result += voyageSerializer.Serialize(voyage);
    //            foreach (var bol in voyage.BOLDetails)
    //            {
    //                result += bolSerializer.Serialize(bol);
    //                foreach (var container in bol.ContainerDetails)
    //                {
    //                    result += containerSerializer.Serialize(container);
    //                    foreach (var consignment in container.ConsignmentDetails)
    //                    {
    //                        result += consignmentSerializer.Serialize(consignment);
    //                    }
    //                }
    //            }
    //        }
    //        else if (fileType == ManifestFileType.FCLContainer)
    //        {
    //            result += voyageSerializer.Serialize(voyage);
    //            foreach (var bol in voyage.BOLDetails)
    //            {
    //                result += bolSerializer.Serialize(bol);
    //                foreach (var container in bol.ContainerDetails)
    //                {
    //                    result += containerSerializer.Serialize(container);
    //                    foreach (var consignment in container.ConsignmentDetails)
    //                    {
    //                        result += consignmentSerializer.Serialize(consignment);
    //                    }
    //                    foreach (var vehicle in container.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
    //                    {
    //                        result += vehicleSerializer.Serialize(vehicle);
    //                    }
    //                }
    //            }
    //        }
    //        else if (fileType == ManifestFileType.FCLContainerRelationNotknown)
    //        {
    //            result += voyageSerializer.Serialize(voyage);
    //            foreach (var bol in voyage.BOLDetails)
    //            {
    //                result += bolSerializer.Serialize(bol);
    //                foreach (var consignment in bol.ConsignmentDetails)
    //                {
    //                    result += consignmentSerializer.Serialize(consignment);
    //                }
    //                foreach (var container in bol.ContainerDetails)
    //                {
    //                    result += containerSerializer.Serialize(container);
    //                }
    //                foreach (var vehicle in bol.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
    //                {
    //                    result += vehicleSerializer.Serialize(vehicle);
    //                }
    //            }
    //        }
    //        result += endSerializer.Serialize(voyage);
    //        return result;
    //    }

    //    public void SerializeTofile(string filename, VoyageDetails voyage)
    //    {
    //        using (StreamWriter writer = new StreamWriter(File.Create(filename)))
    //        {
    //            if (fileType == ManifestFileType.GeneralCargo)
    //            {
    //                writer.Write(voyageSerializer.Serialize(voyage));
    //                foreach (var bol in voyage.BOLDetails)
    //                {
    //                    writer.Write(bolSerializer.Serialize(bol));
    //                    foreach (var consignment in bol.ConsignmentDetails)
    //                    {
    //                        writer.Write(consignmentSerializer.Serialize(consignment));
    //                    }
    //                    foreach (var vehicle in bol.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
    //                    {
    //                        writer.Write(vehicleSerializer.Serialize(vehicle));
    //                    }
    //                }
    //            }
    //            else if (fileType == ManifestFileType.LCLContainer)
    //            {
    //                writer.Write(voyageSerializer.Serialize(voyage));
    //                foreach (var bol in voyage.BOLDetails)
    //                {
    //                    writer.Write(bolSerializer.Serialize(bol));
    //                    foreach (var container in bol.ContainerDetails)
    //                    {
    //                        writer.Write(containerSerializer.Serialize(container));
    //                        foreach (var consignment in container.ConsignmentDetails)
    //                        {
    //                            writer.Write(consignmentSerializer.Serialize(consignment));
    //                        }
    //                    }
    //                }
    //            }
    //            else if (fileType == ManifestFileType.FCLContainer)
    //            {
    //                writer.Write(voyageSerializer.Serialize(voyage));
    //                foreach (var bol in voyage.BOLDetails)
    //                {
    //                    writer.Write(bolSerializer.Serialize(bol));
    //                    foreach (var container in bol.ContainerDetails)
    //                    {
    //                        writer.Write(containerSerializer.Serialize(container));
    //                        foreach (var consignment in container.ConsignmentDetails)
    //                        {
    //                            writer.Write(consignmentSerializer.Serialize(consignment));
    //                        }
    //                        foreach (var vehicle in container.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
    //                        {
    //                            writer.Write(vehicleSerializer.Serialize(vehicle));
    //                        }
    //                    }
    //                }
    //            }
    //            else if (fileType == ManifestFileType.FCLContainerRelationNotknown)
    //            {
    //                writer.Write(voyageSerializer.Serialize(voyage));
    //                foreach (var bol in voyage.BOLDetails)
    //                {
    //                    writer.Write(bolSerializer.Serialize(bol));
    //                    foreach (var consignment in bol.ConsignmentDetails)
    //                    {
    //                        writer.Write(consignmentSerializer.Serialize(consignment));
    //                    }
    //                    foreach (var container in bol.ContainerDetails)
    //                    {
    //                        writer.Write(containerSerializer.Serialize(container));
    //                    }
    //                    foreach (var vehicle in bol.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
    //                    {
    //                        writer.Write(vehicleSerializer.Serialize(vehicle));
    //                    }
    //                }
    //            }
    //            writer.Write(endSerializer.Serialize(voyage));
    //            writer.Flush();
    //        }
    //    }
    //}

    public class EmanifestSerializer<VOY, BOL, CON, CTR, VEH>
        where VOY : IVoyageDetails<BOL, CON, CTR, VEH>, new()
        where BOL : IBOLDetails<CON, CTR, VEH>, new()
        where CON : IConsignmentDetails<VEH>, new()
        where CTR : IContainerDetails<CON, VEH>, new()
        where VEH : IVehicleDetails, new()
    {
        ManifestFileType fileType;
        VoyageSerializer<VOY, BOL, CON, CTR, VEH> voyageSerializer;
        BolSerializer<BOL, CON, CTR, VEH> bolSerializer;
        ConsignmentSerializer<CON, VEH> consignmentSerializer;
        ContainerSerializer<CTR, CON, VEH> containerSerializer;
        VehicleSerializer<VEH> vehicleSerializer;
        EndSerializer<VOY, BOL, CON, CTR, VEH> endSerializer;
        public EmanifestSerializer(ManifestFileType fileType)
        {
            this.fileType = fileType;
            voyageSerializer = new VoyageSerializer<VOY, BOL, CON, CTR, VEH>();
            bolSerializer = new BolSerializer<BOL, CON, CTR, VEH>();
            consignmentSerializer = new ConsignmentSerializer<CON, VEH>();
            containerSerializer = new ContainerSerializer<CTR, CON, VEH>();
            vehicleSerializer = new VehicleSerializer<VEH>();
            endSerializer = new EndSerializer<VOY, BOL, CON, CTR, VEH>();
        }

        public string SerializeToString(VOY voyage)
        {
            using (StringWriter writer = new StringWriter())
            {
                SerializeToWriter(writer, voyage);
                return writer.ToString();
            }
            //string result = "";
            //if (fileType == ManifestFileType.GeneralCargo)
            //{
            //    result += voyageSerializer.Serialize(voyage);
            //    foreach (var bol in voyage.BOLDetails)
            //    {
            //        result += bolSerializer.Serialize(bol);
            //        foreach (var consignment in bol.ConsignmentDetails)
            //        {
            //            result += consignmentSerializer.Serialize(consignment);
            //        }
            //        foreach (var vehicle in bol.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
            //        {
            //            result += vehicleSerializer.Serialize(vehicle);
            //        }
            //    }
            //}
            //else if (fileType == ManifestFileType.LCLContainer)
            //{
            //    result += voyageSerializer.Serialize(voyage);
            //    foreach (var bol in voyage.BOLDetails)
            //    {
            //        result += bolSerializer.Serialize(bol);
            //        foreach (var container in bol.ContainerDetails)
            //        {
            //            result += containerSerializer.Serialize(container);
            //            foreach (var consignment in container.ConsignmentDetails)
            //            {
            //                result += consignmentSerializer.Serialize(consignment);
            //            }
            //        }
            //    }
            //}
            //else if (fileType == ManifestFileType.FCLContainer)
            //{
            //    result += voyageSerializer.Serialize(voyage);
            //    foreach (var bol in voyage.BOLDetails)
            //    {
            //        result += bolSerializer.Serialize(bol);
            //        foreach (var container in bol.ContainerDetails)
            //        {
            //            result += containerSerializer.Serialize(container);
            //            foreach (var consignment in container.ConsignmentDetails)
            //            {
            //                result += consignmentSerializer.Serialize(consignment);
            //            }
            //            foreach (var vehicle in container.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
            //            {
            //                result += vehicleSerializer.Serialize(vehicle);
            //            }
            //        }
            //    }
            //}
            //else if (fileType == ManifestFileType.FCLContainerRelationNotknown)
            //{
            //    result += voyageSerializer.Serialize(voyage);
            //    foreach (var bol in voyage.BOLDetails)
            //    {
            //        result += bolSerializer.Serialize(bol);
            //        foreach (var consignment in bol.ConsignmentDetails)
            //        {
            //            result += consignmentSerializer.Serialize(consignment);
            //        }
            //        foreach (var container in bol.ContainerDetails)
            //        {
            //            result += containerSerializer.Serialize(container);
            //        }
            //        foreach (var vehicle in bol.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
            //        {
            //            result += vehicleSerializer.Serialize(vehicle);
            //        }
            //    }
            //}
            //result += endSerializer.Serialize(voyage);
            //return result;
        }

        public void SerializeTofile(string filename, VOY voyage)
        {
            using (StreamWriter writer = new StreamWriter(File.Create(filename)))
            {                
                SerializeToWriter(writer, voyage);
            }
        }

        private void SerializeToWriter(TextWriter writer, VOY voyage)
        {
            if (fileType == ManifestFileType.GeneralCargo)
            {
                writer.Write(voyageSerializer.Serialize(voyage));
                foreach (var bol in voyage.BOLDetails)
                {
                    writer.Write(bolSerializer.Serialize(bol));
                    foreach (var consignment in bol.ConsignmentDetails)
                    {
                        writer.Write(consignmentSerializer.Serialize(consignment));
                    }
                    foreach (var vehicle in bol.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
                    {
                        writer.Write(vehicleSerializer.Serialize(vehicle));
                    }
                }
            }
            else if (fileType == ManifestFileType.LCLContainer)
            {
                writer.Write(voyageSerializer.Serialize(voyage));
                foreach (var bol in voyage.BOLDetails)
                {
                    writer.Write(bolSerializer.Serialize(bol));
                    foreach (var container in bol.ContainerDetails)
                    {
                        writer.Write(containerSerializer.Serialize(container));
                        foreach (var consignment in container.ConsignmentDetails)
                        {
                            writer.Write(consignmentSerializer.Serialize(consignment));
                        }
                    }
                }
            }
            else if (fileType == ManifestFileType.FCLContainer)
            {
                writer.Write(voyageSerializer.Serialize(voyage));
                foreach (var bol in voyage.BOLDetails)
                {
                    writer.Write(bolSerializer.Serialize(bol));
                    foreach (var container in bol.ContainerDetails)
                    {
                        writer.Write(containerSerializer.Serialize(container));
                        foreach (var consignment in container.ConsignmentDetails)
                        {
                            writer.Write(consignmentSerializer.Serialize(consignment));
                        }
                        foreach (var vehicle in container.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
                        {
                            writer.Write(vehicleSerializer.Serialize(vehicle));
                        }
                    }
                }
            }
            else if (fileType == ManifestFileType.FCLContainerRelationNotknown)
            {
                writer.Write(voyageSerializer.Serialize(voyage));
                foreach (var bol in voyage.BOLDetails)
                {
                    writer.Write(bolSerializer.Serialize(bol));
                    foreach (var consignment in bol.ConsignmentDetails)
                    {
                        writer.Write(consignmentSerializer.Serialize(consignment));
                    }
                    foreach (var container in bol.ContainerDetails)
                    {
                        writer.Write(containerSerializer.Serialize(container));
                    }
                    foreach (var vehicle in bol.ConsignmentDetails.SelectMany(c => c.VehicleDetails))
                    {
                        writer.Write(vehicleSerializer.Serialize(vehicle));
                    }
                }
            }
            writer.Write(endSerializer.Serialize(voyage));
            writer.Flush();
        }
    }
}
