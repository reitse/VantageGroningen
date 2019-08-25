using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class ClubUpdateModel
    {
        [StringLength(20)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
    }
}