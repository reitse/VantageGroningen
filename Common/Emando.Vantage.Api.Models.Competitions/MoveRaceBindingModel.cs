using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class MoveRaceBindingModel
    {
        [Range(1, int.MaxValue)]
        public int Round { get; set; }

        [Range(1, int.MaxValue)]
        public int Heat { get; set; }

        [Range(0, int.MaxValue)]
        public int Lane { get; set; }
    }
}