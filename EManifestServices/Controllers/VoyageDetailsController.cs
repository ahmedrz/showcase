using EManifestServices.Attributes;
using EManifestServices.DAL;
using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EManifestServices.Controllers
{
    [BasicAuthentication]
    [Authorize(Roles = "Admin,PortClient")]
    [GzipCompression]
    public class VoyageDetailsController : ODataController
    {
        EmanifestContext db = new EmanifestContext();
        private bool VoyageDetailsExists(Guid key)
        {
            return db.VoyageDetails.Any(p => p.VoyageDetailsId == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [SecureAccess(MaxExpansionDepth = 5)]
        public IQueryable<VoyageDetails> Get()
        {
            return db.VoyageDetails;
        }
        [SecureAccess(MaxExpansionDepth = 5)]
        public SingleResult<VoyageDetails> Get([FromODataUri] Guid key)
        {
            IQueryable<VoyageDetails> result = db.VoyageDetails.Where(p => p.VoyageDetailsId == key);
            return SingleResult.Create(result);
        }
        public async Task<IHttpActionResult> Post(VoyageDetails voyageDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.VoyageDetails.Add(voyageDetails);
            await db.SaveChangesAsync();
            return Created(voyageDetails);

        }
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<VoyageDetails> voyageDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.VoyageDetails.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            voyageDetails.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageDetailsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(entity);
        }
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, VoyageDetails voyageDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != voyageDetails.VoyageDetailsId)
            {
                return BadRequest();
            }
            db.Entry(voyageDetails).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageDetailsExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(voyageDetails);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            var voyageDetails = await db.VoyageDetails.FindAsync(key);
            if (voyageDetails == null)
            {
                return NotFound();
            }
            db.VoyageDetails.Remove(voyageDetails);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}