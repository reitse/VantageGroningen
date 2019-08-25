using System;
using System.Windows.Data;
using Emando.Vantage.Windows.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    [ValueConversion(typeof(RaceLapsGroup), typeof(bool))]
    public class TeamPairsRaceLapWarningDeterminator : PairsRaceLapWarningDeterminator
    {
        protected override TimeSpan MinimumLapTime => TimeSpan.FromSeconds(10);
    }
}