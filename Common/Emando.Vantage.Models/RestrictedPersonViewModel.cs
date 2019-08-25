using System;

namespace Emando.Vantage.Models
{
    public class RestrictedPersonViewModel
    {
        public Guid Id { get; set; }

        public NameViewModel Name { get; set; }

        public Gender Gender { get; set; }

        public RestrictedAddressViewModel Address { get; set; }

        public string NationalityCode { get; set; }

        public DateTime BirthDate { get; set; }
    }
}