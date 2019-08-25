using System;

namespace Emando.Vantage.Windows.Competitions
{
    public delegate void EditRaceLapEventHandler(object sender, EditRaceLapEventArgs e);

    public class EditRaceLapEventArgs : EventArgs
    {
        public EditRaceLapEventArgs(RaceLapViewModel lap, TimeSpan newTime)
        {
            this.Lap = lap;
            this.NewTime = newTime;
        }

        public RaceLapViewModel Lap { get; }

        public TimeSpan NewTime { get; }
    }
}