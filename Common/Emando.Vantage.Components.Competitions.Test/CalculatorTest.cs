using System.Collections.Generic;
using Emando.Vantage.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Components.Competitions.Test
{
    [TestClass]
    public class CalculatorTest
    {
        private IList<IVenueSegment> segments;

        [TestInitialize]
        public void InitializeTest()
        {
            segments = new List<IVenueSegment>
            {
                new MockVenueSegment(1, 2, 0, 110),
                new MockVenueSegment(2, 3, 0, 50),
                new MockVenueSegment(3, 4, 0, 50),
                new MockVenueSegment(4, 5, 0, 90),
                new MockVenueSegment(5, 1, 0, 100)
            };
        }

        [TestMethod]
        public void SameStartFinishPassedLengthTest()
        {
            //var loops = new MockDistanceLaneLocations(null, 2, 0, 2);

            //Assert.AreEqual(50, Calculator.PassedLength(0, 3, loops, segments));
            //Assert.AreEqual(400, Calculator.PassedLength(0, 2, loops, segments));
            //Assert.AreEqual(400 + 50, Calculator.PassedLength(1, 3, loops, segments));
            //Assert.AreEqual(3 * 400 + 50 + 50 + 90 + 100, Calculator.PassedLength(3, 1, loops, segments));
        }

        [TestMethod]
        public void DifferentStartFinishPassedLengthTest()
        {
            //var loops = new MockDistanceLaneLocations(null, 5, 0, 2);

            //Assert.AreEqual(100, Calculator.PassedLength(0, 1, loops, segments));
            //Assert.AreEqual(100 + 110 + 50, Calculator.PassedLength(0, 3, loops, segments));
            //Assert.AreEqual(100 + 110, Calculator.PassedLength(0, 2, loops, segments));
            //Assert.AreEqual(100 + 110 + 50, Calculator.PassedLength(1, 3, loops, segments));
            //Assert.AreEqual(100 + 110 + 2 * 400 + 50 + 50 + 90 + 100, Calculator.PassedLength(3, 1, loops, segments));
        }
    }
}