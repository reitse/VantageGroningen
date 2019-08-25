namespace Emando.Vantage.Models.Competitions.Events
{
    public class HeatCommittedEventViewModel : HeatEventViewModelBase
    {
        public RaceStateViewModel[] Races { get; set; }
    }
}