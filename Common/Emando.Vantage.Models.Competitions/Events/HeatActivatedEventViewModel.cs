namespace Emando.Vantage.Models.Competitions.Events
{
    public class HeatActivatedEventViewModel : HeatEventViewModelBase
    {
        public RaceStateViewModel[] Races { get; set; }
    }
}