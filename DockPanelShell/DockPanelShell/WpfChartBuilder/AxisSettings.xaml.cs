using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using Utility;

namespace ChartBuilder
{
    public partial class AxisSettings : UserControl
    {
        private readonly bool _initialized;
        private DisplayAxis _axis;
        private bool _enabled;

        public AxisSettings()
        {
            AxisType = typeof (LinearAxis);

            InitializeComponent();
            _initialized = true;

            AxisOrientation.ItemsSource = new[]
                                              {
                                                  System.Windows.Controls.DataVisualization.Charting.AxisOrientation.X.
                                                      ToString(),
                                                  System.Windows.Controls.DataVisualization.Charting.AxisOrientation.Y.
                                                      ToString(),
                                              };
            AxisOrientation.SelectedIndex = 1;
            AxisAxisType.ItemsSource = new[]
                                           {
                                               typeof (LinearAxis),
                                               typeof (CategoryAxis),
                                               typeof (DateTimeAxis),
                                           }.Select(type => new KeyValuePair<string, Type>(type.Name, type));
            AxisAxisType.SelectedIndex = 0;
        }

        public Type AxisType { get; private set; }

        public DisplayAxis Axis
        {
            get
            {
                if (null == _axis)
                {
                    _axis = Activator.CreateInstance(AxisType) as DisplayAxis;
                    _axis.Orientation = AxisOrientation.GetSelectedEnumValue<AxisOrientation>();
                }
                return _axis;
            }
            set { _axis = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                Visibility = _enabled ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public event EventHandler SettingsChanged;

        public void SetOrientation(AxisOrientation orientation)
        {
            AxisOrientation.SelectedItem = orientation.ToString();
        }

        public void SetAxisType(Type type)
        {
            AxisAxisType.SelectedItem =
                AxisAxisType.Items.Cast<KeyValuePair<string, Type>>().Where(item => item.Value == type).FirstOrDefault();
        }

        private void AxisOrientation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Axis.Location = AxisLocation.Auto;
            Axis.Orientation = AxisOrientation.GetSelectedEnumValue<AxisOrientation>();
            OnSettingsChanged();
        }

        private void AxisAxisType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool oldAxisRemoved = false;
            Collection<IAxis> chartAxesCollection = (null != Page.CurrentPage.Chart)
                                                        ? Page.CurrentPage.Chart.Axes
                                                        : null;
            if (null != chartAxesCollection)
            {
                if (chartAxesCollection.Contains(Axis))
                {
                    chartAxesCollection.Remove(Axis);
                    oldAxisRemoved = true;
                }
            }
            Axis = null;
            AxisType = ((KeyValuePair<string, Type>) AxisAxisType.SelectedItem).Value;
            bool supportsMinimumMaximumInterval = typeof (CategoryAxis) != AxisType;
            AxisMinimum.IsEnabled = supportsMinimumMaximumInterval;
            AxisMaximum.IsEnabled = supportsMinimumMaximumInterval;
            AxisInterval.IsEnabled = supportsMinimumMaximumInterval;
            if ((null != chartAxesCollection) && oldAxisRemoved)
            {
                chartAxesCollection.Add(Axis);
            }
            UpdateProperties();
            OnSettingsChanged();
        }

        private void ValueChanged_UpdateProperties(object sender, EventArgs e)
        {
            if (_initialized)
            {
                UpdateProperties();
            }
        }

        private void UpdateProperties()
        {
            Axis.Title = AxisTitle.IsChecked ? AxisTitle.Text : null;
            Axis.ShowGridLines = ShowGridLines.IsChecked();
            DateTime today = DateTime.Now.Date;

            var linearAxis = Axis as LinearAxis;
            var dateTimeAxis = Axis as DateTimeAxis;

            if (null != linearAxis)
            {
                linearAxis.Minimum = AxisMinimum.IsChecked ? AxisMinimum.Value : new double?();
                linearAxis.Maximum = AxisMaximum.IsChecked ? AxisMaximum.Value : new double?();
                linearAxis.Interval = AxisInterval.IsChecked ? AxisInterval.Value : new double?();
            }
            else if (null != dateTimeAxis)
            {
                dateTimeAxis.Minimum = AxisMinimum.IsChecked ? today.AddDays(AxisMinimum.Value) : new DateTime?();
                dateTimeAxis.Maximum = AxisMaximum.IsChecked ? today.AddDays(AxisMaximum.Value) : new DateTime?();
                dateTimeAxis.IntervalType = DateTimeIntervalType.Days;
                dateTimeAxis.Interval = AxisInterval.IsChecked ? AxisInterval.Value : new double?();
            }

            OnSettingsChanged();
        }

        private void OnSettingsChanged()
        {
            SettingsChanged.InvokeEmpty(this);
        }
    }
}