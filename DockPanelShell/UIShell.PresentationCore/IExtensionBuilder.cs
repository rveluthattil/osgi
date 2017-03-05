using System;
using System.Collections.Generic;
using System.Xml;
using UIShell.OSGi;
using UIShell.OSGi.Utility;

namespace UIShell.PresentationCore
{
    public interface IExtensionBuilder
    {
        void Build(IEnumerable<XmlNode> xmlNodes, IBundle owner);
        void Build(XmlNode xmlNode, IBundle owner);
        void Build(XmlNodeList nodeList, IBundle owner);
        event EventHandler<EventArgs<object>> ItemAdded;
        event EventHandler<EventArgs<object>> ItemRemoved;
        void Reset();
    }
}