using WeifenLuo.WinFormsUI.Docking;

namespace Jbe.CABExtension.SmartPartInfos
{
    public interface IShowStrategy
    {
        /// <summary>
        /// Shows the IDockContent inside the IDockPanelWorkspace.
        /// This method is only for internal use!
        /// </summary>
        void Show(IDockPanelWorkspace workspace, IDockContent content);
    }
}