using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Emando.Vantage.Windows.Controls
{
    [ValueConversion(typeof(Enum), typeof(bool), ParameterType = typeof(string))]
    public class EnumValueToBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return parameter == null;

            var parameterString = parameter as string;
            if (parameterString == null)
                return DependencyProperty.UnsetValue;

            if (!Enum.IsDefined(value.GetType(), parameterString))
                return DependencyProperty.UnsetValue;

            var flag = (Enum)Enum.Parse(value.GetType(), parameterString);
            return value.Equals(flag);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}