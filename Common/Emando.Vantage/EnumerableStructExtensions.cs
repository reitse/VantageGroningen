using System;
using System.Collections.Generic;
using System.Linq;

namespace Emando.Vantage
{
    public static class EnumerableStructExtensions
    {
        public static T? FirstOrNull<T>(this IEnumerable<T> presentationSources, Func<T, bool> predicate)
            where T : struct
        {
            return (from p in presentationSources
                    where predicate(p)
                    select new T?(p)).FirstOrDefault();
        }
    }
}