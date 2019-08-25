using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emando.Vantage.Components.Adapters.Competitions;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Mylaps;
using Emando.Vantage.Data.Competitions.SpeedSkating.LongTrack.Sara2;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.KNSB
{
    [Adapter("SARA/ETWClock Transponders")]
    public class KnsbSaraTranspondersImportAdapter : IRaceTranspondersImportAdapter
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding(1252);
        private readonly Func<ICompetitionContext> contextFactory;

        public KnsbSaraTranspondersImportAdapter(Func<ICompetitionContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        #region IRaceTranspondersAdapter Members

        public async Task<ICollection<RaceTransponder>> LoadFromStreamAsync(Guid competitionId, Guid distanceId, Stream stream)
        {
            using (var context = contextFactory())
            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var transponders = await context.Transponders.ToListAsync();
                    var converter = new MylapsTransponderCodeConverter();

                    var position = stream.Position;
                    var transpondersReader = new StreamReader(stream, Encoding);
                    var transponderKeyComparer = new TransponderKeyComparer();
                    var transpondersInFile = AFile.ReadTransponders(transpondersReader, MylapsTransponderCodeConverter.ProChipType,
                        l =>
                        {
                            long code;
                            return converter.TryConvertLabel(MylapsTransponderCodeConverter.ProChipType, l, out code) ? code : new long?();
                        });

                    foreach (var transponder in transpondersInFile)
                        if (!transponders.Contains(transponder, transponderKeyComparer))
                        {
                            context.Transponders.Add(transponder);
                            transponders.Add(transponder);
                        }

                    var existingTransponders = await (from r in context.RaceTransponders
                                                      where r.Race.DistanceId == distanceId
                                                      select r).ToListAsync();
                    foreach (var transponder in existingTransponders)
                        context.RaceTransponders.Remove(transponder);

                    await context.SaveChangesAsync();

                    var distance = await context.Distances.FirstAsync(d => d.CompetitionId == competitionId && d.Id == distanceId);
                    var races = await (from r in context.Races.Include(r => r.Competitor)
                                       where r.Distance.CompetitionId == competitionId && r.DistanceId == distanceId
                                       select r).ToListAsync();

                    await (from tcm in context.TeamCompetitorMembers.Include(m => m.Member)
                           where tcm.Team.Races.Any(r => r.Distance.CompetitionId == competitionId && r.DistanceId == distanceId)
                           select tcm).LoadAsync();

                    stream.Seek(position, SeekOrigin.Begin);
                    var raceTransponders = new List<RaceTransponder>();
                    var raceTranspondersReader = new StreamReader(stream, Encoding);
                    foreach (var transponder in AFile.ReadRaceTransponders(raceTranspondersReader, races, transponders, ShouldReversePeople(distance)))
                    {
                        context.RaceTransponders.Add(transponder);
                        raceTransponders.Add(transponder);
                    }
                    await context.SaveChangesAsync();

                    transaction.Commit();
                    return raceTransponders;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        #endregion

        private static bool ShouldReversePeople(Distance distance)
        {
            switch (distance.Discipline)
            {
                case "SpeedSkating.LongTrack.PairsDistance.TeamSprint":
                    return true;
                default:
                    return false;
            }
        }
    }
}