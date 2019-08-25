using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Emando.Vantage.Windows.Controls
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToInvisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}