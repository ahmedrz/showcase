using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortSystem.DAL.Core
{
    public class ManifestSaver
    {
        EmanifestContext db;
        public ManifestSaver()
        {
            db = new EmanifestContext();
        }
        public void Save(VoyageDetails voyage)
        {
            try
            {
                //remove userid
                voyage.UserId = null;
                //remove status 
                voyage.Status = null;
                var dbVoyages = db.VoyageDetails
                    .Include("BOLDetails.ContainerDetails.ConsignmentDetails.VehicleDetails")
                    .Include("BOLDetails.ConsignmentDetails.VehicleDetails")
                    .Where(v => v.VesselName == voyage.VesselName && v.PortCodeOfDischarge == voyage.PortCodeOfDischarge && v.AgentVoyageNumber == voyage.AgentVoyageNumber).ToList();
                //    var dbVoyage = db.VoyageDetails
                //.Include("BOLDetails.ContainerDetails.ConsignmentDetails.VehicleDetails")
                //.Include("BOLDetails.ConsignmentDetails.VehicleDetails")
                //.FirstOrDefault(v => v.VoyageDetailsId == voyage.VoyageDetailsId);
                //if (dbVoyage == null)
                //{
                //    //throw new Exception("Voyage not found.");
                //}
                var allNewBols = voyage.BOLDetails.ToList();
                var allInsertedBols = dbVoyages.SelectMany(v => v.BOLDetails).ToList();
                var dbVessel = db.Vessels.Find(voyage.VesselId);
                if (dbVessel != null)
                {
                    voyage.Vessels = null;
                    voyage.VesselId = dbVessel.VesselId;
                }

                var dbCarrier = db.Carriers.Find(voyage.CarrierId);
                if (dbCarrier != null && voyage.Carriers != null)
                {
                    voyage.Carriers = null;
                    voyage.CarrierId = dbCarrier.CarrierId;
                }

                if (!dbVoyages.Any())
                {
                    db.VoyageDetails.Add(voyage);
                }
                else
                {
                    var dbVoyage = dbVoyages.FirstOrDefault(v => v.VoyageDetailsId == voyage.VoyageDetailsId);
                    if (dbVoyage != null)
                    {
                        //update 
                        db.BulkMerge(voyage.BOLDetails.Where(v => v.Inserted != true));
                    }
                    else
                    {
                        //insert the voyage
                        voyage.BOLDetails = null;
                        db.VoyageDetails.Add(voyage);
                        //new installment
                        List<string> newBolNumbers = allNewBols.Select(b => b.BillOfLadingNumber).ToList();
                        //bols to update
                        var groupedDbBols = allInsertedBols.Where(b => newBolNumbers.Contains(b.BillOfLadingNumber)).ToList()
                            .GroupBy(b => b.BillOfLadingNumber);
                        ProcessAlreadySavedBol(groupedDbBols, allNewBols);

                        //new bols to insert
                        var dbbols = allInsertedBols.Select(b => b.BillOfLadingNumber).ToList();
                        db.BOLDetails.AddRange(allNewBols.Where(b => !dbbols.Contains(b.BillOfLadingNumber)));

                    }
                }
                db.BulkSaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        private void DeleteBOL(BOLDetails bol)
        {
            foreach (var container in bol.ContainerDetails.ToList())
            {
                foreach (var con in container.ConsignmentDetails.ToList())
                {
                    foreach (var vehicle in con.VehicleDetails.ToList())
                    {
                        db.VehicleDetails.Remove(vehicle);
                    }
                    db.ConsignmentDetails.Remove(con);
                }
                db.ContainerDetails.Remove(container);
            }
            foreach (var con in bol.ConsignmentDetails.ToList())
            {
                foreach (var vehicle in con.VehicleDetails.ToList())
                {
                    db.VehicleDetails.Remove(vehicle);
                }
                db.ConsignmentDetails.Remove(con);
            }
            db.BOLDetails.Remove(bol);
        }
        private void DeleteBOLs(List<BOLDetails> bols)
        {
            var consignmentVehicles = bols.SelectMany(b => b.ConsignmentDetails.SelectMany(c => c.VehicleDetails)).ToList();
            var containerVehicles = bols.SelectMany(b => b.ContainerDetails.SelectMany(c => c.ConsignmentDetails.SelectMany(con => con.VehicleDetails))).ToList();
            var allVehicles = consignmentVehicles.Concat(containerVehicles).ToList();

            var bolConsignments = bols.SelectMany(b => b.ConsignmentDetails).ToList();
            var containerConsignments = bols.SelectMany(b => b.ContainerDetails.SelectMany(c => c.ConsignmentDetails)).ToList();

            var allConsignments = bolConsignments.Concat(containerConsignments).ToList();

            var allContainers = bols.SelectMany(b => b.ContainerDetails).ToList();

            db.VehicleDetails.RemoveRange(allVehicles);
            db.ConsignmentDetails.RemoveRange(allConsignments);
            db.ContainerDetails.RemoveRange(allContainers);
            db.BOLDetails.RemoveRange(bols);
        }


        private void ProcessAlreadySavedBol(IEnumerable<IGrouping<string, BOLDetails>> bolGroups, List<BOLDetails> newBols)
        {
            foreach (var group in bolGroups)
            {
                var sortedBols = group.OrderBy(g => g.VoyageDetails.UploadDate).ToArray();
                if (sortedBols.Count() == 1)
                {
                    var dbBol = sortedBols.Last();
                    var newBol = newBols.FirstOrDefault(b => b.BillOfLadingNumber == dbBol.BillOfLadingNumber);
                    if (dbBol.Inserted != true)
                    {
                        DeleteBOL(dbBol);
                    }
                    else
                    {
                        dbBol.Deleted = true;
                        dbBol.UpdatedBOLDetailsId = newBol.BOLDetailsId;
                        newBol.Updated = true;
                    }
                    db.BOLDetails.Add(newBol);
                }
                else
                {
                    var dbBol = sortedBols.Last();
                    var newBol = newBols.FirstOrDefault(b => b.BillOfLadingNumber == dbBol.BillOfLadingNumber);
                    if (dbBol.Inserted != true)
                    {
                        var parentBol = sortedBols[sortedBols.Count() - 2];
                        parentBol.UpdatedBOLDetailsId = newBol.BOLDetailsId;
                        DeleteBOL(dbBol);
                    }
                    else
                    {
                        dbBol.Deleted = true;
                        dbBol.UpdatedBOLDetailsId = newBol.BOLDetailsId;
                    }
                    db.BOLDetails.Add(newBol);
                }
            }
        }

    }
}
