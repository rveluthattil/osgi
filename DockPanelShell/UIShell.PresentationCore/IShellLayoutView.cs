using System.Windows.Forms;

namespace UIShell.PresentationCore
{
    public interface IShellLayoutView
    {
        void AddMenuItem(ToolStripItem control);
        void AddToolStrip(ToolStrip control);
        void RemoveMenuItem(ToolStripItem control);
        void RemoveToolStrip(ToolStrip control);
        void SetStatusLabel(string text);

        /// <summary>
        /// Shows SmartPart using the given SmartPartInfo
        /// </summary>
        /// <param name="smartPart">Smart part to show.</param>
        /// <param name="smartPartInfo"></param>
        void Show(object smartPart, object smartPartInfo);

        /// <summary>
        /// Shows a smart part in the UI.
        /// </summary>
        /// <param name="smartPart">Smart part to show.</param>
        void Show(object smartPart);

        void Invoke(Action action);
    }
}