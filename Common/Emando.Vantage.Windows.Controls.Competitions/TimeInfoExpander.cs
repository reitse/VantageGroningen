using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions
{
    [ValueConversion(typeof(TimeInfo?), typeof(IEnumerable<TimeInfo>))]
    public class TimeInfoExpander : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeInfo = (TimeInfo?)value;
            return timeInfo.HasValue ? timeInfo.Value.Expand() : Enumerable.Empty<TimeInfo>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}