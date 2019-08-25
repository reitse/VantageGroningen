using System;
using System.ComponentModel.DataAnnotations;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class GroupModel
    {
        public DistanceDrawGroupMode GroupMode { get; set; }

        [Range(0, int.MaxValue)]
        public int CategoryLength { get; set; }

        [Range(1, int.MaxValue)]
        public int GroupSize { get; set; }
        
        [Required]
        public Guid[] DistanceCombinations { get; set; }

        [Required]
        public HistoricalTimeSelectorBindingModel[] Selectors { get; set; }
    }
}