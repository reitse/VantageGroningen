namespace Emando.Vantage.Models.Competitions.Events
{
    public class RaceNextLapIndexChangedEventViewModel : RaceEventViewModelBase
    {
        public int Index { get; set; }

        public decimal Rounds { get; set; }

        public decimal RoundsToGo { get; set; }

        public int PassedLength { get; set; }
    }
}