using System.IO;
using System.Threading.Tasks;

namespace Emando.Vantage.Components.Adapters
{
    public interface ILicenseesExportAdapter : IExportAdapter
    {
        Task SaveToStreamAsync(string issuerId, string discipline, string category, bool includeDetails, Stream stream);
    }
}