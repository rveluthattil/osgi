using System;
using System.Collections.Generic;
using UIShell.OSGi;
using UIShell.OSGi.Utility;
using UIShell.PresentationCore;

namespace ChartBuildManager.Services.Impl
{
    internal class ControlViewManager : IControlViewManager
    {
        //[EventPublication(EventTopicNames.ControlViewsChanged, PublicationScope.WorkItem)]
        private readonly List<IControlView> controlViews;

        private readonly ICommandBusService service;

        public ControlViewManager()
        {
            Factories = new Dictionary<string, IControlViewFactory>();
            controlViews = new List<IControlView>();
            SelectedControlViews = new List<IControlView>();
            service = BundleRuntime.Instance.GetFirstOrDefaultService<ICommandBusService>();
            ControlViewsChanged += ControlViewManager_ControlViewsChanged;
            var layoutView = BundleRuntime.Instance.GetFirstOrDefaultService<IShellLayoutView>();
            service.SubscribeCommandEvent(Constants.ShowDevice,
                                          (sender, e) => layoutView.Invoke(() => OpenDevice(sender, e)));
            service.SubscribeCommandEvent(Constants.CloseDevice,
                                          (sender, e) => layoutView.Invoke(() => RemoveDevice(sender, e)));
        }

        #region IControlViewManager Members

        public Dictionary<string, IControlViewFactory> Factories { get; private set; }

        public IList<IControlView> ControlViews
        {
            get { return new List<IControlView>(controlViews).AsReadOnly(); }
        }

        public IList<IControlView> SelectedControlViews { get; set; }

        public void Register(IControlViewFactory factory)
        {
            Factories.Add(factory.ProfileName, factory);
            Register(factory.Create());
        }

        public void Register(IControlView controlView)
        {
            controlViews.Add(controlView);
            OnControlViewsChanged(controlViews);
        }

        #endregion

        public event EventHandler<EventArgs<List<IControlView>>> ControlViewsChanged;

        private void ControlViewManager_ControlViewsChanged(object sender, EventArgs<List<IControlView>> e)
        {
            //var service = BundleRuntime.Instance.ServiceManager.GetFirstOrDefaultService<IMessageBusService>();
            //service.Publish<ControlViewsContact>(new ControlViewsContact() { ControlViews = e.Item.ToArray() });
            service.PublicCommand(Constants.ControlViewsChanged);
        }


        protected virtual void OnControlViewsChanged(List<IControlView> devices)
        {
            if (ControlViewsChanged != null)
            {
                ControlViewsChanged(this, new EventArgs<List<IControlView>>(devices));
            }
        }

        //[CommandHandler(CommandNames.ShowDevice)]
        public void OpenDevice(object sender, object e)
        {
            foreach (IControlView controlView in SelectedControlViews)
            {
                controlView.Show();
            }
        }

        //[CommandHandler(CommandNames.CloseDevice)]
        public void RemoveDevice(object sender, object e)
        {
            foreach (IControlView controlView in SelectedControlViews)
            {
                controlView.Close();
            }
        }
    }
}