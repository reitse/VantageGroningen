using System;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.Test.LongTrack
{
    [TestClass]
    public class LongTrackDisciplineExpertTest
    {
        private readonly LongTrackDisciplineCalculator calculator = new LongTrackDisciplineCalculator();

        [TestMethod]
        public void SeasonAgeTest()
        {
            Assert.AreEqual(12, calculator.SeasonAge(calculator.CurrentSeason, new DateTime(2002, 7, 1)));
            Assert.AreEqual(11, calculator.SeasonAge(calculator.CurrentSeason, new DateTime(2003, 7, 1)));
            Assert.AreEqual(12, calculator.SeasonAge(calculator.CurrentSeason, new DateTime(2002, 7, 2)));
            Assert.AreEqual(13, calculator.SeasonAge(calculator.CurrentSeason, new DateTime(2002, 2, 26)));
        }
    }
}