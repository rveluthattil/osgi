using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using Utility;
#if SILVERLIGHT
using System.Windows.Browser;
#else

#endif

namespace ChartBuilder
{
    public partial class Page : UserControl
    {
        private const string silverlightNamespace = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
        private const string xamlNamespace = "http://schemas.microsoft.com/winfx/2006/xaml";
        private const string systemNamespace = "clr-namespace:System;assembly=mscorlib";

        private const string datavisNamespace =
            "clr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit";

        private const string chartingNamespace =
            "clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit";

        private const string utilityNamespace = "clr-namespace:Utility;assembly=ChartBuilder";

        private readonly List<AxisSettings> _axisSettingsCollection = new List<AxisSettings>();
        private readonly bool _initialized;
        private readonly List<SeriesSettings> _seriesSettingsCollection = new List<SeriesSettings>();

        private readonly XmlWriterSettings _xmlWriterSettings;

        private UIElement _error;

        public Page()
        {
            CurrentPage = this;
            InitializeComponent();
            _initialized = true;

            _xmlWriterSettings = new XmlWriterSettings();
            _xmlWriterSettings.Indent = true;
            _xmlWriterSettings.NewLineOnAttributes = true;
            _xmlWriterSettings.OmitXmlDeclaration = true;

            for (int i = 0; i < (int) NumberOfSeries.Maximum; i++)
            {
                var seriesSettings = new SeriesSettings();
                string seriesTitle = string.Format(CultureInfo.CurrentCulture, "Series {0}", i + 1);
                seriesSettings.SeriesTitle.Text = seriesTitle;
                seriesSettings.IndependentAxisSettings.AxisTitle.Text = string.Format(CultureInfo.CurrentCulture,
                                                                                      "{0} {1}", seriesTitle,
                                                                                      "Independent");
                seriesSettings.IndependentAxisSettings.AxisAxisType.IsEnabled = false;
                seriesSettings.IndependentAxisSettings.AxisOrientation.IsEnabled = false;
                seriesSettings.DependentAxisSettings.AxisTitle.Text = string.Format(CultureInfo.CurrentCulture,
                                                                                    "{0} {1}", seriesTitle, "Dependent");
                seriesSettings.DependentAxisSettings.AxisAxisType.IsEnabled = false;
                seriesSettings.DependentAxisSettings.AxisOrientation.IsEnabled = false;
                seriesSettings.SettingsChanged += ValueChanged_UpdateChart;
                _seriesSettingsCollection.Add(seriesSettings);
            }
            SeriesSettingsContainer.ItemsSource = _seriesSettingsCollection;
            for (int i = 0; i < (int) NumberOfAxes.Maximum; i++)
            {
                var axisSettings = new AxisSettings();
                axisSettings.AxisTitle.Text = string.Format(CultureInfo.CurrentCulture, "Axis {0}", i + 1);
                axisSettings.AxisAxisType.SelectedIndex = i%2;
                axisSettings.AxisOrientation.SelectedIndex = (i + 1)%2;
                axisSettings.SettingsChanged += ValueChanged_UpdateChart;
                _axisSettingsCollection.Add(axisSettings);
            }
            AxisSettingsContainer.ItemsSource = _axisSettingsCollection;
            UpdateSeriesSettings();
            UpdateAxisSettings();
            CreateNewChart();

            var toolTip = new ToolTip();
            ToolTipService.SetToolTip(AxesHeader, toolTip);
            toolTip.Opened += AxesHeader_ToolTip_Opened;
        }

        public Chart Chart { get; private set; }
        public static Page CurrentPage { get; private set; }

        private void ValueChanged_CreateChart(object sender, EventArgs e)
        {
            if (_initialized)
            {
                CallAndReportExceptions(() => { CreateNewChart(); });
            }
        }

        private void ValueChanged_UpdateChart(object sender, EventArgs e)
        {
            if (_initialized)
            {
                CallAndReportExceptions(() =>
                                            {
                                                UpdateChartProperties();
                                                UpdateSeriesSettings();
                                                UpdateAxisSettings();
                                                CreateChartXaml();
                                            });
            }
        }

        private void AxesHeader_ToolTip_Opened(object sender, RoutedEventArgs e)
        {
            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, _xmlWriterSettings))
            {
                writer.WriteStartElement("Chart.ActualAxes");
                foreach (DisplayAxis axis in Chart.ActualAxes)
                {
                    writer.WriteStartElement(axis.GetType().Name);
                    writer.WriteAttributeString("Orientation", axis.Orientation.ToString());
                    writer.WriteAttributeString("Location", axis.Location.ToString());
                    if (null != axis.Title)
                    {
                        writer.WriteAttributeString("Title", axis.Title as string);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
            (sender as ToolTip).Content = sb.ToString();
        }

        private void UpdateChartProperties()
        {
            Chart.Title = ChartTitle.IsChecked ? ChartTitle.Text : null;
            Chart.LegendTitle = LegendTitle.IsChecked ? LegendTitle.Text : null;
        }

        private void UpdateSeriesSettings()
        {
            var numberOfSeries = (int) NumberOfSeries.Value;
            for (int i = 0; i < _seriesSettingsCollection.Count; i++)
            {
                bool seriesInUse = (i < numberOfSeries);
                _seriesSettingsCollection[i].Enabled = seriesInUse;
                if (null != Chart)
                {
                    DataPointSeries series = _seriesSettingsCollection[i].Series;
                    if (seriesInUse)
                    {
                        if (!Chart.Series.Contains(series))
                        {
                            Chart.Series.Insert(i, series);
                        }
                    }
                    else
                    {
                        if (Chart.Series.Contains(series))
                        {
                            Chart.Series.Remove(series);
                        }
                    }
                }
            }
        }

        private void UpdateAxisSettings()
        {
            NumberOfAxes.IsEnabled = (0 == NumberOfSeries.Value);
            var numberOfAxes = (int) NumberOfAxes.Value;
            for (int i = 0; i < _axisSettingsCollection.Count; i++)
            {
                bool axisInUse = (i < numberOfAxes);
                _axisSettingsCollection[i].Enabled = axisInUse;
                if (null != Chart)
                {
                    DisplayAxis axis = _axisSettingsCollection[i].Axis;
                    if (axisInUse)
                    {
                        if (!Chart.Axes.Contains(axis))
                        {
                            Chart.Axes.Add(axis);
                        }
                    }
                    else
                    {
                        if (Chart.Axes.Contains(axis))
                        {
                            Chart.Axes.Remove(axis);
                        }
                    }
                }
            }
        }

        private void CreateNewChart()
        {
            string chartXaml = CreateChartXaml();
            CreateChartFromXaml(chartXaml);
        }

        private void CreateChartFromXaml(string xaml)
        {
            ChartDisplayPanel.Children.Clear();
#if SILVERLIGHT
            var panel = XamlReader.Load(xaml) as Panel;
#else
            Panel panel;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xaml)))
            {
                panel = XamlReader.Load(stream) as Panel;
            }
#endif
            if (AllowXamlEditing.IsChecked())
            {
                Chart = null;
            }
            else
            {
                Chart = panel.Children[0] as Chart;
                for (int i = 0; i < Chart.Series.Count; i++)
                {
                    var series = Chart.Series[i] as DataPointSeries;
                    _seriesSettingsCollection[i].Series = null;
                    _seriesSettingsCollection[i].Series = series;
                }
                for (int i = 0; i < Chart.Axes.Count; i++)
                {
                    var axis = Chart.Axes[i] as DisplayAxis;
                    _axisSettingsCollection[i].Axis = axis;
                }
            }
            ChartDisplayPanel.Children.Add(panel);
        }

        private string CreateChartXaml()
        {
            ChartXamlTextBox.Text = "";

            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, _xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Grid", silverlightNamespace);
                writer.WriteAttributeString("xmlns", "x", null, xamlNamespace);
                writer.WriteAttributeString("xmlns", "sys", null, systemNamespace);
                writer.WriteAttributeString("xmlns", "datavis", null, datavisNamespace);
                writer.WriteAttributeString("xmlns", "charting", null, chartingNamespace);
                writer.WriteAttributeString("xmlns", "utility", null, utilityNamespace);

                writer.WriteStartElement("Chart", chartingNamespace);
                if (ChartTitle.IsChecked)
                {
                    writer.WriteAttributeString("Title", ChartTitle.Text);
                }
                if (LegendTitle.IsChecked)
                {
                    writer.WriteAttributeString("LegendTitle", LegendTitle.Text);
                }
                if (0 < _seriesSettingsCollection.Count)
                {
                    writer.WriteStartElement("Chart.Series", chartingNamespace);
                    foreach (SeriesSettings seriesSettings in _seriesSettingsCollection)
                    {
                        if (seriesSettings.Enabled)
                        {
                            writer.WriteStartElement(seriesSettings.ClassName, chartingNamespace);
                            if (seriesSettings.SeriesTitle.IsChecked)
                            {
                                writer.WriteAttributeString("Title", seriesSettings.SeriesTitle.Text);
                            }
                            if (seriesSettings.SelectionEnabled.IsChecked())
                            {
                                writer.WriteAttributeString("IsSelectionEnabled",
                                                            seriesSettings.SelectionEnabled.IsChecked().ToString());
                            }
                            if (null != seriesSettings.DependentValueBindingPath)
                            {
                                writer.WriteAttributeString("DependentValueBinding",
                                                            string.Format(CultureInfo.InvariantCulture,
                                                                          "{{Binding {0}}}",
                                                                          seriesSettings.DependentValueBindingPath));
                            }
                            if (null != seriesSettings.IndependentValueBindingPath)
                            {
                                writer.WriteAttributeString("IndependentValueBinding",
                                                            string.Format(CultureInfo.InvariantCulture,
                                                                          "{{Binding {0}}}",
                                                                          seriesSettings.IndependentValueBindingPath));
                            }
                            writer.WriteStartElement(seriesSettings.ClassName + ".ItemsSource", chartingNamespace);
                            writer.WriteStartElement("ObservableObjectCollection", utilityNamespace);
                            foreach (object obj in seriesSettings.ItemsSource)
                            {
                                var pair = obj as Pair;
                                if (null != pair)
                                {
                                    writer.WriteStartElement("Pair", utilityNamespace);
                                    writer.WriteAttributeString("First",
                                                                string.Format(CultureInfo.InvariantCulture, "{0}",
                                                                              pair.First));
                                    writer.WriteAttributeString("Second",
                                                                string.Format(CultureInfo.InvariantCulture, "{0}",
                                                                              pair.Second));
                                    writer.WriteEndElement();
                                }
                                else
                                {
                                    writer.WriteElementString(obj.GetType().Name, systemNamespace,
                                                              string.Format(CultureInfo.InvariantCulture, "{0}", obj));
                                }
                            }
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                            if (seriesSettings.IndependentAxis.IsChecked())
                            {
                                writer.WriteStartElement(
                                    seriesSettings.ClassName + "." +
                                    seriesSettings.Series.GetIndependentAxisPropertyName(), chartingNamespace);
                                CreateAxisXaml(writer, seriesSettings.IndependentAxisSettings);
                                writer.WriteEndElement();
                            }
                            if (seriesSettings.DependentAxis.IsChecked())
                            {
                                writer.WriteStartElement(
                                    seriesSettings.ClassName + "." +
                                    seriesSettings.Series.GetDependentAxisPropertyName(), chartingNamespace);
                                CreateAxisXaml(writer, seriesSettings.DependentAxisSettings);
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();
                        }
                    }
                    writer.WriteEndElement();
                }
                if (_axisSettingsCollection.Any(axisSettings => axisSettings.Enabled))
                {
                    writer.WriteStartElement("Chart.Axes", chartingNamespace);
                    foreach (AxisSettings axisSettings in _axisSettingsCollection)
                    {
                        if (axisSettings.Enabled)
                        {
                            CreateAxisXaml(writer, axisSettings);
                        }
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            string chartXaml = sb.ToString();

            ChartXamlTextBox.Text = chartXaml;

            return chartXaml;
        }

        private static void CreateAxisXaml(XmlWriter writer, AxisSettings axisSettings)
        {
            writer.WriteStartElement(axisSettings.AxisType.Name, chartingNamespace);
            writer.WriteAttributeString("Orientation", axisSettings.Axis.Orientation.ToString());
            if (axisSettings.ShowGridLines.IsChecked())
            {
                writer.WriteAttributeString("ShowGridLines", axisSettings.ShowGridLines.IsChecked().ToString());
            }
            if (axisSettings.AxisTitle.IsChecked)
            {
                writer.WriteAttributeString("Title", axisSettings.AxisTitle.Text);
            }
            if (axisSettings.AxisMinimum.IsChecked)
            {
                writer.WriteAttributeString("Minimum",
                                            string.Format(CultureInfo.InvariantCulture, "{0}",
                                                          axisSettings.AxisMinimum.Value));
            }
            if (axisSettings.AxisMaximum.IsChecked)
            {
                writer.WriteAttributeString("Maximum",
                                            string.Format(CultureInfo.InvariantCulture, "{0}",
                                                          axisSettings.AxisMaximum.Value));
            }
            if (axisSettings.AxisInterval.IsChecked)
            {
                writer.WriteAttributeString("Interval",
                                            string.Format(CultureInfo.InvariantCulture, "{0}",
                                                          axisSettings.AxisInterval.Value));
            }
            writer.WriteEndElement();
        }

        private void AllowXamlEditing_Changed(object sender, RoutedEventArgs e)
        {
            bool allowXamlEditing = AllowXamlEditing.IsChecked();
            ChartXamlTextBox.IsReadOnly = !allowXamlEditing;
            //ChartXamlTextBox.Background = allowXamlEditing ? null : Application.Current.Resources["ReadOnlyTextBoxBackground"] as Brush;
            foreach (
                Control control in
                    new Control[]
                        {
                            ChartTitle, LegendTitle, RecreateChart, NumberOfSeries, SeriesSettingsContainer, NumberOfAxes,
                            AxisSettingsContainer
                        })
            {
                control.IsEnabled = !allowXamlEditing;
            }
            if (allowXamlEditing)
            {
                ChartXamlTextBox_TextChanged(null, null);
            }
            else
            {
                CreateNewChart();
            }
        }

        private void ChartXamlTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AllowXamlEditing.IsChecked())
            {
                CallAndReportExceptions(() => { CreateChartFromXaml(ChartXamlTextBox.Text); });
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Deliberately a catch-all method.")]
        private void CallAndReportExceptions(Action action)
        {
            ChartDisplayPanel.Children.Remove(_error);
            try
            {
                action();
            }
            catch (Exception e)
            {
                ReportException(e);
            }
        }

        internal void ReportException(Exception e)
        {
            ChartDisplayPanel.Children.Remove(_error);

            var message = new TextBox
                              {
                                  IsReadOnly = true,
                                  Background = new SolidColorBrush(Color.FromArgb(200, 255, 100, 100)),
                                  HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                                  VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                              };
            message.Text = string.Join("\n",
                                       new[]
                                           {
                                               e.GetType().Name, e.Message, "", e.StackTrace, "",
                                               "Note: Internal Chart state may now be inconsistent."
                                           });
            _error = message;

            ChartDisplayPanel.Children.Add(_error);
        }

        private void BlogLinkMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var uri = new Uri((sender as TextBlock).Text);
#if SILVERLIGHT
            HtmlPage.Window.Navigate(uri, "_blank");
#else
            Process.Start(uri.ToString());
#endif
        }
    }
}