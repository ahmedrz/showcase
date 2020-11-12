using IQManClient.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMan.Helpers
{
    public class SerialNumberHelper
    {
        public static void AlterSerialNumbers(VoyageDetails Voyage)
        {
            if (Voyage == null)
            {
                return;
            }
            //modify ides
            foreach (var bol in Voyage.BOLDetails)
            {
                bol.VoyageDetails = Voyage;
                bol.VoyageDetailsId = Voyage.VoyageDetailsId;
                foreach (var con in bol.ConsignmentDetails)
                {
                    con.BOLDetails = bol;
                    con.BOLDetailsId = bol.BOLDetailsId;
                    foreach (var vehicle in con.VehicleDetails)
                    {
                        vehicle.ConsignmentDetails = con;
                        vehicle.ConsignmentDetailsId = con.ConsignmentDetailsId;
                    }
                }
                foreach (var container in bol.ContainerDetails)
                {
                    container.BOLDetails = bol;
                    container.BOLDetailsId = bol.BOLDetailsId;
                    foreach (var con in container.ConsignmentDetails)
                    {
                        con.ContainerDetails = container;
                        con.ContainerDetailsId = container.ContainerDetailsId;
                        foreach (var vehicle in con.VehicleDetails)
                        {
                            vehicle.ConsignmentDetails = con;
                            vehicle.ConsignmentDetailsId = con.ConsignmentDetailsId;
                        }
                    }
                }
            }
            //alter vehicles serial number with coresponsing consignment
            //first reorder the serial number for the consignment per BOl
            var bolConsignments = Voyage.BOLDetails.SelectMany(b => b.ConsignmentDetails).ToList();
            var groupedbolConsignments = bolConsignments.GroupBy(c => c.BOLDetails).ToList();
            foreach (var bolGroup in groupedbolConsignments)
            {
                var cons = bolGroup.ToList();
                for (int i = 0; i < cons.Count(); i++)
                {
                    cons[i].SerialNumber = i + 1;
                }
            }
            var containersConsignments = Voyage.BOLDetails.SelectMany(b => b.ContainerDetails).SelectMany(c => c.ConsignmentDetails).ToList();
            var groupedContainerConsignments = containersConsignments.GroupBy(c => c.ContainerDetails.BOLDetails);
            foreach (var bolGroup in groupedContainerConsignments)
            {
                var cons = bolGroup.ToList();
                for (int i = 0; i < cons.Count(); i++)
                {
                    cons[i].SerialNumber = i + 1;
                }
            }
            //then alter the inner vehicles serial number
            var allConsignments = bolConsignments.Concat(containersConsignments).ToList();
            var allVehicles = allConsignments.SelectMany(c => c.VehicleDetails);
            foreach (var vehicle in allVehicles)
            {
                vehicle.SerialNumber = vehicle.ConsignmentDetails.SerialNumber;
            }
            //end altering serial number
        }

      
    }
}
