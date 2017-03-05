using System;
using System.Windows.Forms;
using UIShell.Document;
using UIShell.OSGi;

namespace RTFEditor
{
    public partial class EditorView : UserControl, IEditorView
    {
        private readonly IEditManager editManager;

        public EditorView()
        {
            InitializeComponent();

            editManager = BundleRuntime.Instance.GetFirstOrDefaultService<IEditManager>();
            richTextBox.ModifiedChanged += delegate { OnModifiedChanged(); };
        }

        #region IEditorView Members

        public event EventHandler ModifiedChanged;

        public bool Modified
        {
            get { return richTextBox.Modified; }
            set { richTextBox.Modified = value; }
        }

        public void Open(string fileName)
        {
            richTextBox.LoadFile(fileName);
        }

        public void Save(string fileName)
        {
            richTextBox.SaveFile(fileName);
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            //presenter.OnViewReady();
            base.OnLoad(e);
        }

        protected virtual void OnModifiedChanged()
        {
            if (ModifiedChanged != null)
            {
                ModifiedChanged(this, new EventArgs());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                editManager.Deregister(richTextBox);

                //if (presenter != null) { presenter.Dispose(); }
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}