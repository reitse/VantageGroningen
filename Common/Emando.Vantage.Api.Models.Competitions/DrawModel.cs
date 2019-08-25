using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class DrawModel
    {
        [Required]
        public Guid[] DistanceCombinations { get; set; }

        public List<List<Guid>> Groups { get; set; }

        public GroupModel Grouping { get; set; }

        public bool DeleteExisting { get; set; }

        public DistanceDrawMode Mode { get; set; }

        public bool ReverseFilling { get; set; }

        public bool ReverseGroups { get; set; }

        public DistanceDrawSpreading Spreading { get; set; }
    }
}