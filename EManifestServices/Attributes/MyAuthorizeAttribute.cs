using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EManifestServices.DAL;
using EManifestServices.Models;

namespace EManifestServices.Attributes
{
    [Flags]
    public enum UserTypes
    {
        Admin = 1,
        AgentUser = 2,
        PortManagementUser = 3
    }
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        public UserTypes[] UserTypes { get; set; }
        public MyAuthorizeAttribute(params UserTypes[] userTypes)
        {
            UserTypes = userTypes;
        }
        //public bool IsAdmin { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string url = filterContext.HttpContext.Request.RawUrl;
            string action = filterContext.ActionDescriptor.ActionName;
            string controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (filterContext.HttpContext.Session["User"] == null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest()
                    || action.StartsWith("Async"))
                {
                    var urlHelper = new UrlHelper(filterContext.RequestContext);
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            Error = "NotAuthorized",
                            LogOnUrl = urlHelper.Action("Login", "Account")
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    var routeValues = new RouteValueDictionary();
                    routeValues["controller"] = "Account";
                    routeValues["action"] = "Login";
                    if (controller != "" && action != "")
                    {
                        routeValues["url"] = url;
                    }
                    //Other route values if needed.
                    filterContext.Controller.TempData["Error"] = "Please login to access this resource.";
                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
            }
            else
            {
                var sessionUser = (UserModel)filterContext.HttpContext.Session["User"];
                if (this.UserTypes.Any() && !UserTypes.ToList().Contains((UserTypes)sessionUser.UserType))
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest()
                        || action.StartsWith("Async"))
                    {
                        var urlHelper = new UrlHelper(filterContext.RequestContext);
                        filterContext.HttpContext.Response.StatusCode = 403;
                        filterContext.Result = new JsonResult
                        {
                            Data = new
                            {
                                Error = "NotAuthorized",
                                LogOnUrl = urlHelper.Action("Login", "Account")
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        var routeValues = new RouteValueDictionary();
                        filterContext.Controller.TempData["Error"] = "You are not authorized to access this resource.";
                        routeValues["controller"] = "Home";
                        routeValues["action"] = "Index";
                        filterContext.Controller.TempData["Error"] = "You don't have permission to access this resource.";
                        filterContext.Result = new RedirectToRouteResult(routeValues);
                    }
                }
                //if (this.IsAdmin && sessionUser.IsAdmin != true)
                //{
                //    if (filterContext.HttpContext.Request.IsAjaxRequest()
                //        || action.StartsWith("Async"))
                //    {
                //        var urlHelper = new UrlHelper(filterContext.RequestContext);
                //        filterContext.HttpContext.Response.StatusCode = 403;
                //        filterContext.Result = new JsonResult
                //        {
                //            Data = new
                //            {
                //                Error = "NotAuthorized",
                //                LogOnUrl = urlHelper.Action("Login", "Account")
                //            },
                //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                //        };
                //    }
                //    else
                //    {
                //        var routeValues = new RouteValueDictionary();
                //        filterContext.Controller.TempData["Error"] = "You are not authorized to access this resource.";
                //        routeValues["controller"] = "Home";
                //        routeValues["action"] = "Index";
                //        filterContext.Controller.TempData["Error"] = "You don't have permission to access this resource.";
                //        filterContext.Result = new RedirectToRouteResult(routeValues);
                //    }
                //}
            }
        }
    }
}