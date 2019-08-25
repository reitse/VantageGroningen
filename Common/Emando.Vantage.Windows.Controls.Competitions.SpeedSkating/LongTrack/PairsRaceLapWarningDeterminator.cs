using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Emando.Vantage.Windows.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    [ValueConversion(typeof(RaceLapsGroup), typeof(bool))]
    public class PairsRaceLapWarningDeterminator : IValueConverter
    {
        protected virtual TimeSpan DefaultTimeDifferenceThreshold => TimeSpan.FromMilliseconds(10);

        protected virtual TimeSpan ManualTimeDifferenceThreshold => TimeSpan.FromMilliseconds(500);

        protected virtual TimeSpan MinimumLapTime => TimeSpan.FromSeconds(20);

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var group = value as RaceLapsGroup;
            if (group == null)
                return null;

            return IsWarning(group);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected virtual bool IsWarning(RaceLapsGroup group)
        {
            return group.Presented == null
                || group.NotPresented.Any(p => p.PresentationSource.How != "Manual" && (p.Time - group.Presented.Time).Duration() >= DefaultTimeDifferenceThreshold)
                || group.NotPresented.Any(p => p.PresentationSource.How == "Manual" && (p.Time - group.Presented.Time).Duration() >= ManualTimeDifferenceThreshold)
                || group.IsExcess
                || group.Index > 0 && group.Presented.LapTime < MinimumLapTime;
        }
    }
}