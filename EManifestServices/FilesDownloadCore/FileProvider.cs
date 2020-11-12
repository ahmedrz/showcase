﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace EManifestServices.FilesDownloadCore
{
    public class FileProvider : IFileProvider
    {
        private readonly string _filesDirectory;
        string DefaultFileLocation = $"{System.Web.Hosting.HostingEnvironment.MapPath("~/")}\\Files\\";
        private const string AppSettingsKey = "FileProvider.FilesLocation";

        public FileProvider()
        {
            _filesDirectory = DefaultFileLocation;
            var fileLocation = ConfigurationManager.AppSettings[AppSettingsKey];
            if (!String.IsNullOrWhiteSpace(fileLocation))
            {
                _filesDirectory = fileLocation;
            }
        }

        public bool Exists(string name)
        {
            //make sure we dont access directories outside of our store for security reasons
            string file = Directory.GetFiles(_filesDirectory, name, SearchOption.TopDirectoryOnly)
                    .FirstOrDefault();
            return file != null;
        }

        public FileStream Open(string name)
        {
            return File.Open(GetFilePath(name),
                FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public long GetLength(string name)
        {
            return new FileInfo(GetFilePath(name)).Length;
        }

        private string GetFilePath(string name)
        {
            return Path.Combine(_filesDirectory, name);
        }
    }
}