using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using BusinessLayer;
using UIShell.OSGi;

namespace MediaManagement
{
    public class Activator:IBundleActivator
    {
        public void Start(IBundleContext context)
        {
            var builder = context.GetFirstOrDefaultService<ContainerBuilder>();
            builder.RegisterType<MovieManager>().AsImplementedInterfaces();
        }

        public void Stop(IBundleContext context)
        {
        }
    }
}
