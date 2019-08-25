using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2
{
    public static class AFile
    {
        public static IEnumerable<Transponder> ReadTransponders(TextReader reader, string type, Func<string, long?> labelConverter)
        {
            reader.ReadLine();

            while (true)
            {
                string labelLine;
                do
                {
                    labelLine = reader.ReadLine();
                    if (labelLine == null)
                        yield break;
                } while (labelLine.StartsWith("#"));

                int i = 13;
                while (labelLine.Length >= i + 8)
                {
                    string label = labelLine.Substring(i, 8);
                    i += 10;

                    var code = labelConverter(label);
                    if (!code.HasValue)
                        continue;

                    yield return new Transponder
                    {
                        Type = type,
                        Code = code.Value,
                        Label = label
                    };
                }
            }
        }

        public static IEnumerable<RaceTransponder> ReadRaceTransponders(TextReader reader, ICollection<Race> races, ICollection<Transponder> transponders, bool reversePeople)
        {
            reader.ReadLine();

            while (true)
            {
                string labelLine;
                do
                {
                    labelLine = reader.ReadLine();
                    if (labelLine == null)
                        yield break;
                } while (labelLine.StartsWith("#"));

                int startNumber = int.Parse(labelLine.Substring(0, 3), NumberStyles.None);
                if (startNumber == 0)
                    continue;

                foreach (var race in races.Where(r => r.Competitor.StartNumber == startNumber))
                {
                    IList<Guid> personIds;
                    var personCompetitor = race.Competitor as PersonCompetitor;
                    if (personCompetitor != null)
                        personIds = new[] { personCompetitor.PersonId };
                    else
                    {
                        var teamCompetitor = race.Competitor as TeamCompetitor;
                        if (teamCompetitor != null && teamCompetitor.Members != null)
                            personIds = reversePeople
                                ? teamCompetitor.Members.OrderByDescending(m => m.Order).Select(m => m.Member.PersonId).ToList()
                                : teamCompetitor.Members.OrderBy(m => m.Order).Select(m => m.Member.PersonId).ToList();
                        else
                            personIds = new Guid[0];
                    }

                    if (race.Transponders == null)
                        race.Transponders = new Collection<RaceTransponder>();

                    int i = 13;
                    while (labelLine.Length >= i + 8)
                    {
                        string label = labelLine.Substring(i, 8);
                        i += 10;

                        var transponder = transponders.SingleOrDefault(t => t.Label == label);
                        if (transponder == null)
                            continue;

                        var raceTransponder = new RaceTransponder
                        {
                            RaceId = race.Id,
                            Race = race,
                            PersonId = personIds.ElementAtOrDefault(race.Transponders.Count / 2),
                            Type = transponder.Type,
                            Code = transponder.Code,
                            Transponder = transponder
                        };

                        race.Transponders.Add(raceTransponder);

                        yield return raceTransponder;
                    }
                }
            }
        }
    }
}