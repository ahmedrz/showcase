using AutoMapper;
using EManifestServices.Attributes;
using EManifestServices.DAL;
using EManifestServices.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EManifestServices.Controllers
{
    [MyAuthorize]
    public class VesselsController : Controller
    {
        EmanifestContext db;
        public VesselsController()
        {
            db = new EmanifestContext();
        }
        // GET: Vessels
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetVessels([DataSourceRequest]DataSourceRequest request)
        {
            UserModel currentUser = Session["User"] as UserModel;
            return Json(db.Vessels.Where(v => currentUser.UserType == 1 || v.CarrierId == currentUser.CarrierId).Select(v => new VesselModel
            {
                VesselId = v.VesselId,
                VesselName = v.VesselName,
                VesselCountry = v.VesselCountry,
                GRT = v.GRT,
                IMONo = v.IMONo,
                CallSign = v.CallSign,
                LOA = v.LOA,
                LWL = v.LWL,
                DraftAFT = v.DraftAFT,
                DraftFWD = v.DraftFWD,
                CarrierId = v.CarrierId
            }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditVessel(Guid? id)
        {
            UserModel currentUser = Session["User"] as UserModel;
            VesselModel model = new VesselModel();
            if (id == null)
            {
                model.CarrierId = currentUser.CarrierId;
            }
            else
            {
                var dbVessel = db.Vessels.Find(id);
                if (dbVessel == null)
                {
                    TempData["Error"] = "Vessel not found";
                    return RedirectToAction("Index");
                }
                model = Mapper.Map<VesselModel>(dbVessel);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVessel(VesselModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var currentUser = Session["User"] as UserModel;
            Vessels vessel = null;
            if (model.VesselId == Guid.Empty)
            {
                vessel = Mapper.Map<Vessels>(model);
                vessel.VesselId = Guid.NewGuid();
                db.Vessels.Add(vessel);
            }
            else
            {
                vessel = db.Vessels.Find(model.VesselId);
                if (vessel != null)
                {
                    vessel.VesselName = model.VesselName;
                    vessel.VesselCountry = model.VesselCountry;
                    vessel.GRT = model.GRT;
                    vessel.CallSign = model.CallSign;
                    vessel.IMONo = model.IMONo;
                    vessel.LOA = model.LOA;
                    vessel.LWL = model.LWL;
                    vessel.DraftAFT = model.DraftAFT;
                    vessel.DraftFWD = model.DraftFWD;
                    if (currentUser.UserType == 1 || currentUser.CarrierId == model.CarrierId)
                    {
                        vessel.CarrierId = model.CarrierId.Value;
                    }

                }
            }
            db.SaveChanges();
            TempData["Success"] = "Saving vessel done.";
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> DeleteVessel(Guid? id)
        {
            try
            {
                var vessel = await db.Vessels.FindAsync(id);
                if (vessel != null)
                {
                    if (!vessel.VoyageDetails.Any())
                    {
                        db.Vessels.Remove(vessel);
                        await db.SaveChangesAsync();
                        return Json(new { Success = true, Message = "Success" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { Success = false, Message = "Vessel cannot be deleted there are voyages that depend on it" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { Success = true, Message = "Success" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

    }
}