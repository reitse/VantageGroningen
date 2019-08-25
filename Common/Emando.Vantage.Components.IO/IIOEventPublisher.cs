namespace Emando.Vantage.Components.IO
{
    public interface IIOEventPublisher
    {
        event IOEventSubscriberEventHandler Subscribed;

        event IOEventSubscriberEventHandler Unsubscribed;

        void Set(int id, object value);

        void Subscribe(IIOClientChannel channel, string name);

        bool Unsubscribe(IIOClientChannel channel);
    }
}