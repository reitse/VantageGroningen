using System;

namespace Emando.Vantage.Entities.Competitions
{
    public delegate void RaceEventHandler(object sender, RaceEventArgs e);

    public class RaceEventArgs : EventArgs
    {
        public RaceEventArgs(Race race)
        {
            Race = race;
        }

        public Race Race { get; }
    }
}