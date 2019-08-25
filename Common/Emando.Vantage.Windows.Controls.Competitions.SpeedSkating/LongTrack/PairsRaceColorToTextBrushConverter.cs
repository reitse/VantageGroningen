using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    [ValueConversion(typeof(int), typeof(Brush))]
    public class PairsRaceColorToTextBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((PairsRaceColor)(int)value)
            {
                case PairsRaceColor.White:
                case PairsRaceColor.Yellow:
                    return new SolidColorBrush(Colors.Black);
                case PairsRaceColor.Red:
                case PairsRaceColor.Blue:
                    return new SolidColorBrush(Colors.White);
                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}