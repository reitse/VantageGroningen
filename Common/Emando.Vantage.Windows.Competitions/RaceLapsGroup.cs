using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Caliburn.Micro;
using Emando.Vantage.Components.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public class RaceLapsGroup : PropertyChangedBase
    {
        private readonly IDistanceDisciplineCalculator calculator;
        private readonly BindableCollection<RaceLapViewModel> notPresented = new BindableCollection<RaceLapViewModel>();
        private TimeSpan? editTime;
        private bool isEditing;
        private RaceLapViewModel presented;

        public RaceLapsGroup(ITrackRaceViewModel race, IDistanceDisciplineCalculator calculator, int index)
        {
            Race = race;
            this.calculator = calculator;
            Index = index;

            notPresented.CollectionChanged += NotPresentedChanged;
        }

        public ITrackRaceViewModel Race { get; }

        public int Index { get; }

        public decimal RoundsToGo => calculator.RoundsToGo(Race.Distance, Index + 1);

        public decimal PassedLength => calculator.LapPassedLength(Race.Distance, Index + 1);

        public bool IsExcess => Index + 1 > calculator.Laps(Race.Distance);

        public RaceLapViewModel Presented
        {
            get { return presented; }
            internal set
            {
                if (Equals(value, presented))
                    return;

                presented = value;
                NotifyOfPropertyChange(() => Presented);
                NotifyOfPropertyChange(() => CanDeletePresented);
                NotifyOfPropertyChange(() => CanBeginEdit);
                NotifyOfPropertyChange(() => IsEmpty);

                EditTime = presented?.Time;
            }
        }

        public bool HasNotPresented => notPresented.Any();

        public ICollection<RaceLapViewModel> NotPresented => notPresented;

        public TimeSpan? EditTime
        {
            get { return editTime; }
            set
            {
                if (value.Equals(editTime))
                    return;
                editTime = value;
                NotifyOfPropertyChange(() => EditTime);
            }
        }

        public bool IsEditing
        {
            get { return isEditing; }
            private set
            {
                if (value.Equals(isEditing))
                    return;
                isEditing = value;
                NotifyOfPropertyChange(() => IsEditing);
            }
        }

        public bool CanDeletePresented => Presented != null;

        public bool CanBeginEdit => Presented == null
            || Presented.PresentationSource.How == "Optical"
            || Presented.PresentationSource.How == "Manual"
            || Presented.PresentationSource.How == "User";

        public bool IsEmpty => Presented == null && !HasNotPresented;

        private void NotPresentedChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyOfPropertyChange(() => IsEmpty);
            NotifyOfPropertyChange(() => HasNotPresented);
        }

        public event EditRaceLapEventHandler Edit;

        public event EventHandler CanceledEdit;

        public event PresentRaceLapEventHandler Present;

        public event InsertRaceLapEventHandler Insert;

        public event DeleteRaceLapEventHandler Delete;

        protected virtual void OnEdit(EditRaceLapEventArgs e)
        {
            Edit?.Invoke(this, e);
        }

        protected virtual void OnCanceledEdit()
        {
            CanceledEdit?.Invoke(this, EventArgs.Empty);
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

        public void MouseDown(MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2 && CanBeginEdit)
                BeginEdit();
        }

        public void BeginEdit()
        {
            IsEditing = true;
        }

        public void DeletePresented()
        {
            OnDelete(new DeleteRaceLapEventArgs(Presented));
            NotifyOfPropertyChange(() => CanBeginEdit);
        }

        public void CancelEdit()
        {
            EditTime = Presented?.Time;
            IsEditing = false;
            OnCanceledEdit();
        }

        public void KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                CancelEdit();
            else if (e.Key == Key.Return)
            {
                if (EditTime.HasValue)
                    if (Presented != null)
                        OnEdit(new EditRaceLapEventArgs(Presented, EditTime.Value));
                    else
                        OnInsert(new InsertRaceLapEventArgs(EditTime.Value));
                IsEditing = false;
            }
        }

        public void PresentLap(RaceLapViewModel lap)
        {
            OnPresent(new PresentRaceLapEventArgs(Presented, lap));
        }

        public void InsertLap(RaceLapViewModel lap)
        {
            OnPresent(new PresentRaceLapEventArgs(null, lap));
        }

        public void Clear()
        {
            Presented = null;
            notPresented.Clear();
        }
    }
}