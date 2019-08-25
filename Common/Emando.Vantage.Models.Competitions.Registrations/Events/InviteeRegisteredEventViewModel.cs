using Emando.Vantage.Models.Competitions.Events;
using Emando.Vantage.Models.Events;

namespace Emando.Vantage.Models.Competitions.Registrations.Events
{
    public class InviteeRegisteredEventViewModel : CompetitionEventViewModelBase, ILicenseIssuerEventViewModel
    {
        public CompetitionRegistrationViewModel Registration { get; set; }

        public InviteeViewModel Invitee { get; set; }

        public InvitationSettingsViewModel Settings { get; set; }

        #region IHaveLicenseIssuer Members

        public string LicenseIssuerId => Registration.LicenseIssuerId;

        #endregion
    }
}