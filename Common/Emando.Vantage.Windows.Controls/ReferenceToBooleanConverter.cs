using System;
using System.Globalization;
using System.Windows.Data;

namespace Emando.Vantage.Windows.Controls
{
    [ValueConversion(typeof(object), typeof(bool))]
    public class ReferenceToBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}