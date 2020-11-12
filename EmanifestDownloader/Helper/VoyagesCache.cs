using PortSystem.DAL;
using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EmanifestDownloader.Helper
{
    public class VoyagesCache
    {
        EmanifestContext db;
        Dictionary<Guid, VoyageDetails> _voyages;
        ODataClient _client;

        public VoyagesCache(ODataClient client)
        {
            db = new EmanifestContext();
            _voyages = new Dictionary<Guid, VoyageDetails>();
            _client = client;
        }
        public async Task<VoyageDetails> GetVoyage(Guid voyageDetailsId)
        {
            if (voyageDetailsId == Guid.Empty)
                return null;

            if (_voyages.ContainsKey(voyageDetailsId))
            {
                return _voyages[voyageDetailsId];
            }
            else
            {
                var dbVoyage = await db.VoyageDetails
                     .Include("BOLDetails.ContainerDetails.ConsignmentDetails.VehicleDetails")
                    .Include("BOLDetails.ConsignmentDetails.VehicleDetails").FirstOrDefaultAsync(v => v.VoyageDetailsId == voyageDetailsId);

                if (dbVoyage != null)
                {
                    _voyages.Add(voyageDetailsId, dbVoyage);
                    return dbVoyage;
                }
                var voyages = await _client.For<VoyageDetails>()
                    .Expand("Carriers")
                    .Expand(v => v.BOLDetails.Select(b => b.ContainerDetails.Select(c => c.ConsignmentDetails.Select(con => con.VehicleDetails))))
                    .Expand(v => v.BOLDetails.Select(b => b.ConsignmentDetails.Select(con => con.VehicleDetails)))
                    .Expand("Vessels")
                    .Filter(v => v.VoyageDetailsId == voyageDetailsId)
                    .FindEntriesAsync();
                var singleVoyage = voyages.FirstOrDefault();
                if (singleVoyage != null)
                {
                    _voyages.Add(voyageDetailsId, singleVoyage);
                }
                return singleVoyage;
            }


        }


    }
}
