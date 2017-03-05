using System.Windows;
using UIShell.Document;

namespace ImageViewer
{
    public class ImageDocumentFactory : IDocumentFactory
    {
        public ImageDocumentFactory()
        {
            Extensions = new[] {".jpg", ".png", ".bmp"};
        }

        #region IDocumentFactory Members

        public IDocument New()
        {
            MessageBox.Show("Not support create new message yet!");
            return null;
        }

        public IDocument Open(string fileName)
        {
            var document = new ImageDocument();
            document.Open(fileName);
            return document;
        }

        public string Description
        {
            get { return "Images"; }
        }

        public string[] Extensions { //get { return "*.jpg;*.png;*.bmp"; }
            get; private set; }

        #endregion
    }
}