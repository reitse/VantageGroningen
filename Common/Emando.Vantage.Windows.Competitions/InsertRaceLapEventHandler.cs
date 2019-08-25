using System;

namespace Emando.Vantage.Windows.Competitions
{
    public delegate void InsertRaceLapEventHandler(object sender, InsertRaceLapEventArgs e);

    public class InsertRaceLapEventArgs : EventArgs
    {
        public InsertRaceLapEventArgs(TimeSpan time)
        {
            this.Time = time;
        }

        public TimeSpan Time { get; }
    }
}