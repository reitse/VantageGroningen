namespace Emando.Vantage.Models
{
    public class TransponderSetViewModel
    {
        public int Number { get; set; }

        public TransponderSetTransponderViewModel[] Transponders { get; set; }
    }
}