using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    [ValueConversion(typeof(int), typeof(Brush), ParameterType = typeof(string))]
    public class PairsRaceColorToBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color;
            switch ((PairsRaceColor)(int)value)
            {
                case PairsRaceColor.White:
                    color = Colors.White;
                    break;
                case PairsRaceColor.Red:
                    color = Colors.Red;
                    break;
                case PairsRaceColor.Yellow:
                    color = Colors.Yellow;
                    break;
                case PairsRaceColor.Blue:
                    color = Colors.Blue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }

            byte alpha;
            if (byte.TryParse(parameter as string, out alpha))
                color.A = alpha;

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}