using UIShell.Document;

namespace RTFEditor
{
    public class RTFDocumentFactory : IDocumentFactory
    {
        public RTFDocumentFactory()
        {
            Extensions = new[] {".rtf"};
        }

        #region IDocumentFactory Members

        public string Description
        {
            get { return "RTF Document"; }
        }

        public string[] Extensions { get; private set; }

        public IDocument New()
        {
            var document = new RTFDocument();
            document.New();

            return document;
        }

        public IDocument Open(string fileName)
        {
            var document = new RTFDocument();
            document.Open(fileName);

            return document;
        }

        #endregion
    }
}