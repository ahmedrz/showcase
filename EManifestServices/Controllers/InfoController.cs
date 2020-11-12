using EManifestServices.Attributes;
using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EManifestServices.Controllers
{
    [BasicAuthentication]
    [System.Web.Http.Authorize(Roles = "Admin,IQManClient,PortClient")]
    public class InfoController : ApiController
    {
        public IEnumerable<Vessels> GetVessels(Guid CarrierId)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                if (CarrierId == Guid.Empty)
                {
                    return null;
                }
                var dbCarrier = db.Carriers.Find(CarrierId);
                if (dbCarrier == null || dbCarrier.IsActive == false)
                {
                    return null;
                }
                var dbVessels = db.Vessels.Where(m => m.CarrierId == CarrierId)
                      .ToList();
                return dbVessels;
            }
        }
        public IEnumerable<PackageCodes> GetPackageCodes(int serial)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var codes = db.PackageCodes.Where(p => p.Serial > serial)
                    .ToList();
                return codes;
            }
        }
        public IEnumerable<CountryCodes> GetCountryCodes(int serial)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var codes = db.CountryCodes.Where(p => p.Serial > serial)
                    .ToList();
                return codes;
            }
        }
        public IEnumerable<LocationCodes> GetLocationCodes(int serial)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var codes = db.LocationCodes.Where(p => p.Serial > serial)
                    .ToList();
                return codes;
            }
        }
        public IEnumerable<UNHSCodes> GetHSCodes(int serial)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var codes = db.UNHSCodes.Where(p => p.Serial > serial)
                    .ToList();
                return codes;
            }
        }
        public IEnumerable<Lines> GetLines(int serial)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var lines = db.Lines.Where(p => p.Serial > serial)
                    .ToList();
                return lines;
            }
        }
        public IEnumerable<ContainerIsoCodes> GetContainerIsoCodes(int serial)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var containerCodes = db.ContainerIsoCodes.Where(p => p.Serial > serial)
                    .ToList();
                return containerCodes;
            }
        }
        public IEnumerable<UNHazardousCodes> GetUNHazardousCodes(int serial)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var codes = db.UNHazardousCodes.Where(p => p.Serial > serial)
                    .ToList();
                return codes;
            }
        }
    }
}
