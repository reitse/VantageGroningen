using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    public delegate void CalculatedRaceLapEventHandler(object sender, CalculatedRaceLapEventArgs e);

    public class CalculatedRaceLapEventArgs : RaceEventArgs
    {
        public CalculatedRaceLapEventArgs(Race race, ICalculatedLap lap) : base(race)
        {
            Lap = lap;
        }

        public ICalculatedLap Lap { get; set; }
    }
}