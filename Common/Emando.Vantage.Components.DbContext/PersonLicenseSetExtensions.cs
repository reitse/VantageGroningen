using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components
{
    public static class PersonLicenseSetExtensions
    {
        public static async Task<IDictionary<string, Person>> ToPersonDictionaryAsync(this IQueryable<PersonLicense> licenses, string issuerId, string discipline)
        {
            return await (from l in licenses.Include(l => l.Club)
                          where discipline.StartsWith(l.Discipline) && l.IssuerId == issuerId
                          select new
                          {
                              License = l,
                              l.Person
                          }).ToDictionaryAsync(a => a.License.Key, a => a.Person);
        }
    }
}