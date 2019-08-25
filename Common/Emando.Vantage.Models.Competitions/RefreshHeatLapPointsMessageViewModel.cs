using System;

namespace Emando.Vantage.Models.Competitions
{
    public class RefreshHeatLapPointsMessageViewModel : MessageViewModelBase
    {
        public HeatViewModel Heat { get; set; }

        public int? Index { get; set; }
    }
}