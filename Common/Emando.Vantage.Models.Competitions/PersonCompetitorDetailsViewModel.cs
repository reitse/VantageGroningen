using System;

namespace Emando.Vantage.Models.Competitions
{
    public class PersonCompetitorDetailsViewModel : CompetitorDetailsViewModel
    {
        public NameViewModel Name { get; set; }

        public Guid PersonId { get; set; }
    }
}