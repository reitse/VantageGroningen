using System;
using System.Globalization;
using System.Windows.Data;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Controls.SpeedSkating.LongTrack
{
    [ValueConversion(typeof(TimeInvalidReason), typeof(string))]
    public class TimeInvalidReasonFormatter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            switch ((TimeInvalidReason)value)
            {
                case TimeInvalidReason.NotStarted:
                    return "NS";
                case TimeInvalidReason.NotFinished:
                    return "NF";
                case TimeInvalidReason.Disqualified:
                    return "DQ";
                case TimeInvalidReason.Withdrawn:
                    return "WD";
                default:
                    return "??";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}