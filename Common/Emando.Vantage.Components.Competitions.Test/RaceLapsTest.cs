using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Entities.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Components.Competitions.Test
{
    [TestClass]
    public class RaceLapsTest
    {
        [TestMethod]
        public void GroupByPresentedIndexTest()
        {
            var laps = new List<MockRaceLap>
            {
                new MockRaceLap(10, true, "Photofinish", 6),
                new MockRaceLap(20, true, "Photofinish", 12)
            };

            var groups = laps.GroupByPresented().ToList();

            Assert.AreEqual(13, groups.Count);
        }

        [TestMethod]
        public void GroupByPresentedTest()
        {
            var laps = new List<MockRaceLap>();
            var photofinish = new[] { 11, 21 };
            var transponder = new[] { 6, 12, 17, 22 };
            var optical = new[] { 21, 16, 10 };
            laps.AddRange(photofinish.Select((t, i) => new MockRaceLap(t, true, "Photofinish", i * 2 + 1)));
            laps.AddRange(transponder.Select(time => new MockRaceLap(time, true, "Transponder")));
            laps.AddRange(optical.Select(time => new MockRaceLap(time, false, "Optical")));

            var groups = laps.GroupByPresented().ToList();

            Assert.AreEqual(4, groups.Count);
            Assert.AreEqual("Transponder", groups[0].Key.PresentationSource.How);
            Assert.AreEqual("Photofinish", groups[1].Key.PresentationSource.How);
            Assert.AreEqual("Transponder", groups[2].Key.PresentationSource.How);
            Assert.AreEqual("Photofinish", groups[3].Key.PresentationSource.How);

            Assert.AreEqual(0, groups[0].Count());
            Assert.AreEqual(2, groups[1].Count());
            Assert.AreEqual(1, groups[2].Count());
            Assert.AreEqual(2, groups[3].Count());

            Assert.AreEqual("10,12", string.Join(",", groups[1].OrderBy(l => l.Time).Select(l => l.Time.TotalSeconds)));
            Assert.AreEqual("16", string.Join(",", groups[2].OrderBy(l => l.Time).Select(l => l.Time.TotalSeconds)));
            Assert.AreEqual("21,22", string.Join(",", groups[3].OrderBy(l => l.Time).Select(l => l.Time.TotalSeconds)));
        }

        [TestMethod]
        public void GroupByRankingTest()
        {
            var laps = new List<MockRaceLap>
            {
                new MockRaceLap(20, true, "Optical"),
                new MockRaceLap(25, true, "Optical"),
                new MockRaceLap(26, false, "Optical"),
                new MockRaceLap(19, true, "Optical"),
                new MockRaceLap(20, true, "Optical"),
                new MockRaceLap(40, true, "Optical", fixedRanking: 2),
                new MockRaceLap(50, true, "Optical", fixedRanking: 3)
            };

            var groups = laps.GroupByRanking();

            Assert.AreEqual(3, groups.Count);
            Assert.AreEqual(1, groups[0].Key);
            Assert.AreEqual(1, groups[0].Count());
            Assert.AreEqual(2, groups[1].Key);
            Assert.AreEqual(3, groups[1].Count());
            Assert.AreEqual(5, groups[2].Key);
            Assert.AreEqual(2, groups[2].Count());
        }

        #region Nested type: MockLap

        #endregion
    }
}