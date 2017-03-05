using System;
using System.Windows.Forms;
using System.Xml;
using UIShell.OSGi;
using Activator = System.Activator;

namespace WorkspaceShell.Command
{
    public class ToolBarCommand : ToolStripMenuItem
    {
        private readonly XmlNode node;
        private readonly IBundle owner;
        private IViewCommand menuCommand;

        public ToolBarCommand(XmlNode node, IBundle owner, bool createCommand)
        {
            RightToLeft = RightToLeft.Inherit;
            this.node = node;
            this.owner = owner;

            if (createCommand)
            {
                LoadClass();
            }

            if (Image == null)
            {
                string icon = XmlUtility.ReadAttribute(node, "icon");
                if (!string.IsNullOrEmpty(icon))
                {
                    Image = ResourcesUtility.TryGetBitmap(owner.LoadResource(icon, ResourceLoadMode.ClassSpace));
                }
            }
            Text = XmlUtility.ReadAttribute(node, "text");
            ToolTipText = XmlUtility.ReadAttribute(node, "tooltip");

        }

        private void LoadClass()
        {
            string classType = XmlUtility.ReadAttribute(node, "class");
            if (menuCommand == null && !string.IsNullOrEmpty(classType))
            {
                menuCommand = System.Activator.CreateInstance(owner.LoadClass(classType)) as IViewCommand;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (menuCommand == null)
            {
                LoadClass();
            }

            if(menuCommand != null)
            {
                menuCommand.Run();
            }

        }
    }
}