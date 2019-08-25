namespace Emando.Vantage.Entities.Competitions
{
    public class RaceStartEventArgs : RaceEventArgs
    {
        public RaceStartEventArgs(RaceStart start) : base(start.Race)
        {
            this.Start = start;
        }

        public RaceStart Start { get; }
    }

    public delegate void RaceStartEventHandler(object sender, RaceStartEventArgs e);
}