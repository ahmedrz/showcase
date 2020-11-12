using ODataCSharpClient;
using PortSystem.DAL;
using PortSystem.DAL.Core;
using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortSystem.Test
{
    class Program
    {
        static ODataClient client;
        static void Main(string[] args)
        {
            InitializeOdata();
        }
        private static void InitializeOdata()
        {
            var serviceUrl = ConfigurationManager.AppSettings["ServiceUrl"].ToString();
            var uri = new Uri($"{serviceUrl}odata");
            client = EmanifestODataHelper.CreateClient(uri, "Admin", "Admin");
         
            var voyages = client.For<VoyageDetails>()
                .Expand("Carriers")
                .Expand(v => v.BOLDetails.Select(b => b.ContainerDetails.Select(c => c.ConsignmentDetails.Select(con => con.VehicleDetails))))
                .Expand(v => v.BOLDetails.Select(b => b.ConsignmentDetails.Select(con => con.VehicleDetails)))
                .Expand("Vessels")
                .Expand("Status").FindEntriesAsync().GetAwaiter().GetResult().ToList();
            ManifestSaver saver = new ManifestSaver();
            saver.Save(voyages.FirstOrDefault());
        }

    }
}
