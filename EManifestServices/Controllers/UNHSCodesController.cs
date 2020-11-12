using EManifestServices.Attributes;
using EManifestServices.DAL;
using Microsoft.AspNet.OData;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace EManifestServices.Controllers
{
    [BasicAuthentication]
    [Authorize(Roles = "Admin")]
    public class UNHSCodesController : ODataController
    {
        EmanifestContext db = new EmanifestContext();
        private bool HSCodeExists(int key)
        {
            return db.UNHSCodes.Any(p => p.UNHSCodeId == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [EnableQuery]
        public IQueryable<UNHSCodes> Get()
        {
            return db.UNHSCodes;
        }
        [EnableQuery]
        public SingleResult<UNHSCodes> Get([FromODataUri]int key)
        {
            IQueryable<UNHSCodes> result = db.UNHSCodes.Where(p => p.UNHSCodeId == key);
            return SingleResult.Create(result);
        }
        public async Task<IHttpActionResult> Post(UNHSCodes hsCode)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var maxId = db.UNHSCodes.Any() ? db.UNHSCodes.Max(p => p.UNHSCodeId) : 0;
                hsCode.UNHSCodeId = ++maxId;
                var maxSerial = db.UNHSCodes.Any() ? db.UNHSCodes.Max(p => p.Serial) : 0;
                hsCode.Serial = ++maxSerial;
                db.UNHSCodes.Add(hsCode);
                await db.SaveChangesAsync();
                return Created(hsCode);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    var maxId = db.UNHSCodes.Any() ? db.UNHSCodes.Max(p => p.UNHSCodeId) : 0;
                    hsCode.UNHSCodeId = ++maxId;
                    var maxSerial = db.UNHSCodes.Any() ? db.UNHSCodes.Max(p => p.Serial) : 0; ;
                    hsCode.Serial = ++maxSerial;
                    db.UNHSCodes.Add(hsCode);
                    await db.SaveChangesAsync();
                    return Created(hsCode);
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<IHttpActionResult> Patch([FromODataUri]int key, Delta<UNHSCodes> hsCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.UNHSCodes.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            hsCode.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HSCodeExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri]int key, UNHSCodes hsCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != hsCode.UNHSCodeId)
            {
                return BadRequest();
            }
            db.Entry(hsCode).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HSCodeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(hsCode);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri]int key)
        {
            var hscode = await db.UNHSCodes.FindAsync(key);
            if (hscode == null)
            {
                return NotFound();
            }
            db.UNHSCodes.Remove(hscode);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}