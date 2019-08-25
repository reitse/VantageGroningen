using System;
using System.Collections.Generic;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class RaceComparandViewModel
    {
        public RaceComparandType Type { get; set; }

        public Guid? Id { get; set; }

        public string ShortDisplayName { get; set; }

        public string FullDisplayName { get; set; }

        public IList<LapViewModel> Laps { get; set; }

        public IList<PassingViewModel> Passings { get; set; }
    }
}