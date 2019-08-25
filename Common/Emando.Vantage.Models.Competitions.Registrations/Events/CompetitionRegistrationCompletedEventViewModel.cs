using Emando.Vantage.Models.Competitions.Events;
using Emando.Vantage.Models.Events;

namespace Emando.Vantage.Models.Competitions.Registrations.Events
{
    public class CompetitionRegistrationCompletedEventViewModel : CompetitionEventViewModelBase, ILicenseIssuerEventViewModel
    {
        public CompetitionRegistrationViewModel Registration { get; set; }

        #region IHaveLicenseIssuer Members

        public string LicenseIssuerId => Registration.LicenseIssuerId;

        #endregion
    }
}