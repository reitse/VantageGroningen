using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.Competitions
{
    public interface IRaceTranspondersImportAdapter : IAdapter
    {
        Task<ICollection<RaceTransponder>> LoadFromStreamAsync(Guid competitionId, Guid distanceId, Stream stream);
    }
}