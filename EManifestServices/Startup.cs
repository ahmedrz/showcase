using Owin;
using Microsoft.Owin;
using Hangfire;
using EManifestServices.Attributes;

[assembly: OwinStartup(typeof(EManifestServices.Startup))]
namespace EManifestServices
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("EmanifestContext");
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireDashboardAuthorizatinFilter() }
            });
            var options = new BackgroundJobServerOptions { WorkerCount = 1 };
            app.UseHangfireServer(options);

        }
    }
}