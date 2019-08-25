using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class VantageUserUpdateModel
    {
        [Required]
        public NameBindingModel Name { get; set; }
    }
}