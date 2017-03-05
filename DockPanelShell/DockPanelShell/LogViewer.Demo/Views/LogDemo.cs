using System;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;
using log4net.Core;

namespace LogViewer.Demo.Views
{
    public partial class LogDemo : UserControl
    {
        private const string ERROR = "Error";
        private const string DEBUG = "Debug";
        private const string INFO = "Info";
        private readonly List<string> Categories = new List<string>();
        private LoggingEventData logEntry;

        public LogDemo()
        {
            InitializeComponent();
            logEntry = new LoggingEventData();
            severityCombo.DataSource = new[]
                                           {
                                               Level.Alert, Level.All, Level.Critical,
                                               Level.Debug, Level.Emergency, Level.Error,
                                               Level.Fatal, Level.Fine, Level.Finer,
                                               Level.Finest, Level.Info, Level.Notice,
                                               Level.Off, Level.Severe, Level.Trace,
                                               Level.Verbose, Level.Warn
                                           };
        }

        protected override void OnLoad(EventArgs e)
        {
            threadIdentityText.Text = Guid.NewGuid().ToString();
            base.OnLoad(e);
        }

        private void SeverityComboSelectedValueChanged(object sender, EventArgs e)
        {
            logEntry.Level = (Level) severityCombo.SelectedValue;
        }


        private void CategoriesCheckedChanged(object sender, EventArgs e)
        {
            string category = ((CheckBox) sender).Text;
            bool check = ((CheckBox) sender).Checked;

            if (check && !Categories.Contains(category))
            {
                Categories.Add(category);
            }
            else if (!check && Categories.Contains(category))
            {
                Categories.Remove(category);
            }
        }

        private void LogEntryBindingSourceBindingComplete(object sender, BindingCompleteEventArgs e)
        {
            logButton.Enabled = ValidateChildren();
        }

        private void LogButtonClick(object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger("log4net");
            logEntry.Domain = domainName.Text;
            logEntry.Identity = threadIdentityText.Text;
            logEntry.Message = messageText.Text;
            if (Categories.Count == 0)
            {
                logger.Info(logEntry);
            }
            else
            {
                if (Categories.Contains(ERROR))
                {
                    logger.Error(logEntry);
                }
                if (Categories.Contains(DEBUG))
                {
                    logger.Debug(logEntry);
                }
                if (Categories.Contains(INFO))
                {
                    logger.Info(logEntry);
                }
            }
        }
    }
}