using AutoMapper;
using EmanifestParser;
using EManifestServices.App_Start;
using EManifestServices.DAL;
using EManifestServices.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EManifestServices
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            Telerik.Reporting.Services.WebApi.ReportsControllerConfiguration.RegisterRoutes(System.Web.Http.GlobalConfiguration.Configuration);
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //auto mapper 
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<VoyageModel, VoyageDetails>();
                cfg.CreateMap<VoyageDetails, VoyageModel>();
                cfg.CreateMap<StatusModel, Status>();
                cfg.CreateMap<Status, StatusModel>();
                cfg.CreateMap<Notifications, NotificationModel>();
                cfg.CreateMap<Users, UserModel>();
                cfg.CreateMap<Vessels, VesselModel>();
                cfg.CreateMap<VesselModel, Vessels>();
                cfg.CreateMap<ApiClients, ApiClientModel>();
            });
        }
    }
}
