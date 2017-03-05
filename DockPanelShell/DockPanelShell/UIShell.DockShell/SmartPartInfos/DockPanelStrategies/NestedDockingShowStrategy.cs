using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Jbe.CABExtension.SmartPartInfos
{
    public class NestedDockingShowStrategy : IShowStrategy
    {
        public NestedDockingShowStrategy(Control neighbourSmartPart, DockAlignment dockAlignment, double proportion)
        {
            this.NeighbourSmartPart = neighbourSmartPart;
            this.DockAlignment = dockAlignment;
            this.Proportion = proportion;
        }

        public Control NeighbourSmartPart { get; set; }

        public DockAlignment DockAlignment { get; set; }

        public double Proportion { get; set; }

        #region IShowStrategy Members

        void IShowStrategy.Show(IDockPanelWorkspace workspace, IDockContent content)
        {
            DockPane pane = workspace.GetDockContent(NeighbourSmartPart).DockHandler.Pane;
            content.DockHandler.Show(pane, DockAlignment, Proportion);
        }

        #endregion
    }
}