using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class PersonBindingModel
    {
        public NameBindingModel Name { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public AddressBindingModel Address { get; set; }

        public Gender Gender { get; set; }

        public string NationalityCode { get; set; }

        public DateTime BirthDate { get; set; }

        [StringLength(34)]
        public string Iban { get; set; }

        public void SetDefaultCasing()
        {
            Name?.SetDefaultCasing();
            Email = Email?.ToLower();
            Phone = Phone?.ToUpper();
            Address?.SetDefaultCasing();
            NationalityCode = NationalityCode?.ToUpper();
        }
    }
}