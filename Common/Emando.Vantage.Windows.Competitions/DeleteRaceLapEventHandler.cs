using System;

namespace Emando.Vantage.Windows.Competitions
{
    public delegate void DeleteRaceLapEventHandler(object sender, DeleteRaceLapEventArgs e);

    public class DeleteRaceLapEventArgs : EventArgs
    {
        public DeleteRaceLapEventArgs(RaceLapViewModel lap)
        {
            this.Lap = lap;
        }

        public RaceLapViewModel Lap { get; }
    }
}