using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public class DistanceCombinationDistanceViewModel
    {
        public Guid Id { get; set; }

        public string Discipline { get; set; }

        public int Number { get; set; }

        public int Value { get; set; }

        public DistanceValueQuantity ValueQuantity { get; set; }

        public string Name { get; set; }

        public DateTime? Starts { get; set; }
    }
}