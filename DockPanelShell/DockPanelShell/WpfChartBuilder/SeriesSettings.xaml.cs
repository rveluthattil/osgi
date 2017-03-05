using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using Utility;

namespace ChartBuilder
{
    public partial class SeriesSettings : UserControl
    {
        private static readonly Random _random = new Random();

        private readonly bool _initialized;
        private bool _enabled;

        public SeriesSettings()
        {
            InitializeComponent();
            _initialized = true;

            SeriesType.ItemsSource = new[]
                                         {
                                             new SeriesInformation(typeof (ColumnSeries), null, typeof (CategoryAxis),
                                                                   AxisOrientation.X, typeof (LinearAxis),
                                                                   AxisOrientation.Y),
                                             new SeriesInformation(typeof (BarSeries), null, typeof (CategoryAxis),
                                                                   AxisOrientation.Y, typeof (LinearAxis),
                                                                   AxisOrientation.X),
                                             new SeriesInformation(typeof (PieSeries), null, null, null, null, null),
                                             new SeriesInformation(typeof (LineSeries), "", typeof (LinearAxis),
                                                                   AxisOrientation.X, typeof (LinearAxis),
                                                                   AxisOrientation.Y),
                                             new SeriesInformation(typeof (AreaSeries), "", typeof (LinearAxis),
                                                                   AxisOrientation.X, typeof (LinearAxis),
                                                                   AxisOrientation.Y),
                                             new SeriesInformation(typeof (ScatterSeries), "", typeof (LinearAxis),
                                                                   AxisOrientation.X, typeof (LinearAxis),
                                                                   AxisOrientation.Y),
                                             new SeriesInformation(typeof (BubbleSeries), "", typeof (LinearAxis),
                                                                   AxisOrientation.X, typeof (LinearAxis),
                                                                   AxisOrientation.Y),
                                         };
            SeriesType.SelectedIndex = 0;
            ValueSource.ItemsSource = new[]
                                          {
                                              new ValueInformation(ValueType.AutomaticDoubles, "Automatic Doubles"),
                                              new ValueInformation(ValueType.ManualDoubles, "Manual Doubles"),
                                              new ValueInformation(ValueType.ManualPairs, "Manual Pairs"),
                                          };
            ValueSource.SelectedIndex = 0;
            for (int i = 0; i < 8; i++)
            {
                var valueTextBox = new DualTextBox
                                       {
                                           TextA = string.Format(CultureInfo.CurrentCulture, "Value {0}", i + 1),
                                           TextB = (i + 1).ToString(CultureInfo.CurrentCulture),
                                           IsChecked = true,
                                           Tag = i
                                       };
                valueTextBox.CheckedChanged += ValueTextBox_CheckedChanged;
                valueTextBox.TextChanged += ValueTextBox_TextChanged;
                ValueContainer.Children.Insert(i, valueTextBox);
            }
            IndependentAxisSettings.SettingsChanged += ValueChanged_UpdateProperties;
            DependentAxisSettings.SettingsChanged += ValueChanged_UpdateProperties;

            UpdateCollection();
        }

        public DataPointSeries Series { get; set; }

        public string ClassName { get; private set; }

        public string DependentValueBindingPath { get; private set; }

        public string IndependentValueBindingPath { get; private set; }

        public ObservableObjectCollection ItemsSource
        {
            get { return Series.ItemsSource as ObservableObjectCollection; }
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

        private void UpdateSeriesBindings()
        {
            Series.IndependentValueBinding = (null != IndependentValueBindingPath)
                                                 ? new Binding(IndependentValueBindingPath)
                                                 : null;
            Series.DependentValueBinding = (null != DependentValueBindingPath)
                                               ? new Binding(DependentValueBindingPath)
                                               : null;
        }

        private void SeriesType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable previousItemsSource = null;
            if (null != Series)
            {
                previousItemsSource = Series.ItemsSource;
                Series.ItemsSource = null;
                if (null != Series.SeriesHost)
                {
                    Series.SeriesHost.Series.Remove(Series);
                }
            }
            var seriesInformation = SeriesType.SelectedItem as SeriesInformation;
            ClassName = seriesInformation.SeriesType.Name;
            Series = Activator.CreateInstance(seriesInformation.SeriesType) as DataPointSeries;
            Series.ItemsSource = previousItemsSource;
            if (null == DependentValueBindingPath)
            {
                IndependentValueBindingPath = seriesInformation.DefaultIndependentValueBindingPath;
            }
            if (null != seriesInformation.IndependentAxisType)
            {
                IndependentAxisSettings.SetAxisType(seriesInformation.IndependentAxisType);
            }
            if (seriesInformation.IndependentAxisOrientation.HasValue)
            {
                IndependentAxisSettings.SetOrientation(seriesInformation.IndependentAxisOrientation.Value);
            }
            if (null != seriesInformation.DependentAxisType)
            {
                DependentAxisSettings.SetAxisType(seriesInformation.DependentAxisType);
            }
            if (seriesInformation.DependentAxisOrientation.HasValue)
            {
                DependentAxisSettings.SetOrientation(seriesInformation.DependentAxisOrientation.Value);
            }
            if (null == Series as DataPointSeriesWithAxes)
            {
                IndependentAxis.IsChecked = false;
                IndependentAxis.IsEnabled = false;
                DependentAxis.IsChecked = false;
                DependentAxis.IsEnabled = false;
            }
            else
            {
                IndependentAxis.IsEnabled = true;
                DependentAxis.IsEnabled = true;
                IndependentAxis_SelectionChanged(null, null);
                DependentAxis_SelectionChanged(null, null);
            }
            UpdateSeriesBindings();
            UpdateProperties();
        }

        private void ValueSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValueType valueType = (ValueSource.SelectedItem as ValueInformation).ValueType;
            Series.ItemsSource = null;
            bool firstTextActive = false;
            if ((ValueType.AutomaticDoubles == valueType) || (ValueType.ManualDoubles == valueType))
            {
                firstTextActive = false;
                var seriesInformation = SeriesType.SelectedItem as SeriesInformation;
                IndependentValueBindingPath = seriesInformation.DefaultIndependentValueBindingPath;
                DependentValueBindingPath = null;
                UpdateSeriesBindings();
            }
            else if (ValueType.ManualPairs == valueType)
            {
                firstTextActive = true;
                IndependentValueBindingPath = "First";
                DependentValueBindingPath = "Second";
                UpdateSeriesBindings();
            }
            Series.ItemsSource = new ObservableObjectCollection();
            if ((ValueType.ManualDoubles == valueType) || (ValueType.ManualPairs == valueType))
            {
                int i = -1;
                foreach (DualTextBox valueTextBox in GetValueContainerDualTextBoxes())
                {
                    valueTextBox.Tag = i;
                    valueTextBox.IsChecked = false;
                    valueTextBox.IsFirstTextActive = firstTextActive;
                    i--;
                }
            }
            UpdateCollection();
        }

        private void ValueTextBox_CheckedChanged(object sender, EventArgs e)
        {
            var valueTextBox = sender as DualTextBox;
            if (!valueTextBox.IsChecked)
            {
                int index = CurrentIndex(valueTextBox);
                if (index < ItemsSource.Count)
                {
                    ItemsSource.RemoveAt(index);
                }
            }
            UpdateCollection();
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            var valueTextBox = sender as DualTextBox;
            if (valueTextBox.IsChecked)
            {
                int index = CurrentIndex(valueTextBox);
                ValueType valueType = (ValueSource.SelectedItem as ValueInformation).ValueType;
                if (ValueType.ManualDoubles == valueType)
                {
                    ItemsSource[index] = ConvertToDouble(valueTextBox.TextB);
                }
                else if (ValueType.ManualPairs == valueType)
                {
                    var pair = ItemsSource[index] as Pair;
                    pair.First = ConvertToDoubleOrDateTimeOrString(valueTextBox.TextA);
                    pair.Second = ConvertToDoubleOrDateTimeOrString(valueTextBox.TextB);
                }
                UpdateCollection();
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults",
            MessageId = "System.Double.TryParse(System.String,System.Double@)",
            Justification = "Use of default value when TryParse fails is okay.")]
        private static double ConvertToDouble(string input)
        {
            double doubleResult = 0.0;
            double.TryParse(input, out doubleResult);
            return doubleResult;
        }

        private static object ConvertToDoubleOrDateTimeOrString(string input)
        {
            double doubleResult = 0.0;
            if (double.TryParse(input, out doubleResult))
            {
                return doubleResult;
            }
            DateTime dateTimeResult;
            if (DateTime.TryParse(input, out dateTimeResult))
            {
                return dateTimeResult.Date;
            }
            return input;
        }

        private void ValueChanged_UpdateProperties(object sender, EventArgs e)
        {
            if (_initialized)
            {
                UpdateProperties();
            }
        }

        private void ValueChanged_UpdateCollection(object sender, EventArgs e)
        {
            if (_initialized)
            {
                UpdateCollection();
            }
        }

        private void IndependentAxis_SelectionChanged(object sender, EventArgs e)
        {
            bool isChecked = IndependentAxis.IsChecked();
            IndependentAxisSettings.Visibility = isChecked ? Visibility.Visible : Visibility.Collapsed;
            Series.SetIndependentAxis(isChecked ? IndependentAxisSettings.Axis : null);
            UpdateProperties();
        }

        private void DependentAxis_SelectionChanged(object sender, EventArgs e)
        {
            bool isChecked = DependentAxis.IsChecked();
            DependentAxisSettings.Visibility = isChecked ? Visibility.Visible : Visibility.Collapsed;
            Series.SetDependentAxis(isChecked ? DependentAxisSettings.Axis : null);
            UpdateProperties();
        }

        private void AllValuesToType_Click(object sender, RoutedEventArgs e)
        {
            int i = 1;
            DateTime now = DateTime.Now.Date;
            foreach (DualTextBox valueTextBox in GetValueContainerDualTextBoxes())
            {
                if (sender == AllStringsA)
                {
                    valueTextBox.TextA = string.Format(CultureInfo.CurrentCulture, "Value {0}", i);
                }
                else if (sender == AllDoublesA)
                {
                    valueTextBox.TextA = i.ToString(CultureInfo.CurrentCulture);
                }
                else if (sender == AllDoublesB)
                {
                    valueTextBox.TextB = i.ToString(CultureInfo.CurrentCulture);
                }
                else if (sender == AllDatesA)
                {
                    valueTextBox.TextA = now.ToShortDateString();
                }
                i++;
                now = now.AddDays(1);
            }
        }

        private void UpdateProperties()
        {
            Series.Title = SeriesTitle.IsChecked ? SeriesTitle.Text : null;
            Series.IsSelectionEnabled = SelectionEnabled.IsChecked();
            OnSettingsChanged();
        }

        public void UpdateCollection()
        {
            if (null != ItemsSource)
            {
                var numberOfPoints = (int) NumberOfPoints.Value;

                ValueType valueType = (ValueSource.SelectedItem as ValueInformation).ValueType;

                foreach (Control control in new Control[] {NumberOfPoints, StartingValue, EndingValue, RandomizeValues})
                {
                    control.Visibility = (ValueType.AutomaticDoubles == valueType)
                                             ? Visibility.Visible
                                             : Visibility.Collapsed;
                }
                ValueContainer.Visibility = (ValueType.AutomaticDoubles == valueType)
                                                ? Visibility.Collapsed
                                                : Visibility.Visible;

                switch (valueType)
                {
                    case ValueType.AutomaticDoubles:
                        double startingValue = StartingValue.Value;
                        double endingValue = EndingValue.Value;
                        bool randomizeValues = RandomizeValues.IsChecked();
                        for (int i = 0; i < numberOfPoints; i++)
                        {
                            double value = randomizeValues
                                               ? startingValue + (_random.NextDouble()*(endingValue - startingValue))
                                               : startingValue +
                                                 (((double) i/Math.Max(numberOfPoints - 1, 1))*
                                                  (endingValue - startingValue));
                            if (i < ItemsSource.Count)
                            {
                                ItemsSource[i] = value;
                            }
                            else
                            {
                                ItemsSource.Add(value);
                            }
                        }
                        while (numberOfPoints < ItemsSource.Count)
                        {
                            ItemsSource.RemoveAt(ItemsSource.Count - 1);
                        }
                        break;
                    case ValueType.ManualDoubles:
                    case ValueType.ManualPairs:
                        int values = 0;
                        foreach (DualTextBox valueTextBox in GetValueContainerDualTextBoxes())
                        {
                            var tag = (int) valueTextBox.Tag;
                            if (valueTextBox.IsChecked)
                            {
                                if (tag < 0)
                                {
                                    valueTextBox.Tag = -tag - 1;
                                    if (ValueType.ManualDoubles == valueType)
                                    {
                                        ItemsSource.Insert(values, ConvertToDouble(valueTextBox.TextB));
                                    }
                                    else if (ValueType.ManualPairs == valueType)
                                    {
                                        ItemsSource.Insert(values,
                                                           new Pair
                                                               {
                                                                   First =
                                                                       ConvertToDoubleOrDateTimeOrString(
                                                                           valueTextBox.TextA),
                                                                   Second =
                                                                       ConvertToDoubleOrDateTimeOrString(
                                                                           valueTextBox.TextB)
                                                               });
                                    }
                                }
                                if (values < ItemsSource.Count)
                                {
                                    values++;
                                }
                            }
                            else
                            {
                                if (0 <= tag)
                                {
                                    valueTextBox.Tag = -tag - 1;
                                }
                            }
                        }
                        break;
                }

                OnSettingsChanged();
            }
        }

        private int CurrentIndex(DualTextBox valueTextBox)
        {
            int index = 0;
            foreach (DualTextBox element in GetValueContainerDualTextBoxes())
            {
                if (element == valueTextBox)
                {
                    break;
                }
                if (0 <= (int) element.Tag)
                {
                    index++;
                }
            }
            return index;
        }

        private IEnumerable<DualTextBox> GetValueContainerDualTextBoxes()
        {
            return ValueContainer.Children.OfType<DualTextBox>();
        }

        private void OnSettingsChanged()
        {
            SettingsChanged.InvokeEmpty(this);
        }

        #region Nested type: SeriesInformation

        private class SeriesInformation
        {
            public SeriesInformation(Type seriesType, string defaultIndependentValueBindingPath,
                                     Type independentAxisType, AxisOrientation? independentAxisOrientation,
                                     Type dependentAxisType, AxisOrientation? dependentAxisOrientation)
            {
                SeriesType = seriesType;
                DefaultIndependentValueBindingPath = defaultIndependentValueBindingPath;
                IndependentAxisType = independentAxisType;
                IndependentAxisOrientation = independentAxisOrientation;
                DependentAxisType = dependentAxisType;
                DependentAxisOrientation = dependentAxisOrientation;
            }

            public Type SeriesType { get; private set; }
            public string DefaultIndependentValueBindingPath { get; set; }
            public Type IndependentAxisType { get; set; }
            public AxisOrientation? IndependentAxisOrientation { get; set; }
            public Type DependentAxisType { get; set; }
            public AxisOrientation? DependentAxisOrientation { get; set; }

            public override string ToString()
            {
                return SeriesType.Name;
            }
        }

        #endregion

        #region Nested type: ValueInformation

        private class ValueInformation
        {
            public ValueInformation(ValueType valueType, string friendlyName)
            {
                ValueType = valueType;
                FriendlyName = friendlyName;
            }

            public ValueType ValueType { get; private set; }
            private string FriendlyName { get; set; }

            public override string ToString()
            {
                return FriendlyName;
            }
        }

        #endregion

        #region Nested type: ValueType

        private enum ValueType
        {
            AutomaticDoubles,
            ManualDoubles,
            ManualPairs,
        }

        #endregion
    }
}