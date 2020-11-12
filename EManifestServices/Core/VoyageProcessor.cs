using EmanifestParser;
using EManifestServices.DAL;
using EManifestServices.Helpers;
using EManifestServices.Models;
using Hangfire;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace EManifestServices.Core
{
    public class VoyageProcessor
    {
        VoyageDetailsDataValidator validator;
        string tempFolder = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/")}\\TempFiles\\";
        EmanifestContext db;
        public VoyageProcessor(EmanifestContext db)
        {
            this.db = db;
            validator = new VoyageDetailsDataValidator();
        }
        public VoyageProcessor()
        {
            //for hangfire
            validator = new VoyageDetailsDataValidator();
        }
        private string ProcessVoyageFile(HttpPostedFile manifestFile, Guid? userId, Guid? CarrierId, string email)
        {
            Guid? voyageDetailsId = null;
            try
            {
                using (StreamReader reader = new StreamReader(manifestFile.InputStream))
                {
                    var headerLine = reader.ReadLine();
                    List<ValidationResult> validationResult = new List<ValidationResult>();
                    var voyage = VoyageParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>.Parse(headerLine);
                    if (!Validator.TryValidateObject(voyage, new ValidationContext(voyage), validationResult))
                    {
                        throw new Exception(string.Join(Environment.NewLine, validationResult.SelectMany(v => v.ErrorMessage)));
                    }
                    //validate voyage
                    //arrival date validation 
                    var arabianTime = TimeZoneInfo.ConvertTime(DateTime.Now,
             TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time"));
                    if (voyage.ExpectedToArriveDate < arabianTime)
                    {
                        throw new Exception("This is an old manifest , Expected arrival date should be greater than or equal to today's date for arabian standard timezone (+3).");
                    }
                    var checkDbVoyage = db.VoyageDetails
                        .FirstOrDefault(m => m.VesselName == voyage.VesselName
                        && m.AgentVoyageNumber == voyage.AgentVoyageNumber
                        && m.NumberOfInstalment == voyage.NumberOfInstalment
                        && m.PortCodeOfDischarge == voyage.PortCodeOfDischarge);
                    if (checkDbVoyage != null && checkDbVoyage.Status.AllowModify != true)
                    {
                        throw new Exception($"Manifest is in {checkDbVoyage.Status.StatusDesc} state and cannot be modified.");
                    }
                    else
                    {
                        if (checkDbVoyage == null)
                        {
                            var checkVesselQuery = db.Vessels.Where(v => v.VesselName == voyage.VesselName).AsQueryable();
                            if (CarrierId != null)
                            {
                                checkVesselQuery = checkVesselQuery.Where(v => v.CarrierId == CarrierId);
                            }
                            var vesselDb = checkVesselQuery.FirstOrDefault();
                            if (vesselDb == null)
                            {
                                throw new Exception($"The vessel name and information is not found try adding your vessel in Vessels page.");
                            }
                            checkDbVoyage = voyage;
                            checkDbVoyage.VoyageDetailsId = Guid.NewGuid();
                            voyageDetailsId = checkDbVoyage.VoyageDetailsId;
                            checkDbVoyage.VesselId = vesselDb.VesselId;
                            db.VoyageDetails.Add(checkDbVoyage);
                        }
                        checkDbVoyage.MessageType = voyage.MessageType;
                        checkDbVoyage.ManifestType = voyage.ManifestType;
                        checkDbVoyage.ExpectedToArriveDate = voyage.ExpectedToArriveDate;
                        checkDbVoyage.UploadDate = DateTime.UtcNow;
                        checkDbVoyage.CarrierId = CarrierId;
                        checkDbVoyage.UserId = userId;
                        //pending
                        checkDbVoyage.StatusId = new Guid("f2fb713c-8986-44a2-bd3a-a0d64dddeef9");
                        checkDbVoyage.FileName = manifestFile.FileName;
                        db.SaveChanges();
                    }
                    //save to temparory file 
                    var tempFileName = Guid.NewGuid().ToString();
                    manifestFile.SaveAs($"{tempFolder}\\{tempFileName}");
                    //save the voyage and qeue the file for further processing
                    HangFireProcessModel model = new HangFireProcessModel
                    {
                        TempFileName = tempFileName,
                        VoyageDetailsId = checkDbVoyage.VoyageDetailsId,
                        VesselName = voyage.VesselName,
                        VoyageNumber = voyage.AgentVoyageNumber,
                        UserId = userId,
                        CarrierId = CarrierId,
                        Email = email
                    };
                    BackgroundJob.Enqueue(() => ProcessManifestUploadRequest(model));
                    return "";
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                //set the status to invalid for the manifest
                var voyage = db.VoyageDetails.Find(voyageDetailsId);
                if (voyage != null)
                {
                    //invalid
                    voyage.StatusId = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19");
                    db.SaveChanges();

                }
                var outputLines = new List<string>();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                var errors = string.Join(Environment.NewLine, outputLines);
                return errors;
            }
            catch (Exception ex)
            {
                //set the statue to invalid for the manifest
                var voyage = db.VoyageDetails.Find(voyageDetailsId);
                if (voyage != null)
                {
                    //invalid
                    voyage.StatusId = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19");
                    db.SaveChanges();

                }
                return ex.Message;

            }
        }
        public string ProcessVoyageFile(HttpPostedFile manifestFile, ClaimsPrincipal principal)
        {
            try
            {
                var carrierIdString = principal.Claims.FirstOrDefault(c => c.Type == "CarrierId").Value;
                var carrierId = Guid.Parse(carrierIdString);
                var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                return ProcessVoyageFile(manifestFile, null, carrierId, email);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string ProcessVoyageFile(HttpPostedFile manifestFile, UserModel currentUser)
        {
            try
            {
                var userId = currentUser.UserId;
                var carrierid = currentUser.CarrierId;
                var email = currentUser.Email;

                return ProcessVoyageFile(manifestFile, userId, carrierid, email);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        public async Task ProcessManifestUploadRequest(HangFireProcessModel model)
        {
            using (db = new EmanifestContext())
            {
                bool sendEmails = false;
                try
                {
                    try
                    {
                        sendEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmails"]);
                    }
                    catch (Exception)
                    {
                        //ignored
                    }

                    var dbVoyage = db.VoyageDetails.Find(model.VoyageDetailsId);
                    if (dbVoyage == null)
                    {
                        throw new Exception("Voyage is not found in Db.");
                    }
                    var fullPath = $"{tempFolder}\\{model.TempFileName}";
                    EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> manifestParser = new EmanifestParser.EmanifestParser<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(System.IO.File.Open(fullPath, FileMode.Open));
                    var completeVoyage = manifestParser.ParseStream();
                    //data validator
                    var valid = validator.ValidateVoyageData(completeVoyage);
                    completeVoyage.VoyageDetailsId = dbVoyage.VoyageDetailsId;
                    completeVoyage.UploadDate = dbVoyage.UploadDate;
                    completeVoyage.UserId = dbVoyage.UserId;
                    completeVoyage.CarrierId = dbVoyage.CarrierId;
                    completeVoyage.FileName = dbVoyage.FileName;
                    completeVoyage.VesselId = dbVoyage.VesselId;
                    completeVoyage.StatusId = dbVoyage.StatusId;
                    VoyageSaver saver = new VoyageSaver();
                    saver.Save(completeVoyage);

                    //valid
                    db.VoyageDetails
                     .Where(m => m.VoyageDetailsId == model.VoyageDetailsId)
                     .UpdateFromQuery(m => new VoyageDetails
                     {
                         StatusId = new Guid("12f2243e-8545-489d-a854-7ff22fbee723"),
                     });
                    var containerBolNumber = completeVoyage.BOLDetails.Where(b => b.CargoCode == "F" || b.CargoCode == "L" || b.CargoCode == "M").Count();
                    var otherBolNumber = completeVoyage.BOLDetails.Where(b => b.CargoCode != "F" && b.CargoCode != "L" && b.CargoCode != "M").Count();
                    var successNotification = new Notifications
                    {
                        NotificationId = Guid.NewGuid(),
                        Status = true,
                        Header = $"Manifest Uploaded Successfully Vessel Name : {completeVoyage.VesselName}",
                        Details = $"Manifest Uploaded Successfully \r\n Vessel Name : {completeVoyage.VesselName} \r\n Voyage No : {completeVoyage.AgentVoyageNumber} \r\n Port Code : {completeVoyage.PortCodeOfDischarge} \r\n Instalment No : {completeVoyage.NumberOfInstalment} \r\n Voyage agent : {completeVoyage.VoyageAgentCode}\r\n Container Bol Count : {containerBolNumber} Other Bol Count : {otherBolNumber}",
                        NotificationDate = DateTime.UtcNow,
                        UserId = model.UserId,
                        CarrierId = model.CarrierId,

                    };
                    if (model.UserId != null)
                    {

                        db.Notifications.Add(successNotification);
                        db.SaveChanges();
                        NotificationHub.AddNotification(model.UserId.Value, successNotification);
                    }
                    try
                    {
                        if (sendEmails)
                            await EmailSender.SendNotificationEmail(successNotification, model.Email);

                    }
                    catch (Exception ex)
                    {
                        //ignore
                    }

                }

                catch (VoyageValidationException ex)
                {
                    //invalid
                    db.VoyageDetails
                     .Where(m => m.VoyageDetailsId == model.VoyageDetailsId)
                     .UpdateFromQuery(m => new VoyageDetails { StatusId = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19") });

                    var outputLines = new List<string>();
                    foreach (var eve in ex.ValidationErrors)
                    {
                        var entity = eve.Entity;
                        if (entity is VoyageDetails)
                        {
                            var voyageDetails = entity as VoyageDetails;
                            outputLines.Add(string.Format(
                          "Entity of type \"{0}\" with vessel name \"{1}\" has the following validation errors:",
                          eve.EntityType, voyageDetails.VesselName));
                        }
                        if (entity is BOLDetails)
                        {
                            var bol = entity as BOLDetails;
                            outputLines.Add(string.Format(
                          "Entity of type \"{0}\" with No \"{1}\" has the following validation errors:",
                          eve.EntityType, bol.BillOfLadingNumber));
                        }
                        if (entity is ContainerDetails)
                        {
                            var container = entity as ContainerDetails;
                            outputLines.Add(string.Format(
                          "Entity of type \"{0}\" with No \"{1}\"  has the following validation errors:",
                          eve.EntityType, container.ContainerNo));
                        }
                        if (entity is ConsignmentDetails)
                        {
                            var consignment = entity as ConsignmentDetails;
                            outputLines.Add(string.Format(
                          "Entity of type \"{0}\" with HSCode No \"{1}\" has the following validation errors:",
                          eve.EntityType, consignment.CommodityCode));
                        }
                        foreach (var ve in eve.ErrorMessages)
                        {
                            outputLines.Add(ve.ErrorMessage);
                        }
                    }
                    var errors = string.Join(Environment.NewLine, outputLines);
                    var errorNotification = new Notifications
                    {
                        NotificationId = Guid.NewGuid(),
                        Status = false,
                        Header = $"Error uploading the manifest Vessel Name : {model.VesselName} \r\n Voyage No :{model.VoyageNumber}",
                        Details = errors,
                        NotificationDate = DateTime.UtcNow,
                        UserId = model.UserId,
                        CarrierId = model.CarrierId,

                    };
                    if (model.UserId != null)
                    {
                        db.Notifications.Add(errorNotification);
                        db.SaveChanges();
                        NotificationHub.AddNotification(model.UserId.Value, errorNotification);
                    }
                    try
                    {
                        if (sendEmails)
                            await EmailSender.SendNotificationEmail(errorNotification, model.Email);
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    //invalid
                    db.VoyageDetails
                     .Where(m => m.VoyageDetailsId == model.VoyageDetailsId)
                     .UpdateFromQuery(m => new VoyageDetails { StatusId = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19") });

                    var outputLines = new List<string>();
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        var entity = eve.Entry.Entity;

                        if (entity is BOLDetails)
                        {
                            var bol = entity as BOLDetails;
                            outputLines.Add(string.Format(
                          "Entity of type \"{0}\" with No \"{1}\" has the following validation errors:",
                          eve.Entry.Entity.GetType().Name, bol.BillOfLadingNumber));
                        }
                        if (entity is ContainerDetails)
                        {
                            var container = entity as ContainerDetails;
                            var bol = container.BOLDetails;
                            outputLines.Add(string.Format(
                          "Entity of type \"{0}\" with No \"{1}\" in Bill Of Lading No \"{2}\" has the following validation errors:",
                          eve.Entry.Entity.GetType().Name, container.ContainerNo, bol.BillOfLadingNumber));
                        }
                        if (entity is ConsignmentDetails)
                        {
                            var consignment = entity as ConsignmentDetails;
                            var bol = consignment.BOLDetails;
                            if (bol == null)
                            {
                                bol = consignment.ContainerDetails.BOLDetails;
                            }
                            outputLines.Add(string.Format(
                          "Entity of type \"{0}\" with HSCode No \"{1}\" and Bill Of Lading No \"{2}\" has the following validation errors:",
                          eve.Entry.Entity.GetType().Name, consignment.CommodityCode, bol.BillOfLadingNumber));
                        }
                        if (entity is VehicleDetails)
                        {
                            var vehicle = entity as VehicleDetails;
                            var bol = vehicle.ConsignmentDetails.BOLDetails;
                            if (bol == null)
                            {
                                bol = vehicle.ConsignmentDetails.ContainerDetails.BOLDetails;
                            }
                            outputLines.Add(string.Format(
                          "Entity of type \"{0}\" with Chasis No \"{1}\" in Bill Of Lading No \"{2}\" has the following validation errors:",
                          eve.Entry.Entity.GetType().Name, vehicle.ChasisNumber, bol.BillOfLadingNumber));
                        }
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    var errors = string.Join(Environment.NewLine, outputLines);
                    var errorNotification = new Notifications
                    {
                        NotificationId = Guid.NewGuid(),
                        Status = false,
                        Header = $"Error uploading the manifest Vessel Name : {model.VesselName} \r\n Voyage No :{model.VoyageNumber}",
                        Details = errors,
                        NotificationDate = DateTime.UtcNow,
                        UserId = model.UserId,
                        CarrierId = model.CarrierId,

                    };
                    if (model.UserId != null)
                    {
                        db.Notifications.Add(errorNotification);
                        db.SaveChanges();
                        NotificationHub.AddNotification(model.UserId.Value, errorNotification);
                    }
                    try
                    {
                        if (sendEmails)
                            await EmailSender.SendNotificationEmail(errorNotification, model.Email);
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                }
                catch (Exception ex)
                {
                    //invalid
                    db.VoyageDetails
                     .Where(m => m.VoyageDetailsId == model.VoyageDetailsId)
                     .UpdateFromQuery(m => new VoyageDetails { StatusId = new Guid("5aa93c9b-65d1-4378-a35c-537db5ab1d19") });

                    var errorNotification = new Notifications
                    {
                        NotificationId = Guid.NewGuid(),
                        Status = false,
                        Header = $"Error uploading the manifest Vessel Name : {model.VesselName} \r\n Voyage No :{model.VoyageNumber}",
                        Details = ex.Message,
                        NotificationDate = DateTime.UtcNow,
                        UserId = model.UserId,
                        CarrierId = model.CarrierId,

                    };
                    if (model.UserId != null)
                    {
                        db.Notifications.Add(errorNotification);
                        db.SaveChanges();
                        NotificationHub.AddNotification(model.UserId.Value, errorNotification);
                    }
                    try
                    {
                        if (sendEmails)
                            await EmailSender.SendNotificationEmail(errorNotification, model.Email);
                    }
                    catch (Exception)
                    {
                        //ignore
                    }

                }
                finally
                {
                    //delete the temp file
                    File.Delete(model.TempFileName);
                }
            }
        }
    }
}