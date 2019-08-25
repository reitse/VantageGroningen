using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class RenumberCompetitorListBindingModel
    {
        [Range(1, int.MaxValue)]
        public int From { get; set; }

        public int Add { get; set; }
    }
}