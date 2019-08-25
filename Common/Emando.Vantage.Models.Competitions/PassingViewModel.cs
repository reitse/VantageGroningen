using System;

namespace Emando.Vantage.Models.Competitions
{
    public class PassingViewModel
    {
        public TimeSpan Time { get; set; }

        public decimal? Passed { get; set; }

        public decimal? Speed { get; set; }

        public long Where { get; set; }
    }
}