namespace Emando.Vantage.Components
{
    public interface ITransponderCodeConverter
    {
        bool SupportsType(string type);

        bool TryConvertLabel(string type, string label, out long code);
    }
}