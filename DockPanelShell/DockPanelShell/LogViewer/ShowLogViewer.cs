using System.Windows.Forms;
using Jbe.CABExtension.SmartPartInfos;
using LogViewer.Properties;
using LogViewer.Views;
using UIShell.PresentationCore;

namespace LogViewer
{
    //public class ShowLogViewer : ICommand
    //{
    //    private LogView logView;
    //    #region ICommand Members

    //    public void Run()
    //    {
    //        var workSpace = BundleRuntime.Instance.ServiceManager.GetFirstOrDefaultService<IWorkspace>();
    //        if (logView == null)
    //        {
    //            DockPanelSmartPartInfo dpInfo = new DockPanelSmartPartInfo("Log Viewer", "Log Viewer");
    //            dpInfo.Icon = Resources.EventLog;
    //            dpInfo.DockingType = DockingType.TaskView;
    //            dpInfo.ShowStrategy = new DockStateShowStrategy(DockState.DockBottom);

    //            logView = new LogView();
    //            logView.Disposed += new EventHandler(LogViewDisposed);
    //            workSpace.Show(logView, dpInfo);
    //        }

    //        workSpace.Activate(logView);
    //    }

    //    #endregion

    //    private void LogViewDisposed(object sender, EventArgs e)
    //    {
    //        if (BundleRuntime.Instance == null || BundleRuntime.Instance.ServiceManager == null)
    //            return;
    //        var workSpace = BundleRuntime.Instance.ServiceManager.GetFirstOrDefaultService<IWorkspace>();
    //        if(workSpace != null)
    //        {
    //            workSpace.Close(logView);
    //        }
    //        logView = null;
    //    }
    //}

    public class LogViewProvider : AbstractViewProvider
    {
        public LogViewProvider()
            : base(true)
        {
        }

        protected override Control CreateControl()
        {
            return new LogView();
        }

        protected override object CreateViewInfo()
        {
            var dpInfo = new DockPanelSmartPartInfo("Log Viewer", "Log Viewer");
            dpInfo.Icon = Resources.EventLog;
            dpInfo.DockingType = DockingType.TaskView;
            dpInfo.ShowStrategy = new DockStateShowStrategy(DockState.DockBottom);
            return dpInfo;
        }
    }
}