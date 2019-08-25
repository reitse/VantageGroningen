using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    [ValueConversion(typeof(int), typeof(Brush))]
    public class PairsRaceColorToBorderBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;
            switch ((PairsRaceColor)(int)value)
            {
                case PairsRaceColor.White:
                    color = Colors.DarkGray;
                    break;
                case PairsRaceColor.Red:
                    color = Colors.DarkRed;
                    break;
                case PairsRaceColor.Yellow:
                    color = Colors.Goldenrod;
                    break;
                case PairsRaceColor.Blue:
                    color = Colors.DarkBlue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}