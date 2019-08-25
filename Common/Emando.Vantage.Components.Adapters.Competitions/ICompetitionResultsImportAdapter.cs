using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.Adapters.Competitions
{
    public interface ICompetitionResultsImportAdapter : IAdapter
    {
        Task ImportAsync(Guid competitionId, string name, Stream stream, CultureInfo cultureInfo);
    }
}