using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace Jbe.CABExtension.SmartPartInfos
{
    public class FloatingShowStrategy : IShowStrategy
    {
        public FloatingShowStrategy(Rectangle floatWindowBounds)
        {
            this.FloatWindowBounds = floatWindowBounds;
        }

        public Rectangle FloatWindowBounds { get; set; }

        #region IShowStrategy Members

        void IShowStrategy.Show(IDockPanelWorkspace workspace, IDockContent content)
        {
            content.DockHandler.Show(workspace.DockPanel, FloatWindowBounds);
        }

        #endregion
    }
}