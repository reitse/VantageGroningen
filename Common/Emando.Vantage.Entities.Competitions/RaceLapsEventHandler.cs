using System.Collections.Generic;

namespace Emando.Vantage.Entities.Competitions
{
    public delegate void RaceLapsEventHandler(object sender, RaceLapsEventArgs e);

    public class RaceLapsEventArgs : RaceEventArgs
    {
        public RaceLapsEventArgs(Race race, IReadOnlyList<RaceLap> laps) : base(race)
        {
            this.Laps = laps;
        }

        public IReadOnlyList<RaceLap> Laps { get; }
    }
}