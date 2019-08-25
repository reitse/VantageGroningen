using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class GenderPersonTimeSelector : IPersonTimeSelector
    {
        private readonly Gender gender;

        public GenderPersonTimeSelector(Gender gender)
        {
            this.gender = gender;
        }

        public override string ToString()
        {
            return $"Gender: {gender}";
        }

        public string ToShortString()
        {
            return gender.ToLetter();
        }

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where pt.License.Person.Gender == gender
                   select pt;
        }
    }
}