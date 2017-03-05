using System;
using Autofac;
using UIShell.OSGi;
using UIShell.OSGi.MvcWebExtension;
using UIShell.OSGi.Utility;

namespace UIShell.IoCPlugin
{
    class ControllerResolver : IControllerResolver
    {
        public object Resolve(Type type)
        {
            //Try to resolve controller by Autofac, which support non-parameter constructor.
            var container = BundleRuntime.Instance.GetFirstOrDefaultService<IContainer>();
            if (container != null)
            {
                try
                {
                    return container.Resolve(type);
                }
                catch (Exception)
                {
                    FileLogUtility.Warn(string.Format("IOC conatiner can't resolve controller type {0}.", type));
                }
            }
            
            return null;
        }


        public TController Inject<TController>(TController instance)
        { //Try to resolve controller by Autofac, which support non-parameter constructor.
            var container = BundleRuntime.Instance.GetFirstOrDefaultService<IContainer>();
            if (container != null)
            {
                try
                {
                    return container.InjectProperties(instance);//.Resolve(type);
                }
                catch (Exception ex)
                {
                    FileLogUtility.Warn(string.Format("IOC conatiner can't inject controller type {0}.", instance));
                    FileLogUtility.Warn(ex);
                }
            }

            return instance;
        }
    }
}
