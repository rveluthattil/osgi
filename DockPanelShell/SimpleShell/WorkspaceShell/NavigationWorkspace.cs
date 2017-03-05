using System;
using System.Windows.Forms;
using UIShell.OSGi;
using WorkspaceShell.Command;

namespace WorkspaceShell
{
    public partial class NavigationWorkspace : UserControl
    {
        public NavigationWorkspace()
        {
            InitializeComponent();
        }

        internal void AddNavigation(NavigationItem control)
        {
            this.navigationContainer.Items.Add(control);
        }

        private void navigationContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Clicked:"+this.navigationContainer.SelectedItem);
            //BundleRuntime.Instance.GetFirstOrDefaultService<IShellLayoutView>().SetStatusLabel(this.navigationContainer.SelectedItem.ToString());
        }
    }
}
