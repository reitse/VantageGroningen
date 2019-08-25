using System;

namespace Emando.Vantage.Windows.Competitions
{
    public interface IRaceTransponderViewModel
    {
        long Code { get; }

        bool IsActive { get; }

        DateTime LastSeen { get; set; }

        void CheckActive();
    }
}