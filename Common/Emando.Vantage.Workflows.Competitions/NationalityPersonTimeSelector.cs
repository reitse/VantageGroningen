using System;
using System.Linq;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class NationalityPersonTimeSelector : IPersonTimeSelector
    {
        private readonly string nationalityCode;

        public NationalityPersonTimeSelector(string nationalityCode)
        {
            this.nationalityCode = nationalityCode;
        }

        public override string ToString()
        {
            return $"Nationality: {nationalityCode}";
        }

        public string ToShortString()
        {
            return nationalityCode;
        }


        #region IPersonTimeSelector Members

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference)
        {
            return from pt in times
                   where pt.NationalityCode == nationalityCode
                   select pt;
        }

        #endregion
    }
}