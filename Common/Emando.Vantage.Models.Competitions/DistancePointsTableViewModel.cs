using System;

namespace Emando.Vantage.Models.Competitions
{
    public class DistancePointsTableViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DistancePointsViewModel[] Points { get; set; }
    }
}