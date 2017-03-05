using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace UIShell.PresentationCore.Utility
{
    class ControlUtility
    {
        public static void Invoke(Control ctrl, Action action)
        {
            if (ctrl.IsHandleCreated)
            {
                ctrl.Invoke((MethodInvoker)(()=>action()));
            }
            else
            {
                action();
            }
        }
    }
}
