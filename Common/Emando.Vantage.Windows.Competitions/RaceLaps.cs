using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public class RaceLaps : PropertyChangedBase
    {
        private readonly IDistanceDisciplineCalculator calculator;
        private readonly BindableCollection<RaceLapsGroup> groups = new BindableCollection<RaceLapsGroup>();
        private readonly List<RaceLapViewModel> laps = new List<RaceLapViewModel>();
        private readonly ITrackRaceViewModel race;
        private bool isUpdating;
        private RaceLapsGroup lastPresented;
        private RaceLapViewModel lastPresentedLap;

        public RaceLaps(ITrackRaceViewModel race, IDistanceDisciplineCalculator calculator)
        {
            this.race = race;
            this.calculator = calculator;

            Points = new RacePoints(groups);
        }

        public RaceLapsGroup LastPresented
        {
            get { return lastPresented; }
            private set
            {
                if (Equals(value, lastPresented) && (lastPresented == null || Equals(lastPresented.Presented, lastPresentedLap)))
                    return;
                lastPresented = value;
                lastPresentedLap = lastPresented?.Presented;
                OnLastPresentedChanged();
                NotifyOfPropertyChange(() => LastPresented);
            }
        }

        public IObservableCollection<RaceLapsGroup> Groups => groups;

        public RacePoints Points { get; }

        public event EventHandler LastPresentedChanged;

        public event EditRaceLapEventHandler Edit;

        public event PresentRaceLapEventHandler Present;

        public event InsertRaceLapEventHandler Insert;

        public event DeleteRaceLapEventHandler Delete;

        protected virtual void OnEdit(EditRaceLapEventArgs e)
        {
            Edit?.Invoke(this, e);
        }

        protected virtual void OnPresent(PresentRaceLapEventArgs e)
        {
            Present?.Invoke(this, e);
        }

        protected virtual void OnInsert(InsertRaceLapEventArgs e)
        {
            Insert?.Invoke(this, e);
        }

        protected virtual void OnDelete(DeleteRaceLapEventArgs e)
        {
            Delete?.Invoke(this, e);
        }

        protected virtual void OnLastPresentedChanged()
        {
            LastPresentedChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Add(RaceLapViewModel lap)
        {
            if (lap.Flags.HasFlag(RaceEventFlags.Deleted))
                return;

            laps.Add(lap);
            GroupLaps();
        }

        public void BeginUpdate()
        {
            isUpdating = true;
        }

        public void EndUpdate()
        {
            isUpdating = false;
            GroupLaps();
        }

        public void InsertNew()
        {
            GetOrAddGroup(groups.Count).BeginEdit();
        }

        public bool TryUpdate(PresentationSource presentationSource, TimeSpan oldTime, RaceLapState update)
        {
            var index = laps.FindIndex(l => l.PresentationSource == presentationSource && l.Time == oldTime);
            if (index == -1)
                return false;

            if (update.Flags.HasFlag(RaceEventFlags.Deleted))
                laps.RemoveAt(index);
            else
                laps[index].Update(update);
            GroupLaps();
            return true;
        }

        private RaceLapsGroup GetOrAddGroup(int index)
        {
            while (groups.Count <= index)
            {
                var group = new RaceLapsGroup(race, calculator, groups.Count);
                AttachGroupEvents(@group);
                groups.Add(group);
            }
            return groups[index];
        }

        private void AttachGroupEvents(RaceLapsGroup @group)
        {
            group.Edit += GroupEdited;
            group.CanceledEdit += GroupEditCanceled;
            group.Present += GroupPresented;
            group.Insert += GroupLapInserted;
            group.Delete += GroupLapDeleted;
        }

        private void DetachGroupEvents(RaceLapsGroup group)
        {
            group.Edit -= GroupEdited;
            group.CanceledEdit -= GroupEditCanceled;
            group.Present -= GroupPresented;
            group.Insert -= GroupLapInserted;
            group.Delete -= GroupLapDeleted;
        }

        private void GroupLapDeleted(object sender, DeleteRaceLapEventArgs e)
        {
            OnDelete(e);
        }

        private void GroupLapInserted(object sender, InsertRaceLapEventArgs e)
        {
            OnInsert(e);
        }

        private void GroupPresented(object sender, PresentRaceLapEventArgs e)
        {
            OnPresent(e);
        }

        private void GroupEditCanceled(object sender, EventArgs e)
        {
            TrimGroups();
        }

        private void GroupEdited(object sender, EditRaceLapEventArgs e)
        {
            OnEdit(e);
        }

        private void GroupLaps()
        {
            if (isUpdating)
                return;

            foreach (var group in groups)
                group.Clear();

            var groupings = laps.GroupByPresented();
            TimeSpan? previousTime = null;
            for (var i = 0; i < groupings.Count; i++)
            {
                var grouping = groupings[i];
                var group = GetOrAddGroup(i);

                if (grouping.Key != null)
                {
                    grouping.Key.PreviousTime = previousTime;
                    group.Presented = grouping.Key;
                }

                foreach (var notPresented in grouping)
                {
                    notPresented.PreviousTime = previousTime;
                    group.NotPresented.Add(notPresented);
                }

                if (grouping.Key != null)
                    previousTime = grouping.Key.Time;
            }

            TrimGroups();
            groups.Refresh();

            LastPresented = groups.LastOrDefault(g => g.Presented != null);
        }

        private void TrimGroups()
        {
            for (var i = groups.Count - 1; i >= 0; i--)
            {
                var group = groups[i];
                if (group.IsEmpty)
                {
                    DetachGroupEvents(group);
                    groups.RemoveAt(i);
                }
                else
                    break;
            }
        }

        public void Clear()
        {
            for (var i = groups.Count - 1; i >= 0; i--)
            {
                var group = groups[i];
                DetachGroupEvents(group);
                group.Clear();
                groups.RemoveAt(i);
            }

            laps.Clear();
            LastPresented = null;
        }
    }
}