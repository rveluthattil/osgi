using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChartBuildManager.Services;
using UIShell.OSGi;
using UIShell.OSGi.Utility;
using UIShell.PresentationCore;
//using DockShell;
//using Microsoft.Practices.ObjectBuilder;
//using Jbe.TestSuite.Infrastructure.Interface;
//using Jbe.TestSuite.TestDevice.Manager.Constants;
//using Jbe.CABExtension.WinForms.UIElements;
//using Jbe.TestSuite.TestDevice.Manager.Interface.Services;
//using Jbe.TestSuite.TestDevice.Manager.Mapper;
//using Jbe.CABExtension.WinForms;
//using Microsoft.Practices.CompositeUI.Commands;

namespace ChartBuildManager.Views
{
    public partial class TestDevicesView : UserControl
    {
        private readonly ICommandBusService service;

        public TestDevicesView()
        {
            service = BundleRuntime.Instance.GetFirstOrDefaultService<ICommandBusService>();
            //var messageBusService = BundleRuntime.Instance.ServiceManager.GetFirstOrDefaultService<IMessageBusService>();
            //messageBusService.Subscribe<ControlViewsContact>(ControlViewsChanged);
            service.SubscribeCommandEvent(Constants.ControlViewsChanged, ControlViewsChanged);
            InitializeComponent();

            openToolStripButton.Click += openToolStripButton_Click;
            openToolStripMenuItem.Click += openToolStripButton_Click;
            removeToolStripButton.Click += removeToolStripButton_Click;
            removeToolStripMenuItem.Click += removeToolStripButton_Click;

            var controlManager = BundleRuntime.Instance.GetFirstOrDefaultService<IControlViewManager>();
            foreach (var item in controlManager.Factories)
            {
                var factroy = new ToolStripMenuItem(item.Key);
                factroy.Click += (sender, e) => controlManager.Register(item.Value.Create());
                newDeviceToolStripDropDownButton.DropDownItems.Add(factroy);
            }
            UpdateControlViews(controlManager.ControlViews);
        }

        public event EventHandler<EventArgs<List<IControlView>>> SelectedControlViewsChanged;

        protected virtual void OnSelectedControlViewsChanged(List<IControlView> devices)
        {
            if (SelectedControlViewsChanged != null)
            {
                SelectedControlViewsChanged(this, new EventArgs<List<IControlView>>(devices));
            }
        }

        private void removeToolStripButton_Click(object sender, EventArgs e)
        {
            service.PublicCommand(Constants.CloseDevice);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            service.PublicCommand(Constants.ShowDevice);
        }

        private void ControlViewsChanged(object sender, object e)
        {
            var layoutview = BundleRuntime.Instance.GetFirstOrDefaultService<IShellLayoutView>();
            var controlManager = BundleRuntime.Instance.GetFirstOrDefaultService<IControlViewManager>();

            layoutview.Invoke(() => UpdateControlViews(controlManager.ControlViews));
        }

        public void UpdateControlViews(IEnumerable<IControlView> controlViews)
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            listView.Groups.Clear();
            foreach (IControlView controlView in controlViews)
            {
                ListViewItem item = ControlViewMapper.ToListViewItem(controlView);

                if (item.Group != null)
                {
                    ListViewGroup existingGroup = null;
                    foreach (ListViewGroup group in listView.Groups)
                    {
                        if (group.Header == item.Group.Header)
                        {
                            existingGroup = group;
                        }
                    }

                    if (existingGroup != null)
                    {
                        item.Group = existingGroup;
                    }
                    else
                    {
                        listView.Groups.Add(item.Group);
                    }
                }
                listView.Items.Add(item);
            }
            listView.EndUpdate();

            ListViewSelectedIndexChanged();
        }


        private void ListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectedIndexChanged();
        }

        private void ListViewSelectedIndexChanged()
        {
            UpdateUI();

            var controlViews = new List<IControlView>(listView.SelectedItems.Count);
            foreach (ListViewItem item in listView.SelectedItems)
            {
                controlViews.Add(ControlViewMapper.FromListViewItem(item));
            }
            UpdateSelectedControlViews(controlViews);
        }

        public void UpdateSelectedControlViews(List<IControlView> devices)
        {
            var controlManager = BundleRuntime.Instance.GetFirstOrDefaultService<IControlViewManager>();
            controlManager.SelectedControlViews = devices;
        }

        private void ListViewDoubleClick(object sender, EventArgs e)
        {
            service.PublicCommand(Constants.ShowDevice);
        }

        private void UpdateUI()
        {
            bool enabled = (listView.SelectedItems.Count > 0);

            service.PublicCommandStatus(Constants.ShowDevice, enabled ? CommandStatus.Enabled : CommandStatus.Disabled);
            service.PublicCommandStatus(Constants.CloseDevice, enabled ? CommandStatus.Enabled : CommandStatus.Disabled);
        }
    }
}