using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions.Registrations
{
    public class WithdrawModel
    {
        [Required]
        public Guid[] DistanceCombinations { get; set; }
    }
}