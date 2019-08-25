using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class RankedPersonPoints
    {
        private readonly IList<PersonTime> times;

        public RankedPersonPoints(int ranking, PersonLicense license, decimal points, IList<PersonTime> times, bool sameRankingAsPrevious = true)
        {
            this.times = times;
            Ranking = ranking;
            License = license;
            Points = points;
            SameRankingAsPrevious = sameRankingAsPrevious;
        }

        public int Ranking { get; }

        public PersonLicense License { get; }

        public decimal Points { get; }

        public bool SameRankingAsPrevious { get; }

        public PersonTime FirstTime => times.ElementAtOrDefault(0);

        public PersonTime SecondTime => times.ElementAtOrDefault(1);

        public PersonTime ThirdTime => times.ElementAtOrDefault(2);

        public PersonTime FourthTime => times.ElementAtOrDefault(3);
    }
}