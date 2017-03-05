using System.Linq;
using System.Windows.Forms;
using DockShell;
using Jbe.CABExtension.SmartPartInfos;
using UIShell.PresentationCore;
using UIShell.PresentationCore.WinForms;
using WeifenLuo.WinFormsUI.Docking;

namespace WinFormShell
{
    public partial class ShellLayoutView : UserControl
    {
        private readonly WPFDockPanelWorkspace dockPanelWorkspace;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShellLayoutView"/> class.
        /// </summary>
        public ShellLayoutView()
        {
            InitializeComponent();

            dockPanelWorkspace = new WPFDockPanelWorkspace();
            dockPanelWorkspace.Name = "LayoutWorkspace";
            dockPanelWorkspace.DocumentStyle = DocumentStyle.DockingWindow;
            dockPanelWorkspace.Dock = DockStyle.Fill;
            dockPanelWorkspace.DockTopPortion = 180;
            dockPanelWorkspace.DockBottomPortion = 180;
            dockPanelWorkspace.DockLeftPortion = 230;
            dockPanelWorkspace.DockRightPortion = 230;
            //dockPanelWorkspace.ContentAdded += UpdateUI;
            //dockPanelWorkspace.ContentRemoved += UpdateUI;
            //dockPanelWorkspace.ActiveDocumentChanged += UpdateUI;
            dockPanelWorkspace.SmartPartActivated += DockPanelWorkspaceSmartPartActivated;

            toolStripContainer.ContentPanel.Controls.Add(dockPanelWorkspace);
        }

        public IComposableWorkspace<Control, DockPanelSmartPartInfo> Workspace
        {
            get { return dockPanelWorkspace; }
        }


        public bool CloseDocuments()
        {
            var documents = dockPanelWorkspace.Documents.ToList();
            foreach (IDockContent content in documents)
            {
                content.DockHandler.Close();
            }

            return (dockPanelWorkspace.DocumentsCount == 0);
        }

        private void DockPanelWorkspaceSmartPartActivated(object sender, WorkspaceEventArgs e)
        {
            // Workaround to activate the WorkItem (Only necessary for Windows Forms Controls)
            if (dockPanelWorkspace.ActiveSmartPart is Control)
            {
                ((Control) dockPanelWorkspace.ActiveSmartPart).Focus();
            }
        }

        public IShellLayoutView CreateShellLayoutViewProxy()
        {
            return new ShellLayoutViewProxy(this, Show)
                       {
                           MainMenuStrip = mainMenuStrip,
                           MainStatusStrip = mainStatusStrip,
                           StatusLabel = statusLabel,
                           ToolStripContainer = toolStripContainer
                       };
        }

        /// <summary>
        /// Shows SmartPart using the given SmartPartInfo
        /// </summary>
        /// <param name="smartPart">Smart part to show.</param>
        /// <param name="smartPartInfo"></param>
        public void Show(object smartPart, object smartPartInfo)
        {
            var viewInfo = smartPartInfo as ISmartPartInfo;
            if (viewInfo == null)
            {
                Workspace.Show(smartPart);
            }
            else
            {
                Workspace.Show(smartPart, viewInfo);
            }
        }
    }
}