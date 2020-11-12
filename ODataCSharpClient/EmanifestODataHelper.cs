using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ODataCSharpClient
{
    public static class EmanifestODataHelper
    {
        public static ODataClient CreateClient(Uri serviceUri, string username, string password)
        {
            //var serviceCreds = new NetworkCredential("admin", "admin");
            var serviceCreds = new NetworkCredential(username, password);
            var cache = new CredentialCache();
            cache.Add(serviceUri, "Basic", serviceCreds);
            ODataClientSettings setting = new ODataClientSettings
            {
                RequestTimeout = TimeSpan.FromMinutes(5),
                BaseUri = serviceUri,
                Credentials = cache,
                OnApplyClientHandler = handler =>
              {
                  handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
              }
            };
            return new ODataClient(setting);
        }
    }
}
