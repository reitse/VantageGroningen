using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public class TrackRaceViewModel : PropertyChangedBase, ITrackRaceViewModel
    {
        private readonly Race race;
        private readonly BindableCollection<IRaceTransponderViewModel> transponders = new BindableCollection<IRaceTransponderViewModel>();
        private RaceResult result;
        private RaceStatus status;
        private RaceTime time;
        private TimeInfo timeInfo;
        private TimeInvalidReason? timeInvalidReason;

        public TrackRaceViewModel(Race race)
        {
            if (race == null)
                throw new ArgumentNullException(nameof(race));

            this.race = race;

            transponders.AddRange(race.Transponders.Select(t => new RaceTransponderViewModel(t)));
        }

        #region ITrackRaceViewModel Members

        public Guid Id => race.Id;

        public CompetitorBase Competitor => race.Competitor;

        public Distance Distance => race.Distance;

        public IReadOnlyCollection<IRaceTransponderViewModel> Transponders => transponders;

        public Heat Heat => new Heat(race.Round, race.Heat);

        public int Lane => race.Lane;

        public int Color => race.Color;

        public RaceTime Time
        {
            get { return time; }
            set
            {
                if (Equals(value, time))
                    return;
                time = value;
                NotifyOfPropertyChange(() => Time);

                TimeInfo = time?.TimeInfo ?? TimeInfo.None;
            }
        }

        public RaceResult Result
        {
            get { return result; }
            set
            {
                if (Equals(value, result))
                    return;
                result = value;
                NotifyOfPropertyChange(() => Result);

                Status = result != null ? result.Status : RaceStatus.Drawn;
                TimeInvalidReason = result != null ? result.TimeInvalidReason : null;
            }
        }

        public RaceStatus Status
        {
            get { return status; }
            set
            {
                if (value == status)
                    return;
                status = value;
                NotifyOfPropertyChange(() => Status);
            }
        }

        public TimeInfo TimeInfo
        {
            get { return timeInfo; }
            set
            {
                if (value == timeInfo)
                    return;
                timeInfo = value;
                NotifyOfPropertyChange(() => TimeInfo);
            }
        }

        public TimeInvalidReason? TimeInvalidReason
        {
            get { return timeInvalidReason; }
            set
            {
                if (value == timeInvalidReason)
                    return;
                timeInvalidReason = value;
                NotifyOfPropertyChange(() => TimeInvalidReason);
            }
        }

        #endregion
    }
}