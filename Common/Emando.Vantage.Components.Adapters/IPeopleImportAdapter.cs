using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Emando.Vantage.Entities;

namespace Emando.Vantage.Components.Adapters
{
    public interface IPeopleImportAdapter : IAdapter
    {
        Task<ICollection<Person>> LoadFromStreamAsync(Stream stream);
    }
}