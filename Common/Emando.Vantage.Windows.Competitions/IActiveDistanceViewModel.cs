using System;
using System.Threading.Tasks;
using Caliburn.Micro;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public interface IActiveDistanceViewModel : IScreen
    {
        Distance Distance { get; }

        IObservableCollection<ITrackRaceViewModel> Races { get; }

        IObservableCollection<HeatStart> Starts { get; }

        HeatStart SelectedStart { get; set; }

        bool CanRecoverFromStartAsync { get; }

        ITrackRaceViewModel SelectedRace { get; set; }

        Task ActivateDistanceAsync(Guid distanceId);

        Task RecoverFromStartAsync();

        Task UpdateTimeInvalidReasonAsync(TimeInvalidReason? timeInvalidReason);
    }
}