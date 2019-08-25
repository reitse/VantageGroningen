using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class ChangePasswordBindingModel
    {
        public string Current { get; set; }

        [Required]
        public string New { get; set; }
    }
}