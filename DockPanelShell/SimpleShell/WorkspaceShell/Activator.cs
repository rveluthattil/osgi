using System.Xml;
using UIShell.OSGi;
using System.Windows.Forms;
using UIShell.OSGi.Core.Service;
using WorkspaceShell.Command;

namespace WorkspaceShell
{
    public class Activator : IBundleActivator
    {
        private const string ToolStripExtensionPoint = "ToolBar";
        private const string MainMenuExtensionPoint = "MainMenu";
        private const string NavigationExtensionPoint = "Navigation";
        private ShellForm mainForm = new ShellForm();
        private static void SetVisualApplicationSettings()
        {
            Application.EnableVisualStyles();

            var r = new ToolStripProfessionalRenderer();
            r.ColorTable.UseSystemColors = true;
            ToolStripManager.Renderer = r;
        }
        public void Start(IBundleContext context)
        {
            SetVisualApplicationSettings();
            context.AddService<Form>(mainForm);
            context.AddService<IWorkspace>(mainForm);

            var extensionManager = context.GetFirstOrDefaultService<IExtensionManager>();
            extensionManager.ExtensionChanged += extensionManager_ExtensionChanged;
        }

        void extensionManager_ExtensionChanged(object sender, ExtensionEventArgs e)
        {
            if (e.Action == CollectionChangedAction.Add)
            {
                switch (e.ExtensionPoint)
                {
                    case MainMenuExtensionPoint:

                        var newItems = Parser.Build(e.Extension.Data, e.Extension.Owner);
                        mainForm.MenuStrip.Items.AddRange(newItems.ToArray());

                        break;


                    case ToolStripExtensionPoint:

                        ToolStrip toolStrip = FindToolStrip();

                        foreach (var xmlNode in e.Extension.Data)
                        {
                            if (xmlNode.Name == "ToolStrip")
                            {
                                foreach (XmlNode toolbarItem in xmlNode.ChildNodes)
                                {
                                    AddToolbarItem(e, toolStrip, toolbarItem);
                                }
                                
                            }
                            else if (xmlNode.Name == "ToolbarItem")
                            {
                                AddToolbarItem(e, toolStrip, xmlNode);
                            }
                            
                        }
                       
                        break;
                    case NavigationExtensionPoint:
                        foreach (var xmlNode in e.Extension.Data)
                        {
                            mainForm.AddNavigation( new NavigationItem { Text = XmlUtility.ReadAttribute(xmlNode, "text") });
                        }

                        break;
                    default:
                        //Ignore other extensionpoint.
                        break;
                }

            }
            else if (e.Action == CollectionChangedAction.Remove)
            {
                //TODO: remove items.
            }
        }

        private static void AddToolbarItem(ExtensionEventArgs e, ToolStrip toolStrip, XmlNode xmlNode)
        {
            if (xmlNode.Name == "ToolbarItem")
            {
                //insert to default ToolBar
                ToolStripItem item = Parser.Build(xmlNode, e.Extension.Owner);
                toolStrip.Items.Add(item);
            }
        }

        private ToolStrip FindToolStrip()
        {
            ToolStrip toolStrip = null;
            foreach (var control in mainForm.ToolStripContainer.ContentPanel.Controls)
            {
                toolStrip = control as ToolStrip;
                if (toolStrip != null)
                {
                    break;
                }
            }

            if (toolStrip == null)
            {
                toolStrip = new ToolStrip();
                mainForm.ToolStripContainer.ContentPanel.Controls.Add(toolStrip);
            }
            return toolStrip;
        }

        public void Stop(IBundleContext context)
        {
        }
    }
}
