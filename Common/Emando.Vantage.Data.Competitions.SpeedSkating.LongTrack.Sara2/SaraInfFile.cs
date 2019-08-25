using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2
{
    public static class SaraInfFile
    {
        public static IEnumerable<Distance> ReadDistances(TextReader reader, Guid competitionId)
        {
            reader.ReadLine();
            reader.ReadLine();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int number = int.Parse(line.Substring(0, 3), NumberStyles.AllowLeadingWhite);
                int distance = int.Parse(line.Substring(5, 5), NumberStyles.AllowTrailingWhite);
                var date = DateTime.ParseExact(line.Substring(15, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string name = line.Substring(45, 30).Trim();
                var gender = name.StartsWith("Men") || name.StartsWith("Heren") ? Gender.Male : Gender.Female;

                yield return new Distance
                {
                    Id = Guid.NewGuid(),
                    CompetitionId = competitionId,
                    Number = number,
                    Name = name,
                    Starts = date,
                    Value = distance
                };
            }
        }
    }
}