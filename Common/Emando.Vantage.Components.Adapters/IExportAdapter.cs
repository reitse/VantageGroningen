namespace Emando.Vantage.Components.Adapters
{
    public interface IExportAdapter : IAdapter
    {
        string FileExtension { get; }

        string MediaType { get; }
    }
}