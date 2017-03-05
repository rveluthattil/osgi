using UIShell.MessageBusService.Impl;
using UIShell.OSGi;

namespace UIShell.MessageBusService
{
    public class Activator : IBundleActivator
    {
        #region IBundleActivator Members

        public void Start(IBundleContext context)
        {
            context.AddService<IMessageBusService>(new InProcMessageBus());
        }

        public void Stop(IBundleContext context)
        {
        }

        #endregion
    }
}