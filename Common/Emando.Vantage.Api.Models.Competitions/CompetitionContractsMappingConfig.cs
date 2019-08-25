using System;
using System.Linq;
using AutoMapper;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Entities.Competitions.Events;
using Emando.Vantage.Entities.Events;
using Emando.Vantage.Competitions.ViewModels.Events;
using Emando.Vantage.ViewModels;

namespace Emando.Vantage.Competitions.ViewModels
{
    public static class CompetitionContractsMappingConfig
    {
        public static void Register()
        {
            ViewModelsConfig.Register();

            Mapper.CreateMap<Competition, CompetitionViewModel>();
            Mapper.CreateMap<Distance, DistanceViewModel>();
            Mapper.CreateMap<ValidDistance, ValidDistanceViewModel>();
            Mapper.CreateMap<PersonCompetitorList, PersonCompetitorListViewModel>();
            Mapper.CreateMap<TeamCompetitorList, TeamCompetitorListViewModel>();
            Mapper.CreateMap<CompetitorBase, CompetitorViewModel>()
                .Include<PersonCompetitor, PersonCompetitorViewModel>()
                .Include<TeamCompetitor, TeamCompetitorViewModel>();
            Mapper.CreateMap<PersonCompetitor, PersonCompetitorViewModel>();
            Mapper.CreateMap<TeamCompetitor, TeamCompetitorViewModel>();
            Mapper.CreateMap<TeamCompetitorMember, TeamCompetitorMemberViewModel>();
            Mapper.CreateMap<DistanceCombination, DistanceCombinationViewModel>();
            Mapper.CreateMap<Distance, DistanceCombinationDistanceViewModel>();
            Mapper.CreateMap<DistanceCombinationCompetitor, DistanceCombinationCompetitorViewModel>();
            Mapper.CreateMap<DistanceCombinationCompetitor, CompetitorDistanceCombinationViewModel>();
            Mapper.CreateMap<Race, RaceViewModel>()
                .ForMember(r => r.Result, o => o.MapFrom(r => r.PresentedResult))
                .ForMember(r => r.Time, o => o.MapFrom(r => r.PresentedTime));
            Mapper.CreateMap<Race, RaceViewModel>();
            Mapper.CreateMap<Race, RaceChangeViewModel>();
            Mapper.CreateMap<Race, RaceDetailsViewModel>()
                .ForMember(r => r.Laps, o => o.MapFrom(r => r.Laps.GroupBy(l => l.InstanceName)));
            Mapper.CreateMap<IGrouping<string, RaceLap>, InstanceRaceLapsViewModel>()
                .ForMember(g => g.InstanceName, o => o.MapFrom(g => g.Key))
                .ForMember(g => g.Groups, o => o.MapFrom(g => g.GroupByPresented()));
            Mapper.CreateMap<RacePassing, RacePassingViewModel>();
            Mapper.CreateMap<IReadOnlyRacePassing, RacePassingViewModel>();
            Mapper.CreateMap<IGrouping<RaceLap, RaceLap>, RaceLapGroupViewModel>()
                .ForMember(r => r.Presented, o => o.MapFrom(l => l.Key))
                .ForMember(r => r.NotPresented, o => o.MapFrom(l => l));
            Mapper.CreateMap<Race, CompetitorRaceViewModel>()
                .ForMember(r => r.Result, o => o.MapFrom(r => r.PresentedResult))
                .ForMember(r => r.Time, o => o.MapFrom(r => r.PresentedTime));
            Mapper.CreateMap<RaceResult, RaceResultViewModel>();
            Mapper.CreateMap<RaceTime, RaceTimeViewModel>();
            Mapper.CreateMap<RaceLap, RaceLapViewModel>();
            Mapper.CreateMap<IReadOnlyRaceLap, RaceLapViewModel>();
            Mapper.CreateMap<RaceTransponder, RaceTransponderViewModel>()
                .ForMember(r => r.Label, o => o.MapFrom(r => r.Transponder.Label));
            Mapper.CreateMap<Weather, WeatherViewModel>();

            Mapper.CreateMap<EventBase, EventViewModelBase>()
                .Include<InstanceSwitchedEvent, InstanceSwitchedEventViewModel>()
                .Include<CompetitionEventBase, CompetitionEventViewModelBase>()
                .Include<CompetitionAddedEvent, CompetitionAddedEventViewModel>()
                .Include<CompetitionChangedEvent, CompetitionChangedEventViewModel>()
                .Include<CompetitionDeletedEvent, CompetitionDeletedEventViewModel>()
                .Include<DistanceEventBase, DistanceEventViewModelBase>()
                .Include<DistanceActivatedEvent, DistanceActivatedEventViewModel>()
                .Include<DistanceDeactivatedEvent, DistanceDeactivatedEventViewModel>()
                .Include<DistanceRacesAddedEvent, DistanceRacesAddedEventViewModel>()
                .Include<DistanceRacesChangedEvent, DistanceRacesChangedEventViewModel>()
                .Include<DistanceRacesDeletedEvent, DistanceRacesDeletedEventViewModel>()
                .Include<HeatEventBase, HeatEventViewModelBase>()
                .Include<HeatActivatedEvent, HeatActivatedEventViewModel>()
                .Include<HeatClearedEvent, HeatClearedEventViewModel>()
                .Include<HeatStartedEvent, HeatStartedEventViewModel>()
                .Include<HeatCommittedEvent, HeatCommittedEventViewModel>()
                .Include<HeatDeactivatedEvent, HeatDeactivatedEventViewModel>()
                .Include<RaceEventBase, RaceEventViewModelBase>()
                .Include<RacePassingAddedEvent, RacePassingAddedEventViewModel>()
                .Include<RacePassingUpdatedEvent, RacePassingUpdatedEventViewModel>()
                .Include<RaceLapAddedEvent, RaceLapAddedEventViewModel>()
                .Include<RaceLapUpdatedEvent, RaceLapUpdatedEventViewModel>();

            Mapper.CreateMap<InstanceSwitchedEvent, InstanceSwitchedEventViewModel>();
            Mapper.CreateMap<CompetitionAddedEvent, CompetitionAddedEventViewModel>();
            Mapper.CreateMap<CompetitionChangedEvent, CompetitionChangedEventViewModel>();
            Mapper.CreateMap<CompetitionDeletedEvent, CompetitionDeletedEventViewModel>();
            Mapper.CreateMap<DistanceActivatedEvent, DistanceActivatedEventViewModel>();
            Mapper.CreateMap<DistanceDeactivatedEvent, DistanceDeactivatedEventViewModel>();
            Mapper.CreateMap<DistanceRacesAddedEvent, DistanceRacesAddedEventViewModel>();
            Mapper.CreateMap<DistanceRacesChangedEvent, DistanceRacesChangedEventViewModel>();
            Mapper.CreateMap<DistanceRacesDeletedEvent, DistanceRacesDeletedEventViewModel>();
            Mapper.CreateMap<HeatActivatedEvent, HeatActivatedEventViewModel>();
            Mapper.CreateMap<HeatClearedEvent, HeatClearedEventViewModel>();
            Mapper.CreateMap<HeatStartedEvent, HeatStartedEventViewModel>()
                .ForMember(v => v.Clock, o => o.ResolveUsing(e => DateTime.UtcNow - e.Started));
            Mapper.CreateMap<HeatCommittedEvent, HeatCommittedEventViewModel>();
            Mapper.CreateMap<HeatDeactivatedEvent, HeatDeactivatedEventViewModel>();
            Mapper.CreateMap<RacePassingAddedEvent, RacePassingAddedEventViewModel>();
            Mapper.CreateMap<RacePassingUpdatedEvent, RacePassingUpdatedEventViewModel>();
            Mapper.CreateMap<RaceLapAddedEvent, RaceLapAddedEventViewModel>();
            Mapper.CreateMap<RaceLapUpdatedEvent, RaceLapUpdatedEventViewModel>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}