using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Emando.Vantage.Workflows.Reporting
{
    public interface ILoadedReport
    {
        string Title { get; }

        IEnumerable<ReportFormat> SupportedFormats { get; }

        Task SaveAsync(Stream stream, ReportFormat format = ReportFormat.Pdf);
    }
}