using System.Windows.Forms;

namespace UIShell.PresentationCore
{
    public interface IViewProvider
    {
        Control View { get; }

        /// <summary>
        /// 指定如何现实一个View，如Dock,Anchor
        /// </summary>
        object ViewInfo { get; }
    }
}