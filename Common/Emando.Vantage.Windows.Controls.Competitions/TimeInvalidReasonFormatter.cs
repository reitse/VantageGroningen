using System;
using System.Globalization;
using System.Windows.Data;
using Emando.Vantage.Competitions;
using Emando.Vantage.Windows.Controls.Competitions.Properties;

namespace Emando.Vantage.Windows.Controls.Competitions
{
    [ValueConversion(typeof(TimeInvalidReason?), typeof(string))]
    public class TimeInvalidReasonFormatter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeInvalidReason = (TimeInvalidReason?)value;
            if (!timeInvalidReason.HasValue)
                return null;

            return Resources.ResourceManager.GetString($"TimeInvalidReason_{(int)timeInvalidReason.Value}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}