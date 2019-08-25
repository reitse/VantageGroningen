using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions.Registrations
{
    public class RegisterWithNewTemporaryLicenseModel
    {
        [Required]
        public PersonBindingModel Person { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public Guid[] DistanceCombinations { get; set; }

        public void SetDefaultCasing()
        {
            Person?.SetDefaultCasing();
            Email = Email?.ToLower();
        }
    }
}