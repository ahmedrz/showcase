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
    public class ContainerIsoCodesController : ODataController
    {
        EmanifestContext db = new EmanifestContext();
        private bool ContainerIsoCodeExists(string key)
        {
            return db.ContainerIsoCodes.Any(p => p.IsoTypeCode == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [EnableQuery]
        public IQueryable<ContainerIsoCodes> Get()
        {
            return db.ContainerIsoCodes;
        }
        [EnableQuery]
        public SingleResult<ContainerIsoCodes> Get([FromODataUri] string key)
        {
            IQueryable<ContainerIsoCodes> result = db.ContainerIsoCodes.Where(p => p.IsoTypeCode == key);
            return SingleResult.Create(result);
        }
        public async Task<IHttpActionResult> Post(ContainerIsoCodes containerCode)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var maxSerial = db.ContainerIsoCodes.Any() ? db.ContainerIsoCodes.Max(p => p.Serial) : 0;
                containerCode.Serial = ++maxSerial;
                db.ContainerIsoCodes.Add(containerCode);
                await db.SaveChangesAsync();
                return Created(containerCode);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    var maxSerial = db.ContainerIsoCodes.Any() ? db.ContainerIsoCodes.Max(p => p.Serial) : 0;
                    containerCode.Serial = ++maxSerial;
                    db.ContainerIsoCodes.Add(containerCode);
                    await db.SaveChangesAsync();
                    return Created(containerCode);
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<ContainerIsoCodes> containerCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.ContainerIsoCodes.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            containerCode.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerIsoCodeExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] string key, ContainerIsoCodes containerCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != containerCode.IsoTypeCode)
            {
                return BadRequest();
            }
            db.Entry(containerCode).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContainerIsoCodeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(containerCode);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            var containerCode = await db.ContainerIsoCodes.FindAsync(key);
            if (containerCode == null)
            {
                return NotFound();
            }
            db.ContainerIsoCodes.Remove(containerCode);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
