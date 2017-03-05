using WorkspaceShell;
using UIShell.OSGi;
using System.Windows.Forms;

namespace Monitor
{
    public class MonitorCommand : IViewCommand
    {
        protected UserControl1 _view = new UserControl1();

        #region IViewCommand 成员

        public void Run()
        {
            IWorkspace workspace = BundleRuntime.Instance.GetFirstOrDefaultService<IWorkspace>();
            workspace.Show(_view, null);
        }

        #endregion
    }
    public class SmsServer : IViewCommand
    {
        protected UserControl2 _view = new UserControl2();

        #region IViewCommand 成员

        public void Run()
        {
            var shellForm = BundleRuntime.Instance.GetFirstOrDefaultService<IWorkspace>();
            shellForm.Show(_view,null);
        }

        #endregion
    }

}
