using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Simple.OData.Client;

namespace TestODataCSharpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                InitilizeOdata();
            }
            catch (Exception ex)
            {

            }
        }
        private static void InitilizeOdata()
        {
            var serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"].ToString();
            var serviceUri = new Uri($"{serviceUrl}odata");
            //var container = new ODataCSharpClient.Default.ODataContext(serviceUri);
            var serviceCreds = new NetworkCredential("admin", "admin");
            var cache = new CredentialCache();
            cache.Add(serviceUri, "Basic", serviceCreds);


            //var voyages = container.VoyageDetails
            //    .Expand(v => v.Status).ToList();
            ODataClientSettings setting = new ODataClientSettings
            {
                BaseUri = serviceUri,
                Credentials = cache
            };

            var client = new ODataClient(setting);
            //var voyages = client.For<VoyageDetails>()
            //    .Expand(v => v.Status)
            //.FindEntriesAsync().GetAwaiter().GetResult().ToList();

            //var voyage = client.For<VoyageDetails>()
            //    .Key(new Guid("3bcd01ac-f0ea-42eb-834d-1e5e6c9d9af0"))
            //    .Set(new { StatusId = "12f2243e-8545-489d-a854-7ff22fbee723" })
            //    .UpdateEntryAsync().GetAwaiter().GetResult();


        }
    }
}
