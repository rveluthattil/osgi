using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BlogPlugin
{
    public class BlogAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Blog"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BlogPlugin",
                "Blog/{controller}/{action}/{id}",
                new { controller = "Blog", action = "Index", id = UrlParameter.Optional },
                new[] { "BlogPlugin.Controllers" }
            );
        }
    }
}