using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcOSGi.Shell
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Root",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Plugin",
                url: "{plugin}/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );
        }
    }
}