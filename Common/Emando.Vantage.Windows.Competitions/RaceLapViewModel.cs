using System;
using Caliburn.Micro;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public class RaceLapViewModel : PropertyChangedBase, IReadOnlyRaceLap
    {
        private RaceLapState lap;
        private TimeSpan? previousTime;

        public RaceLapViewModel(RaceLapState lap)
        {
            this.lap = lap;
        }

        public string HowShort => lap.PresentationSource.How.Substring(0, 1);

        public string How => lap.PresentationSource.How;

        public TimeSpan LapTime => lap.Time - (previousTime ?? TimeSpan.Zero);

        public TimeSpan? PreviousTime
        {
            get { return previousTime; }
            set
            {
                if (value.Equals(previousTime))
                    return;
                previousTime = value;
                NotifyOfPropertyChange(() => PreviousTime);
                NotifyOfPropertyChange(() => LapTime);
            }
        }

        public int? Index => lap.Index;

        public int? Ranking => lap.Ranking;

        #region IReadOnlyRaceLap Members

        public Guid RaceId => lap.RaceId;

        public string InstanceName => lap.InstanceName;

        public DateTime When => lap.When;

        public TimeSpan Time => lap.Time;

        public RaceEventFlags Flags => lap.Flags;

        public PresentationSource PresentationSource => lap.PresentationSource;

        public decimal? Points => lap.Points;

        public int? FixedIndex => lap.FixedIndex;

        public int? FixedRanking => lap.FixedRanking;

        #endregion

        public void Update(RaceLapState update)
        {
            lap = update;
            NotifyOfPropertyChange();
        }
    }
}