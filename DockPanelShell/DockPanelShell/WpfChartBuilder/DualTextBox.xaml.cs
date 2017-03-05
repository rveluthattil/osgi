using System;
using System.Windows;
using System.Windows.Controls;
using Utility;

namespace ChartBuilder
{
    public partial class DualTextBox : UserControl
    {
        private bool _isFirstTextActive;

        public DualTextBox()
        {
            InitializeComponent();
            UpdateIsEnabled();
        }

        public bool IsFirstTextActive
        {
            get { return _isFirstTextActive; }
            set
            {
                _isFirstTextActive = value;
                UpdateIsEnabled();
            }
        }

        public string TextA
        {
            get { return ElementTextBoxA.Text; }
            set { ElementTextBoxA.Text = value; }
        }

        public string TextB
        {
            get { return ElementTextBoxB.Text; }
            set { ElementTextBoxB.Text = value; }
        }

        public bool IsChecked
        {
            get { return ElementCheckBox.IsChecked(); }
            set { ElementCheckBox.IsChecked = value; }
        }

        public event EventHandler TextChanged;

        public event EventHandler CheckedChanged;

        private void UpdateIsEnabled()
        {
            ElementTextBoxA.IsEnabled = IsChecked && _isFirstTextActive;
            ElementTextBoxB.IsEnabled = IsChecked;
        }

        private void ElementCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateIsEnabled();
            OnCheckedChanged();
        }

        private void ElementTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OnTextChanged();
        }

        private void ElementTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.SelectAll();
        }

        private void OnCheckedChanged()
        {
            CheckedChanged.InvokeEmpty(this);
        }

        private void OnTextChanged()
        {
            TextChanged.InvokeEmpty(this);
        }
    }
}