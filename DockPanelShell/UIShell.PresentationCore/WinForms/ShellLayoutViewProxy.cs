using System.Windows.Forms;
using UIShell.OSGi.Utility;

namespace UIShell.PresentationCore.WinForms
{
    public class ShellLayoutViewProxy : IShellLayoutView
    {
        private readonly Control _owner;
        private readonly Action<object, object> _showSmartPart;

        public ShellLayoutViewProxy(Control owner, Action<object, object> showSmartPart)
        {
            AssertUtility.ArgumentNotNull(owner, "owner");
            _owner = owner;
            _showSmartPart = showSmartPart;
        }

        #region IShellLayoutView 成员

        public void RemoveMenuItem(ToolStripItem control)
        {
            Invoke(() => MainMenuStrip.Items.Remove(control));
        }

        public void RemoveToolStrip(ToolStrip control)
        {
            Invoke(() =>
                       {
                           if (control.Parent != null)
                           {
                               control.Parent.Controls.Remove(control);
                           }
                       });
        }
        public void AddMenuItem(ToolStripItem control)
        {
            Invoke(() => MainMenuStrip.Items.Add(control));
        }

        public void AddToolStrip(ToolStrip control)
        {
            //The first row is reserved for main menu.
            Invoke(() => ToolStripContainer.TopToolStripPanel.Join(control, 1));
        }

        public void SetStatusLabel(string text)
        {
            Invoke(() => StatusLabel.Text = text);
        }

        public void Invoke(Action action)
        {
            if (_owner.InvokeRequired)
            {
                _owner.BeginInvoke(new MethodInvoker(action));
            }
            else
            {
                action();
            }
        }

        #endregion

        #region IShellLayoutView 成员

        public void Show(object smartPart, object smartPartInfo)
        {
            Invoke(() => _showSmartPart(smartPart, smartPartInfo));
        }

        public void Show(object smartPart)
        {
            Show(smartPart, null);
        }

        #endregion

        public ToolStripContainer ToolStripContainer { get; set; }
        public StatusStrip MainStatusStrip { get; set; }
        public MenuStrip MainMenuStrip { get; set; }
        public ToolStripStatusLabel StatusLabel { get; set; }
    }
}