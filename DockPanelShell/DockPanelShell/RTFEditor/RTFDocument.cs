using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DockShell;
using Jbe.CABExtension.SmartPartInfos;
using RTFEditor.Properties;
using UIShell.Document;
using UIShell.OSGi;
using UIShell.PresentationCore;

namespace RTFEditor
{
    public class RTFDocument : IDocument
    {
        private readonly IEditorView editorView;
        private readonly IconSmartPartInfo smartPartInfo;

        public RTFDocument()
        {
            editorView = new EditorView();
            smartPartInfo = new IconSmartPartInfo(SmartPartTitle, SmartPartTitle);
            smartPartInfo.Icon = Resources.TextDoc;
        }

        public string DocumentTitle
        {
            get
            {
                if (FileName == null)
                {
                    return "New Document";
                }

                return Path.GetFileName(FileName);
            }
        }

        private string SmartPartTitle
        {
            get { return DocumentTitle + (editorView.Modified ? "*" : ""); }
        }

        #region IDocument Members

        public event EventHandler DocumentActivated;
        public event EventHandler DocumentDeactivated;
        public event EventHandler Disposed;

        public IDocumentType DocumentType { get; set; }

        public string FileName { get; private set; }

        public void Save(string fileName)
        {
            editorView.Save(fileName);
            FileName = fileName;
            editorView.Modified = false;
            UpdateUI();
        }

        #endregion

        public void New()
        {
            //WorkItem.Activated += new EventHandler(WorkItemActivated);
            //WorkItem.Deactivated += new EventHandler(WorkItemDeactivated);


            ShowView();
        }

        private void ShowView()
        {
            var workspace = BundleRuntime.Instance.GetFirstOrDefaultService<IWorkspace>();

            workspace.Show(editorView, smartPartInfo);
            workspace.SmartPartClosing += WorkspaceSmartPartClosing;

            editorView.Disposed += EditorViewDisposed;
            editorView.ModifiedChanged += EditorViewModifiedChanged;
            editorView.Modified = true;
        }

        private void WorkspaceSmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (e.SmartPart == editorView)
            {
                if (editorView.Modified)
                {
                    DialogResult result = MessageBox.Show("Do you want to save the changes to " + DocumentTitle + "?",
                                                          Application.ProductName, MessageBoxButtons.YesNoCancel,
                                                          MessageBoxIcon.Warning);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            BundleRuntime.Instance.GetFirstOrDefaultService<ICommandBusService>().PublicCommand(
                                CommandNames.Save);
                            //WorkItem.Commands[CommandNames.Save].Execute();
                            break;
                        case DialogResult.No:
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                        default:
                            Debug.Assert(false, "The default case is not allowed!");
                            break;
                    }
                }
            }
        }

        public void Open(string fileName)
        {
            editorView.Open(fileName);
            FileName = fileName;
            editorView.Modified = false;
            ShowView();
            UpdateUI();
        }

        private void UpdateUI()
        {
            smartPartInfo.Title = SmartPartTitle;
            smartPartInfo.Description = SmartPartTitle;
            var workspace = BundleRuntime.Instance.GetFirstOrDefaultService<IWorkspace>();
            workspace.ApplySmartPartInfo(editorView, smartPartInfo);
        }

        #region Command and Event handler

        private void WorkItemActivated(object sender, EventArgs e)
        {
            if (DocumentActivated != null)
            {
                DocumentActivated(this, e);
            }
        }

        private void WorkItemDeactivated(object sender, EventArgs e)
        {
            if (DocumentDeactivated != null)
            {
                DocumentDeactivated(this, e);
            }
        }

        private void EditorViewDisposed(object sender, EventArgs e)
        {
            //WorkItem.Terminate();
            if (Disposed != null)
            {
                Disposed(this, e);
            }
        }

        private void EditorViewModifiedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        #endregion
    }
}