using System.Linq;
using AutoMapper;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Events;
using Emando.Vantage.Models.Competitions;
using Emando.Vantage.Models.Competitions.Events;
using Emando.Vantage.Models.Events;
using Emando.Vantage.Workflows.Competitions;
using Emando.Vantage.Workflows.Competitions.Events;
using Newtonsoft.Json;

namespace Emando.Vantage.Services.Competitions
{
    public static class CompetitionModelsMappingConfig
    {
        public static void Register()
        {
            Mapper.CreateMap<CompetitionSerie, CompetitionSerieViewModel>();
            Mapper.CreateMap<CompetitionSerie, CompetitionSerieDetailsViewModel>();
            Mapper.CreateMap<Competition, CompetitionViewModel>();
            Mapper.CreateMap<Competition, CompetitionStructureViewModel>();
            Mapper.CreateMap<Distance, DistanceViewModel>();
            Mapper.CreateMap<Distance, DistanceCombinationDistanceViewModel>();
            Mapper.CreateMap<DistancePointsTable, DistancePointsTableViewModel>();
            Mapper.CreateMap<DistancePoints, DistancePointsViewModel>();
            Mapper.CreateMap<DistanceDrawSettings, DistanceDrawSettingsViewModel>()
                .ForMember(m => m.Selectors, c => c.ResolveUsing(s => JsonConvert.DeserializeObject<HistoricalTimeSelectorViewModel[]>(s.Selectors)));
            Mapper.CreateMap<ValidDistance, ValidDistanceViewModel>();
            Mapper.CreateMap<CompetitorListBase, CompetitorListViewModel>()
                .Include<PersonCompetitorList, PersonCompetitorListViewModel>()
                .Include<TeamCompetitorList, TeamCompetitorListViewModel>();
            Mapper.CreateMap<CompetitorListBase, CompetitorListDetailsViewModel>()
                .Include<PersonCompetitorList, PersonCompetitorListDetailsViewModel>()
                .Include<TeamCompetitorList, TeamCompetitorListDetailsViewModel>();
            Mapper.CreateMap<PersonCompetitorList, PersonCompetitorListViewModel>();
            Mapper.CreateMap<PersonCompetitorList, PersonCompetitorListDetailsViewModel>();
            Mapper.CreateMap<TeamCompetitorList, TeamCompetitorListViewModel>();
            Mapper.CreateMap<TeamCompetitorList, TeamCompetitorListDetailsViewModel>();
            Mapper.CreateMap<CompetitorBase, CompetitorViewModel>()
                .Include<PersonCompetitor, PersonCompetitorViewModel>()
                .Include<TeamCompetitor, TeamCompetitorViewModel>();
            Mapper.CreateMap<CompetitorBase, CompetitorDetailsViewModel>()
                .Include<PersonCompetitor, PersonCompetitorDetailsViewModel>()
                .Include<TeamCompetitor, TeamCompetitorDetailsViewModel>();
            Mapper.CreateMap<PersonCompetitor, PersonCompetitorViewModel>();
            Mapper.CreateMap<PersonCompetitor, PersonCompetitorDetailsViewModel>();
            Mapper.CreateMap<TeamCompetitor, TeamCompetitorViewModel>();
            Mapper.CreateMap<TeamCompetitor, TeamCompetitorDetailsViewModel>();
            Mapper.CreateMap<TeamCompetitorMember, TeamCompetitorMemberViewModel>();
            Mapper.CreateMap<DrawCompetitor, DrawCompetitorViewModel>();
            Mapper.CreateMap<ClassifiedCompetitor, ClassifiedCompetitorViewModel>();
            Mapper.CreateMap<IPersonLicenseTime, HistoricalTimeViewModel>();
            Mapper.CreateMap<DistanceCombination, DistanceCombinationViewModel>();
            Mapper.CreateMap<DistanceCombination, DistanceCombinationCompetitorsViewModel>();
            Mapper.CreateMap<DistanceCombination, DistanceDistanceCombinationViewModel>();
            Mapper.CreateMap<DistanceCombinationCompetitor, DistanceCombinationCompetitorViewModel>();
            Mapper.CreateMap<DistanceCombinationCompetitor, CompetitorDistanceCombinationViewModel>();
            Mapper.CreateMap<Heat, HeatViewModel>();
            Mapper.CreateMap<HeatViewModel, Heat>();
            Mapper.CreateMap<Lap, LapViewModel>();
            Mapper.CreateMap<Passing, PassingViewModel>()
                .Include<RacePassing, RacePassingViewModel>();
            Mapper.CreateMap<Race, RaceViewModel>()
                .ForMember(r => r.Result, o => o.MapFrom(r => r.PresentedResult))
                .ForMember(r => r.Time, o => o.MapFrom(r => r.PresentedTime))
                .ForMember(r => r.Laps, o => o.MapFrom(r => r.PresentedLaps));
            Mapper.CreateMap<Race, RaceChangeViewModel>();
            Mapper.CreateMap<Race, RaceDetailsViewModel>()
                .ForMember(r => r.Laps, o => o.MapFrom(r => r.Laps.GroupBy(l => l.InstanceName, l => (IReadOnlyRaceLap)l)));
            Mapper.CreateMap<IGrouping<string, IReadOnlyRaceLap>, InstanceRaceLapsViewModel>()
                .ForMember(g => g.InstanceName, o => o.MapFrom(g => g.Key))
                .ForMember(g => g.Groups, o => o.MapFrom(g => g.GroupByPresented()));
            Mapper.CreateMap<IGrouping<IReadOnlyRaceLap, IReadOnlyRaceLap>, RaceLapGroupViewModel>()
                .ForMember(r => r.Presented, o => o.MapFrom(l => l.Key))
                .ForMember(r => r.NotPresented, o => o.MapFrom(l => l));
            Mapper.CreateMap<Race, CompetitorRaceViewModel>()
                .ForMember(r => r.Result, o => o.MapFrom(r => r.PresentedResult))
                .ForMember(r => r.Time, o => o.MapFrom(r => r.PresentedTime));
            Mapper.CreateMap<RankedRace, RankedRaceViewModel>();
            Mapper.CreateMap<RaceStart, RaceStartViewModel>();
            Mapper.CreateMap<RacePassing, RacePassingViewModel>();
            Mapper.CreateMap<IReadOnlyRacePassing, PassingViewModel>();
            Mapper.CreateMap<IReadOnlyRacePassing, RacePassingViewModel>();
            Mapper.CreateMap<RaceResult, RaceResultViewModel>();
            Mapper.CreateMap<RaceTime, RaceTimeViewModel>();
            Mapper.CreateMap<RaceLap, RaceLapViewModel>();
            Mapper.CreateMap<IReadOnlyRaceLap, RaceLapViewModel>();
            Mapper.CreateMap<ICalculatedLap, CalculatedLapViewModel>();
            Mapper.CreateMap<RaceLapState, RaceLapPointsViewModel>();
            Mapper.CreateMap<RaceTransponder, RaceTransponderViewModel>()
                .ForMember(t => t.Label, o => o.MapFrom(t => t.Transponder.Label));
            Mapper.CreateMap<RaceState, RaceStateViewModel>();
            Mapper.CreateMap<Weather, WeatherViewModel>();
            Mapper.CreateMap<PersonTime, PersonTimeViewModel>();
            Mapper.CreateMap<PersonTime, PersonLicenseTimeViewModel>();
            Mapper.CreateMap<RecordTime, RecordTimeViewModel>();
            Mapper.CreateMap<RankedPersonTime, RankedPersonTimeViewModel>();
            Mapper.CreateMap<RankedPersonPoints, RankedPersonPointsViewModel>();

            Mapper.CreateMap<EventBase, EventViewModelBase>()
                .Include<InstanceSwitchedEvent, InstanceSwitchedEventViewModel>()
                .Include<HeartbeatEvent, HeartbeatEventViewModel>()
                .Include<RecoverBeginEvent, RecoverBeginEventViewModel>()
                .Include<RecoverEndEvent, RecoverEndEventViewModel>()
                .Include<CompetitionEventBase, CompetitionEventViewModelBase>()
                .Include<CompetitionAddedEvent, CompetitionAddedEventViewModel>()
                .Include<CompetitionChangedEvent, CompetitionChangedEventViewModel>()
                .Include<CompetitionDeletedEvent, CompetitionDeletedEventViewModel>()
                .Include<DistanceEventBase, DistanceEventViewModelBase>()
                .Include<DistanceRacesAddedEvent, DistanceRacesAddedEventViewModel>()
                .Include<DistanceRacesChangedEvent, DistanceRacesChangedEventViewModel>()
                .Include<DistanceRacesDeletedEvent, DistanceRacesDeletedEventViewModel>()
                .Include<DistanceDrawChangedEvent, DistanceDrawChangedEventViewModel>()
                .Include<DistanceResultChangedEvent, DistanceResultChangedEventViewModel>()
                .Include<DistanceCombinationClassificationResultChangedEvent, DistanceCombinationClassificationResultChangedEventViewModel>();

            Mapper.CreateMap<InstanceSwitchedEvent, InstanceSwitchedEventViewModel>();
            Mapper.CreateMap<HeartbeatEvent, HeartbeatEventViewModel>();
            Mapper.CreateMap<RecoverBeginEvent, RecoverBeginEventViewModel>();
            Mapper.CreateMap<RecoverEndEvent, RecoverEndEventViewModel>();
            Mapper.CreateMap<CompetitionAddedEvent, CompetitionAddedEventViewModel>();
            Mapper.CreateMap<CompetitionChangedEvent, CompetitionChangedEventViewModel>();
            Mapper.CreateMap<CompetitionDeletedEvent, CompetitionDeletedEventViewModel>();
            Mapper.CreateMap<DistanceRacesAddedEvent, DistanceRacesAddedEventViewModel>();
            Mapper.CreateMap<DistanceRacesChangedEvent, DistanceRacesChangedEventViewModel>();
            Mapper.CreateMap<DistanceRacesDeletedEvent, DistanceRacesDeletedEventViewModel>();
            Mapper.CreateMap<DistanceDrawChangedEvent, DistanceDrawChangedEventViewModel>();
            Mapper.CreateMap<DistanceResultChangedEvent, DistanceResultChangedEventViewModel>();
            Mapper.CreateMap<DistanceCombinationClassificationResultChangedEvent, DistanceCombinationClassificationResultChangedEventViewModel>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}