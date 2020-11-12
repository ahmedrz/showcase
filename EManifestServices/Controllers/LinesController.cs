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
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EManifestServices.Controllers
{
    [BasicAuthentication]
    [Authorize(Roles = "Admin")]
    public class LinesController : ODataController
    {
        EmanifestContext db = new EmanifestContext();
        private bool LineExists(string key)
        {
            return db.Lines.Any(p => p.LineCode == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [EnableQuery]
        public IQueryable<Lines> Get()
        {
            return db.Lines;
        }
        [EnableQuery]
        public SingleResult<Lines> Get([FromODataUri] string key)
        {
            IQueryable<Lines> result = db.Lines.Where(p => p.LineCode == key);
            return SingleResult.Create(result);
        }
        public async Task<IHttpActionResult> Post(Lines line)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
               
                var maxSerial = db.Lines.Any() ? db.Lines.Max(p => p.Serial) : 0;
                line.Serial = ++maxSerial;
                db.Lines.Add(line);
                await db.SaveChangesAsync();
                return Created(line);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    var maxSerial = db.Lines.Any() ? db.Lines.Max(p => p.Serial) : 0;
                    line.Serial = ++maxSerial;
                    db.Lines.Add(line);
                    await db.SaveChangesAsync();
                    return Created(line);
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Lines> line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.Lines.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            line.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Lines line)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != line.LineCode)
            {
                return BadRequest();
            }
            db.Entry(line).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(line);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            var line = await db.Lines.FindAsync(key);
            if (line == null)
            {
                return NotFound();
            }
            db.Lines.Remove(line);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
