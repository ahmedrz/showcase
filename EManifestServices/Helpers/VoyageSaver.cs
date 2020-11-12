using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EManifestServices.Helpers
{
    public class VoyageSaver
    {
        public void Save(VoyageDetails voyage)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                if (voyage == null)
                {
                    throw new Exception("Voyage is null parsing did not work properly.");
                }
                if (!voyage.BOLDetails.Any())
                {
                    throw new Exception("Voyage should have at least one BOLDetails.");
                }
                var dbVoyage = db.VoyageDetails.Find(voyage.VoyageDetailsId);
                if (dbVoyage == null)
                {
                    db.VoyageDetails.Add(voyage);
                }
                else
                {
                    Delete(voyage.VesselName, voyage.AgentVoyageNumber, voyage.NumberOfInstalment, voyage.PortCodeOfDischarge);
                    foreach (var bol in voyage.BOLDetails)
                    {
                        dbVoyage.BOLDetails.Add(bol);
                    }
                    // db.VoyageDetails.Add(dbVoyage);
                    db.Entry(dbVoyage).State = System.Data.Entity.EntityState.Modified;
                }
                db.BulkSaveChanges();
            }
        }

        public void Delete(string vesselName, string voyageNumber, int? installment, string portCode)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                var checkVoyage = db.VoyageDetails
                    .FirstOrDefault(v => v.VesselName == vesselName
                    && v.AgentVoyageNumber == voyageNumber
                    && v.NumberOfInstalment == installment
                    && v.PortCodeOfDischarge == portCode);

                if (checkVoyage != null)
                {
                    db.VehicleDetails
                        .Where(v => v.ConsignmentDetails.BOLDetails.VoyageDetailsId == checkVoyage.VoyageDetailsId
                        || v.ConsignmentDetails.ContainerDetails.BOLDetails.VoyageDetailsId == checkVoyage.VoyageDetailsId)
                        .DeleteFromQuery();

                    db.ConsignmentDetails
                      .Where(v => v.ContainerDetails.BOLDetails.VoyageDetailsId == checkVoyage.VoyageDetailsId
                      || v.BOLDetails.VoyageDetailsId == checkVoyage.VoyageDetailsId).DeleteFromQuery();
                    db.ContainerDetails
                        .Where(c => c.BOLDetails.VoyageDetailsId == checkVoyage.VoyageDetailsId).DeleteFromQuery();

                    db.BOLDetails
                       .Where(c => c.VoyageDetailsId == checkVoyage.VoyageDetailsId).DeleteFromQuery();

                }

            }
        }
    }
}
