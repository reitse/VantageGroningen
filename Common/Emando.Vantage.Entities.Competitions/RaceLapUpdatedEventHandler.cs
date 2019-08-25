using System;

namespace Emando.Vantage.Entities.Competitions
{
    public delegate void RaceLapUpdatedEventHandler(object sender, RaceLapUpdatedEventArgs e);

    public class RaceLapUpdatedEventArgs : RaceLapEventArgs
    {
        public RaceLapUpdatedEventArgs(PresentationSource presentationSource, TimeSpan oldTime, RaceLap lap) : base(lap)
        {
            this.OldTime = oldTime;
            this.PresentationSource = presentationSource;
        }

        public PresentationSource PresentationSource { get; }

        public TimeSpan OldTime { get; }
    }
}