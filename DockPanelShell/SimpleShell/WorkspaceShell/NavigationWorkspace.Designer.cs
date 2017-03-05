namespace WorkspaceShell
{
    partial class NavigationWorkspace
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.navigationContainer = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // navigationContainer
            // 
            this.navigationContainer.DisplayMember = "Text";
            this.navigationContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationContainer.FormattingEnabled = true;
            this.navigationContainer.ItemHeight = 12;
            this.navigationContainer.Location = new System.Drawing.Point(0, 0);
            this.navigationContainer.Name = "navigationContainer";
            this.navigationContainer.Size = new System.Drawing.Size(151, 292);
            this.navigationContainer.TabIndex = 1;
            this.navigationContainer.SelectedIndexChanged += new System.EventHandler(this.navigationContainer_SelectedIndexChanged);
            // 
            // NavigationWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navigationContainer);
            this.Name = "NavigationWorkspace";
            this.Size = new System.Drawing.Size(151, 301);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox navigationContainer;
    }
}
