using DockShell;

namespace LogViewer.Demo.Views
{
    partial class LogDemo
    {

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.threadName = new System.Windows.Forms.Label();
            this.threadIdentityText = new System.Windows.Forms.TextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.messageText = new System.Windows.Forms.TextBox();
            this.domain = new System.Windows.Forms.Label();
            this.domainName = new System.Windows.Forms.TextBox();
            this.categoriesLabel = new System.Windows.Forms.Label();
            this.generalCheck = new System.Windows.Forms.CheckBox();
            this.debugCheck = new System.Windows.Forms.CheckBox();
            this.traceCheck = new System.Windows.Forms.CheckBox();
            this.eventInfoLabel = new System.Windows.Forms.Label();
            this.logLabel = new System.Windows.Forms.Label();
            this.logButton = new System.Windows.Forms.Button();
            this.categoriesInfoText = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.severityLabel = new System.Windows.Forms.Label();
            this.severityCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // threadName
            // 
            this.threadName.AutoSize = true;
            this.threadName.Location = new System.Drawing.Point(32, 39);
            this.threadName.Name = "threadName";
            this.threadName.Size = new System.Drawing.Size(53, 12);
            this.threadName.TabIndex = 1;
            this.threadName.Text = "Identity";
            // 
            // threadIdentityText
            // 
            this.errorProvider.SetIconPadding(this.threadIdentityText, 3);
            this.threadIdentityText.Location = new System.Drawing.Point(94, 36);
            this.threadIdentityText.Name = "threadIdentityText";
            this.threadIdentityText.Size = new System.Drawing.Size(106, 21);
            this.threadIdentityText.TabIndex = 2;
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(31, 67);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(47, 12);
            this.messageLabel.TabIndex = 3;
            this.messageLabel.Text = "Message";
            // 
            // messageText
            // 
            this.messageText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.messageText.Location = new System.Drawing.Point(94, 64);
            this.messageText.Multiline = true;
            this.messageText.Name = "messageText";
            this.messageText.Size = new System.Drawing.Size(289, 67);
            this.messageText.TabIndex = 4;
            // 
            // domain
            // 
            this.domain.AutoSize = true;
            this.domain.Location = new System.Drawing.Point(43, 142);
            this.domain.Name = "domain";
            this.domain.Size = new System.Drawing.Size(41, 12);
            this.domain.TabIndex = 5;
            this.domain.Text = "Domain";
            // 
            // domainName
            // 
            this.errorProvider.SetIconPadding(this.domainName, 3);
            this.domainName.Location = new System.Drawing.Point(94, 139);
            this.domainName.Name = "domainName";
            this.domainName.Size = new System.Drawing.Size(106, 21);
            this.domainName.TabIndex = 6;
            // 
            // categoriesLabel
            // 
            this.categoriesLabel.AutoSize = true;
            this.categoriesLabel.Location = new System.Drawing.Point(24, 198);
            this.categoriesLabel.Name = "categoriesLabel";
            this.categoriesLabel.Size = new System.Drawing.Size(65, 12);
            this.categoriesLabel.TabIndex = 9;
            this.categoriesLabel.Text = "Categories";
            // 
            // generalCheck
            // 
            this.generalCheck.AutoSize = true;
            this.generalCheck.Location = new System.Drawing.Point(94, 197);
            this.generalCheck.Name = "generalCheck";
            this.generalCheck.Size = new System.Drawing.Size(48, 16);
            this.generalCheck.TabIndex = 10;
            this.generalCheck.Text = "Info";
            this.generalCheck.UseVisualStyleBackColor = true;
            this.generalCheck.CheckedChanged += new System.EventHandler(this.CategoriesCheckedChanged);
            // 
            // debugCheck
            // 
            this.debugCheck.AutoSize = true;
            this.debugCheck.Location = new System.Drawing.Point(94, 220);
            this.debugCheck.Name = "debugCheck";
            this.debugCheck.Size = new System.Drawing.Size(54, 16);
            this.debugCheck.TabIndex = 11;
            this.debugCheck.Text = "Debug";
            this.debugCheck.UseVisualStyleBackColor = true;
            this.debugCheck.CheckedChanged += new System.EventHandler(this.CategoriesCheckedChanged);
            // 
            // traceCheck
            // 
            this.traceCheck.AutoSize = true;
            this.traceCheck.Location = new System.Drawing.Point(94, 243);
            this.traceCheck.Name = "traceCheck";
            this.traceCheck.Size = new System.Drawing.Size(54, 16);
            this.traceCheck.TabIndex = 12;
            this.traceCheck.Text = "Error";
            this.traceCheck.UseVisualStyleBackColor = true;
            this.traceCheck.CheckedChanged += new System.EventHandler(this.CategoriesCheckedChanged);
            // 
            // eventInfoLabel
            // 
            this.eventInfoLabel.AutoSize = true;
            this.eventInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventInfoLabel.ForeColor = System.Drawing.Color.Gray;
            this.eventInfoLabel.Location = new System.Drawing.Point(3, 9);
            this.eventInfoLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            this.eventInfoLabel.Name = "eventInfoLabel";
            this.eventInfoLabel.Size = new System.Drawing.Size(161, 13);
            this.eventInfoLabel.TabIndex = 0;
            this.eventInfoLabel.Text = "Enter the event information";
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logLabel.ForeColor = System.Drawing.Color.Gray;
            this.logLabel.Location = new System.Drawing.Point(3, 289);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(197, 13);
            this.logLabel.TabIndex = 14;
            this.logLabel.Text = "Raise the information as log entry";
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(94, 316);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(75, 23);
            this.logButton.TabIndex = 15;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.LogButtonClick);
            // 
            // categoriesInfoText
            // 
            this.categoriesInfoText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.categoriesInfoText.Location = new System.Drawing.Point(242, 198);
            this.categoriesInfoText.Multiline = true;
            this.categoriesInfoText.Name = "categoriesInfoText";
            this.categoriesInfoText.ReadOnly = true;
            this.categoriesInfoText.Size = new System.Drawing.Size(141, 62);
            this.categoriesInfoText.TabIndex = 13;
            this.categoriesInfoText.Text = "Info\r\nIf no category is selected the Logging Application Block uses the default c" +
                "ategory.";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // severityLabel
            // 
            this.severityLabel.AutoSize = true;
            this.severityLabel.Location = new System.Drawing.Point(36, 170);
            this.severityLabel.Name = "severityLabel";
            this.severityLabel.Size = new System.Drawing.Size(53, 12);
            this.severityLabel.TabIndex = 7;
            this.severityLabel.Text = "Severity";
            // 
            // severityCombo
            // 
            this.severityCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.severityCombo.FormattingEnabled = true;
            this.severityCombo.Location = new System.Drawing.Point(94, 167);
            this.severityCombo.Name = "severityCombo";
            this.severityCombo.Size = new System.Drawing.Size(106, 20);
            this.severityCombo.TabIndex = 8;
            this.severityCombo.SelectedValueChanged += new System.EventHandler(this.SeverityComboSelectedValueChanged);
            // 
            // LogDemo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.severityCombo);
            this.Controls.Add(this.categoriesInfoText);
            this.Controls.Add(this.logButton);
            this.Controls.Add(this.traceCheck);
            this.Controls.Add(this.debugCheck);
            this.Controls.Add(this.generalCheck);
            this.Controls.Add(this.domainName);
            this.Controls.Add(this.categoriesLabel);
            this.Controls.Add(this.severityLabel);
            this.Controls.Add(this.domain);
            this.Controls.Add(this.messageText);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.threadIdentityText);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.eventInfoLabel);
            this.Controls.Add(this.threadName);
            this.Name = "LogDemo";
            this.Size = new System.Drawing.Size(409, 352);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label threadName;
        private System.Windows.Forms.TextBox threadIdentityText;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.TextBox messageText;
        private System.Windows.Forms.Label domain;
        private System.Windows.Forms.TextBox domainName;
        private System.Windows.Forms.Label categoriesLabel;
        private System.Windows.Forms.CheckBox generalCheck;
        private System.Windows.Forms.CheckBox debugCheck;
        private System.Windows.Forms.CheckBox traceCheck;
        private System.Windows.Forms.Label eventInfoLabel;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.TextBox categoriesInfoText;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox severityCombo;
        private System.Windows.Forms.Label severityLabel;
    }
}

