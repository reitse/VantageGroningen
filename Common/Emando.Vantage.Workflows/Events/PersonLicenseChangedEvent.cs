using Emando.Vantage.Entities;
using Emando.Vantage.Events;

namespace Emando.Vantage.Workflows.Events
{
    public class PersonLicenseChangedEvent : EventBase
    {
        public PersonLicenseChangedEvent(PersonLicense license)
        {
            License = license;
        }

        public PersonLicense License { get; }
    }
}