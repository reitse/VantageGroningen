using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.Adapters.Competitions
{
    public interface ICompetitionExportAdapter : IExportAdapter
    {
        Task ExportAsync(Guid competitionId, Stream stream, CultureInfo culture);
    }
}