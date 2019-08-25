using System;

namespace Emando.Vantage.Models
{
    public class PersonViewModel
    {
        public Guid Id { get; set; }

        public NameViewModel Name { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Address Address { get; set; }

        public string NationalityCode { get; set; }

        public DateTime BirthDate { get; set; }
    }
}