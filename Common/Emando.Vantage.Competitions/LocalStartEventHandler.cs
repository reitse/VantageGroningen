using System;

namespace Emando.Vantage.Competitions
{
    public class LocalStartEventArgs : EventArgs
    {
        public LocalStartEventArgs(DateTime localTime)
        {
            this.LocalTime = localTime;
        }

        public DateTime LocalTime { get; }
    }

    public delegate void LocalStartEventHandler(object sender, LocalStartEventArgs e);
}