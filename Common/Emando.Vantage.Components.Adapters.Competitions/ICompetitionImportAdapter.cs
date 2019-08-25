using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.Competitions
{
    public interface ICompetitionImportAdapter : IAdapter
    {
        Task ImportAsync(string name, Stream stream, bool importPeople = true, Func<Competition, int> overrideClass = null);
    }
}