using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.Competitions
{
    public interface IPersonCompetitorsImportAdapter : IAdapter
    {
        Task<ICollection<PersonCompetitor>> LoadFromStreamAsync(Guid competitionId, Guid listId, Stream stream);
    }
}