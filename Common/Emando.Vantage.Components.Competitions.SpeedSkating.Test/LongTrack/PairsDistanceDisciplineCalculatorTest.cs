using Emando.Vantage.Competitions;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Entities.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Components.Competitions.SpeedSkating.Test.LongTrack
{
    [TestClass]
    public class IndividualPairsDistanceCalculatorTest
    {
        private const decimal L400 = 400;
        private const decimal L333 = 333;

        public IDistance Distance(decimal trackLength, int length)
        {
            return new Distance
            {
                TrackLength = trackLength,
                Value = length,
                ValueQuantity = DistanceValueQuantity.Length
            };
        }

        [TestMethod]
        public void CalculateLapsTest()
        {
            Assert.AreEqual(1, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 167)));
            Assert.AreEqual(1, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 300)));
            Assert.AreEqual(1, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 333)));
            Assert.AreEqual(2, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 500)));
            Assert.AreEqual(3, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 700)));
            Assert.AreEqual(3, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 1000)));
            Assert.AreEqual(5, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 1500)));
            Assert.AreEqual(9, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 3000)));
            Assert.AreEqual(15, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 5000)));
            Assert.AreEqual(30, IndividualPairsDistanceCalculator.Default.Laps(Distance(L333, 10000)));
        }

        [TestMethod]
        public void CalculateLapDistanceTest()
        {
            Assert.AreEqual(0, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 1000), 0));
            Assert.AreEqual(100, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L400, 100), 1));

            Assert.AreEqual(300, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 300), 1));

            Assert.AreEqual(167, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 500), 1));
            Assert.AreEqual(500, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 500), 2));

            Assert.AreEqual(33, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 700), 1));
            Assert.AreEqual(367, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 700), 2));
            Assert.AreEqual(700, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 700), 3));

            Assert.AreEqual(333, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 5000), 1));
            Assert.AreEqual(667, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 5000), 2));
            Assert.AreEqual(1000, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 5000), 3));
            Assert.AreEqual(1333, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 5000), 4));
            Assert.AreEqual(1667, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 5000), 5));
            Assert.AreEqual(2000, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 5000), 6));

            Assert.AreEqual(167, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 1500), 1));
            Assert.AreEqual(500, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 1500), 2));
            Assert.AreEqual(833, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 1500), 3));
            Assert.AreEqual(1167, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 1500), 4));
            Assert.AreEqual(1500, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 1500), 5));

            Assert.AreEqual(200, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L400, 1000), 1));
            Assert.AreEqual(600, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L400, 1000), 2));
            Assert.AreEqual(1000, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L400, 1000), 3));

            Assert.AreEqual(400, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L400, 10000), 1));
            Assert.AreEqual(800, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L400, 10000), 2));

            Assert.AreEqual(1000, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L333, 1000), 100));
            Assert.AreEqual(1000, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L400, 1000), 100));
            Assert.AreEqual(800, IndividualPairsDistanceCalculator.Default.LapPassedLength(Distance(L400, 800), 100));
        }

        [TestMethod]
        public void CalculateExpectedLaneTest()
        {
            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 1000), 0, Lane.Inner));

            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 167), 1, Lane.Inner));
            Assert.AreEqual(Lane.Outer, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 300), 1, Lane.Inner));
            Assert.AreEqual(Lane.Outer, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 333), 1, Lane.Inner));
            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 500), 1, Lane.Inner));
            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 700), 1, Lane.Inner));
            Assert.AreEqual(Lane.Outer, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 1000), 1, Lane.Inner));

            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 1500), 1, Lane.Inner));
            Assert.AreEqual(Lane.Outer, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 1500), 2, Lane.Inner));
            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L333, 1500), 3, Lane.Inner));

            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L400, 1000), 1, Lane.Inner));
            Assert.AreEqual(Lane.Outer, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L400, 1000), 2, Lane.Inner));
            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L400, 1000), 3, Lane.Inner));

            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L400, 1500), 1, Lane.Inner));
            Assert.AreEqual(Lane.Outer, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L400, 1500), 2, Lane.Inner));
            Assert.AreEqual(Lane.Inner, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L400, 1500), 3, Lane.Inner));

            Assert.AreEqual(Lane.Outer, IndividualPairsDistanceCalculator.Default.ExpectedLane(Distance(L400, 10000), 1, Lane.Inner));
        }
    }
}