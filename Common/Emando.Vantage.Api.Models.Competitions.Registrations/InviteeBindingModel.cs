using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions.Registrations
{
    public class InviteeBindingModel
    {
        [StringLength(50)]
        [Required]
        public string LicenseIssuerId { get; set; }

        [StringLength(100)]
        [Required]
        public string LicenseDiscipline { get; set; }

        [StringLength(100)]
        [Required]
        public string LicenseKey { get; set; }

        [EmailAddress]
        [StringLength(100)]
        [Required]
        public string Email { get; set; }

        public bool IsInvited { get; set; }

        [Range(1, int.MaxValue)]
        public int? Reserve { get; set; }
    }
}