using EManifestServices.Attributes;
using EManifestServices.DAL;
using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EManifestServices.Controllers
{
    [BasicAuthentication]
    [Authorize(Roles = "Admin")]
    public class LocationCodesController : ODataController
    {
        EmanifestContext db = new EmanifestContext();
        private bool LocationCodeExists(int key)
        {
            return db.LocationCodes.Any(p => p.LocationCodeId == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [EnableQuery]
        public IQueryable<LocationCodes> Get()
        {
            return db.LocationCodes;
        }
        [EnableQuery]
        public SingleResult<LocationCodes> Get([FromODataUri] int key)
        {
            IQueryable<LocationCodes> result = db.LocationCodes.Where(p => p.LocationCodeId == key);
            return SingleResult.Create(result);
        }
        public async Task<IHttpActionResult> Post(LocationCodes locationCode)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var maxId = db.LocationCodes.Any() ? db.LocationCodes.Max(p => p.LocationCodeId) : 0;
                var maxSerial = db.LocationCodes.Any() ? db.LocationCodes.Max(p => p.Serial) : 0;
                locationCode.Serial = ++maxSerial;
                locationCode.LocationCodeId = ++maxId;
                db.LocationCodes.Add(locationCode);
                await db.SaveChangesAsync();
                return Created(locationCode);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    var maxId = db.LocationCodes.Any() ? db.LocationCodes.Max(p => p.LocationCodeId) : 0;
                    var maxSerial = db.LocationCodes.Any() ? db.LocationCodes.Max(p => p.Serial) : 0;
                    locationCode.Serial = ++maxSerial;
                    locationCode.LocationCodeId = ++maxId;
                    db.LocationCodes.Add(locationCode);
                    await db.SaveChangesAsync();
                    return Created(locationCode);
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<LocationCodes> locationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.LocationCodes.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            locationCode.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationCodeExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, LocationCodes locationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != locationCode.LocationCodeId)
            {
                return BadRequest();
            }
            db.Entry(locationCode).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationCodeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(locationCode);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var locationCode = await db.LocationCodes.FindAsync(key);
            if (locationCode == null)
            {
                return NotFound();
            }
            db.LocationCodes.Remove(locationCode);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}