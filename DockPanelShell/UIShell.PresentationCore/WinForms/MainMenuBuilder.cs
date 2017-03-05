using System.Windows.Forms;
using System.Xml;
using UIShell.OSGi;

namespace UIShell.PresentationCore.WinForms
{
    public class MainMenuBuilder : BuilderBase<ToolStripItem>
    {
        public override void Build(XmlNode xmlNode, IBundle owner)
        {
            ToolStripItem newItem = ToolbarItemBuilder.Build(xmlNode, owner);
            Items.Add(newItem);
            OnItemAdded(newItem);
        }
    }
}