using System.Windows.Forms;
using Jbe.CABExtension.SmartPartInfos;
using LogViewer.Demo.Views;
using UIShell.PresentationCore;

namespace LogViewer.Demo
{
    public class CreateLogDemo : AbstractViewProvider
    {
        public CreateLogDemo() : base(true)
        {
        }

        //#region ICommand Members

        //public void Run()
        //{
        //    var workSpace = BundleRuntime.Instance.ServiceManager.GetFirstOrDefaultService<IWorkspace>();
        //    IconSmartPartInfo dpInfo = new IconSmartPartInfo("Log Demo", "Log Demo");
        //    dpInfo.Icon = Resources.ReportEdit;
        //    LogDemo logDemo = new LogDemo();
        //    workSpace.Show(logDemo, dpInfo);
        //}

        //#endregion

        protected override Control CreateControl()
        {
            return new LogDemo();
        }

        protected override object CreateViewInfo()
        {
            var dpInfo = new IconSmartPartInfo("Log Demo", "Log Demo");
            dpInfo.Icon = Resources.ReportEdit;
            return dpInfo;
        }
    }
}