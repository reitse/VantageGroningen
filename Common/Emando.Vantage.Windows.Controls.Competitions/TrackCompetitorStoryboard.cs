using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Windows.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions
{
    public class TrackCompetitorStoryboard : Storyboard
    {
        private const double DefaultSpeed = 11;
        private readonly IDistanceDisciplineCalculator calculator;
        private readonly IActiveTrackRaceViewModel race;
        private FrameworkElement containingObject;
        private IList<TimeSpan> estimatedLapTimes;
        private DateTime? startTime;

        public TrackCompetitorStoryboard(IActiveTrackRaceViewModel race, IDistanceDisciplineCalculator calculator)
        {
            if (race == null)
                throw new ArgumentNullException(nameof(race));

            this.race = race;
            this.calculator = calculator;
        }

        public void AttachAndBegin(FrameworkElement obj)
        {
            containingObject = obj;
            race.Started += RaceStarted;
            race.Cleared += RaceCleared;
            race.EstimatedLaps.CollectionChanged += EstimatedLapsChanged;
            race.Laps.LastPresentedChanged += LastPresentedChanged;
            startTime = race.StartTime;

            UpdateEstimatedLaps();
        }

        private void UpdateEstimatedLaps()
        {
            if (race.EstimatedLaps != null)
            {
                estimatedLapTimes = new TimeSpan[race.EstimatedLaps.Count];
                for (var i = 0; i < race.EstimatedLaps.Count; i++)
                    estimatedLapTimes[i] = race.EstimatedLaps[i].LapTime;
            }
            Update();
        }

        public void DetachAndStop()
        {
            estimatedLapTimes = null;
            race.Started -= RaceStarted;
            race.Cleared -= RaceCleared;
            race.EstimatedLaps.CollectionChanged += EstimatedLapsChanged;
            race.Laps.LastPresentedChanged -= LastPresentedChanged;
            Stop(containingObject);
        }

        private void RaceStarted(object sender, LocalStartEventArgs e)
        {
            startTime = e.LocalTime;
            Start();
        }

        private void Start()
        {
            Stop(containingObject);
            if (!startTime.HasValue)
                return;

            var estimatedLapTime = race.EstimatedLaps != null
                ? race.EstimatedLaps.Select(l => new TimeSpan?(l.LapTime)).FirstOrDefault()
                : new TimeSpan?();
            var openingLapLength = calculator.LapPassedLength(race.Distance, 1);
            var speed = estimatedLapTime.HasValue
                ? openingLapLength / estimatedLapTime.Value.TotalSeconds
                : DefaultSpeed;

            Duration = TimeSpan.FromSeconds(openingLapLength);
            Begin(containingObject, true);
            Seek(containingObject, TimeSpan.FromSeconds(speed * (DateTime.Now - startTime.Value).TotalSeconds), TimeSeekOrigin.BeginTime);
            SetSpeedRatio(containingObject, speed);
        }

        private void RaceCleared(object sender, EventArgs e)
        {
            SeekAlignedToLastTick(containingObject, TimeSpan.Zero, TimeSeekOrigin.BeginTime);
            Stop(containingObject);
            startTime = null;
        }

        private void LastPresentedChanged(object sender, EventArgs e)
        {
            Update();
        }

        private void EstimatedLapsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateEstimatedLaps();
        }

        private void Update()
        {
            if (!startTime.HasValue)
                return;

            var lastPresented = race.Laps.LastPresented;
            if (lastPresented == null)
            {
                Start();
                return;
            }

            if (lastPresented.Presented == null)
                return;

            var passed = (double)calculator.LapPassedLength(race.Distance, lastPresented.Index + 1);
            if (lastPresented.Index >= calculator.Laps(race.Distance) - 1)
            {
                Seek(containingObject, TimeSpan.FromSeconds(passed), TimeSeekOrigin.BeginTime);
                return;
            }

            var lapLength = calculator.LapPassedLength(race.Distance, lastPresented.Index + 2) - passed;
            double speed;
            if (estimatedLapTimes != null && estimatedLapTimes.Count > lastPresented.Index + 1)
            {
                var nextEstimatedLapTime = estimatedLapTimes[lastPresented.Index + 1];
                speed = lapLength / nextEstimatedLapTime.TotalSeconds;
            }
            else
            {
                var openingLapLength = calculator.LapPassedLength(race.Distance, 1);
                speed = (lastPresented.Index == 0 ? openingLapLength : lapLength) / lastPresented.Presented.LapTime.TotalSeconds;
            }

            passed += speed * Math.Max(0, (DateTime.Now - startTime.Value - lastPresented.Presented.Time).TotalSeconds);

            Duration = TimeSpan.FromSeconds(passed + lapLength);
            Begin(containingObject, true);
            Seek(containingObject, TimeSpan.FromSeconds(passed), TimeSeekOrigin.BeginTime);
            SetSpeedRatio(containingObject, speed);
        }
    }
}