using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Components.Competitions.Test
{
    [TestClass]
    public class ClubFilterTest
    {
        [TestMethod]
        public void TestClubFilter()
        {
            const string filter = "1000, d; 1100 - 1500, 3414 4551";

            Assert.IsFalse(ClubFilter.IsMatch(filter, null));
            Assert.IsFalse(ClubFilter.IsMatch(filter, -1));
            Assert.IsTrue(ClubFilter.IsMatch(filter, 1000));
            Assert.IsFalse(ClubFilter.IsMatch(filter, 1001));
            Assert.IsFalse(ClubFilter.IsMatch(filter, 1009));
            Assert.IsTrue(ClubFilter.IsMatch(filter, 1100));
            Assert.IsTrue(ClubFilter.IsMatch(filter, 1300));
            Assert.IsTrue(ClubFilter.IsMatch(filter, 1500));
            Assert.IsFalse(ClubFilter.IsMatch(filter, 1501));
            Assert.IsTrue(ClubFilter.IsMatch(filter, 3414));
            Assert.IsTrue(ClubFilter.IsMatch(filter, 4551));
            Assert.IsFalse(ClubFilter.IsMatch(filter, 4552));
        }
    }
}