using ChartBuildManager.Services;
using ChartBuildManager.Services.Impl;
using ChartBuildManager.Views;
using UIShell.OSGi;

namespace ChartBuildManager
{
    public class Activator : IBundleActivator
    {
        private IControlViewManager _controlViewManager;

        #region IBundleActivator Members

        public void Start(IBundleContext context)
        {
            _controlViewManager = new ControlViewManager();
            context.AddService(_controlViewManager);
            _controlViewManager.Register(new ChartBuilderFactory());
        }

        public void Stop(IBundleContext context)
        {
        }

        #endregion
    }
}