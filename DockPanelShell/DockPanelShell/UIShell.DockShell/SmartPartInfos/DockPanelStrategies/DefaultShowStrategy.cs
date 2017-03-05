using WeifenLuo.WinFormsUI.Docking;

namespace Jbe.CABExtension.SmartPartInfos
{
    public class DefaultShowStrategy : IShowStrategy
    {
        #region IShowStrategy Members

        void IShowStrategy.Show(IDockPanelWorkspace workspace, IDockContent content)
        {
            content.DockHandler.Show(workspace.DockPanel);
        }

        #endregion
    }
}