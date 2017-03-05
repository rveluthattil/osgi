using ChartBuildManager.Properties;
using ChartBuildManager.Views;
using DockShell;
using Jbe.CABExtension.SmartPartInfos;
using UIShell.OSGi;
using UIShell.PresentationCore;

namespace ChartBuildManager
{
    public class ShowChartBuilderManager : ICommand
    {
        private DockPanelSmartPartInfo smartPartInfo;
        private TestDevicesView view;

        #region ICommand Members

        public void Run()
        {
            var workSpace = BundleRuntime.Instance.GetFirstOrDefaultService<IWorkspace>();
            if (view == null)
            {
                smartPartInfo = new DockPanelSmartPartInfo("Test Charts", "Test Charts");
                smartPartInfo.Icon = Resources.DeviceManager;
                smartPartInfo.DockingType = DockingType.TaskView;
                smartPartInfo.ShowStrategy = new DockStateShowStrategy(DockState.DockLeft);
                view = new TestDevicesView();
                workSpace.SmartPartClosing += SmartPartClosing;

                workSpace.Show(view, smartPartInfo);
            }

            workSpace.Activate(view);
        }

        #endregion

        private void SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (e.SmartPart == view)
            {
                var workSpace = BundleRuntime.Instance.GetFirstOrDefaultService<IWorkspace>();
                // Do not close the window, only hide it.
                workSpace.Hide(view);
                e.Cancel = true;
            }
        }
    }
}