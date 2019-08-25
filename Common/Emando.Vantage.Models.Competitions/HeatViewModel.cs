using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Models.Competitions
{
    public struct HeatViewModel
    {
        public int Round { get; set; }

        public int Number { get; set; }

        public static implicit operator Heat(HeatViewModel model)
        {
            return new Heat(model.Round, model.Number);
        }

        public override string ToString()
        {
            return $"{Round}.{Number}";
        }
    }
}