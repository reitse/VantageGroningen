using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Reporting;
using Telerik.Reporting.Processing;

namespace Emando.Vantage.Workflows.Reporting.TelerikReports
{
    public class TelerikLoadedReport : ILoadedReport
    {
        private readonly IDictionary<ReportFormat, string> formats = new Dictionary<ReportFormat, string>
        {
            { ReportFormat.Pdf, "PDF" },
            { ReportFormat.OoXmlDocument, "DOCX" },
            { ReportFormat.OoXmlSpreadsheet, "XLSX" },
            { ReportFormat.OoXmlPresentation, "PPTX" }
        };
        private readonly IReportDocument report;

        public string Title { get; private set; }

        public TelerikLoadedReport(IReportDocument report)
        {
            this.report = report;
        }

        #region IReportWriter Members

        public IEnumerable<ReportFormat> SupportedFormats => formats.Keys;

        public async Task SaveAsync(Stream stream, ReportFormat format)
        {
            if (!formats.ContainsKey(format))
                throw new NotSupportedException();

            var processor = new ReportProcessor();
            var source = new InstanceReportSource
            {
                ReportDocument = report
            };
            var result = processor.RenderReport(formats[format], source, null);
            if (result.HasErrors)
                if (result.Errors.Length > 1)
                    throw new AggregateException(result.Errors);
                else
                    throw result.Errors[0];

            Title = result.DocumentName;
            await stream.WriteAsync(result.DocumentBytes, 0, result.DocumentBytes.Length);
        }

        #endregion
    }
}