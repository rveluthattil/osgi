using System.Windows.Forms;
using ChartBuildManager.Services;

namespace ChartBuildManager
{
    public static class ControlViewMapper
    {
        public static ListViewItem ToListViewItem(IControlView device)
        {
            return new DeviceListViewItem(device);
        }

        public static IControlView FromListViewItem(ListViewItem listViewItem)
        {
            var deviceListViewItem = (DeviceListViewItem) listViewItem;
            return deviceListViewItem.Device;
        }

        #region Nested type: DeviceListViewItem

        private class DeviceListViewItem : ListViewItem
        {
            private readonly IControlView device;

            public DeviceListViewItem(IControlView device)
            {
                this.device = device;
                Name = device.Name;
                Text = device.Name;
                Group = new ListViewGroup("ChartBuilder");

                SubItems.Add(new ListViewSubItem(this, "     " + "WPF Toolkit"));
                SubItems.Add(new ListViewSubItem(this, "     " + "Microsoft"));
            }

            public IControlView Device
            {
                get { return device; }
            }
        }

        #endregion
    }
}