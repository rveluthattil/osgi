using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WorkspaceShell
{
    internal partial class DeckWorkspace : UserControl
    {
        public DeckWorkspace()
        {
            InitializeComponent();
        }
        Dictionary<Control, object> _controls = new Dictionary<Control, object>();
        /// <summary>
        /// 现实或激活一个control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="controlInfo">更加ControlInfo的设置，定制现实的方式，如Dock、Anchor等</param>
        public void Show(Control control,object controlInfo)
        {
            if (_controls.ContainsKey(control))
            {
                control.BringToFront();
            }
            else
            {
                this.Controls.Add(control);
                control.Disposed += new EventHandler(control_Disposed);
                if (controlInfo != null)
                {
                    //更加ControlInfo的设置，定制现实的方式，如Dock、Anchor等
                }
                else
                {
                    control.Dock = DockStyle.Fill;
                }
            }
            this._controls[control] = controlInfo;
        }

        void control_Disposed(object sender, EventArgs e)
        {
            var ctrl = sender as Control;
            if (ctrl != null)
            {
                _controls.Remove(ctrl);
                this.Controls.Remove(ctrl);
            }
        }

        
    }
}
