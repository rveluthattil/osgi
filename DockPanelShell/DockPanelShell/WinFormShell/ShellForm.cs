using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinFormShell
{
    public partial class ShellForm : Form
    {
        public ShellForm()
        {
            InitializeComponent();
            FormClosing += ShellFormFormClosing;
        }

        public event EventHandler<CancelEventArgs> ShellClosing;

        private void ShellFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (ShellClosing != null)
            {
                var eventArgs = new CancelEventArgs();
                ShellClosing(this, eventArgs);
                e.Cancel = eventArgs.Cancel;
            }
        }

        public void ShowLayoutIvew(ShellLayoutView view)
        {
            Controls.Clear();
            Controls.Add(view);
            view.Dock = DockStyle.Fill;
        }
    }
}