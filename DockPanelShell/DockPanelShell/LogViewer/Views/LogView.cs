using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LogViewer.Views
{
    public partial class LogView : UserControl, ILogView
    {
        public LogView()
        {
            InitializeComponent();

            logEntryBindingSource.DataSource = LogRepository.LogEntries;

            LogRepository.LogEntryAdded += LogRecordChanged;
            LogRepository.LogCleared += LogRecordChanged;
            LogRepository.Enabled = true;
            UpdateStartStopButtons();
        }

        private void LogRecordChanged(object sender, EventArgs e)
        {
            logEntryBindingSource.ResetBindings(false);
            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.CurrentCell = dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[0];
                dataGridView.CurrentCell = null;
            }
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            LogRepository.Enabled = true;
            UpdateStartStopButtons();
        }

        private void StopButtonClick(object sender, EventArgs e)
        {
            LogRepository.Enabled = false;
            UpdateStartStopButtons();
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            LogRepository.Clear();
        }

        private void UpdateStartStopButtons()
        {
            stopButton.Enabled = LogRepository.Enabled;
            startButton.Enabled = !LogRepository.Enabled;
        }

        private void DataGridViewDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            entriesLabel.Text = dataGridView.Rows.Count + " Entries";
        }

        private void DataGridViewCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value is ICollection<string>)
            {
                var builder = new StringBuilder();
                foreach (string str in (ICollection<string>) e.Value)
                {
                    builder.Append(str);
                    builder.Append(", ");
                }
                if (builder.Length >= 2)
                {
                    builder.Remove(builder.Length - 2, 2);
                }
                e.Value = builder.ToString();
                e.FormattingApplied = true;
            }
        }
    }
}