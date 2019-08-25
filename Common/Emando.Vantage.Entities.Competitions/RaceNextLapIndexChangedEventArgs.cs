using Emando.Vantage.Competitions;

namespace Emando.Vantage.Entities.Competitions
{
    public delegate void RaceNextLapIndexChangedEventHandler(object sender, RaceNextLapIndexChangedEventArgs e);

    public class RaceNextLapIndexChangedEventArgs : RaceEventArgs
    {
        public RaceNextLapIndexChangedEventArgs(Race race, int index) : base(race)
        {
            Index = index;
        }

        public int Index { get; }
    }
}