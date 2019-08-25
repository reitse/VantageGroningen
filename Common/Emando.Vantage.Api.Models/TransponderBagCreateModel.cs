using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class TransponderBagCreateModel
    {
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
    }
}