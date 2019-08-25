using System;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class DistanceCombinationCreateModel
    {
        [Range(1, int.MaxValue)]
        public int Number { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string ClassFilter { get; set; }
        
        [Required, StringLength(200)]
        public string CategoryFilter { get; set; }

        [Range(1, int.MaxValue)]
        public int ClassificationWeight { get; set; }

        public DateTime? Starts { get; set; }
    }
}