namespace Emando.Vantage.Components
{
    public class TestTransponderCodeConverter : ITransponderCodeConverter
    {
        #region ITransponderCodeConverter Members

        public bool SupportsType(string type)
        {
            return type == "Test";
        }

        public bool TryConvertLabel(string type, string label, out long code)
        {
            return long.TryParse(label, out code);
        }

        #endregion
    }
}