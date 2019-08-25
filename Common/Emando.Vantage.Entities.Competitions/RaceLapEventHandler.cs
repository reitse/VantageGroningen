using System;

namespace Emando.Vantage.Entities.Competitions
{
    public delegate void RaceLapEventHandler(object sender, RaceLapEventArgs e);

    public class RaceLapEventArgs : RaceEventArgs
    {
        public RaceLapEventArgs(RaceLap lap) : base(lap.Race)
        {
            Lap = lap;
        }

        public RaceLap Lap { get; }
    }
}