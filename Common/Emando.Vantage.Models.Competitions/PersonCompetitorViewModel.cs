using System;

namespace Emando.Vantage.Models.Competitions
{
    public class PersonCompetitorViewModel : CompetitorViewModel
    {
        public NameViewModel Name { get; set; }

        public Guid PersonId { get; set; }
    }
}