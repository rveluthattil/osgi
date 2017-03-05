using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MvcOSGi.Shell;
using MvcOSGi.Shell.Extension;
using MvcOSGi.Shell.Models;
using UIShell.Extension;
using UIShell.OSGi;
using UIShell.OSGi.Core.Service;
using UIShell.OSGi.MvcWebExtension;

namespace MvcOSGi.Shell
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : BundleRuntimeMvcApplication
    {
        public ApplicationViewModel ViewModel { get; set; }
        private ExtensionHooker _extensionHooker;

        protected override void Application_Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);

            MonitorExtension();

            //Register the default controller provider source.
            DefaultControllerConfig.Register(typeof(MvcApplication).Assembly);
        }

        public override void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        public override void RegisterWebApiConfig()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }

        public override void RegisterRoutes(RouteCollection routes)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public override void RegisterResourceBundles()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void MonitorExtension()
        {
            ViewModel = new ApplicationViewModel();

            //Register pages in Shell project.
            ViewModel.MainMenuItems.Add(new MenuItem
            {
                Text = "Home",
                URL = "/"
            });

            BundleRuntime.Instance.AddService<ApplicationViewModel>(ViewModel);
            _extensionHooker = new ExtensionHooker(BundleRuntime.Instance.GetFirstOrDefaultService<IExtensionManager>());
            _extensionHooker.HookExtension("MainMenu", new MainMenuExtensionHandler(ViewModel));
            _extensionHooker.HookExtension("SidebarMenu", new SidebarExtensionHandler(ViewModel));
        }
    }
}