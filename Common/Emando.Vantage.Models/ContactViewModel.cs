namespace Emando.Vantage.Models
{
    public class ContactViewModel
    {
        public string OrganizationName { get; set; }

        public NameViewModel Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public AddressViewModel Address { get; set; }

        public string Extra { get; set; }

        public string Url { get; set; }
    }
}