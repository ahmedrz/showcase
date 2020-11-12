using EManifestClient.Core;
using IQManClient.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMan.Helpers
{
    public class DownloadInfoHelper
    {
        public EmanifestContext db;
        DataDownloader<Vessels, Users, VoyageDetails, CountryCodes, LocationCodes, UNHSCodes, PackageCodes, Lines, ContainerIsoCodes, UNHazardousCodes> downloader;
        public DownloadInfoHelper(EmanifestContext db)
        {
            this.db = db;
            var currentClient = Settings.User.Carriers?.ApiClients?.FirstOrDefault();
            if (currentClient != null)
            {
                downloader = new DataDownloader<Vessels, Users, VoyageDetails, CountryCodes, LocationCodes, UNHSCodes, PackageCodes, Lines, ContainerIsoCodes, UNHazardousCodes>(currentClient.ApiClientName, currentClient.ApiClientPassword);
                
            }
        }
        public async Task DownloadAndProcessVessels()
        {
            if (downloader == null)
                return;

            var currentUser = Settings.User;
            if (currentUser.CarrierId != null)
            {
                var onlineVessels = await downloader.GetVessels(currentUser.CarrierId.Value);
                db.BulkMerge(onlineVessels);
            }
        }
        public async Task DownloadAndProcessCountryCodes()
        {
            if (downloader == null)
                return;
            var currentSerial = db.CountryCodes.Max(c => c.Serial);
            var onlineCountryCodes = await downloader.GetCountryCodes(currentSerial);
            db.BulkMerge(onlineCountryCodes);

        }
        public async Task DownloadAndProcessLocationCodes()
        {
            if (downloader == null)
                return;
            var currentSerial = db.LocationCodes.Max(c => c.Serial);
            var onlineLocationCodes = await downloader.GetLocationCodes(currentSerial);
            db.BulkMerge(onlineLocationCodes);

        }
        public async Task DownloadAndProcessHSCodes()
        {
            if (downloader == null)
                return;
            var currentSerial = db.UNHSCodes.Max(c => c.Serial);
            var onlineHSCodes = await downloader.GetHSCodes(currentSerial);
            db.BulkMerge(onlineHSCodes);

        }
        public async Task DownloadAndProcessPackagesCodes()
        {
            if (downloader == null)
                return;
            var currentSerial = db.PackageCodes.Max(c => c.Serial);
            var onlinePackageCodes = await downloader.GetPackageCodes(currentSerial);
            db.BulkMerge(onlinePackageCodes);

        }
        public async Task DownloadAndProcessLines()
        {
            if (downloader == null)
                return;
            //var currentSerial = db.Lines.Any() ? db.Lines.Max(c => c.Serial) : 0;
            var onlineLines = await downloader.GetLines(0);
            db.BulkMerge(onlineLines);

        }
        public async Task DownloadAndProcessContainerIsoCodes()
        {
            if (downloader == null)
                return;
            //var currentSerial = db.Lines.Any() ? db.Lines.Max(c => c.Serial) : 0;
            var containerCodes = await downloader.GetContainerIsoCodes(0);
            db.BulkMerge(containerCodes);

        }
        public async Task DownloadAndProcessUNHazardousCodes()
        {
            if (downloader == null)
                return;
            var currentSerial = db.UNHazardousCodes.Any() ? db.UNHazardousCodes.Max(c => c.Serial) : 0;
            var codes = await downloader.GetUNHazardousCodes(currentSerial);
            db.BulkMerge(codes);

        }
    }
}
