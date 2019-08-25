using System.Collections.Generic;
using System.Linq;

namespace Emando.Vantage.Entities
{
    public static class TranspondersExtensions
    {
        public static IEnumerable<T> WithTransponder<T>(this IEnumerable<T> entities, long code) where T : IHaveTransponders
        {
            return from e in entities
                   where e.HasTransponder(code)
                   select e;
        }

        public static bool HasTransponder(this IHaveTransponders entity, long code)
        {
            return entity.Transponders.Any(k => k.Code == code);
        }
    }
}