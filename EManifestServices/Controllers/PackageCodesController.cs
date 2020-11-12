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
    public class PackageCodesController : ODataController
    {
        EmanifestContext db = new EmanifestContext();
        private bool PackageCodeExists(int key)
        {
            return db.PackageCodes.Any(p => p.PackageCodeId == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [EnableQuery]
        public IQueryable<PackageCodes> Get()
        {
            return db.PackageCodes;
        }
        [EnableQuery]
        public SingleResult<PackageCodes> Get([FromODataUri] int key)
        {
            IQueryable<PackageCodes> result = db.PackageCodes.Where(p => p.PackageCodeId == key);
            return SingleResult.Create(result);
        }
        public async Task<IHttpActionResult> Post(PackageCodes packageCode)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var maxId = db.PackageCodes.Any() ? db.PackageCodes.Max(p => p.PackageCodeId) : 0;
                var maxSerial = db.PackageCodes.Any() ? db.PackageCodes.Max(p => p.Serial) : 0;
                packageCode.PackageCodeId = ++maxId;
                packageCode.Serial = ++maxSerial;
                db.PackageCodes.Add(packageCode);
                await db.SaveChangesAsync();
                return Created(packageCode);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    var maxId = db.PackageCodes.Any() ? db.PackageCodes.Max(p => p.PackageCodeId) : 0;
                    var maxSerial = db.PackageCodes.Any() ? db.PackageCodes.Max(p => p.Serial) : 0;
                    packageCode.PackageCodeId = ++maxId;
                    packageCode.Serial = ++maxSerial;
                    db.PackageCodes.Add(packageCode);
                    await db.SaveChangesAsync();
                    return Created(packageCode);
                }
                else
                {
                    throw;
                }
            }

        }
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<PackageCodes> packageCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.PackageCodes.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            packageCode.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageCodeExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, PackageCodes packageCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != packageCode.PackageCodeId)
            {
                return BadRequest();
            }
            db.Entry(packageCode).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageCodeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(packageCode);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var packageCode = await db.PackageCodes.FindAsync(key);
            if (packageCode == null)
            {
                return NotFound();
            }
            db.PackageCodes.Remove(packageCode);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}