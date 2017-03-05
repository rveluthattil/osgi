using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace UIShell.Document
{
    internal partial class UIUtility
    {
        public static object FindFocusedControl()
        {
            //var container = root as ContainerControl;
            //while (container != null)
            //{
            //    root = container.ActiveControl;
            //    container = root as ContainerControl;
            //}
            Control root = GetFocusedControl();
            var wpfRoot = root as ElementHost;
            if (wpfRoot != null)
            {
                object result = FindWpfFocusedControl(wpfRoot.Child);
                if (result != null)
                {
                    return result;
                }
            }
            return root;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr GetFocus();

        private static Control GetFocusedControl()
        {
            Control focusedControl = null;
            // To get hold of the focused control:
            IntPtr focusedHandle = GetFocus();
            if (focusedHandle != IntPtr.Zero)
                // Note that if the focused Control is not a .Net control, then this will return null.
                focusedControl = Control.FromHandle(focusedHandle);
            return focusedControl;
        }
    }
}