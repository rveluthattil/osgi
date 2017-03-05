using System;
using WeifenLuo.WinFormsUI.Docking;

namespace Jbe.CABExtension.SmartPartInfos
{
    public class DockStateShowStrategy : IShowStrategy
    {
        public DockStateShowStrategy(DockState dockState)
        {
            this.DockState = dockState;
        }

        public DockState DockState { get; set; }

        #region IShowStrategy Members

        void IShowStrategy.Show(IDockPanelWorkspace workspace, IDockContent content)
        {
            // Mapping of the enum
            var value = (int) DockState;
            var dockState =
                (WeifenLuo.WinFormsUI.Docking.DockState) Enum.ToObject(
                    typeof (WeifenLuo.WinFormsUI.Docking.DockState), value);

            content.DockHandler.Show(workspace.DockPanel, dockState);
        }

        #endregion
    }

    public enum DockState
    {
        Unknown = 0,
        Float = 1,
        DockTopAutoHide = 2,
        DockLeftAutoHide = 3,
        DockBottomAutoHide = 4,
        DockRightAutoHide = 5,
        Document = 6,
        DockTop = 7,
        DockLeft = 8,
        DockBottom = 9,
        DockRight = 10,
        Hidden = 11
    }
}