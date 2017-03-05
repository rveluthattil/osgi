using System;
using System.IO;
using System.Reflection;
using UIShell.OSGi;
using log4net.Config;

namespace LogViewer
{
    public class Activator : IBundleActivator
    {
        public static string configFile = "Log.config";

        #region IBundleActivator Members

        public void Start(IBundleContext context)
        {
            XmlConfigurator.Configure(
                new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), configFile),
                        UriKind.Absolute));
        }

        public void Stop(IBundleContext context)
        {
        }

        #endregion
    }
}