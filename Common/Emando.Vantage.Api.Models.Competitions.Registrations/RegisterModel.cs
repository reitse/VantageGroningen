using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions.Registrations
{
    public class RegisterModel
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public bool RegisterForSerie { get; set; }

        [Required]
        public Guid[] DistanceCombinations { get; set; }
    }
}