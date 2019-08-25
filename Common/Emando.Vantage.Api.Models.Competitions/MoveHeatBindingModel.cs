using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class MoveHeatBindingModel
    {
        [Range(1, int.MaxValue)]
        public int Round { get; set; }

        [Range(1, int.MaxValue)]
        public int Heat { get; set; }
    }
}