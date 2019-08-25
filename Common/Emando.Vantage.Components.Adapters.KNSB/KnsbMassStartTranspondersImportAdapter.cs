using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emando.Vantage.Components.Adapters.Competitions;
using Emando.Vantage.Components.Adapters.KNSB.Properties;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.KNSB
{
    [Adapter("KNSB Mass Start Transponders")]
    public class KnsbMassStartTranspondersImportAdapter : IRaceTranspondersImportAdapter
    {
        private const string TransponderType = "MYLAPS ProChip";
        private static readonly Encoding Encoding = Encoding.GetEncoding(1252);
        private readonly Func<ICompetitionContext> contextFactory;
        private readonly ITransponderCodeConverter transponderCodeConverter;

        public KnsbMassStartTranspondersImportAdapter(Func<ICompetitionContext> contextFactory, ITransponderCodeConverter transponderCodeConverter)
        {
            this.contextFactory = contextFactory;
            this.transponderCodeConverter = transponderCodeConverter;
        }

        #region IRaceTranspondersImportAdapter Members

        public async Task<ICollection<RaceTransponder>> LoadFromStreamAsync(Guid competitionId, Guid distanceId, Stream stream)
        {
            using (var context = contextFactory())
            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
            using (var reader = new StreamReader(stream, Encoding))
                try
                {
                    var transponders = await context.Transponders.ToListAsync();
                    var races = await (from r in context.Races
                                       where r.Distance.CompetitionId == competitionId && r.DistanceId == distanceId
                                       select new
                                       {
                                           r.Id,
                                           r.Lane,
                                           r.Competitor,
                                           Existing = r.Transponders
                                       }).ToListAsync();
                    var raceTransponders = new List<RaceTransponder>();

                    var i = 0;
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        i++;

                        var parts = line.Split(',', ';', '\t');
                        if (parts.Length < 2)
                            throw new FormatException(string.Format(Resources.TooFewFields, 2, i));

                        int lane;
                        if (!int.TryParse(parts[0], out lane))
                            throw new FormatException(string.Format(Resources.InvalidLane, parts[0], i));

                        int startNumber;
                        if (!int.TryParse(parts[1], out startNumber))
                            throw new FormatException(string.Format(Resources.InvalidStartNumber, parts[1], i));

                        var race = races.SingleOrDefault(r => r.Competitor.StartNumber == startNumber && r.Lane == lane);
                        if (race == null)
                            throw new FormatException(string.Format(Resources.RaceNotFound, startNumber, lane, i));

                        var competitor = race.Competitor as PersonCompetitor;
                        if (competitor == null)
                            throw new FormatException(string.Format(Resources.InvalidCompetitorClass, i));

                        foreach (var existing in race.Existing)
                            context.RaceTransponders.Remove(existing);
                        await context.SaveChangesAsync();

                        foreach (var label in parts.Skip(2))
                        {
                            long code;
                            if (!transponderCodeConverter.TryConvertLabel(TransponderType, label, out code))
                                throw new FormatException(string.Format(Resources.InvalidTransponderLabel, label, TransponderType, i));

                            var transponder = transponders.SingleOrDefault(t => t.Type == TransponderType && t.Code == code);
                            if (transponder == null)
                            {
                                transponder = new Transponder
                                {
                                    Type = TransponderType,
                                    Code = code,
                                    Label = label
                                };
                                context.Transponders.Add(transponder);
                                transponders.Add(transponder);
                            }

                            var raceTransponder = new RaceTransponder
                            {
                                Transponder = transponder,
                                PersonId = competitor.PersonId,
                                RaceId = race.Id
                            };
                            raceTransponders.Add(raceTransponder);

                            context.RaceTransponders.Add(raceTransponder);
                            await context.SaveChangesAsync();
                        }
                    }

                    transaction.Commit();
                    return raceTransponders;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
        }

        #endregion
    }
}