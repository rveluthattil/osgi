using System;
using System.Windows;
using System.Windows.Controls;
using Utility;

namespace ChartBuilder
{
    public partial class LabeledTextBox : UserControl
    {
        public LabeledTextBox()
        {
            InitializeComponent();
            ElementTextBox.IsEnabled = IsChecked;
        }

        public string Label
        {
            get { return ElementCheckBox.Content as string; }
            set { ElementCheckBox.Content = value; }
        }

        public string Text
        {
            get { return ElementTextBox.Text; }
            set
            {
                ElementTextBox.Text = value;
                OnTextChanged();
            }
        }

        public bool IsChecked
        {
            get { return ElementCheckBox.IsChecked(); }
            set
            {
                ElementCheckBox.IsChecked = value;
                OnCheckedChanged();
            }
        }

        public event EventHandler TextChanged;

        public event EventHandler CheckedChanged;

        private void ElementCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ElementTextBox.IsEnabled = ElementCheckBox.IsChecked();
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