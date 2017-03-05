using System;
using System.Windows.Forms;
using UIShell.OSGi;

namespace UIShell.Document
{
    public class ActiveControlMonitor
    {
        public void Start()
        {
            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            object focusedControl = UIUtility.FindFocusedControl();
            if (focusedControl == null)
                return;

            var editManager = BundleRuntime.Instance.GetFirstOrDefaultService<IEditManager>();
            if (editManager != null)
            {
                editManager.RegisterActiveElement(focusedControl);
            }
        }

        public void Stop()
        {
            Application.Idle -= Application_Idle;
        }
    }
}