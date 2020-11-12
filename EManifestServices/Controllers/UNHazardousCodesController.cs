using EManifestServices.DAL;
using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EManifestServices.Controllers
{
    public class UNHazardousCodesController : ODataController
    {
        EmanifestContext db = new EmanifestContext();
        private bool HazardousCodeExists(string key)
        {
            return db.UNHazardousCodes.Any(p => p.Code == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [EnableQuery]
        public IQueryable<UNHazardousCodes> Get()
        {
            return db.UNHazardousCodes;
        }
        [EnableQuery]
        public SingleResult<UNHazardousCodes> Get([FromODataUri] string key)
        {
            IQueryable<UNHazardousCodes> result = db.UNHazardousCodes.Where(p => p.Code == key);
            return SingleResult.Create(result);
        }
        public async Task<IHttpActionResult> Post(UNHazardousCodes code)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var maxSerial = db.UNHazardousCodes.Any() ? db.UNHazardousCodes.Max(p => p.Serial) : 0;
                code.Serial = ++maxSerial;
                db.UNHazardousCodes.Add(code);
                await db.SaveChangesAsync();
                return Created(code);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    var maxSerial = db.UNHazardousCodes.Any() ? db.UNHazardousCodes.Max(p => p.Serial) : 0;
                    code.Serial = ++maxSerial;
                    db.UNHazardousCodes.Add(code);
                    await db.SaveChangesAsync();
                    return Created(code);
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<UNHazardousCodes> code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.UNHazardousCodes.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            code.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HazardousCodeExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] string key, UNHazardousCodes code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != code.Code)
            {
                return BadRequest();
            }
            db.Entry(code).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HazardousCodeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(code);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            var code = await db.UNHazardousCodes.FindAsync(key);
            if (code == null)
            {
                return NotFound();
            }
            db.UNHazardousCodes.Remove(code);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
