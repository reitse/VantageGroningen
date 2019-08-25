using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class VantageUserCreateModel
    {
        [Required]
        public string Password { get; set; }

        public NameBindingModel Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}