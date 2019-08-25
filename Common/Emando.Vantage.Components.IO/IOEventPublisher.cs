using System;
using System.Collections.Concurrent;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Common.Logging;

namespace Emando.Vantage.Components.IO
{
    public class IOEventPublisher : IIOEventPublisher
    {
        private readonly ConcurrentDictionary<int, ReplaySubject<object>> channelSubjects = new ConcurrentDictionary<int, ReplaySubject<object>>();
        private readonly ConcurrentDictionary<IIOClientChannel, IOEventSubscription> eventSubscriptions = new ConcurrentDictionary<IIOClientChannel, IOEventSubscription>();
        private readonly ILog log = LogManager.GetCurrentClassLogger();

        protected virtual void OnSubscribed(IOEventSubscriberEventArgs e)
        {
            var handler = Subscribed;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnUnsubscribed(IOEventSubscriberEventArgs e)
        {
            var handler = Unsubscribed;
            if (handler != null)
                handler(this, e);
        }

        private IDisposable Subscribe(int id, IObservable<object> observable, IIOClientChannel channel)
        {
            return observable.ObserveOn(TaskPoolScheduler.Default).Subscribe(value =>
            {
                try
                {
                    channel.Update(id, value);
                }
                catch
                {
                    Unsubscribe(channel);
                }
            });
        }

        private ReplaySubject<object> AddSubject(int id)
        {
            var subject = new ReplaySubject<object>(1);
            foreach (var subscription in eventSubscriptions)
            {
                var channel = subscription.Key;
                subscription.Value.Handles.Add(Subscribe(id, subject, channel));
            }
            return subject;
        }

        #region IIOEventPublisher Members

        public event IOEventSubscriberEventHandler Subscribed;

        public event IOEventSubscriberEventHandler Unsubscribed;

        public void Set(int id, object value)
        {
            var channelSubject = channelSubjects.GetOrAdd(id, AddSubject);
            channelSubject.OnNext(value);
        }

        public void Subscribe(IIOClientChannel channel, string name)
        {
            var subscription = new IOEventSubscription(name);
            foreach (var channelSubject in channelSubjects)
            {
                int id = channelSubject.Key;
                subscription.Handles.Add(Subscribe(id, channelSubject.Value, channel));
            }
            eventSubscriptions.AddOrUpdate(channel, subscription, (c, s) =>
            {
                s.Dispose();
                return subscription;
            });
            OnSubscribed(new IOEventSubscriberEventArgs(subscription));
        }

        public bool Unsubscribe(IIOClientChannel channel)
        {
            IOEventSubscription subscription;
            if (!eventSubscriptions.TryRemove(channel, out subscription))
                return false;

            OnUnsubscribed(new IOEventSubscriberEventArgs(subscription));
            subscription.Dispose();
            return true;
        }

        #endregion
    }
}