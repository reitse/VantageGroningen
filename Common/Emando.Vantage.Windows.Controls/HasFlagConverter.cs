using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Emando.Vantage.Windows.Controls
{
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class HasFlagConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            var flag = (Enum)Enum.Parse(value.GetType(), parameterString);
            return ((Enum)value).HasFlag(flag);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}