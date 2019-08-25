using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2.Test.Properties;
using Emando.Vantage.Entities.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2.Test
{
    [TestClass]
    public class SaraInfFileTest
    {
        [TestMethod]
        public void ReadDistancesTest()
        {
            string fileContents = Resources.SaraInfFile;

            IList<Distance> distances;
            using (var reader = new StringReader(fileContents))
                distances = SaraInfFile.ReadDistances(reader, Guid.Empty).ToList();

            Assert.AreEqual(10, distances.Count);
            Assert.AreEqual(2, distances[1].Number);
            Assert.AreEqual(1000, distances[3].Value);
            Assert.AreEqual(new DateTime(2008, 1, 6), distances[4].Starts);
            Assert.AreEqual("Heren Neo-/Senioren 1000 meter", distances[7].Name);
        }
    }
}