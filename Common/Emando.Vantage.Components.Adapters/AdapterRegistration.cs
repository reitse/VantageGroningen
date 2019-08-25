namespace Emando.Vantage.Components.Adapters
{
    public struct AdapterRegistration
    {
        public string Name { get; private set; }
        public string FriendlyName { get; private set; }

        public AdapterRegistration(string name, string friendlyName) : this()
        {
            Name = name;
            FriendlyName = friendlyName;
        }
    }
}