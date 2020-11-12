using AutoMapper;
using EmanifestParser;
using EManifestServices.Attributes;
using EManifestServices.Core;
using EManifestServices.DAL;
using EManifestServices.DAL.Enums;
using EManifestServices.FilesDownloadCore;
using EManifestServices.Helpers;
using EManifestServices.Models;
using ExcelDataReader;
using Hangfire;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace EManifestServices.Controllers
{
    [MyAuthorize]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ManifestController : Controller
    {
        //string serviceUrl;
        string tempFolder = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/")}\\TempFiles\\";
        EmanifestContext db;
        VoyageProcessor processor;
        //FileDataExtractor extractor;
        //HttpClient client;
        //private void InitializeClient()
        //{
        //    serviceUrl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        //    client = new HttpClient();
        //    //var byteArray = Encoding.ASCII.GetBytes($"{clientUserName}:{clientPassword}");
        //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        //    // Update port # in the following line.
        //    client.BaseAddress = new Uri(serviceUrl);
        //}
        public ManifestController()
        {
            db = new EmanifestContext();
            processor = new VoyageProcessor(db);
            //extractor = new FileDataExtractor();
            //InitializeClient();
        }
        // GET: Manifest
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Async_Save(IEnumerable<HttpPostedFileBase> files)
        {
            //Guid? voyageDetailsId = null;
            //try
            //{
            //    if (files != null && files.Any())
            //    {
            //        var manifestFile = files.First();
            //        //save the file to the web server
            //        using (StreamReader reader = new StreamReader(manifestFile.InputStream))
            //        {
            //            var currentUser = (UserModel)Session["User"];
            //            var headerLine = reader.ReadLine();
            //            List<ValidationResult> validationResult = new List<ValidationResult>();
            //            var voyage = VoyageParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>.Parse(headerLine);
            //            if (!Validator.TryValidateObject(voyage, new System.ComponentModel.DataAnnotations.ValidationContext(voyage), validationResult))
            //            {
            //                throw new Exception(string.Join(Environment.NewLine, validationResult.SelectMany(v => v.ErrorMessage)));
            //            }
            //            //validate voyage
            //            //arrival date validation 
            //            var arabianTime = TimeZoneInfo.ConvertTime(DateTime.Now,
            //     TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"));
            //            if (voyage.ExpectedToArriveDate < arabianTime)
            //            {
            //                throw new Exception("This is an old manifest , Expected arrival date should be greater than or equal to today's date for arabian standard timezone (+3).");
            //            }
            //            var checkDbVoyage = db.VoyageDetails
            //                .FirstOrDefault(m => m.VesselName == voyage.VesselName
            //                && m.AgentVoyageNumber == voyage.AgentVoyageNumber
            //                && m.NumberOfInstalment == voyage.NumberOfInstalment
            //                && m.PortCodeOfDischarge == voyage.PortCodeOfDischarge);
            //            if (checkDbVoyage != null && checkDbVoyage.Status.AllowModify != true)
            //            {
            //                throw new Exception($"Manifest is in {checkDbVoyage.Status.StatusDesc}state and cannot be modified.");
            //            }
            //            else
            //            {
            //                if (checkDbVoyage == null)
            //                {
            //                    var checkVesselQuery = db.Vessels.Where(v => v.VesselName == voyage.VesselName).AsQueryable();
            //                    if (currentUser.CarrierId != null)
            //                    {
            //                        checkVesselQuery = checkVesselQuery.Where(v => v.CarrierId == currentUser.CarrierId);
            //                    }
            //                    var vesselDb = checkVesselQuery.FirstOrDefault();
            //                    if (vesselDb == null)
            //                    {
            //                        throw new Exception($"The vessel name and information is not found try adding your vessel in Vessels page.");
            //                    }
            //                    checkDbVoyage = voyage;
            //                    checkDbVoyage.VoyageDetailsId = Guid.NewGuid();
            //                    voyageDetailsId = checkDbVoyage.VoyageDetailsId;
            //                    checkDbVoyage.VesselId = vesselDb.VesselId;
            //                    db.VoyageDetails.Add(checkDbVoyage);
            //                }
            //                checkDbVoyage.MessageType = voyage.MessageType;
            //                checkDbVoyage.ManifestType = voyage.ManifestType;
            //                checkDbVoyage.ExpectedToArriveDate = voyage.ExpectedToArriveDate;
            //                checkDbVoyage.UploadDate = DateTime.UtcNow;
            //                checkDbVoyage.CarrierId = currentUser.CarrierId;
            //                checkDbVoyage.UserId = currentUser.UserId;
            //                //pending
            //                checkDbVoyage.StatusId = new Guid("f2fb713c-8986-44a2-bd3a-a0d64dddeef9");
            //                checkDbVoyage.FileName = files.First().FileName;
            //                db.SaveChanges();
            //            }
            //            //save to temparory file 
            //            var tempFileName = Guid.NewGuid().ToString();
            //            manifestFile.SaveAs($"{tempFolder}\\{tempFileName}");
            //            //save the voyage and qeue the file for further processing
            //            HangFireProcessModel model = new HangFireProcessModel
            //            {
            //                TempFileName = tempFileName,
            //                VoyageDetailsId = checkDbVoyage.VoyageDetailsId,
            //                VesselName = voyage.VesselName,
            //                VoyageNumber = voyage.AgentVoyageNumber,
            //                UserId = currentUser.UserId,
            //                CarrierId = currentUser.CarrierId,
            //                Email = currentUser.Email
            //            };
            //            BackgroundJob.Enqueue(() => ProcessManifestUploadRequest(model));
            //        }
            //    }
            //    return Content("");
            //}
            //catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            //{
            //    //set the status to invalid for the manifest
            //    var voyage = db.VoyageDetails.Find(voyageDetailsId);
            //    if (voyage != null)
            //    {
            //        //invalid
            //        voyage.StatusId = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19");
            //        db.SaveChanges();

            //    }
            //    var outputLines = new List<string>();
            //    foreach (var eve in ex.EntityValidationErrors)
            //    {
            //        outputLines.Add(string.Format(
            //            "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State));
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            outputLines.Add(string.Format(
            //                "- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage));
            //        }
            //    }
            //    var errors = string.Join(Environment.NewLine, outputLines);
            //    return Content(errors);
            //}
            //catch (Exception ex)
            //{
            //    //set the statue to invalid for the manifest
            //    var voyage = db.VoyageDetails.Find(voyageDetailsId);
            //    if (voyage != null)
            //    {
            //        //invalid
            //        voyage.StatusId = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19");
            //        db.SaveChanges();

            //    }
            //    return Content(ex.Message);

            //}
            var currentUser = (UserModel)Session["User"];
            if (files.Count() > 0)
            {
                var file = files.FirstOrDefault();
                var constructorInfo = typeof(HttpPostedFile).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0];
                var obj = (HttpPostedFile)constructorInfo
                          .Invoke(new object[] { file.FileName, file.ContentType, file.InputStream });
                var result = processor.ProcessVoyageFile(obj, currentUser);
                return Content(result);
            }
            return Content("No file uploaded");
        }
        //public async Task<ActionResult> Async_Save(IEnumerable<HttpPostedFileBase> files)
        //{
        //    try
        //    {
        //        if (files != null)
        //        {
        //            IExcelDataReader reader;
        //            reader = ExcelReaderFactory.CreateReader(files.FirstOrDefault().InputStream);
        //            var conf = new ExcelDataSetConfiguration
        //            {
        //                ConfigureDataTable = _ => new ExcelDataTableConfiguration
        //                {
        //                    // UseHeaderRow = true
        //                }
        //            };
        //            var manifest = extractor.GetData(reader);
        //            System.ComponentModel.DataAnnotations.ValidationContext context = new System.ComponentModel.DataAnnotations.ValidationContext(manifest, null, null);
        //            var results = new List<ValidationResult>();
        //            var isValid = Validator.TryValidateObject(manifest, context, results, true);
        //            if (isValid)
        //            {
        //                var checkDb = db.EManifests
        //               .FirstOrDefault(m => m.VesselName == manifest.VesselName && m.Voyage == manifest.Voyage);
        //                if (checkDb != null)
        //                {
        //                    throw new Exception($"Manifest already entered for Veseel : {manifest.VesselName} and Voyage : {manifest.Voyage}");
        //                }
        //                var mappedManifest = Mapper.Map<EManifests>(manifest);
        //                var currentUser = (Users)Session["User"];
        //                mappedManifest.CarrierId = currentUser.CarrierId;
        //                //save the manifest
        //                ManifestStatus newStatus = new ManifestStatus
        //                {
        //                    ManifestStatusId = Guid.NewGuid(),
        //                    ManifestId = manifest.ManifestId,
        //                    StatusId = new Guid("f2fb713c-8986-44a2-bd3a-a0d64dddeef9"),
        //                    StatusDate = DateTime.Now,
        //                };
        //                //extract items from manifest object to insert it as bulk for speed
        //                var manifestItems = mappedManifest.ManifestItems.ToList();
        //                mappedManifest.ManifestItems.Clear();
        //                //
        //                db.EManifests.Add(mappedManifest);
        //                db.ManifestStatus.Add(newStatus);
        //                await db.SaveChangesAsync();

        //                //bulk insert the items after extracting it 

        //                await db.BulkInsertAsync(manifestItems);

        //            }
        //            else
        //            {
        //                string result = "";
        //                foreach (var validationResult in results)
        //                {
        //                    if (validationResult is GeniusDMValidationResult geniusResult)
        //                    {
        //                        foreach (var innerResult in geniusResult.NestedResults
        //                            .Cast<GeniusDMValidationResult>()
        //                            .Where(r => r.NestedResults.Count > 0)
        //                            .SelectMany(r => r.NestedResults))
        //                        {
        //                            result += innerResult.ErrorMessage + Environment.NewLine;
        //                        }
        //                    }
        //                    else
        //                        result += validationResult.ErrorMessage + Environment.NewLine;
        //                }
        //                throw new Exception(result);
        //            }

        //        }
        //        return Content("");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}


        public ActionResult Async_Remove(string[] fileNames)
        {
            return Content("");
        }
        public ActionResult GetManifests([DataSourceRequest]DataSourceRequest request)
        {
            var currentUser = (UserModel)Session["User"];
            var voyages = db.VoyageDetails.AsQueryable();
            if (currentUser.CarrierId != null)
            {
                voyages = voyages.Where(m => m.CarrierId == currentUser.CarrierId);
            }
            return Json(
                voyages
                .Select(m => new VoyageModel
                {
                    VoyageDetailsId = m.VoyageDetailsId,
                    VesselName = m.VesselName,
                    ExpectedToArriveDate = m.ExpectedToArriveDate,
                    LineCode = m.LineCode,
                    VoyageAgentCode = m.VoyageAgentCode,
                    AgentVoyageNumber = m.AgentVoyageNumber,
                    PortCodeOfDischarge = m.PortCodeOfDischarge,
                    NumberOfInstalment = m.NumberOfInstalment,
                    AgentManifestSequenceNumber = m.AgentManifestSequenceNumber,
                    RotationNumber = m.RotationNumber,
                    UploadDate = m.UploadDate,
                    FileName = m.FileName,
                    Status = new StatusModel()
                    {
                        StatusId = m.StatusId.Value,
                        StatusDesc = m.Status.StatusDesc
                    },

                }).OrderByDescending(m => m.ExpectedToArriveDate).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetApprovalManifests([DataSourceRequest]DataSourceRequest request)
        {
            var currentUser = (UserModel)Session["User"];
            //valid
            //var voyages = db.VoyageDetails.Where(v => v.StatusId == new Guid("12f2243e-8545-489d-a854-7ff22fbee723")).AsQueryable();
            var voyages = db.VoyageDetails.AsQueryable();
            var validStatusId = Guid.Parse("12f2243e-8545-489d-a854-7ff22fbee723");
            var approvedStatusId = Guid.Parse("d7b43b80-ea2e-4d6c-a436-b6572ef236ac");
            voyages = voyages.Where(v => v.StatusId == validStatusId || v.StatusId == approvedStatusId);
            return Json(
                voyages
                .Select(m => new VoyageModel
                {
                    VoyageDetailsId = m.VoyageDetailsId,
                    VesselName = m.VesselName,
                    ExpectedToArriveDate = m.ExpectedToArriveDate,
                    LineCode = m.LineCode,
                    VoyageAgentCode = m.VoyageAgentCode,
                    AgentVoyageNumber = m.AgentVoyageNumber,
                    PortCodeOfDischarge = m.PortCodeOfDischarge,
                    NumberOfInstalment = m.NumberOfInstalment,
                    UploadDate = m.UploadDate,
                    FileName = m.FileName,
                    Status = new StatusModel()
                    {
                        StatusId = m.StatusId.Value,
                        StatusDesc = m.Status.StatusDesc
                    },

                }).OrderByDescending(m => m.ExpectedToArriveDate).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVoyageBolDetails(Guid voyageDetailsId, [DataSourceRequest] DataSourceRequest request)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return Json(db.BOLDetails
                    .Where(b => b.VoyageDetailsId == voyageDetailsId)
                    .ToDataSourceResult(request));
            }
        }
        public ActionResult GetBolContainerDetails(Guid bolDetailsId, [DataSourceRequest] DataSourceRequest request)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return Json(db.ContainerDetails
                    .Where(c => c.BOLDetailsId == bolDetailsId)
                    .ToDataSourceResult(request));
            }
        }
        public ActionResult GetBolConsignmentDetails(Guid bolDetailsId, [DataSourceRequest] DataSourceRequest request)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return Json(db.ConsignmentDetails
                    .Where(c => c.BOLDetailsId == bolDetailsId)
                    .ToDataSourceResult(request));
            }
        }
        public ActionResult GetContainerConsignmentDetails(Guid containerDetailsId, [DataSourceRequest] DataSourceRequest request)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return Json(db.ConsignmentDetails.OrderBy(c => c.SerialNumber)
                    .Where(c => c.ContainerDetailsId == containerDetailsId)
                    .ToDataSourceResult(request));
            }
        }
        public ActionResult GetConsignmentVehicleDetails(Guid consignmentDetailsId, [DataSourceRequest] DataSourceRequest request)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                return Json(db.VehicleDetails
                          .Where(v => v.ConsignmentDetailsId == consignmentDetailsId)
                          .ToDataSourceResult(request));
            }

        }
        public async Task<ActionResult> DeleteManifest(Guid? id)
        {
            try
            {
                var voyage = await db.VoyageDetails.FindAsync(id);
                if (voyage != null)
                {
                    if (voyage.Status.AllowModify == true)
                    {
                        VoyageSaver saver = new VoyageSaver();
                        saver.Delete(voyage.VesselName, voyage.AgentVoyageNumber, voyage.NumberOfInstalment, voyage.PortCodeOfDischarge);
                        db.VoyageDetails.Remove(voyage);
                        await db.SaveChangesAsync();
                        return Json(new { Success = true, Message = "Success" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { Success = false, Message = "Voyage cannot be deleted" }, JsonRequestBehavior.AllowGet);
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



        //public FileResult GetSampleFile()
        //{
        //    return File(@"\Content\Files\IQMan.zip", "application/zip", "IQMan.zip");
        //    //if (HttpContext.Request.Headers["Range"] != null)
        //    //{
        //    //    client.DefaultRequestHeaders.Add("Range", HttpContext.Request.Headers["Range"]);
        //    //}
        //    //HttpResponseMessage response = client.GetAsync($"{serviceUrl}/api/Files/Get?filename=IQMan.zip").Result;
        //    //return new FileDownloadResult(response);


        //}
        //public FileResult GetTechnicalFile()
        //{
        //    return File(@"\Content\Files\manifest_file_format.pdf", "application/pdf", "manifest_file_format.pdf");

        //}

        public JsonNetResult GetNotifications()
        {

            var currentUser = (UserModel)Session["User"];
            var query = db.Notifications.Where(n => n.UserId == currentUser.UserId);
            var dbNotifications = query.OrderByDescending(n => n.NotificationDate).Take(30)
               .Select(n => new NotificationModel
               {
                   NotificationId = n.NotificationId,
                   Header = n.Header,
                   Status = n.Status,
                   NotificationDate = n.NotificationDate,
                   Read = n.Read
               }).ToList();
            //delete old read notifications 
            Task.Run(() =>
            {
                var ides = dbNotifications.Select(n => n.NotificationId).ToList();
                using (EmanifestContext context = new EmanifestContext())
                {
                    context.Notifications.Where(n => n.UserId == currentUser.UserId && n.Read == true
                       && !ides.Contains(n.NotificationId)).DeleteFromQuery();
                }
            });
            dbNotifications.Where(n => n.NotificationDate != null).ToList()
                .ForEach(n =>
                {
                    //n.NotificationDate = n.NotificationDate.Value.AddMinutes((double)offset * -1);
                    n.NotificationDate = DateTime.SpecifyKind(n.NotificationDate.Value, DateTimeKind.Utc);
                });

            NotificationsModel model = new NotificationsModel();
            model.Notifications = dbNotifications.OrderBy(n => n.NotificationDate).ToList();
            model.NewCount = dbNotifications.Where(n => n.Read != true).Count();
            return new JsonNetResult(model);
        }
        public async Task<ActionResult> NotificationRead(Guid? id)
        {
            try
            {
                var notification = await db.Notifications.FindAsync(id);
                if (notification != null)
                {
                    if (notification.Read == true)
                    {
                        return Json(new { Success = false, Message = "Notification already marked as read" }, JsonRequestBehavior.AllowGet);
                    }
                    notification.Read = true;
                    await db.SaveChangesAsync();
                }
                return Json(new { Success = true, Message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> GetNotification(Guid? id)
        {
            try
            {
                var currentUser = (UserModel)Session["User"];
                var notification = await db.Notifications.FindAsync(id);
                if (notification.UserId != currentUser.UserId && notification.CarrierId != currentUser.CarrierId)
                {
                    TempData["Error"] = "Notification not found";
                    return View(new NotificationModel());
                }
                if (notification != null)
                {
                    var model = Mapper.Map<NotificationModel>(notification);
                    return View(model);
                }
                TempData["Error"] = "Notification not found";
                return View(new NotificationModel());
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new NotificationModel());
            }
        }
        public ActionResult GetAllNotifications([DataSourceRequest]DataSourceRequest request)
        {
            var currentUser = (UserModel)Session["User"];
            var query = db.Notifications.Where(n => n.UserId == currentUser.UserId).AsQueryable();
            if (currentUser.CarrierId != null)
            {
                query = query.Where(m => m.CarrierId == currentUser.CarrierId);
            }
            return Json(query
                .Select(n => new NotificationModel
                {
                    NotificationId = n.NotificationId,
                    Header = n.Header,
                    Details = n.Details,
                    Status = n.Status,
                    NotificationDate = n.NotificationDate
                }).OrderByDescending(m => m.NotificationDate).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        //hangfire
        //public async Task ProcessManifestUploadRequest(HangFireProcessModel model)
        //{
        //    using (db = new EmanifestContext())
        //    {
        //        bool sendEmails = false;
        //        try
        //        {
        //            try
        //            {
        //                sendEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmails"]);
        //            }
        //            catch (Exception)
        //            {
        //                //ignored
        //            }

        //            var dbVoyage = db.VoyageDetails.Find(model.VoyageDetailsId);
        //            if (dbVoyage == null)
        //            {
        //                throw new Exception("Voyage is not found in Db.");
        //            }
        //            var fullPath = $"{tempFolder}\\{model.TempFileName}";
        //            EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestParser = new EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(System.IO.File.Open(fullPath, FileMode.Open));
        //            var completeVoyage = manifestParser.ParseStream();
        //            completeVoyage.VoyageDetailsId = dbVoyage.VoyageDetailsId;
        //            completeVoyage.UploadDate = dbVoyage.UploadDate;
        //            completeVoyage.UserId = dbVoyage.UserId;
        //            completeVoyage.CarrierId = dbVoyage.CarrierId;
        //            completeVoyage.FileName = dbVoyage.FileName;
        //            completeVoyage.VesselId = dbVoyage.VesselId;
        //            completeVoyage.StatusId = dbVoyage.StatusId;
        //            VoyageSaver saver = new VoyageSaver();
        //            saver.Save(completeVoyage);

        //            //valid
        //            db.VoyageDetails
        //             .Where(m => m.VoyageDetailsId == model.VoyageDetailsId)
        //             .UpdateFromQuery(m => new VoyageDetails
        //             {
        //                 StatusId = new Guid("12f2243e-8545-489d-a854-7ff22fbee723"),
        //             });
        //            var containerBolNumber = completeVoyage.BOLDetails.Where(b => b.CargoCode == "F" || b.CargoCode == "L" || b.CargoCode == "M").Count();
        //            var otherBolNumber = completeVoyage.BOLDetails.Where(b => b.CargoCode != "F" && b.CargoCode != "L" && b.CargoCode != "M").Count();
        //            var successNotification = new Notifications
        //            {
        //                NotificationId = Guid.NewGuid(),
        //                Status = true,
        //                Header = $"Manifest Uploaded Successfully Vessel Name : {completeVoyage.VesselName}",
        //                Details = $"Manifest Uploaded Successfully \r\n Vessel Name : {completeVoyage.VesselName} \r\n Voyage No : {completeVoyage.AgentVoyageNumber} \r\n Port Code : {completeVoyage.PortCodeOfDischarge} \r\n Instalment No : {completeVoyage.NumberOfInstalment} \r\n Voyage agent : {completeVoyage.VoyageAgentCode}\r\n Container Bol Count : {containerBolNumber} Other Bol Count : {otherBolNumber}",
        //                NotificationDate = DateTime.UtcNow,
        //                UserId = model.UserId,
        //                CarrierId = model.CarrierId,

        //            };
        //            db.Notifications.Add(successNotification);
        //            db.SaveChanges();
        //            NotificationHub.AddNotification(model.UserId, successNotification);
        //            try
        //            {
        //                if (sendEmails)
        //                    await EmailSender.SendNotificationEmail(successNotification, model.Email);

        //            }
        //            catch (Exception ex)
        //            {
        //                //ignore
        //            }

        //        }
        //        catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        //        {
        //            //invalid
        //            db.VoyageDetails
        //             .Where(m => m.VoyageDetailsId == model.VoyageDetailsId)
        //             .UpdateFromQuery(m => new VoyageDetails { StatusId = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19") });

        //            var outputLines = new List<string>();
        //            foreach (var eve in ex.EntityValidationErrors)
        //            {
        //                outputLines.Add(string.Format(
        //                    "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                    eve.Entry.Entity.GetType().Name, eve.Entry.State));
        //                foreach (var ve in eve.ValidationErrors)
        //                {
        //                    outputLines.Add(string.Format(
        //                        "- Property: \"{0}\", Error: \"{1}\"",
        //                        ve.PropertyName, ve.ErrorMessage));
        //                }
        //            }
        //            var errors = string.Join(Environment.NewLine, outputLines);
        //            var errorNotification = new Notifications
        //            {
        //                NotificationId = Guid.NewGuid(),
        //                Status = false,
        //                Header = $"Error uploading the manifest Vessel Name : {model.VesselName} \r\n Voyage No :{model.VoyageNumber}",
        //                Details = errors,
        //                NotificationDate = DateTime.UtcNow,
        //                UserId = model.UserId,
        //                CarrierId = model.CarrierId,

        //            };
        //            db.Notifications.Add(errorNotification);
        //            db.SaveChanges();
        //            NotificationHub.AddNotification(model.UserId, errorNotification);
        //            try
        //            {
        //                if (sendEmails)
        //                    await EmailSender.SendNotificationEmail(errorNotification, model.Email);
        //            }
        //            catch (Exception)
        //            {
        //                //ignore
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //invalid
        //            db.VoyageDetails
        //             .Where(m => m.VoyageDetailsId == model.VoyageDetailsId)
        //             .UpdateFromQuery(m => new VoyageDetails { StatusId = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19") });

        //            var errorNotification = new Notifications
        //            {
        //                NotificationId = Guid.NewGuid(),
        //                Status = false,
        //                Header = $"Error uploading the manifest Vessel Name : {model.VesselName} \r\n Voyage No :{model.VoyageNumber}",
        //                Details = ex.Message,
        //                NotificationDate = DateTime.UtcNow,
        //                UserId = model.UserId,
        //                CarrierId = model.CarrierId,

        //            };
        //            db.Notifications.Add(errorNotification);
        //            db.SaveChanges();
        //            NotificationHub.AddNotification(model.UserId, errorNotification);
        //            try
        //            {
        //                if (sendEmails)
        //                    await EmailSender.SendNotificationEmail(errorNotification, model.Email);
        //            }
        //            catch (Exception)
        //            {
        //                //ignore
        //            }

        //        }
        //        finally
        //        {
        //            //delete the temp file
        //            System.IO.File.Delete(model.TempFileName);
        //        }
        //    }
        //}
        //
        public ActionResult ManifestApproval()
        {
            return View();
        }
        public async Task<ActionResult> ManifestView(Guid voyageDetailsId)
        {
            Sources sources = new Sources();
            ViewData["TradeCodesSource"] = sources.TradeCodesSource;
            ViewData["YesNoSource"] = sources.YesNoSource;
            ViewData["UsedNewSource"] = sources.UsedNewSource;
            ViewData["TemperaturesSource"] = sources.TemperaturesSource;
            ViewData["CargoCodesSource"] = sources.CargoCodesSource;
            ViewData["StorageRequestCodesSource"] = sources.StorageRequestCodesSource;
            ViewData["TransshipModesSource"] = sources.TransshipModesSource;
            ViewData["ContainerServiceTypesSource"] = sources.ContainerServiceTypesSource;
            ViewData["ContractCarriageConditionsSource"] = sources.ContractCarriageConditionsSource;
            ViewData["VehicleEquipmentIndicatorsSource"] = sources.VehicleEquipmentIndicatorsSource;
            ViewData["RollingStaticSource"] = sources.RollingStaticSource;
            ViewData["ContainerOwnerTypes"] = sources.ContainerOwnerTypes;
            using (EmanifestContext db = new EmanifestContext())
            {
                var validStatusId = Guid.Parse("12f2243e-8545-489d-a854-7ff22fbee723");
                var approvedStatusId = Guid.Parse("d7b43b80-ea2e-4d6c-a436-b6572ef236ac");
                db.Configuration.ProxyCreationEnabled = false;
                var voyage = await db.VoyageDetails.FindAsync(voyageDetailsId);
                if (voyage.StatusId != validStatusId && voyage.StatusId != approvedStatusId)
                {
                    TempData["Error"] = "Manifest is not in valid or approved state";
                    return RedirectToAction("ManifestApproval");
                }
                if (voyage.ManifestType == ManifestFileType.FCLContainer.ToString())
                {
                    return View("ManifestViewFCL", voyage);
                }
                else if (voyage.ManifestType == ManifestFileType.LCLContainer.ToString())
                {
                    return View("ManifestViewLCL", voyage);
                }
                else if (voyage.ManifestType == ManifestFileType.GeneralCargo.ToString())
                {
                    return View("ManifestViewGC", voyage);
                }
                else if (voyage.ManifestType == ManifestFileType.FCLContainerRelationNotknown.ToString())
                {
                    return View("ManifestViewFCLNR", voyage);
                }
                else
                    throw new Exception("Manifest type not correct");

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ManifestView(VoyageDetails model)
        {
            var sendEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmails"]);
            var voyage = db.VoyageDetails.Include("Users").FirstOrDefault(v => v.VoyageDetailsId == model.VoyageDetailsId);
            if (voyage != null)
            {
                if (voyage.StatusId == new Guid("d7b43b80-ea2e-4d6c-a436-b6572ef236ac"))
                    return RedirectToAction("ManifestApproval");
                //valid
                if (voyage.StatusId != new Guid("12f2243e-8545-489d-a854-7ff22fbee723"))
                {
                    Sources sources = new Sources();
                    ViewData["TradeCodesSource"] = sources.TradeCodesSource;
                    ViewData["YesNoSource"] = sources.YesNoSource;
                    ViewData["UsedNewSource"] = sources.UsedNewSource;
                    ViewData["TemperaturesSource"] = sources.TemperaturesSource;
                    ViewData["CargoCodesSource"] = sources.CargoCodesSource;
                    ViewData["StorageRequestCodesSource"] = sources.StorageRequestCodesSource;
                    ViewData["TransshipModesSource"] = sources.TransshipModesSource;
                    ViewData["ContainerServiceTypesSource"] = sources.ContainerServiceTypesSource;
                    ViewData["ContractCarriageConditionsSource"] = sources.ContractCarriageConditionsSource;
                    ViewData["VehicleEquipmentIndicatorsSource"] = sources.VehicleEquipmentIndicatorsSource;
                    ViewData["RollingStaticSource"] = sources.RollingStaticSource;
                    TempData["Error"] = "Manifest is not in valid status and cannot be approved";
                    if (voyage.ManifestType == ManifestFileType.FCLContainer.ToString())
                    {
                        return View("ManifestViewFCL", voyage);
                    }
                    else if (voyage.ManifestType == ManifestFileType.LCLContainer.ToString())
                    {
                        return View("ManifestViewLCL", voyage);
                    }
                    else if (voyage.ManifestType == ManifestFileType.GeneralCargo.ToString())
                    {
                        return View("ManifestViewGC", voyage);
                    }
                    else if (voyage.ManifestType == ManifestFileType.FCLContainerRelationNotknown.ToString())
                    {
                        return View("ManifestViewFCLNR", voyage);
                    }
                    return RedirectToAction("ManifestApproval");
                }

                //approved
                voyage.StatusId = new Guid("d7b43b80-ea2e-4d6c-a436-b6572ef236ac");
                Notifications successNotification = null;
                if (voyage.UserId != null)
                {
                    successNotification = new Notifications
                    {
                        NotificationId = Guid.NewGuid(),
                        Status = true,
                        Header = $"Manifest Approved Vessel Name : {voyage.VesselName}",
                        Details = $"Manifest Approved \r\n Vessel Name : {voyage.VesselName} \r\n Voyage No : {voyage.AgentVoyageNumber} \r\n Port Code : {voyage.PortCodeOfDischarge} \r\n Instalment No : {voyage.NumberOfInstalment} \r\n Voyage agent : {voyage.VoyageAgentCode}",
                        NotificationDate = DateTime.UtcNow,
                        UserId = voyage.UserId,
                        CarrierId = voyage.CarrierId,

                    };
                    db.Notifications.Add(successNotification);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("ManifestApproval");
                }

                if (voyage.UserId != null)
                {
                    NotificationHub.AddNotification(voyage.UserId.Value, successNotification);
                    if (sendEmails)
                    {
                        try
                        {
                             await EmailSender.SendNotificationEmail(successNotification, voyage.Users.Email);
                        }
                        catch (Exception)
                        {
                            //ignore
                        }
                       
                    }
                }

            }
            TempData["Success"] = "Manifest Approved";
            return RedirectToAction("ManifestApproval");
        }

        public async Task<JsonResult> DeclineVoyage(Guid voyageDetailsId, string declineReason = "No Reason")
        {
            var sendEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmails"]);
            var voyage = db.VoyageDetails.Include("Users").FirstOrDefault(v => v.VoyageDetailsId == voyageDetailsId);
            if (voyage != null)
            {
                if (voyage.StatusId == new Guid("bec15f0a-734f-43a3-a5ad-bb9908a62c87"))
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                //approved
                if (voyage.StatusId == new Guid("d7b43b80-ea2e-4d6c-a436-b6572ef236ac"))
                    return Json(new { Success = false, Message = "Manifest is already approved and cannot be declined." }, JsonRequestBehavior.AllowGet);

                //valid
                if (voyage.StatusId != new Guid("12f2243e-8545-489d-a854-7ff22fbee723"))
                    return Json(new { Success = false, Message = "Manifest is not in valid state and cannot be declined." }, JsonRequestBehavior.AllowGet);

                //declined
                voyage.StatusId = new Guid("bec15f0a-734f-43a3-a5ad-bb9908a62c87");
                Notifications successNotification = null;
                if (voyage.UserId != null)
                {
                    successNotification = new Notifications
                    {
                        NotificationId = Guid.NewGuid(),
                        Status = false,
                        Header = $"Manifest Declined Vessel Name : {voyage.VesselName}",
                        Details = $"Manifest Declined \r\n Vessel Name : {voyage.VesselName} \r\n Voyage No : {voyage.AgentVoyageNumber} \r\n Port Code : {voyage.PortCodeOfDischarge} \r\n Instalment No : {voyage.NumberOfInstalment} \r\n Voyage Agent : {voyage.VoyageAgentCode} \r\n Decline Reason : {declineReason ?? "reason not known"}",
                        NotificationDate = DateTime.UtcNow,
                        UserId = voyage.UserId,
                        CarrierId = voyage.CarrierId,

                    };
                    db.Notifications.Add(successNotification);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, ex.Message }, JsonRequestBehavior.AllowGet);
                }

                if (voyage.UserId != null)
                {
                    NotificationHub.AddNotification(voyage.UserId.Value, successNotification);
                    if (sendEmails)
                    {
                        await EmailSender.SendNotificationEmail(successNotification, voyage.Users.Email);
                    }
                }
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ManifestReport(Guid voyageDetailsId)
        {
            var dbVoyage = db.VoyageDetails.Find(voyageDetailsId);
            if (dbVoyage != null && dbVoyage.StatusId == Guid.Parse("d7b43b80-ea2e-4d6c-a436-b6572ef236ac"))
            {
                return View(dbVoyage);
            }
            else
            {
                TempData["Error"] = "Manifest have to be approved first.";
                return RedirectToAction("ManifestView", new { voyageDetailsId });
            }



        }


    }
}
