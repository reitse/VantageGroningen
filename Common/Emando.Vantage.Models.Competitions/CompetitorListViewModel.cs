using System;

namespace Emando.Vantage.Models.Competitions
{
    public abstract class CompetitorListViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public int CompetitorsCount { get; set; }

        public string TypeName { get; set; }
    }
}