using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using EManifestServices.Attributes;
using EManifestServices.DAL;
using Microsoft.AspNet.OData;

namespace EManifestServices.Controllers
{
    [BasicAuthentication]
    [Authorize(Roles = "Admin")]
    public class CountryCodesController : ODataController
    {
        EmanifestContext db = new EmanifestContext();
        private bool CountryCodeExists(int key)
        {
            return db.CountryCodes.Any(p => p.CountryCodeId == key);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [EnableQuery]
        public IQueryable<CountryCodes> Get()
        {
            return db.CountryCodes;
        }
        [EnableQuery]
        public SingleResult<CountryCodes> Get([FromODataUri] int key)
        {
            IQueryable<CountryCodes> result = db.CountryCodes.Where(p => p.CountryCodeId == key);
            return SingleResult.Create(result);
        }
        public async Task<IHttpActionResult> Post(CountryCodes countryCode)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var maxId = db.CountryCodes.Any() ? db.CountryCodes.Max(p => p.CountryCodeId) : 0;
                countryCode.CountryCodeId = ++maxId;
                var maxSerial = db.CountryCodes.Any() ? db.CountryCodes.Max(p => p.Serial) : 0;
                countryCode.Serial = ++maxSerial;
                db.CountryCodes.Add(countryCode);
                await db.SaveChangesAsync();
                return Created(countryCode);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    var maxId = db.CountryCodes.Any() ? db.CountryCodes.Max(p => p.CountryCodeId) : 0;
                    countryCode.CountryCodeId = ++maxId;
                    var maxSerial = db.CountryCodes.Any() ? db.CountryCodes.Max(p => p.Serial) : 0;
                    countryCode.Serial = ++maxSerial;
                    db.CountryCodes.Add(countryCode);
                    await db.SaveChangesAsync();
                    return Created(countryCode);
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<CountryCodes> countryCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = await db.CountryCodes.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }
            countryCode.Patch(entity);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryCodeExists(key))
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
        public async Task<IHttpActionResult> Put([FromODataUri] int key, CountryCodes countryCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != countryCode.CountryCodeId)
            {
                return BadRequest();
            }
            db.Entry(countryCode).State = EntityState.Modified;
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryCodeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(countryCode);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var countryCode = await db.CountryCodes.FindAsync(key);
            if (countryCode == null)
            {
                return NotFound();
            }
            db.CountryCodes.Remove(countryCode);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
