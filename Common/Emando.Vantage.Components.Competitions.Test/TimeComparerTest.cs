using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Components.Competitions.Test
{
    [TestClass]
    public class TimeComparerTest
    {
        [TestMethod]
        public void TimeCompareTest()
        {
            var comparer = new TimeSpanPrecisionComparer(TimeSpan.FromMilliseconds(10));

            Assert.IsTrue(comparer.Equals(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2)));
            Assert.IsTrue(comparer.Equals(TimeSpan.FromSeconds(2.3), TimeSpan.FromSeconds(2.3)));
            Assert.IsTrue(comparer.Equals(TimeSpan.FromSeconds(2.34), TimeSpan.FromSeconds(2.34)));

            Assert.IsFalse(comparer.Equals(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3)));
            Assert.IsFalse(comparer.Equals(TimeSpan.FromSeconds(2.3), TimeSpan.FromSeconds(2.4)));
            Assert.IsFalse(comparer.Equals(TimeSpan.FromSeconds(2.34), TimeSpan.FromSeconds(2.35)));

            Assert.IsTrue(comparer.Equals(TimeSpan.FromSeconds(2.345), TimeSpan.FromSeconds(2.345)));
            Assert.IsTrue(comparer.Equals(TimeSpan.FromSeconds(2.345), TimeSpan.FromSeconds(2.346)));
            Assert.IsTrue(comparer.Equals(TimeSpan.FromSeconds(2.340), TimeSpan.FromSeconds(2.349)));
        }

        [TestMethod]
        public void TimeCompareWithoutTruncationTest()
        {
            var comparer = new TimeSpanPrecisionComparer(TimeSpan.Zero);

            Assert.IsTrue(comparer.Equals(TimeSpan.FromTicks(1), TimeSpan.FromTicks(1)));
            Assert.IsFalse(comparer.Equals(TimeSpan.FromTicks(1), TimeSpan.FromTicks(2)));
        }
    }
}