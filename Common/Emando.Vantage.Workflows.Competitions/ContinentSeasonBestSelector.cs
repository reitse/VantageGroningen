using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class ContinentSeasonBestSelector : SeasonTimeSelectorBase
    {
        private readonly string continentCode;

        public ContinentSeasonBestSelector(DateTime from, DateTime to, string continentCode) : base(from, to)
        {
            this.continentCode = continentCode;
        }

        public override IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference)
        {
            return from pt in base.Query(calculator, times, reference)
                   where pt.Venue.ContinentCode == continentCode
                   select pt;
        }

        public override string ToShortString()
        {
            return string.Format("{0}-{1}", base.ToShortString(), continentCode);
        }
    }
}