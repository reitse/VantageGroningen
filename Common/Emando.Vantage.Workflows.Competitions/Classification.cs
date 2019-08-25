using System.Collections.Generic;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class Classification
    {
        public Classification(IList<Distance> distances, IList<ClassifiedCompetitor> competitors, string category)
        {
            Distances = distances;
            Competitors = competitors;
            Category = category;
        }

        public IList<Distance> Distances { get; }

        public IList<ClassifiedCompetitor> Competitors { get; }

        public string Category { get; }
    }
}