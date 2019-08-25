using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models
{
    public class ReportTemplateUpdateModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}