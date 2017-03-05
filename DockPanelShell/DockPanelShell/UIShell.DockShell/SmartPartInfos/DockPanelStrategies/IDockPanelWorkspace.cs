using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jbe.CABExtension.SmartPartInfos
{
    public interface IDockPanelWorkspace
    {
        /// <summary>
        /// The DockPanel used by the Workspace
        /// </summary>
        DockPanel DockPanel { get; }

        /// <summary>
        /// Get the IDockContent object that holds the smart part.
        /// </summary>
        IDockContent GetDockContent(Control smartPart);
    }
}