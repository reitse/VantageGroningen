using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class LapViewModel
    {
        public TimeSpan Time { get; set; }

        public decimal? Points { get; set; }
    }
}