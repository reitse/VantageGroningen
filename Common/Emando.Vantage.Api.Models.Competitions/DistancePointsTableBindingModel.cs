using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class DistancePointsTableBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DistancePointsBindingModel[] Points { get; set; }
    }
}