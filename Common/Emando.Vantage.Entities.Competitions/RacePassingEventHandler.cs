namespace Emando.Vantage.Entities.Competitions
{
    public delegate void RacePassingEventHandler(object sender, RacePassingEventArgs e);

    public class RacePassingEventArgs : RaceEventArgs
    {
        public RacePassingEventArgs(RacePassing passing)
            : base(passing.Race)
        {
            Passing = passing;
        }

        public RacePassing Passing { get; }
    }
}