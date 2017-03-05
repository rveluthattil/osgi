using System.Windows.Forms;
using WorkspaceShell.Command;

namespace WorkspaceShell
{
    public partial class ShellForm : Form, WorkspaceShell.IWorkspace
    {
        public ShellForm()
        {
            InitializeComponent();
        }

        public void Show(object control, object controlInfo)
        {
            this.deckWorkspace1.Show((Control)control, controlInfo);
        }

        public void AddNavigation(NavigationItem control)
        {
            this.navigationWorkspace1.AddNavigation(control);
        }

        public MenuStrip MenuStrip { get { return mainMenuStrip; }}

        public ToolStripContainer ToolStripContainer { get { return toolStripContainer; } }
    }
}
