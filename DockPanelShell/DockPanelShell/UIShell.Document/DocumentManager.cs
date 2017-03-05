using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using UIShell.Document.Views;
using UIShell.OSGi;
using UIShell.PresentationCore;
using Action = UIShell.PresentationCore.Action;

namespace UIShell.Document
{
    public class DocumentManager : IDocumentManager, IDisposable
    {
        private readonly Dictionary<Collection<string>, IDocumentFactory> documentFactories;
        private readonly OpenFileDialog openFileDialog;
        private readonly SaveFileDialog saveFileDialog;
        private IDocument activeDocument;

        public DocumentManager()
        {
            documentFactories = new Dictionary<Collection<string>, IDocumentFactory>();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();

            var commandBus = BundleRuntime.Instance.GetFirstOrDefaultService<ICommandBusService>();
            commandBus.SubscribeCommandEvent(CommandNames.Save, Save);
            commandBus.SubscribeCommandEvent(CommandNames.SaveAs, SaveAs);
            commandBus.SubscribeCommandEvent(CommandNames.Open, Open);
            commandBus.SubscribeCommandEvent(CommandNames.NewDocument, New);
        }

        private IDocument ActiveDocument
        {
            get { return activeDocument; }
            set
            {
                activeDocument = value;

                CommandStatus status = CommandStatus.Disabled;
                if (activeDocument != null)
                {
                    status = CommandStatus.Enabled;
                }
                if (BundleRuntime.Instance != null)
                {
                    var commandBus = BundleRuntime.Instance.GetFirstOrDefaultService<ICommandBusService>();
                    if (commandBus != null)
                    {
                        commandBus.PublicCommandStatus(CommandNames.Save, status);
                        commandBus.PublicCommandStatus(CommandNames.SaveAs, status);
                    }
                }
                //workItem.Commands[CommandNames.Save].Status = status;
                //workItem.Commands[CommandNames.SaveAs].Status = status;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IDocumentManager Members

        public void Clear()
        {
            documentFactories.Clear();
        }

        public ReadOnlyCollection<IDocumentType> DocumentTypes
        {
            get
            {
                var array = new IDocumentType[documentFactories.Count];
                int i = 0;
                foreach (IDocumentFactory factory in documentFactories.Values)
                {
                    array[i] = factory;
                    i++;
                }
                return new ReadOnlyCollection<IDocumentType>(array);
            }
        }
        public void UnRegister(IDocumentFactory documentFactory)
        {
            foreach (var factory in documentFactories)
            {
                if (factory.Value == documentFactory)
                {
                    documentFactories.Remove(factory.Key);
                    break;
                }
            }

            //todo: update openFileDialog.Filter
        }
        public void Register(IDocumentFactory documentFactory)
        {
            documentFactories.Add(new Collection<string>(documentFactory.Extensions), documentFactory);
            openFileDialog.Filter = AppendFilter(openFileDialog.Filter, documentFactory);
        }

        public void New(IDocumentType documentType)
        {
            if (documentType == null)
                return;
            var factory = (IDocumentFactory) documentType;
            IDocument document = factory.New();

            if (document != null)
            {
                document.DocumentType = factory;
                InitializeDocument(document);
            }
        }

        #endregion

        ~DocumentManager()
        {
            Dispose(false);
        }

        public void New(object sender, object e)
        {
            //IDocumentFactory firstFactory = null;
            //foreach (var item in this.documentFactories.Values)
            //{
            //    firstFactory = item;
            //    break;
            //}

            UIInvoke(() =>
                         {
                             IDocumentFactory firstFactory = null;
                             ReadOnlyCollection<IDocumentType> types = DocumentTypes;
                             if (types.Count == 1)
                             {
                                 foreach (IDocumentFactory item in documentFactories.Values)
                                 {
                                     firstFactory = item;
                                     break;
                                 }
                             }
                             else
                             {
                                 firstFactory =
                                     new NewFileDialog().SelectDocumentType(DocumentTypes) as IDocumentFactory;
                             }
                             if (firstFactory == null)
                             {
                                 return;
                             }
                             New(firstFactory);
                         });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (openFileDialog != null)
                {
                    openFileDialog.Dispose();
                }
                if (saveFileDialog != null)
                {
                    saveFileDialog.Dispose();
                }
            }
        }

        private void Save(string fileName)
        {
            if (ActiveDocument == null)
                return;
            if (fileName == null)
            {
                saveFileDialog.Filter = AppendFilter("", ActiveDocument.DocumentType);

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                fileName = saveFileDialog.FileName;
            }

            ActiveDocument.Save(fileName);
        }

        private void InitializeDocument(IDocument document)
        {
            document.DocumentActivated += DocumentActivated;
            document.DocumentDeactivated += DocumentDeactivated;
            document.Disposed += DocumentDisposed;
            ActiveDocument = document;
        }

        private static string AppendFilter(string filter, IDocumentType extension)
        {
            bool appendDesc = extension.Extensions.Length == 1;

            foreach (string item in extension.Extensions)
            {
                if (!String.IsNullOrEmpty(filter))
                {
                    filter += "|";
                }
                if (appendDesc)
                {
                    filter += extension.Description;
                }

                filter += "|*" + item;
            }


            return filter;
        }

        #region Command and Event handler

        public void Open(object sender, object e)
        {
            UIInvoke(() => Open(sender, e as EventArgs));
        }

        private void UIInvoke(Action action)
        {
            var layoutView = BundleRuntime.Instance.GetFirstOrDefaultService<IShellLayoutView>();
            layoutView.Invoke(() => action());
        }

        //[CommandHandler(CommandNames.Open)]
        public void Open(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(openFileDialog.FileName);

                IDocumentFactory documentFactory = null; // documentFactories[extension];
                foreach (var item in documentFactories)
                {
                    if (item.Key.Contains(extension))
                    {
                        documentFactory = item.Value;
                        break;
                    }
                }
                if (documentFactory == null)
                {
                    throw new Exception("Not support file type:" + extension);
                }
                IDocument document = documentFactory.Open(openFileDialog.FileName);

                if (document != null)
                {
                    InitializeDocument(document);
                }
            }
        }

        public void Save(object sender, object e)
        {
            UIInvoke(() => Save(sender, e as EventArgs));
        }

        //[CommandHandler(CommandNames.Save)]
        public void Save(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                Save(ActiveDocument.FileName);
        }

        public void SaveAs(object sender, object e)
        {
            UIInvoke(() => SaveAs(sender, e as EventArgs));
        }

        //[CommandHandler(CommandNames.SaveAs)]
        public void SaveAs(object sender, EventArgs e)
        {
            Save(null);
        }

        private void DocumentActivated(object sender, EventArgs e)
        {
            ActiveDocument = (IDocument) sender;
        }

        private void DocumentDeactivated(object sender, EventArgs e)
        {
            ActiveDocument = null;
        }

        private void DocumentDisposed(object sender, EventArgs e)
        {
            var document = (IDocument) sender;
            document.DocumentActivated -= DocumentActivated;
            document.DocumentDeactivated -= DocumentDeactivated;
            document.Disposed -= DocumentDisposed;
            ActiveDocument = null;
        }

        #endregion
    }
}