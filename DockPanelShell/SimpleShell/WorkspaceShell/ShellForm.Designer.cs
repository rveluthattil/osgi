namespace WorkspaceShell
{
    partial class ShellForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.navigationWorkspace1 = new WorkspaceShell.NavigationWorkspace();
            this.deckWorkspace1 = new WorkspaceShell.DeckWorkspace();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer.ContentPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(645, 416);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(645, 440);
            this.toolStripContainer.TabIndex = 1;
            this.toolStripContainer.Text = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.mainMenuStrip);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.navigationWorkspace1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.deckWorkspace1);
            this.splitContainer1.Size = new System.Drawing.Size(645, 416);
            this.splitContainer1.SplitterDistance = 215;
            this.splitContainer1.TabIndex = 1;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(645, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // navigationWorkspace1
            // 
            this.navigationWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.navigationWorkspace1.Name = "navigationWorkspace1";
            this.navigationWorkspace1.Size = new System.Drawing.Size(215, 416);
            this.navigationWorkspace1.TabIndex = 0;
            // 
            // deckWorkspace1
            // 
            this.deckWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckWorkspace1.Location = new System.Drawing.Point(0, 0);
            this.deckWorkspace1.Name = "deckWorkspace1";
            this.deckWorkspace1.Size = new System.Drawing.Size(426, 416);
            this.deckWorkspace1.TabIndex = 0;
            // 
            // ShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 440);
            this.Controls.Add(this.toolStripContainer);
            this.Name = "ShellForm";
            this.Text = "MainForm";
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private DeckWorkspace deckWorkspace1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private NavigationWorkspace navigationWorkspace1;
    }
}