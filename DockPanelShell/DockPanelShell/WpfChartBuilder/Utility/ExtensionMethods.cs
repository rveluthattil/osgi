using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.Primitives;

namespace Utility
{
    public static class ExtensionMethods
    {
        private static readonly Dictionary<Type, SeriesPropertyInformation> _seriesPropertyInformations =
            new Dictionary<Type, SeriesPropertyInformation>
                {
                    {
                        typeof (AreaSeries),
                        new SeriesPropertyInformation
                            {
                                IndependentAxisProperty = AreaSeries.IndependentAxisProperty,
                                IndependentAxisPropertyName = "IndependentAxis",
                                DependentAxisProperty = AreaSeries.DependentRangeAxisProperty,
                                DependentAxisPropertyName = "DependentRangeAxis"
                            }
                        },
                    {
                        typeof (BarSeries),
                        new SeriesPropertyInformation
                            {
                                IndependentAxisProperty = BarSeries.IndependentAxisProperty,
                                IndependentAxisPropertyName = "IndependentAxis",
                                DependentAxisProperty = BarSeries.DependentRangeAxisProperty,
                                DependentAxisPropertyName = "DependentRangeAxis"
                            }
                        },
                    {
                        typeof (BubbleSeries),
                        new SeriesPropertyInformation
                            {
                                IndependentAxisProperty = BubbleSeries.IndependentAxisProperty,
                                IndependentAxisPropertyName = "IndependentAxis",
                                DependentAxisProperty = BubbleSeries.DependentRangeAxisProperty,
                                DependentAxisPropertyName = "DependentRangeAxis"
                            }
                        },
                    {
                        typeof (ColumnSeries),
                        new SeriesPropertyInformation
                            {
                                IndependentAxisProperty = ColumnSeries.IndependentAxisProperty,
                                IndependentAxisPropertyName = "IndependentAxis",
                                DependentAxisProperty = ColumnSeries.DependentRangeAxisProperty,
                                DependentAxisPropertyName = "DependentRangeAxis"
                            }
                        },
                    {typeof (PieSeries), new SeriesPropertyInformation {}},
                    {
                        typeof (LineSeries),
                        new SeriesPropertyInformation
                            {
                                IndependentAxisProperty = LineSeries.IndependentAxisProperty,
                                IndependentAxisPropertyName = "IndependentAxis",
                                DependentAxisProperty = LineSeries.DependentRangeAxisProperty,
                                DependentAxisPropertyName = "DependentRangeAxis"
                            }
                        },
                    {
                        typeof (ScatterSeries),
                        new SeriesPropertyInformation
                            {
                                IndependentAxisProperty = ScatterSeries.IndependentAxisProperty,
                                IndependentAxisPropertyName = "IndependentAxis",
                                DependentAxisProperty = ScatterSeries.DependentRangeAxisProperty,
                                DependentAxisPropertyName = "DependentRangeAxis"
                            }
                        },
                };

        public static bool IsChecked(this ToggleButton toggleButton)
        {
            return toggleButton.IsChecked.GetValueOrDefault();
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter",
            Justification = "Convenience method.")]
        public static T GetSelectedEnumValue<T>(this Selector selector)
        {
            var orientationString = selector.SelectedItem as string;
            return (T) Enum.Parse(typeof (T), orientationString, false);
        }

        public static void InvokeEmpty(this EventHandler eventHandler, object sender)
        {
            EventHandler handler = eventHandler;
            if (null != handler)
            {
                handler.Invoke(sender, EventArgs.Empty);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "Method only makes sense on DataPointSeries.")]
        public static void SetIndependentAxis(this DataPointSeries series, DisplayAxis axis)
        {
            SeriesPropertyInformation seriesPropertyInformation = _seriesPropertyInformations[series.GetType()];
            if (null != seriesPropertyInformation.IndependentAxisProperty)
            {
                series.SetValue(seriesPropertyInformation.IndependentAxisProperty, axis);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "Method only makes sense on DataPointSeries.")]
        public static DisplayAxis GetIndependentAxis(this DataPointSeries series)
        {
            SeriesPropertyInformation seriesPropertyInformation = _seriesPropertyInformations[series.GetType()];
            if (null != seriesPropertyInformation.IndependentAxisProperty)
            {
                return series.GetValue(seriesPropertyInformation.IndependentAxisProperty) as DisplayAxis;
            }
            return null;
        }

        public static string GetIndependentAxisPropertyName(this DataPointSeries series)
        {
            SeriesPropertyInformation seriesPropertyInformation = _seriesPropertyInformations[series.GetType()];
            return seriesPropertyInformation.IndependentAxisPropertyName;
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "Method only makes sense on DataPointSeries.")]
        public static void SetDependentAxis(this DataPointSeries series, DisplayAxis axis)
        {
            SeriesPropertyInformation seriesPropertyInformation = _seriesPropertyInformations[series.GetType()];
            if (null != seriesPropertyInformation.DependentAxisProperty)
            {
                series.SetValue(seriesPropertyInformation.DependentAxisProperty, axis);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public static DisplayAxis GetDependentAxis(this DataPointSeries series)
        {
            SeriesPropertyInformation seriesPropertyInformation = _seriesPropertyInformations[series.GetType()];
            if (null != seriesPropertyInformation.DependentAxisProperty)
            {
                return series.GetValue(seriesPropertyInformation.DependentAxisProperty) as DisplayAxis;
            }
            return null;
        }

        public static string GetDependentAxisPropertyName(this DataPointSeries series)
        {
            SeriesPropertyInformation seriesPropertyInformation = _seriesPropertyInformations[series.GetType()];
            return seriesPropertyInformation.DependentAxisPropertyName;
        }

        #region Nested type: SeriesPropertyInformation

        private class SeriesPropertyInformation
        {
            public DependencyProperty IndependentAxisProperty { get; set; }
            public string IndependentAxisPropertyName { get; set; }
            public DependencyProperty DependentAxisProperty { get; set; }
            public string DependentAxisPropertyName { get; set; }
        }

        #endregion
    }
}