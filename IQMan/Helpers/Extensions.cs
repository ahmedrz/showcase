using EManifestClient.Core;
using EmanifestParser;
using IQManClient.DAL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IQMan.Helpers
{
    public static class Extensions
    {
        static string TempFolder = "TempFolder";
        public static void GenerateEDI(this VoyageDetails voyage)
        {
            var context = new ValidationContext(voyage, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            var isValid = Validator.TryValidateObject(voyage, context, results);

            if (!isValid)
            {
                string error = "";
                foreach (var validationResult in results)
                {
                    error += validationResult.ErrorMessage + Environment.NewLine;
                }
            }
            else
            {
                ManifestFileType fileType = (ManifestFileType)Enum.Parse(typeof(ManifestFileType), voyage.ManifestType);
                EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> serializer = new EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(fileType);
                string test = serializer.SerializeToString(voyage);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == true)
                    serializer.SerializeTofile(saveFileDialog.FileName, voyage);
            }
        }

        public static string SaveEDIToTempFolder(this VoyageDetails voyage)
        {
            var context = new ValidationContext(voyage, serviceProvider: null, items: null);
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

            var isValid = Validator.TryValidateObject(voyage, context, results);

            if (!isValid)
            {
                string error = "";
                foreach (var validationResult in results)
                {
                    error += validationResult.ErrorMessage + Environment.NewLine;
                }
                throw new Exception(error);
            }
            else
            {
                ManifestFileType fileType = (ManifestFileType)Enum.Parse(typeof(ManifestFileType), voyage.ManifestType);
                EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails> serializer = new EmanifestSerializer<VoyageDetails, BOLDetails, ConsignmentDetails, ContainerDetails, VehicleDetails>(fileType);
                string exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var tempFolderFullPath = Path.Combine(exePath, TempFolder);
                if (!Directory.Exists(tempFolderFullPath))
                {
                    Directory.CreateDirectory(TempFolder);
                }
                string fileName = Path.Combine(tempFolderFullPath, $"{ Guid.NewGuid().ToString() }.txt");
                serializer.SerializeTofile(fileName, voyage);
                return fileName;
            }
        }
        public async static Task UploadManifest(this VoyageDetails voyage, FileProgressEventHandler progressHandler, CancellationToken cancelToken)
        {
            string fileName = "";
            try
            {
                fileName = voyage.SaveEDIToTempFolder();
                //upload
                var currentClient = Settings.User.Carriers?.ApiClients?.FirstOrDefault();
                if (currentClient != null)
                {
                    var downloader = new DataDownloader<Vessels, Users, VoyageDetails, CountryCodes, LocationCodes, UNHSCodes, PackageCodes, Lines, ContainerIsoCodes, UNHazardousCodes>(currentClient.ApiClientName, currentClient.ApiClientPassword);
                    downloader.FileProgress += progressHandler;
                    var result = await downloader.Upload(File.Open(fileName, FileMode.Open), cancelToken);
                }
                else
                    throw new Exception("No apiClient for current user.");
            }
            catch (TaskCanceledException ex)
            {
                //task canceled
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fileName != "")
                {
                    File.Delete(fileName);
                }
            }
        }

        public static void UndoingChangesDbContextLevel(this DbContext context)
        {
            foreach (DbEntityEntry entry in context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }
        public static bool IsDirty(this DbContext context)
        {
            // Query the change tracker entries for any adds, modifications, or deletes.
            IEnumerable<DbEntityEntry> res = from e in context.ChangeTracker.Entries()
                                             where e.State.HasFlag(EntityState.Added) ||
                                                 e.State.HasFlag(EntityState.Modified) ||
                                                 e.State.HasFlag(EntityState.Deleted)
                                             select e;

            if (res.Any())
                return true;

            return false;

        }
    }
}
