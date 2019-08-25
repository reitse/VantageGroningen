using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class UpdateHeatBindingModel
    {
        [Required]
        public List<Guid?> Competitors { get; set; }
    }
}