using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UIShell.Document.Views
{
    public partial class NewFileDialog : Form
    {
        public NewFileDialog()
        {
            InitializeComponent();
        }

        public IDocumentType SelectDocumentType(IList<IDocumentType> documentTypes)
        {
            listBox1.DataSource = documentTypes;
            if (ShowDialog() == DialogResult.OK)
            {
                return (IDocumentType) listBox1.SelectedItem;
            }
            return null;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}