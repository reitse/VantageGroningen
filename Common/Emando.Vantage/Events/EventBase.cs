namespace Emando.Vantage.Events
{
    public abstract class EventBase
    {
        public string TypeName => GetType().Name;
    }
}