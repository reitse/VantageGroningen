using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public interface IActiveTrackRaceViewModel : ITrackRaceViewModel, IDisposable
    {
        DateTime? StartTime { get; }

        TimeSpan? Correction { get; }

        RaceLaps Laps { get; }

        IObservableCollection<CalculatedLap> EstimatedLaps { get; }

        int NextLapIndex { get; set; }

        event EventHandler Activated;

        event LocalStartEventHandler Started;

        event EventHandler Cleared;

        event EventHandler Committed;

        event EventHandler Deactivated;

        event EventHandler TimeInvalidReasonChanged;

        void UpdateLaps(IEnumerable<RaceLapState> laps);

        void UpdateEstimatedLaps(IReadOnlyList<CalculatedLap> laps);

        void Activate();

        void Start(DateTime localTime);

        void Clear();

        void Commit();

        void Deactivate();

        void AddPassing(RacePassingState passing);

        void UpdatePassing(PresentationSource presentationSource, TimeSpan oldTime, RacePassingState passing);

        void AddLap(RaceLapState lap);

        void UpdateLap(PresentationSource presentationSource, TimeSpan oldTime, RaceLapState lap);
    }
}