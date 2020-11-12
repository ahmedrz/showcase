using EManifestServices.Attributes;
using EManifestServices.DAL;
using System;
using System.Text;
using System.Web.Mvc;

namespace EManifestServices.Controllers
{
    [MyAuthorize(UserTypes.Admin)]
    public class CodesController : Controller
    {
        string username = "admin";
        string password = "admin";
        public CodesController()
        {
            FillApiClient();
        }

        private void FillApiClient()
        {
            try
            {
                using (EmanifestContext db = new EmanifestContext())
                {
                    var clientId = Guid.Parse("42061E62-13AD-45F3-9427-BE8651DB13F4");
                    var adminClient = db.ApiClients.Find(clientId);
                    if (adminClient != null)
                    {
                        username = adminClient.ApiClientName;
                        password = adminClient.ApiClientPassword;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // GET: Codes
        public ActionResult CountryCodes()
        {

            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            ViewBag.Token = "Basic " + svcCredentials;
            return View();
        }
        public ActionResult LocationCodes()
        {

            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            ViewBag.Token = "Basic " + svcCredentials;
            return View();
        }
        public ActionResult PackageCodes()
        {

            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            ViewBag.Token = "Basic " + svcCredentials;
            return View();
        }
        public ActionResult HSCodes()
        {

            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            ViewBag.Token = "Basic " + svcCredentials;
            return View();
        }
        public ActionResult Lines()
        {

            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            ViewBag.Token = "Basic " + svcCredentials;
            return View();
        }
        public ActionResult ContainerIsoCodes()
        {

            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            ViewBag.Token = "Basic " + svcCredentials;
            return View();
        }
        public ActionResult UNHazardousCodes()
        {

            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            ViewBag.Token = "Basic " + svcCredentials;
            return View();
        }
    }
}