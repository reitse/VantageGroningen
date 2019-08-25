using System;

namespace Emando.Vantage.Components.Competitions
{
    public static class DisciplineExpertExtensions
    {
        public static int Season(this IDisciplineCalculator calculator, DateTime? reference = null)
        {
            reference = reference ?? DateTime.UtcNow.Date;
            return calculator.Season(reference.Value);
        }
    }
}