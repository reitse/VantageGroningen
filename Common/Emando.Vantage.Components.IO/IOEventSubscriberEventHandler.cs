using System;

namespace Emando.Vantage.Components.IO
{
    public delegate void IOEventSubscriberEventHandler(object sender, IOEventSubscriberEventArgs e);

    public class IOEventSubscriberEventArgs : EventArgs
    {
        private readonly IOEventSubscription subscription;

        public IOEventSubscriberEventArgs(IOEventSubscription subscription)
        {
            this.subscription = subscription;
        }

        public IOEventSubscription Subscription
        {
            get { return subscription; }
        }
    }
}