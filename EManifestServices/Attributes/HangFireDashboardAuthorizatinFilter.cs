using EManifestServices.Models;
using Hangfire.Dashboard;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Attributes
{
    public class HangFireDashboardAuthorizatinFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // In case you need an OWIN context, use the next line, `OwinContext` class
            // is the part of the `Microsoft.Owin` package.
            var owinContext = new OwinContext(context.GetOwinEnvironment());
            var httpContext = owinContext.Environment["System.Web.HttpContextBase"] as HttpContextBase;
            var user = httpContext?.Session?["User"] as UserModel;
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext != null && user != null && user.UserType == 1;
        }

    }
}