using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    [ValueConversion(typeof(TimeSpan), typeof(string), ParameterType = typeof(string))]
    public class TimeFormatter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (TimeSpan?)value;
            if (!time.HasValue)
                return null;

            var digits = 2;
            var intParameter = parameter as int?;
            if (intParameter != null)
                digits = intParameter.Value;
            else
            {
                var stringParameter = parameter as string;
                if (stringParameter != null)
                    int.TryParse(stringParameter, out digits);
            }

            if (time.Value.TotalMinutes < 1)
                return time.Value.ToString("s\\." + new string('f', digits), CultureInfo.GetCultureInfo("en-US"));

            return time.Value.ToString("m\\:ss\\." + new string('f', digits), CultureInfo.GetCultureInfo("en-US"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var regex = new Regex("^((\\d{1,2})[\\:\\.])?(\\d{1,2})(\\.(\\d{1,3}))?$");
            var match = regex.Match((string)value);
            if (!match.Success)
                return DependencyProperty.UnsetValue;

            int minutes = 0;
            int seconds;
            string millisecondsText = "";
            if (match.Groups[4].Success && match.Groups[5].Success)
            {
                minutes = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;
                seconds = int.Parse(match.Groups[3].Value);
                millisecondsText = match.Groups[5].Value;
            }
            else if (match.Groups[2].Success)
            {
                seconds = int.Parse(match.Groups[2].Value);
                millisecondsText = match.Groups[3].Value;
            }
            else
                seconds = int.Parse(match.Groups[3].Value);

            int milliseconds = 0;
            switch (millisecondsText.Length)
            {
                case 1:
                    milliseconds = int.Parse(millisecondsText) * 100;
                    break;
                case 2:
                    milliseconds = int.Parse(millisecondsText) * 10;
                    break;
                case 3:
                    milliseconds = int.Parse(millisecondsText);
                    break;
            }

            return new TimeSpan(0, 0, minutes, seconds, milliseconds);
        }

        #endregion
    }
}