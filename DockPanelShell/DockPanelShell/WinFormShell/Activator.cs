using System.Windows.Forms;
using DockShell;
using UIShell.OSGi;
using WinFormShell.Properties;

namespace WinFormShell
{
    public class Activator : IBundleActivator
    {
        #region IBundleActivator 成员

        public void Start(IBundleContext context)
        {
            var splashForm = new SplashForm(Resources.Splashscreen);
            splashForm.Show();

            context.AddService(typeof (IWPFUIElementAdapter), new WPFUIElementAdapter());

            var layoutView = new ShellLayoutView();
            context.AddService<IWorkspace>(layoutView.Workspace);
            context.AddService(layoutView.CreateShellLayoutViewProxy());


            var form = new ShellForm();
            context.AddService<Form>(form);
            form.ShowLayoutIvew(layoutView);

            form.Activated += (sender, e) => splashForm.Close();
        }

        public void Stop(IBundleContext context)
        {
        }

        #endregion
    }
}