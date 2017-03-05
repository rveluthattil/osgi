using System;
using System.Windows.Forms;
using RichTextBox = System.Windows.Forms.RichTextBox;
using TextBox = System.Windows.Controls.TextBox;

namespace UIShell.Document.EditAdapters
{
    public class TextBoxAdapterFactory : IAdapterFactory<IEditHandler>
    {
        #region IAdapterFactory<IEditHandler> Members

        public IEditHandler GetAdapter(object element)
        {
            //Guard.ArgumentNotNull(element, "element");

            if (element is TextBox)
            {
                return new WPFTextBoxAdapter((TextBox) element);
            }
            if (element is RichTextBox)
            {
                return new RichTextBoxAdapter((RichTextBox) element);
            }
            else if (element is TextBoxBase)
            {
                return new TextBoxBaseAdapter((TextBoxBase) element);
            }

            throw new ArgumentException("ArgumentNotSupported");
        }

        public bool Supports(object element)
        {
            //Guard.ArgumentNotNull(element, "element");

            return (element is TextBox || element is TextBoxBase);
        }

        #endregion
    }
}