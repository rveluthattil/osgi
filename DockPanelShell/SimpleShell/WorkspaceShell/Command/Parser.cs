using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using UIShell.OSGi;

namespace WorkspaceShell.Command
{
    public class Parser
    {
        public static List<ToolStripItem> Build(XmlNodeList nodes, IBundle owner)
        {
            var result = new List<ToolStripItem>();
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].NodeType == XmlNodeType.Element)
                    result.Add(Build(nodes[i], owner));
            }
            return result;
        }

        public static List<ToolStripItem> Build(IEnumerable<XmlNode> nodes, IBundle owner)
        {
            var result = new List<ToolStripItem>();
            foreach (XmlNode item in nodes)
            {
                if (item.NodeType == XmlNodeType.Element)
                    result.Add(Build(item, owner));
            }
            return result;
        }

        public static ToolStripItem Build(XmlNode node, IBundle owner)
        {
            string type = XmlUtility.ReadAttribute(node, "type");
            if (string.IsNullOrEmpty(type))
            {
                type = "Item";
            }
            ToolStripItem result = null;
            bool createCommand = XmlUtility.ReadAttribute(node, "loadclasslazy") == "false";

            switch (type)
            {
                case "Separator":
                    result = new ToolStripSeparator();
                    break;
                    //case "CheckBox":
                    //    return new ToolBarCheckBox(codon, caller);
                case "Item":
                    result = new ToolBarCommand(node, owner, createCommand);
                    break;
                case "Menu":
                    result = new ToolStripMenuItem();
                    result.Text = XmlUtility.ReadAttribute(node, "text");
                    break;
                    //case "ComboBox":
                    //    return new ToolBarComboBox(codon, caller);
                    //case "DropDownButton":
                    //    return new ToolBarDropDownButton(codon, caller);
                default:
                    throw new NotSupportedException("unsupported menu item type : " + type);
            }
            if (node.ChildNodes.Count > 0)
            {
                var dropDownItem = result as ToolStripDropDownItem;
                foreach (XmlNode item in node.ChildNodes)
                {
                    dropDownItem.DropDownItems.Add(Build(item, owner));
                }
            }
            return result;
        }
    }
}