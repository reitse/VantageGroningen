using System;
using System.Globalization;
using System.Windows.Data;
using Emando.Vantage.Competitions;
using Emando.Vantage.Windows.Controls.Competitions.Properties;

namespace Emando.Vantage.Windows.Controls.Competitions
{
    [ValueConversion(typeof(TimeInfo?), typeof(string))]
    public class TimeInfoFormatter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeInfo = (TimeInfo?)value;
            if (!timeInfo.HasValue)
                return null;

            return Resources.ResourceManager.GetString($"TimeInfo_{(int)timeInfo.Value}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}