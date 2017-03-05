using System;
using System.ComponentModel;
using System.Globalization;

namespace Utility
{
    public class Pair : INotifyPropertyChanged
    {
        private object _first;

        private object _second;

        public object First
        {
            get { return _first; }
            set
            {
                if (!Equals(_first, value))
                {
                    _first = ConvertedValue(value);
                    OnPropertyChanged("First");
                }
            }
        }

        public object Second
        {
            get { return _second; }
            set
            {
                if (!Equals(_second, value))
                {
                    _second = ConvertedValue(value);
                    OnPropertyChanged("Second");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private static object ConvertedValue(object value)
        {
            object convertedValue = value;
            var valueString = value as string;
            if (null != valueString)
            {
                double valueDouble;
                DateTime valueDateTime;
                if (double.TryParse(valueString, NumberStyles.Number, CultureInfo.InvariantCulture, out valueDouble))
                {
                    convertedValue = valueDouble;
                }
                else if (DateTime.TryParse(valueString, CultureInfo.InvariantCulture, DateTimeStyles.None,
                                           out valueDateTime))
                {
                    convertedValue = valueDateTime;
                }
            }
            return convertedValue;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}