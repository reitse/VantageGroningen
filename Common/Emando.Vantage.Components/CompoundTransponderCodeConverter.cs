using System.Linq;

namespace Emando.Vantage.Components
{
    public class CompoundTransponderCodeConverter : ITransponderCodeConverter
    {
        private readonly ITransponderCodeConverter[] converters;

        public CompoundTransponderCodeConverter(ITransponderCodeConverter[] converters)
        {
            this.converters = converters;
        }

        #region ITransponderCodeConverter Members

        public bool TryConvertLabel(string type, string label, out long code)
        {
            var converter = converters.FirstOrDefault(c => c.SupportsType(type));

            code = default(long);
            return converter != null && converter.TryConvertLabel(type, label, out code);
        }

        public bool SupportsType(string type)
        {
            return converters.Any(c => c.SupportsType(type));
        }

        #endregion
    }
}