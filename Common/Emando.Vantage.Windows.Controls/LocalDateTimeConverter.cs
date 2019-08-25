using System;
using System.Globalization;
using System.Windows.Data;

namespace Emando.Vantage.Windows.Controls
{
    [ValueConversion(typeof(DateTime), typeof(DateTime))]
    public class LocalDateTimeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime?)value)?.ToLocalTime();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}