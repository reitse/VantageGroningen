namespace Emando.Vantage.Models
{
    public class PersonRestrictedDetailsViewModel : RestrictedPersonViewModel
    {
        public PersonLicenseViewModel[] Licenses { get; set; }
    }
}