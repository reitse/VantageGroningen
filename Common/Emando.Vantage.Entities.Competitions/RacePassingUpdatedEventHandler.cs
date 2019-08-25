using System;

namespace Emando.Vantage.Entities.Competitions
{
    public delegate void RacePassingUpdatedEventHandler(object sender, RacePassingUpdatedEventArgs e);

    public class RacePassingUpdatedEventArgs : RacePassingEventArgs
    {
        public RacePassingUpdatedEventArgs(Guid raceId, PresentationSource presentationSource, TimeSpan oldTime, RacePassing passing)
            : base(passing)
        {
            this.RaceId = raceId;
            this.OldTime = oldTime;
            this.PresentationSource = presentationSource;
        }

        public Guid RaceId { get; }

        public PresentationSource PresentationSource { get; }

        public TimeSpan OldTime { get; }
    }
}