using EManifestServices.Attributes;
using EManifestServices.DAL;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System.Linq;

using System.Web.Http;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Batch;
using EManifestServices.Core;

namespace EManifestServices.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // TODO: Add any additional configuration code.
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //ODATA
            ODataBatchHandler odataBatchHandler =
            new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer);
            //odataBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;
            //odataBatchHandler.MessageQuotas.MaxPartsPerBatch = 10;
            config.MapODataServiceRoute("odata", "odata", GetModel(), odataBatchHandler);
            config.Select().Expand().Filter().OrderBy().MaxTop(null).Count();
            //

            // config.Filters.Add(new BasicAuthenticationAttribute()); 
            // WebAPI when dealing with JSON & JavaScript!
            // Setup json serialization to serialize classes to camel (std. Json format)
            //var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            //formatter.SerializerSettings.ContractResolver =
            //    new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            // remove default Xml handler
            var matches = config.Formatters
                                .Where(f => f.SupportedMediaTypes
                                             .Where(m => m.MediaType.ToString() == "application/xml" ||
                                                         m.MediaType.ToString() == "text/xml")
                                             .Count() > 0)
                                .ToList();
            foreach (var match in matches)
                config.Formatters.Remove(match);

            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

            //xml circular fix
            //var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            //var voyagedcs = new DataContractSerializer(typeof(VoyageDetails), null, int.MaxValue,
            //    false, /* preserveObjectReferences: */ true, null);
            //xml.SetSerializer<VoyageDetails>(voyagedcs);

            //var boldcs = new DataContractSerializer(typeof(BOLDetails), null, int.MaxValue,
            //    false, /* preserveObjectReferences: */ true, null);
            //xml.SetSerializer<BOLDetails>(boldcs);

            //var condcs = new DataContractSerializer(typeof(ConsignmentDetails), null, int.MaxValue,
            //  false, /* preserveObjectReferences: */ true, null);
            //xml.SetSerializer<ConsignmentDetails>(condcs);

            //var vehdcs = new DataContractSerializer(typeof(VehicleDetails), null, int.MaxValue,
            //  false, /* preserveObjectReferences: */ true, null);
            //xml.SetSerializer<VehicleDetails>(vehdcs);

            //var ctrdcs = new DataContractSerializer(typeof(ContainerDetails), null, int.MaxValue,
            //   false, /* preserveObjectReferences: */ true, null);
            //xml.SetSerializer<ContainerDetails>(ctrdcs);
        }
        public static IEdmModel GetModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.ContainerName = "ODataContext";
            builder.EntitySet<CountryCodes>("CountryCodes");
            builder.EntitySet<LocationCodes>("LocationCodes");
            builder.EntitySet<UNHSCodes>("UNHSCodes");
            builder.EntitySet<PackageCodes>("PackageCodes");
            builder.EntitySet<VoyageDetails>("VoyageDetails");
            builder.EntitySet<BOLDetails>("BOLDetails");
            builder.EntitySet<ContainerDetails>("ContainerDetails");
            builder.EntitySet<ConsignmentDetails>("ConsignmentDetails");
            builder.EntitySet<VehicleDetails>("VehicleDetails");
            builder.EntitySet<Lines>("Lines");
            builder.EntitySet<ContainerIsoCodes>("ContainerIsoCodes");
            builder.EntitySet<UNHazardousCodes>("UNHazardousCodes");
            builder.EntityType<LocationCodes>().Property(p => p.FullCode).IsOptional();
            return builder.GetEdmModel();
        }
    }
}