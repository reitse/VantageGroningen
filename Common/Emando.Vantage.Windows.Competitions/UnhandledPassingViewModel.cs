using System;

namespace Emando.Vantage.Windows.Competitions
{
    public class UnhandledPassingViewModel
    {
        public UnhandledPassingViewModel(PresentationSource presentationSource, TimeSpan time)
        {
            PresentationSource = presentationSource;
            Time = time;
        }

        public PresentationSource PresentationSource { get; }

        public TimeSpan Time { get; }
    }
}