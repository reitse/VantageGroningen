using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2.Test.Properties;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2.Test
{
    [TestClass]
    public class CFileTest
    {
        private static readonly Dictionary<string, Person> Persons = new Dictionary<string, Person>
        {
            {
                "HV42013", new Person
                {
                    Id = new Guid("3d14ef53-c620-4989-82f2-83985bafe702"),
                    Name = new Name
                    {
                        FirstName = "Wieteke",
                        Surname = "Cramer"
                    }
                }
            }
        };

        private static Person PersonLookup(string key, Name name, string nationalityCode, Action<Person> createdCallback = null)
        {
            if (Persons.ContainsKey(key))
                return Persons[key];

            var person = new Person
            {
                Id = Guid.NewGuid(),
                Licenses = new Collection<PersonLicense>
                {
                    new PersonLicense
                    {
                        IssuerId = "Test",
                        Key = key
                    }
                },
                Name = name,
                NationalityCode = nationalityCode
            };
            Persons.Add(key, person);
            if (createdCallback != null)
                createdCallback(person);

            return person;
        }

        [TestMethod]
        public void ReadPersonCompetitorsTest()
        {
            string file = Resources.CFile;
            int personsCreated = 0;
            var personLookup = new CFile.PersonLookup((key, name, nationalityCode) => PersonLookup(key, name, nationalityCode, p => { personsCreated++; }));

            IList<PersonCompetitor> competitors;
            using (var reader = new StringReader(file))
                competitors = CFile.ReadPersonCompetitors(reader, personLookup, "NED").ToList();

            Assert.AreEqual(21, competitors.Count);
            Assert.AreEqual(Persons["HV42013"], competitors[3].Person);
            Assert.AreEqual(20, personsCreated);
            Assert.IsTrue(competitors.All(c => c.NationalityCode == "NED"));
            Assert.AreEqual(21, competitors[0].StartNumber);
            Assert.AreEqual("Annouk", competitors[0].Name.FirstName);
            Assert.AreEqual("vd", competitors[0].Name.SurnamePrefix);
            Assert.AreEqual("Weijden", competitors[0].Name.Surname);
            Assert.AreEqual("Wüst", competitors[17].Name.Surname);
        }

        [TestMethod]
        public void ReadDrawTest()
        {
            string file = Resources.CFile;
            var personLookup = new CFile.PersonLookup((key, name, nationalityCode) => PersonLookup(key, name, nationalityCode));
            var competitorList = new PersonCompetitorList();
            using (var reader = new StringReader(file))
                competitorList.Competitors = new List<CompetitorBase>(CFile.ReadPersonCompetitors(reader, personLookup));

            IList<Race> draw;
            using (var reader = new StringReader(file))
                draw = CFile.ReadDraw(reader, competitorList.Competitors).ToList();

            Assert.AreEqual(21, draw.Count);
            Assert.IsTrue(draw.Select(r => r.Heat).Distinct().SequenceEqual(Enumerable.Range(1, 11)));
            Assert.IsTrue(draw.All(r => r.Competitor != null));
            Assert.AreEqual(0, draw[9].Lane);
            Assert.AreEqual(1, draw[10].Color);
        }
    }
}