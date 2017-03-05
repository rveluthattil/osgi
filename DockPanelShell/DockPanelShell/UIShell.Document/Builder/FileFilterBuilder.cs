using System.Xml;
using UIShell.OSGi;
using UIShell.PresentationCore;
using UIShell.PresentationCore.Utility;

namespace UIShell.Document.Builder
{
    internal class FileFilterBuilder : BuilderBase<IDocumentFactory>
    {
        private const string ATTR_TYPE = "type";


        public override void Build(XmlNode xmlNode, IBundle owner)
        {
            string documentFactory = XmlUtility.ReadAttribute(xmlNode, ATTR_TYPE);
            if (string.IsNullOrEmpty(documentFactory))
                return;
            var result = System.Activator.CreateInstance(owner.LoadClass(documentFactory)) as IDocumentFactory;
            if (result != null)
            {
                AddItem(result);
            }
        }
    }
}