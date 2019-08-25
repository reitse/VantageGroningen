using System;

namespace Emando.Vantage.Workflows.Reporting
{
    public enum ReportFormat
    {
        Pdf,
        Html,
        Text,
        Csv,
        OoXmlDocument,
        OoXmlSpreadsheet,
        OoXmlPresentation,
        Raw
    }

    public static class ReportFormatExtensions
    {
        public static string GetAttachmentMediaType(this ReportFormat format)
        {
            switch (format)
            {
                case ReportFormat.Pdf:
                    return "application/pdf";
                case ReportFormat.Html:
                    return "text/html";
                case ReportFormat.Text:
                    return "text/plain";
                case ReportFormat.Csv:
                    return "text/csv";
                case ReportFormat.OoXmlDocument:
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ReportFormat.OoXmlSpreadsheet:
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ReportFormat.OoXmlPresentation:
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                default:
                    throw new ArgumentOutOfRangeException(nameof(format));
            }
        }

        public static string GetDefaultExtension(this ReportFormat format)
        {
            switch (format)
            {
                case ReportFormat.Pdf:
                    return ".pdf";
                case ReportFormat.Html:
                    return ".html";
                case ReportFormat.Text:
                    return ".txt";
                case ReportFormat.Csv:
                    return ".csv";
                case ReportFormat.OoXmlDocument:
                    return ".docx";
                case ReportFormat.OoXmlSpreadsheet:
                    return ".xlsx";
                case ReportFormat.OoXmlPresentation:
                    return ".pptx";
                default:
                    throw new ArgumentOutOfRangeException(nameof(format));
            }
        }
    }
}