using System;

namespace Emando.Vantage.Api.Models.Competitions
{
    public class DistanceReportsViewModel
    {
        public Guid Id { get; set; }

        public string[] Reports { get; set; }
    }
}