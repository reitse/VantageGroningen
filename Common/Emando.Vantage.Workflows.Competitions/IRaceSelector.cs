using System.Linq;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Workflows.Competitions
{
    public interface IRaceSelector
    {
        IQueryable<Race> Query(IQueryable<Race> times);
    }
}