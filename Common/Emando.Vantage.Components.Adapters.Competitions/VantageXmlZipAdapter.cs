using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Emando.Vantage.Entities.Competitions;
using System.IO.Compression;

namespace Emando.Vantage.Components.Adapters.Competitions
{
    [Adapter("Vantage (VANX)", 11)]
    public class VantageXmlZipAdapter : ICompetitionExportAdapter, ICompetitionImportAdapter, ICompetitionResultsImportAdapter
    {
        private readonly VantageXmlAdapter adapter;

        public VantageXmlZipAdapter(VantageXmlAdapter adapter)
        {
            this.adapter = adapter;
        }

        public string FileExtension => ".vanx";

        public string MediaType => "application/octet-stream";

        private string EntryName => "data.xml";

        public async Task ExportAsync(Guid competitionId, Stream stream, CultureInfo culture)
        {
            using (stream)
            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                using (var entryStream = archive.CreateEntry(EntryName).Open())
                    await adapter.ExportAsync(competitionId, entryStream, culture);

                memoryStream.Position = 0;
                await memoryStream.CopyToAsync(stream);
            }
        }

        public async Task ImportAsync(string name, Stream stream, bool importPeople = true, Func<Competition, int> overrideClass = null)
        {
            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
                using (var entryStream = archive.GetEntry(EntryName).Open())
                    await adapter.ImportAsync(name, entryStream, importPeople, overrideClass);
        }

        public async Task ImportAsync(Guid competitionId, string name, Stream stream, CultureInfo cultureInfo)
        {
            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read))
                using (var entryStream = archive.GetEntry(EntryName).Open())
                    await adapter.ImportAsync(competitionId, name, entryStream, cultureInfo);
        }
    }
}
