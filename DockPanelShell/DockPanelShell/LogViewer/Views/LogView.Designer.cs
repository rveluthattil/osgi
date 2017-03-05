namespace LogViewer.Views
{
    partial class LogView
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
                //LogRecorder.LogEntryAdded -= LogRecordChanged;
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.entriesLabel = new System.Windows.Forms.ToolStripLabel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.logEntryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.domainDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exceptionStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationInfoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loggerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertiesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.threadNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logEntryBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.stopButton,
            this.clearButton,
            this.toolStripSeparator1,
            this.entriesLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.toolStrip.Size = new System.Drawing.Size(569, 26);
            this.toolStrip.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.Image = global::LogViewer.Properties.Resources.PlayHS;
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(119, 21);
            this.startButton.Text = "Start Recording";
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // stopButton
            // 
            this.stopButton.Image = global::LogViewer.Properties.Resources.StopHS;
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(55, 21);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // clearButton
            // 
            this.clearButton.Image = global::LogViewer.Properties.Resources.Clear;
            this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(58, 21);
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.ClearButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 24);
            // 
            // entriesLabel
            // 
            this.entriesLabel.Name = "entriesLabel";
            this.entriesLabel.Size = new System.Drawing.Size(58, 21);
            this.entriesLabel.Text = "0 Entries";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.domainDataGridViewTextBoxColumn,
            this.exceptionStringDataGridViewTextBoxColumn,
            this.identityDataGridViewTextBoxColumn,
            this.levelDataGridViewTextBoxColumn,
            this.locationInfoDataGridViewTextBoxColumn,
            this.loggerNameDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn1,
            this.propertiesDataGridViewTextBoxColumn,
            this.threadNameDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn2,
            this.userNameDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.logEntryBindingSource;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 26);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(569, 141);
            this.dataGridView.TabIndex = 1;
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewCellFormatting);
            this.dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DataGridViewDataBindingComplete);
            // 
            // logEntryBindingSource
            // 
            this.logEntryBindingSource.DataSource = typeof(LogViewer.LogEntry);
            // 
            // domainDataGridViewTextBoxColumn
            // 
            this.domainDataGridViewTextBoxColumn.DataPropertyName = "Domain";
            this.domainDataGridViewTextBoxColumn.HeaderText = "Domain";
            this.domainDataGridViewTextBoxColumn.Name = "domainDataGridViewTextBoxColumn";
            // 
            // exceptionStringDataGridViewTextBoxColumn
            // 
            this.exceptionStringDataGridViewTextBoxColumn.DataPropertyName = "ExceptionString";
            this.exceptionStringDataGridViewTextBoxColumn.HeaderText = "ExceptionString";
            this.exceptionStringDataGridViewTextBoxColumn.Name = "exceptionStringDataGridViewTextBoxColumn";
            // 
            // identityDataGridViewTextBoxColumn
            // 
            this.identityDataGridViewTextBoxColumn.DataPropertyName = "Identity";
            this.identityDataGridViewTextBoxColumn.HeaderText = "Identity";
            this.identityDataGridViewTextBoxColumn.Name = "identityDataGridViewTextBoxColumn";
            // 
            // levelDataGridViewTextBoxColumn
            // 
            this.levelDataGridViewTextBoxColumn.DataPropertyName = "Level";
            this.levelDataGridViewTextBoxColumn.HeaderText = "Level";
            this.levelDataGridViewTextBoxColumn.Name = "levelDataGridViewTextBoxColumn";
            // 
            // locationInfoDataGridViewTextBoxColumn
            // 
            this.locationInfoDataGridViewTextBoxColumn.DataPropertyName = "LocationInfo";
            this.locationInfoDataGridViewTextBoxColumn.HeaderText = "LocationInfo";
            this.locationInfoDataGridViewTextBoxColumn.Name = "locationInfoDataGridViewTextBoxColumn";
            // 
            // loggerNameDataGridViewTextBoxColumn
            // 
            this.loggerNameDataGridViewTextBoxColumn.DataPropertyName = "LoggerName";
            this.loggerNameDataGridViewTextBoxColumn.HeaderText = "LoggerName";
            this.loggerNameDataGridViewTextBoxColumn.Name = "loggerNameDataGridViewTextBoxColumn";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Message";
            this.dataGridViewTextBoxColumn1.HeaderText = "Message";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // propertiesDataGridViewTextBoxColumn
            // 
            this.propertiesDataGridViewTextBoxColumn.DataPropertyName = "Properties";
            this.propertiesDataGridViewTextBoxColumn.HeaderText = "Properties";
            this.propertiesDataGridViewTextBoxColumn.Name = "propertiesDataGridViewTextBoxColumn";
            // 
            // threadNameDataGridViewTextBoxColumn
            // 
            this.threadNameDataGridViewTextBoxColumn.DataPropertyName = "ThreadName";
            this.threadNameDataGridViewTextBoxColumn.HeaderText = "ThreadName";
            this.threadNameDataGridViewTextBoxColumn.Name = "threadNameDataGridViewTextBoxColumn";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TimeStamp";
            this.dataGridViewTextBoxColumn2.HeaderText = "TimeStamp";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // userNameDataGridViewTextBoxColumn
            // 
            this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
            this.userNameDataGridViewTextBoxColumn.HeaderText = "UserName";
            this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
            // 
            // LogView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.toolStrip);
            this.Name = "LogView";
            this.Size = new System.Drawing.Size(569, 167);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logEntryBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton startButton;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource logEntryBindingSource;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.ToolStripLabel entriesLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn domainDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn exceptionStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn identityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationInfoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn loggerNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn propertiesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn threadNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
    }
}
