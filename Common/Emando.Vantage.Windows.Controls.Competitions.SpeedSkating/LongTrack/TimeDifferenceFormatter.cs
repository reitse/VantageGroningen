using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    public class TimeDifferenceFormatter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
                return null;

            var low = ((TimeSpan)values[0]).Ticks / 10000;
            var high = ((TimeSpan)values[1]).Ticks / 10000;
            var sign = high > low ? '+' : '-';
            var diff = Math.Abs(high - low);

            if (diff >= 1000)
                return sign + (diff / 1000D).ToString("N0", culture) + "s";

            return sign + diff.ToString("N0", culture);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}