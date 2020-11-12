using EManifestServices.DAL;
using System.Linq;
using System.Web.Http;

namespace EManifestServices.Controllers
{
    public class PublicController : ApiController
    {
        public Users GetUser(string userName, string password)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var dbUser = db.Users.Include("Carriers.ApiClients").Where(u => u.UserName == userName && u.Password == password && u.UserType == 2).FirstOrDefault();
                if (dbUser == null)
                {
                    return null;
                }
                return dbUser;
            }
        }
    }
}
