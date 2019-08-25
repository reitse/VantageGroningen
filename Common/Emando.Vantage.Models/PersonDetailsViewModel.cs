namespace Emando.Vantage.Models
{
    public class PersonDetailsViewModel : PersonViewModel, IPerson
    {
        public PersonLicenseViewModel[] Licenses { get; set; }

        Name IPerson.Name => new Name(Name?.Initials, Name?.FirstName, Name?.SurnamePrefix, Name?.Surname);

        public string Iban { get; set; }
    }
}