using System;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Emando.Vantage.Components;
using Emando.Vantage.Entities;
using Emando.Vantage.Workflows.Events;

namespace Emando.Vantage.Workflows.Competitions
{
    public class CompetitorLicenseChangedEventListener : IEventListener, IDisposable
    {
        private readonly IEventSource source;
        private readonly CompositeDisposable subscriptions = new CompositeDisposable();
        private readonly Func<CompetitionsWorkflow> workflowFactory;
        private bool isDisposed;

        public CompetitorLicenseChangedEventListener(IEventSource source, Func<CompetitionsWorkflow> workflowFactory)
        {
            this.source = source;
            this.workflowFactory = workflowFactory;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IEventListener Members

        public void Start()
        {
            subscriptions.Add(source
                .OfType<PersonLicenseChangedEvent>()
                .ObserveOn(NewThreadScheduler.Default)
                .SelectMany(e => PersonLicenseChangedAsync(e.License).ToObservable())
                .Subscribe());
        }

        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    subscriptions.Dispose();

                isDisposed = true;
            }
        }

        ~CompetitorLicenseChangedEventListener()
        {
            Dispose(false);
        }

        private async Task PersonLicenseChangedAsync(PersonLicense license)
        {
            using (var workflow = workflowFactory())
                try
                {
                    await workflow.UpdateCompetitorsByLicenseAsync(license);
                }
                catch (Exception e)
                {
                    Trace.TraceError("Failed to update competitors by license {0}: {1}", license, e.Message);
                }
        }
    }
}