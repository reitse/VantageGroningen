using System;

namespace Emando.Vantage.Windows.Competitions
{
    public delegate void PresentRaceLapEventHandler(object sender, PresentRaceLapEventArgs e);

    public class PresentRaceLapEventArgs : EventArgs
    {
        public PresentRaceLapEventArgs(RaceLapViewModel oldLap, RaceLapViewModel newLap)
        {
            this.OldLap = oldLap;
            this.NewLap = newLap;
        }

        public RaceLapViewModel OldLap { get; }

        public RaceLapViewModel NewLap { get; }
    }
}