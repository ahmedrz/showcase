using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EManifestServices.Attributes;
using EManifestServices.Core;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;
using System.Security.Claims;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace EManifestServices.Controllers
{
    [System.Web.Http.Authorize]
    [BasicAuthentication]
    public class VoyageController : ApiController
    {
        [System.Web.Http.Authorize(Roles = "Admin,PortClient")]
        public dynamic GetValidVoyages(string portCode, bool showApproved = false, bool showDeclined = false)
        {
            var info = TimeZoneInfo.FindSystemTimeZoneById("Arabic Standard Time");
            DateTimeOffset localServerTime = DateTimeOffset.Now;
            DateTimeOffset localTime = TimeZoneInfo.ConvertTime(localServerTime, info);
            List<Guid> statuses = new List<Guid>() { Guid.Parse("12f2243e-8545-489d-a854-7ff22fbee723") };
            if (showApproved)
            {
                statuses.Add(Guid.Parse("d7b43b80-ea2e-4d6c-a436-b6572ef236ac"));
            }
            if (showDeclined)
            {
                statuses.Add(Guid.Parse("bec15f0a-734f-43a3-a5ad-bb9908a62c87"));
            }
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var validVoyages = db.VoyageDetails
                    .Where(m => m.StatusId != null &&
                    statuses.Contains(m.StatusId.Value)
                    && m.PortCodeOfDischarge == portCode
                    && m.ExpectedToArriveDate >= localTime.DateTime
                    )
                    .Select(m => new { m.VesselName, Voyage = m.AgentVoyageNumber, PortCode = m.PortCodeOfDischarge, ExpectedArrivalDate = m.ExpectedToArriveDate, Status = m.Status.StatusDesc })
                    .Distinct().ToList();
                return validVoyages;
            }
        }
        [System.Web.Http.Authorize(Roles = "Admin,PortClient")]
        public List<VoyageDetails> GetVoyage(string vesselName, string voyage, string portCode)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var voyages = db.VoyageDetails
                              .Include("BOLDetails.ConsignmentDetails.VehicleDetails")
                              .Include("Status")
                              .Include("Vessels")
                              .Include("Carriers")
                              .Include("BOLDetails.ContainerDetails.ConsignmentDetails.VehicleDetails")
                              .Where(v => v.VesselName == vesselName
                              && v.AgentVoyageNumber == voyage
                              && v.PortCodeOfDischarge == portCode).ToList();

                return voyages;
            }
        }
        [System.Web.Http.Authorize(Roles = "Admin,PortClient")]
        [System.Web.Http.HttpGet]
        public async Task<dynamic> ApproveVoyage(Guid voyageDetailsId)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                var sendEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmails"]);
                db.Configuration.ProxyCreationEnabled = false;
                var voyage = db.VoyageDetails.Include("Users").FirstOrDefault(v => v.VoyageDetailsId == voyageDetailsId);
                if (voyage != null)
                {
                    if (voyage.StatusId == new Guid("d7b43b80-ea2e-4d6c-a436-b6572ef236ac"))
                        return new { Success = true };

                    //approved
                    voyage.StatusId = new Guid("d7b43b80-ea2e-4d6c-a436-b6572ef236ac");

                    var successNotification = new Notifications
                    {
                        NotificationId = Guid.NewGuid(),
                        Status = true,
                        Header = $"Manifest Approved Vessel Name : {voyage.VesselName}",
                        Details = $"Manifest Approved \r\n Vessel Name : {voyage.VesselName} \r\n Voyage No : {voyage.AgentVoyageNumber} \r\n Port Code {voyage.PortCodeOfDischarge} \r\n Instalment No : {voyage.NumberOfInstalment} \r\n Agent Code : {voyage.VoyageAgentCode}",
                        NotificationDate = DateTime.UtcNow,
                        UserId = voyage.UserId,
                        CarrierId = voyage.CarrierId,

                    };
                    db.Notifications.Add(successNotification);
                    db.SaveChanges();
                    if (voyage.UserId != null)
                    {
                        NotificationHub.AddNotification(voyage.UserId.Value, successNotification);
                        if (sendEmails)
                        {
                            await EmailSender.SendNotificationEmail(successNotification, voyage.Users.Email);
                        }
                    }
                }
                return new { Success = true };
            }
        }
        [System.Web.Http.Authorize(Roles = "Admin,PortClient")]
        [System.Web.Http.HttpGet]
        public async Task<dynamic> DeclineVoyage(Guid voyageDetailsId, string declineReason = "No Reason")
        {
            var sendEmails = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmails"]);
            using (EmanifestContext db = new EmanifestContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                var voyage = db.VoyageDetails.Include("Users").FirstOrDefault(v => v.VoyageDetailsId == voyageDetailsId);
                if (voyage != null)
                {
                    if (voyage.StatusId == new Guid("bec15f0a-734f-43a3-a5ad-bb9908a62c87"))
                        return new { Success = true };

                    //declined
                    voyage.StatusId = new Guid("bec15f0a-734f-43a3-a5ad-bb9908a62c87");

                    var successNotification = new Notifications
                    {
                        NotificationId = Guid.NewGuid(),
                        Status = false,
                        Header = $"Manifest Declined Vessel Name : {voyage.VesselName}",
                        Details = $"Manifest Declined \r\n Vessel Name : {voyage.VesselName} \r\n Voyage No : {voyage.AgentVoyageNumber} \r\n Port Code {voyage.PortCodeOfDischarge} \r\n Instalment No : {voyage.NumberOfInstalment} \r\n Agent Code : {voyage.VoyageAgentCode} \r\n Decline Reason : {declineReason ?? "reason not known"}",
                        NotificationDate = DateTime.UtcNow,
                        UserId = voyage.UserId,
                        CarrierId = voyage.CarrierId,

                    };
                    db.Notifications.Add(successNotification);
                    db.SaveChanges();
                    if (voyage.UserId != null)
                    {
                        NotificationHub.AddNotification(voyage.UserId.Value, successNotification);
                        if (sendEmails)
                        {
                            await EmailSender.SendNotificationEmail(successNotification, voyage.Users.Email);
                        }
                    }
                }
                return new { Success = true };

            }


        }
        public async Task<Guid> GetStatus(Guid voyageDetailsId)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                var voyage = await db.VoyageDetails.FindAsync(voyageDetailsId);
                if (voyage != null && voyage.StatusId != null)
                {
                    return voyage.StatusId.Value;
                }
                else
                    return Guid.Empty;

            }
        }
        [HttpPost]
        public dynamic PortManifestFile()
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                VoyageProcessor processor = new VoyageProcessor(db);
                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count > 0)
                {
                    var file = httpRequest.Files[0] as HttpPostedFile;
                    var result = processor.ProcessVoyageFile(file, User as ClaimsPrincipal);
                    if (result == "")
                    {
                        return new { Success = true, Message = "Manifest Uploaded." };
                    }
                    else
                    {
                        return new { Success = false, Message = result };
                    }
                }
                return new { Success = false, Message = "No files uploaded." };
            }
        }

        //private Stream DecompressFileStream(Stream inputStream)
        //{
        //    try
        //    {
        //        // Read in the input stream, then decompress in to the outputstream.
        //        // Doing this asynronously, but not really required at this point
        //        // since we end up waiting on it right after this.
        //        Stream outputStream = new MemoryStream();

        //        var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);

        //        gzipStream.CopyTo(outputStream);
        //        gzipStream.Dispose();

        //        outputStream.Seek(0, SeekOrigin.Begin);

        //        return outputStream;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

    }
    //
    //private byte[] CreateZip(string data)
    //{
    //    using (var ms = new MemoryStream())
    //    {
    //        using (var ar = new ZipArchive(ms, ZipArchiveMode.Create, true))
    //        {
    //            var file = ar.CreateEntry("file.txt");

    //            using (var entryStream = file.Open())
    //            using (var sw = new StreamWriter(entryStream))
    //            {
    //                sw.Write(value);
    //            }
    //        }
    //        return ms.ToArray();
    //    }
    //}
    //public HttpResponseMessage GetVoyageFile(Guid voyageDetailsId)
    //{
    //    using (EmanifestContext db = new EmanifestContext())
    //    {
    //        db.Configuration.ProxyCreationEnabled = false;
    //        var voyage = db.VoyageDetails
    //                .Include("BOLDetails.ConsignmentDetails.VehicleDetails")
    //                      .Include("BOLDetails.ContainerDetails.ConsignmentDetails.VehicleDetails")
    //                      .FirstOrDefault(v => v.VoyageDetailsId == voyageDetailsId);
    //        if (voyage == null)
    //        {
    //            return null;
    //        }
    //        ManifestFileType manifestType = (ManifestFileType)Enum.Parse(typeof(ManifestFileType), voyage.ManifestType);
    //        EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> serializer = new EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(manifestType);
    //        var data = serializer.SerializeToString(voyage);
    //        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
    //        result.Content = new ByteArrayContent(CreateZip(data));
    //        result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip, application/octet-stream");
    //        return result;
    //    }
    //}

}

