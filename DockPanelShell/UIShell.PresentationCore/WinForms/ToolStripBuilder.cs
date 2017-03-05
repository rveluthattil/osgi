using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using UIShell.OSGi;
using UIShell.PresentationCore.Utility;

namespace UIShell.PresentationCore.WinForms
{
    public class ToolStripBuilder : CatagorizedBuilder<ToolStrip>
    {
        public override void Build(XmlNode xmlNode, IBundle owner)
        {
            ToolStrip toolStrip = null;
            if (xmlNode.Name == "ToolStrip")
            {
                toolStrip = GetOrCreate(XmlUtility.ReadAttribute(xmlNode, "id"));
            }
            else if (xmlNode.Name == "ToolbarItem")
            {
                //insert to default ToolBar
                toolStrip = GetOrCreate(string.Empty);
                ToolStripItem item = ToolbarItemBuilder.Build(xmlNode, owner);
                ControlUtility.Invoke(toolStrip, () => toolStrip.Items.Add(item));
            }
            if (toolStrip != null)
            {
                List<ToolStripItem> items = ToolbarItemBuilder.Build(xmlNode.ChildNodes, owner);
                ControlUtility.Invoke(toolStrip, ()=>toolStrip.Items.AddRange(items.ToArray()));
            }
        }
    }
}