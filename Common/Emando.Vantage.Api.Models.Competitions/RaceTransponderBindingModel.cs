using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class RaceTransponderBindingModel
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public long Code { get; set; }

        [Required]
        public Guid PersonId { get; set; }

        public int? Set { get; set; }
    }
}