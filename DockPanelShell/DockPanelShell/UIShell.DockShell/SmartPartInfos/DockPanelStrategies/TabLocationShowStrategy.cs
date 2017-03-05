using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jbe.CABExtension.SmartPartInfos
{
    public class TabLocationShowStrategy : IShowStrategy
    {
        public TabLocationShowStrategy(Control beforeSmartPart)
        {
            this.BeforeSmartPart = beforeSmartPart;
        }

        public Control BeforeSmartPart { get; set; }

        #region IShowStrategy Members

        void IShowStrategy.Show(IDockPanelWorkspace workspace, IDockContent content)
        {
            IDockContent beforeContent = workspace.GetDockContent(BeforeSmartPart);
            content.DockHandler.Show(beforeContent.DockHandler.Pane, beforeContent);
        }

        #endregion
    }
}