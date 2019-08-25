using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class ContactBindingModel
    {
        [StringLength(100)]
        public string OrganizationName { get; set; }
        [Required]
        public NameBindingModel Name { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [Required]
        public AddressBindingModel Address { get; set; }

        public string Extra { get; set; }

        [StringLength(200)]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}