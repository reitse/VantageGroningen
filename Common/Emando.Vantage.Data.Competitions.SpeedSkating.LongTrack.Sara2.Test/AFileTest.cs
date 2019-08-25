using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2.Test.Properties;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2.Test
{
    [TestClass]
    public class AFileTest
    {
        [TestMethod]
        public void ReadTranspondersTest()
        {
            const string transponderType = "MYLAPS ProChip";

            var file = Resources.AFile;
            var converter = new MylapsTransponderCodeConverter();
            var labelConverter = new Func<string, long?>(s =>
            {
                long code;
                return converter.TryConvertLabel(transponderType, s, out code) ? code : new long?();
            });

            IList<Transponder> transponders;
            using (var reader = new StringReader(file))
                transponders = AFile.ReadTransponders(reader, transponderType, labelConverter).ToList();

            Assert.AreEqual(78, transponders.Count);
            Assert.IsTrue(transponders.All(t => t.Type == transponderType));
            Assert.AreEqual(102061448, transponders[38].Code);
            Assert.AreEqual("CX-98152", transponders[38].Label);
            Assert.AreEqual(102000459, transponders[39].Code);
            Assert.AreEqual("CX-37163", transponders[39].Label);
            Assert.AreEqual("HC-81093", transponders.Last().Label);
        }

        [TestMethod]
        public void ReadRaceTranspondersTest()
        {
            string file = Resources.AFile;
            var races = new List<Race>
            {
                new Race
                {
                    Heat = 16,
                    Lane = 1,
                    Competitor = new PersonCompetitor
                    {
                        PersonId = new Guid("{4F6178BB-0D05-493C-A600-5A9E2D3C40E1}"),
                        StartNumber = 4,
                        Name = new Name("S.", "Shannon", "Rempel")
                    }
                }
            };
            var transponders = new List<Transponder>
            {
                new Transponder
                {
                    Code = 111140411,
                    Label = "NZ-77115"
                },
                new Transponder
                {
                    Code = 102433532,
                    Label = "FG-70236"
                }
            };

            IList<RaceTransponder> raceTransponders;
            using (var reader = new StringReader(file))
                raceTransponders = AFile.ReadRaceTransponders(reader, races, transponders, false).ToList();

            Assert.AreEqual(2, raceTransponders.Count);
            Assert.AreEqual("Shannon Rempel", raceTransponders[0].Race.Competitor.FullName);
            Assert.AreEqual(102433532, raceTransponders[1].Transponder.Code);
            Assert.AreEqual(new Guid("{4F6178BB-0D05-493C-A600-5A9E2D3C40E1}"), raceTransponders[0].PersonId);
        }
    }
}