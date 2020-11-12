using IQManClient.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMan.Helpers
{
    public class DataDeletor
    {
        private EmanifestContext db;
        public DataDeletor(EmanifestContext context)
        {
            db = context;
        }
        public void DeleteVoyageChildren(VoyageDetails voyage)
        {
            if (db == null)
            {
                return;
            }
            db.VehicleDetails
                         .Where(v => v.ConsignmentDetails.BOLDetails.VoyageDetailsId == voyage.VoyageDetailsId
                         || v.ConsignmentDetails.ContainerDetails.BOLDetails.VoyageDetailsId == voyage.VoyageDetailsId)
                         .DeleteFromQuery();

            db.ConsignmentDetails
              .Where(v => v.ContainerDetails.BOLDetails.VoyageDetailsId == voyage.VoyageDetailsId
              || v.BOLDetails.VoyageDetailsId == voyage.VoyageDetailsId).DeleteFromQuery();
            db.ContainerDetails
                .Where(c => c.BOLDetails.VoyageDetailsId == voyage.VoyageDetailsId).DeleteFromQuery();

            db.BOLDetails
               .Where(c => c.VoyageDetailsId == voyage.VoyageDetailsId).DeleteFromQuery();

            db.VoyageDetails.Where(v => v.VoyageDetailsId == voyage.VoyageDetailsId).DeleteFromQuery();

        }
        public void DeleteBolChildren(BOLDetails BOL)
        {
            if (db == null)
            {
                return;
            }
            db.VehicleDetails
                         .Where(v => v.ConsignmentDetails.BOLDetailsId == BOL.BOLDetailsId
                         || v.ConsignmentDetails.ContainerDetails.BOLDetailsId == BOL.BOLDetailsId)
                         .DeleteFromQuery();

            db.ConsignmentDetails
              .Where(v => v.ContainerDetails.BOLDetailsId == BOL.BOLDetailsId
              || v.BOLDetailsId == BOL.BOLDetailsId).DeleteFromQuery();
            db.ContainerDetails
                .Where(c => c.BOLDetailsId == BOL.BOLDetailsId).DeleteFromQuery();

            db.BOLDetails.Where(b => b.BOLDetailsId == BOL.BOLDetailsId).DeleteFromQuery();

        }
        public void DeleteContainerChildren(ContainerDetails container)
        {
            if (db == null)
            {
                return;
            }
            db.VehicleDetails
                         .Where(v => v.ConsignmentDetails.ContainerDetailsId == container.ContainerDetailsId)
                         .DeleteFromQuery();

            db.ConsignmentDetails
              .Where(v => v.ContainerDetailsId == container.ContainerDetailsId).DeleteFromQuery();

            db.ContainerDetails.Where(c => c.ContainerDetailsId == container.ContainerDetailsId).DeleteFromQuery();
        }
        public void DeleteConChildren(ConsignmentDetails CON)
        {
            if (db == null)
            {
                return;
            }
            db.VehicleDetails
                           .Where(v => v.ConsignmentDetailsId == CON.ConsignmentDetailsId)
                           .DeleteFromQuery();

            db.ConsignmentDetails.Where(c => c.ConsignmentDetailsId == CON.ConsignmentDetailsId).DeleteFromQuery();
            //re initilize db context
        }
        public void DeleteVehicle(VehicleDetails vehicle)
        {
            if (db == null)
            {
                return;
            }
            db.VehicleDetails.Where(v => v.VehicleDetailsId == vehicle.VehicleDetailsId).DeleteFromQuery();
        }
    }
}
