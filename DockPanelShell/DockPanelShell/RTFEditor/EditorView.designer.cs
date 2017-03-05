//using Microsoft.Practices.ObjectBuilder;
//using DockShell;
namespace RTFEditor
{
    //[SmartPart]
    partial class EditorView
    {
        ///// <summary>
        ///// The presenter used by this view.
        ///// </summary>
        //private Jbe.TestSuite.RTFEditor.EditorViewPresenter presenter = null;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        ///// <summary>
        ///// Sets the presenter. The dependency injection system will automatically
        ///// create a new presenter for you.
        ///// </summary>
        //[CreateNew]
        //public EditorViewPresenter Presenter
        //{
        //    set
        //    {
        //        presenter = value;
        //        presenter.View = this;
        //    }
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Location = new System.Drawing.Point(3, 3);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(372, 327);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // EditorView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.richTextBox);
            this.Name = "EditorView";
            this.Size = new System.Drawing.Size(378, 333);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
    }
}

