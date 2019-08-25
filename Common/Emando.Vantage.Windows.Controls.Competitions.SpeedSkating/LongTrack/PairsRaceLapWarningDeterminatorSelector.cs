using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using Emando.Vantage.Windows.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    [ValueConversion(typeof(RaceLapsGroup), typeof(string))]
    public class PairsRaceLapWarningDeterminatorSelector : IValueConverter
    {
        private static readonly IDictionary<string, IValueConverter> Converters = new Dictionary<string, IValueConverter>
        {
            { "SpeedSkating.LongTrack.PairsDistance.Individual", new IndividualPairsRaceLapWarningDeterminator() },
            { "SpeedSkating.LongTrack.PairsDistance.TeamPursuit", new TeamPairsRaceLapWarningDeterminator() },
            { "SpeedSkating.LongTrack.PairsDistance.TeamSprint", new TeamPairsRaceLapWarningDeterminator() }
        };

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var group = value as RaceLapsGroup;
            if (group == null)
                return null;

            IValueConverter converter;
            if (Converters.TryGetValue(group.Race.Distance.Discipline, out converter))
                return converter.Convert(value, targetType, parameter, culture);

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}