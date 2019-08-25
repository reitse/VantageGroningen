using System;
using Caliburn.Micro;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Competitions
{
    public class RaceTransponderViewModel : PropertyChangedBase, IRaceTransponderViewModel
    {
        private static readonly TimeSpan InactivityTimeout = TimeSpan.FromMinutes(10);
        private readonly RaceTransponder transponder;
        private DateTime lastSeen;

        public RaceTransponderViewModel(RaceTransponder transponder)
        {
            this.transponder = transponder;
        }

        #region IRaceTransponderViewModel Members

        public long Code => transponder.Code;

        public bool IsActive => DateTime.Now - lastSeen < InactivityTimeout;

        public DateTime LastSeen
        {
            get { return lastSeen; }
            set
            {
                if (value.Equals(lastSeen))
                    return;
                lastSeen = value;
                NotifyOfPropertyChange(() => LastSeen);
                NotifyOfPropertyChange(() => IsActive);
            }
        }

        public void CheckActive()
        {
            NotifyOfPropertyChange(() => IsActive);
        }

        #endregion
    }
}