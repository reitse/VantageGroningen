namespace Emando.Vantage.Models
{
    public class TransponderViewModel
    {
        public string Label { get; set; }

        public string Type { get; set; }

        public long Code { get; set; }

        public TransponderKey Key => new TransponderKey(Type, Code);
    }
}