using System.Windows.Forms;
using Jbe.CABExtension.SmartPartInfos;
using UIShell.OSGi;

namespace DockShell
{
    /// <summary>
    /// Extends the DockPanelWorkspace class with the support for hosting of WPF controls.
    /// </summary>
    public class WPFDockPanelWorkspace : DockPanelWorkspace
    {
        private ElementHostWorkspaceComposerAdapter<Control, DockPanelSmartPartInfo> composer;

        public WPFDockPanelWorkspace()
        {
            composer.WPFUIElementAdapter = BundleRuntime.Instance.GetFirstOrDefaultService<IWPFUIElementAdapter>();
        }

        //[ServiceDependency]
        public IWPFUIElementAdapter WPFUIElementAdapter
        {
            set { composer.WPFUIElementAdapter = value; }
        }

        protected override IWorkspaceComposer<Control> CreateWorkspaceComposer()
        {
            composer = new ElementHostWorkspaceComposerAdapter<Control, DockPanelSmartPartInfo>(this);
            return composer;
        }
    }
}