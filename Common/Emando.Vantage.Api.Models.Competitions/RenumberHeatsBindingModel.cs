using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class RenumberHeatsBindingModel
    {
        public bool ContinuousNumbering { get; set; }

        [Range(1, int.MaxValue)]
        public int? FirstHeat { get; set; }

        [Range(1, int.MaxValue)]
        public int From { get; set; }

        public int Add { get; set; }
    }
}