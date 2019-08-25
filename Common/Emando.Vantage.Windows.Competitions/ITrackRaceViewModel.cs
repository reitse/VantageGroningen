using System;
using System.Collections.Generic;
using System.ComponentModel;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public interface ITrackRaceViewModel : INotifyPropertyChanged
    {
        Guid Id { get; }

        CompetitorBase Competitor { get; }

        Distance Distance { get; }

        Heat Heat { get; }

        int Lane { get; }

        int Color { get; }

        IReadOnlyCollection<IRaceTransponderViewModel> Transponders { get; }

        RaceTime Time { get; set; }

        RaceResult Result { get; set; }

        RaceStatus Status { get; set; }

        TimeInfo TimeInfo { get; set; }

        TimeInvalidReason? TimeInvalidReason { get; set; }
    }
}