namespace Emando.Vantage.Models.Events
{
    public class PersonLicenseChangedEventViewModel : EventViewModelBase, ILicenseIssuerEventViewModel
    {
        public PersonLicenseDetailsViewModel License { get; set; }

        #region IHaveLicenseIssuer Members

        public string LicenseIssuerId => License.IssuerId;

        #endregion
    }
}