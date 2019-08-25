using System;
using System.Collections.Generic;
using System.ComponentModel;
using Caliburn.Micro;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public interface IActiveHeatViewModel
    {
        Distance Distance { get; }

        Heat? Heat { get; }

        bool HasResults { get; }

        IObservableCollection<IActiveTrackRaceViewModel> Races { get; }

        ICollectionView RacesView { get; }

        bool CanDeactivateAsync { get; }

        bool CanCommitAsync { get; }

        bool CanClearAsync { get; }

        decimal? Rounds { get; }

        decimal? RoundsToGo { get; }

        DateTime? ApplianceStartTime { get; }

        TimeSpan? Clock { get; }

        IObservableCollection<UnhandledPassingViewModel> UnhandledPassings { get; }

        event EventHandler Activated;

        event EventHandler Cleared;

        event EventHandler Deactivated;

        event LocalStartEventHandler Started;

        event EventHandler Committed;

        void Activate(Heat heat, IReadOnlyCollection<IActiveTrackRaceViewModel> activatedRaces);

        void Clear();

        void Deactivate();

        void Start(DateTime applianceTime, DateTime localTime);

        void Commit();

        void SetNextLapIndex(int index);

        void AddRacePassing(RacePassingState passing);

        void UpdateRacePassing(PresentationSource presentationSource, TimeSpan oldTime, RacePassingState passing);

        void AddRaceLap(RaceLapState lap);

        void UpdateRaceLap(PresentationSource presentationSource, TimeSpan oldTime, RaceLapState lap);

        void SetNextRaceLapIndex(Guid raceId, int index);

        void UpdateTimeInfo(Guid raceId, TimeInfo timeInfo);

        void UpdateTimeInvalidReason(Guid raceId, TimeInvalidReason? timeInvalidReason);

        void UpdateEstimatedLaps(Guid raceId, CalculatedLap[] estimatedLaps);
        void AddUnhandledPassing(UnhandledPassing passing);
    }
}