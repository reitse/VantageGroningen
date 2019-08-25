using System;
using System.Linq;
using Emando.Vantage.Components;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public class CategoryPersonTimeSelector : IPersonTimeSelector
    {
        private readonly string[] categories;

        public CategoryPersonTimeSelector(string[] categories)
        {
            this.categories = categories;
        }

        #region IPersonTimeSelector Members

        public IQueryable<PersonTime> Query(IDisciplineCalculator calculator, IQueryable<PersonTime> times, DateTime? reference = null)
        {
            return from pt in times
                   where categories.Contains(pt.License.Category)
                   select pt;
        }

        #endregion

        public override string ToString()
        {
            return $"Category: {string.Join(", ", categories)}";
        }

        public string ToShortString()
        {
            return string.Join("-", categories);
        }
    }
}