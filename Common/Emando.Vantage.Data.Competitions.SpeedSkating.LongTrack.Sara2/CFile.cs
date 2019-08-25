using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2
{
    public static class CFile
    {
        #region Delegates

        public delegate Person PersonLookup(string key, Name name, string nationalityCode);

        #endregion

        public static IEnumerable<PersonCompetitor> ReadPersonCompetitors(TextReader reader, PersonLookup personLookup, string nationalityCodeOverride = null)
        {
            reader.ReadLine();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int startNumber = int.Parse(line.Substring(6, 3), NumberStyles.None);
                if (startNumber == 0)
                    continue;

                var name = Name.Parse(line.Substring(10, 25).Trim());
                string nationalityCode = nationalityCodeOverride ?? line.Substring(36, 3).Trim();
                string personKey = line.Substring(48, 7);
                var person = personLookup(personKey, name, nationalityCode);

                yield return new PersonCompetitor
                {
                    Id = Guid.NewGuid(),
                    StartNumber = startNumber,
                    NationalityCode = nationalityCode,
                    Name = name,
                    PersonId = person.Id,
                    Person = person
                };
            }
        }

        public static IEnumerable<Race> ReadDraw(TextReader reader, ICollection<CompetitorBase> competitors)
        {
            reader.ReadLine();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int startNumber = int.Parse(line.Substring(6, 3), NumberStyles.None);
                if (startNumber == 0)
                    continue;

                var competitor = competitors.SingleOrDefault(c => c.StartNumber == startNumber);
                if (competitor == null)
                    continue;

                int pair = int.Parse(line.Substring(0, 3), NumberStyles.None);
                var lane = line[4] == 'I' ? Lane.Inner : Lane.Outer;
                var color = lane == Lane.Inner ? PairsRaceColor.White : PairsRaceColor.Red;

                yield return new Race
                {
                    Id = Guid.NewGuid(),
                    CompetitorId = competitor.Id,
                    Competitor = competitor,
                    Heat = pair,
                    Lane = (int)lane,
                    Color = (int)color
                };
            }
        }
    }
}