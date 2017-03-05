using System.Windows.Forms;

namespace UIShell.Document.EditAdapters
{
    public class RichTextBoxAdapter : TextBoxBaseAdapter
    {
        private readonly RichTextBox adaptee;

        public RichTextBoxAdapter(RichTextBox adaptee) : base(adaptee)
        {
            this.adaptee = adaptee;
        }

        public override bool CanRedo
        {
            get { return adaptee.CanRedo; }
        }

        public override bool CanPaste
        {
            get
            {
                IDataObject data = Clipboard.GetDataObject();
                foreach (string format in data.GetFormats())
                {
                    if (adaptee.CanPaste(DataFormats.GetFormat(format)))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public override void Redo()
        {
            adaptee.Redo();
        }
    }
}