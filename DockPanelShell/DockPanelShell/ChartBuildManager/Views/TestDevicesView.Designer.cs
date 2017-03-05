namespace ChartBuildManager.Views
{
    partial class TestDevicesView
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
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView = new System.Windows.Forms.ListView();
            this.nameColumn = new System.Windows.Forms.ColumnHeader();
            this.productNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.manufacturerColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newDeviceToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.removeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.productNameColumnHeader,
            this.manufacturerColumnHeader});
            this.listView.ContextMenuStrip = this.contextMenuStrip;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 30);
            this.listView.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(322, 306);
            this.listView.TabIndex = 0;
            this.listView.TileSize = new System.Drawing.Size(168, 48);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.ListViewSelectedIndexChanged);
            this.listView.DoubleClick += new System.EventHandler(this.ListViewDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(124, 48);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.removeToolStripMenuItem.Text = "&Remove";
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDeviceToolStripDropDownButton,
            this.openToolStripButton,
            this.removeToolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.toolStrip.Size = new System.Drawing.Size(322, 26);
            this.toolStrip.TabIndex = 1;
            // 
            // newDeviceToolStripDropDownButton
            // 
            this.newDeviceToolStripDropDownButton.Image = global::ChartBuildManager.Properties.Resources.cog_add;
            this.newDeviceToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newDeviceToolStripDropDownButton.Name = "newDeviceToolStripDropDownButton";
            this.newDeviceToolStripDropDownButton.Size = new System.Drawing.Size(98, 21);
            this.newDeviceToolStripDropDownButton.Text = "New Chart";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::ChartBuildManager.Properties.Resources.cog_edit;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 21);
            this.openToolStripButton.Text = "Open";
            // 
            // removeToolStripButton
            // 
            this.removeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeToolStripButton.Image = global::ChartBuildManager.Properties.Resources.cog_delete;
            this.removeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeToolStripButton.Name = "removeToolStripButton";
            this.removeToolStripButton.Size = new System.Drawing.Size(23, 21);
            this.removeToolStripButton.Text = "Remove";
            // 
            // TestDevicesView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolStrip);
            this.Name = "TestDevicesView";
            this.Size = new System.Drawing.Size(322, 336);
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton newDeviceToolStripDropDownButton;
        private System.Windows.Forms.ColumnHeader productNameColumnHeader;
        private System.Windows.Forms.ColumnHeader manufacturerColumnHeader;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton removeToolStripButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader nameColumn;
    }
}

