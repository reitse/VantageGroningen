namespace Emando.Vantage.Events
{
    public class InstanceSwitchedEvent : EventBase
    {
        public InstanceSwitchedEvent(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}