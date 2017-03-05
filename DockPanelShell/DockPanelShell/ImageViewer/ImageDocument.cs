using System;
using System.IO;
using DockShell;
using Jbe.CABExtension.SmartPartInfos;
using UIShell.Document;
using UIShell.OSGi;

namespace ImageViewer
{
    internal class ImageDocument : IDocument
    {
        private readonly ImageViewPanel view = new ImageViewPanel();
        private IconSmartPartInfo smartPartInfo;

        #region IDocument Members

        public event EventHandler DocumentActivated;

        public event EventHandler DocumentDeactivated;

        public event EventHandler Disposed;

        public IDocumentType DocumentType { get; set; }

        public string FileName { get; private set; }

        public void Save(string fileName)
        {
            //throw new NotImplementedException();
        }

        #endregion

        public void Open(string fileName)
        {
            var workspace = BundleRuntime.Instance.GetFirstOrDefaultService<IWorkspace>();
            view.DataContext = fileName;
            FileName = fileName;
            smartPartInfo = new IconSmartPartInfo(Path.GetFileName(fileName), string.Empty);
            workspace.Show(view, smartPartInfo);
        }
    }
}