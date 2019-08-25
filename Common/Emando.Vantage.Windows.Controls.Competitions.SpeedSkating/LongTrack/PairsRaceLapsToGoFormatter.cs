using System;
using System.Globalization;
using System.Windows.Data;
using Emando.Vantage.Windows.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    [ValueConversion(typeof(RaceLapsGroup), typeof(string))]
    public class PairsRaceLapsToGoFormatter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var group = value as RaceLapsGroup;
            return @group != null ? $"{@group.PassedLength:0}m/{@group.RoundsToGo:0.#}" : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}