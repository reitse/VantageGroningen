using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Emando.Vantage.Competitions;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Components.Adapters.Competitions;
using Emando.Vantage.Components.Adapters.KNSB.Properties;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Adapters.KNSB
{
    [Adapter("KNSB ETWClock", 300)]
    public class KnsbETWClockCompetitionAdapter : ICompetitionResultsImportAdapter, ICompetitionExportAdapter
    {
        private const string ResultInstanceName = "ETWClock";
#if DEBUG
        private const string ScheduleFileName = "SARAINFO.DAT.txt";
        private const string DistanceFileName = "{0}{1:00000}M.D{2:00}.txt";
#else
        private const string ScheduleFileName = "SARAINFO.DAT";
        private const string DistanceFileName = "{0}{1:00000}M.D{2:00}";
#endif

        private static readonly Encoding Encoding = Encoding.GetEncoding(1252);
        private static readonly CultureInfo CultureInfo = CultureInfo.GetCultureInfo("nl-NL");

        private readonly Func<ICompetitionContext> contextFactory;
        private readonly IDistanceDisciplineCalculatorManager calculatorManager;
        private const int DefaultClassificationWeight = 500;

        public KnsbETWClockCompetitionAdapter(Func<ICompetitionContext> contextFactory, IDistanceDisciplineCalculatorManager calculatorManager)
        {
            this.contextFactory = contextFactory;
            this.calculatorManager = calculatorManager;
        }

        #region ICompetitionExportAdapter Members

        public string FileExtension => ".zip";

        public string MediaType => "application/zip";

        public async Task ExportAsync(Guid competitionId, Stream stream, CultureInfo culture)
        {
            if (!stream.CanSeek)
                stream = new NonSeekableStreamWrapper(stream);

            using (var context = contextFactory())
            using (var archive = new ZipArchive(stream, ZipArchiveMode.Create))
            {
                var competition = await context.Competitions
                    .Include(c => c.Distances.Select(d => d.Races.Select(r => r.Results)))
                    .Include(c => c.Distances.Select(d => d.Races.Select(r => r.Times)))
                    .Include(c => c.Distances.Select(d => d.Races.Select(r => r.Transponders.Select(t => t.Transponder))))
                    .FirstOrDefaultAsync(c => c.Id == competitionId);
                if (competition == null)
                    throw new CompetitionNotFoundException();

                await context.Competitors.Where(c => c.List.CompetitionId == competitionId).LoadAsync();

                using (var scheduleStream = archive.CreateEntry(ScheduleFileName).Open())
                    ExportSchedule(competition, scheduleStream);

                var distances = competition.Distances.Where(d => d.Discipline.StartsWith("SpeedSkating.LongTrack.PairsDistance")).ToList();
                foreach (var distance in distances)
                {
                    using (var racesStream = archive.CreateEntry(GetDistanceFileName(distance, 'C')).Open())
                        ExportRaces(distance, distances.Where(d => d.Number < distance.Number), racesStream);

                    using (var transpondersStream = archive.CreateEntry(GetDistanceFileName(distance, 'A')).Open())
                        ExportTransponders(distance, transpondersStream);
                }
            }
        }

        #endregion

        private string GetDistanceFileName(IDistance distance, char type)
        {
            var calculator = calculatorManager.Get(distance.Discipline);
            return string.Format(DistanceFileName, type, calculator.Length(distance), distance.Number);
        }

        private void ExportSchedule(Competition competition, Stream stream)
        {
            using (var writer = new StreamWriter(stream, Encoding))
            {
                writer.WriteLine("NR. |AFSTAND  |DATUM     |BAAN|ET-filenaam");
                writer.WriteLine("----+---------+----------+----+-----------");

                foreach (var distance in competition.Distances.OrderBy(d => d.Number))
                {
                    var calculator = calculatorManager.Get(distance.Discipline);
                    writer.WriteLine($"{distance.Number,3}  " +
                        $"{calculator.Length(distance),-5}m.   " +
                        $"{distance.Starts:dd'/'MM'/'yyyy}  " +
                        $"{competition.VenueCode,-3} " +
                        $"{GetDistanceFileName(distance, 'C')}   " +
                        $"{new string(distance.Name.Take(30).ToArray()),-30}  " +
                        $"{distance.Id}");
                }
            }
        }

        private void ExportRaces(Distance distance, IEnumerable<Distance> previousDistances, Stream stream)
        {
            var calculator = calculatorManager.Get(distance.Discipline);

            using (var writer = new StreamWriter(stream, Encoding))
            {
                var lastPair = distance.Races.Select(r => r.Heat).DefaultIfEmpty(distance.FirstHeat).Max();
                var pairs = distance.Races.Any() ? lastPair - distance.FirstHeat + 1 : 0;
                writer.WriteLine($"{pairs:000} {distance.FirstHeat - 1:000} E #{distance.Number:00}: {distance.Name}");

                for (var pair = distance.FirstHeat; pair <= lastPair; pair++)
                    for (var lane = 0; lane <= 1; lane++)
                    {
                        var race = distance.Races.FirstOrDefault(r => r.Round == 1 && r.Heat == pair && r.Lane == lane);
                        if (race == null)
                            writer.WriteLine($"{pair - distance.FirstHeat + 1:000} " +
                                $"{LaneToString(lane)} " +
                                $"{0:000}");
                        else
                            writer.WriteLine($"{pair - distance.FirstHeat + 1:000} " +
                                $"{LaneToString(lane)} " +
                                $"{race.Competitor.StartNumber:000} " +
                                $"{new string(race.Competitor.FullName.Take(25).ToArray()),-25} " +
                                $"{new string(race.Competitor.ClubShortName?.Take(7).ToArray() ?? new char[0]),-7} " +
                                $"{new string(race.Competitor.Category.Take(3).ToArray()),-3} " +
                                $"{new string(race.Competitor.LicenseKey?.Take(8).ToArray() ?? new char[0]),-8}" +
                                $"{new string(race.Competitor.ShortName.ToUpperInvariant().Take(16).ToArray()),-16} " +
                                $"{TimeSpanToString(race.PersonalBest)} " +
                                $"{calculator.Length(distance):00000} m " +
                                $"{PointsToString(null)} " +
                                $"{PointsToString(race.PresentedResult?.Points)} " +
                                $"{TimeSpanToString(race.PresentedTime?.Time)} " +
                                $"({RaceToTimeInfoString(race)}) " +
                                $"{TimeSpanToString(race.SeasonBest)}");
                    }
            }
        }

        private void ExportTransponders(Distance distance, Stream stream)
        {
            var calculator = calculatorManager.Get(distance.Discipline);

            using (var writer = new StreamWriter(stream, Encoding))
            {
                writer.WriteLine("SNR Pas DID  Trans-L1  Trans-R1  Trans-L2  Trans-R2  Trans-L3  Trans-R3  Trans-L4  Trans-R4");

                int passage;
                switch (distance.Discipline)
                {
                    case "SpeedSkating.LongTrack.PairsDistance.TeamPursuit":
                        passage = 3;
                        break;
                    default:
                        passage = 1;
                        break;
                }

                int loopId;
                switch (calculator.Length(distance))
                {
                    case 1000:
                        loopId = 10;
                        break;
                    default:
                        loopId = 5;
                        break;
                }

                foreach (var race in distance.Races.OrderBy(r => r.Round).ThenBy(r => r.Heat).ThenBy(r => r.Lane))
                    writer.WriteLine($"{race.Competitor.StartNumber:000} " +
                        $"{passage:000} " +
                        $"{loopId:000}  " +
                        $"{string.Join("  ", race.Transponders.Select(t => t.Transponder.Label))}");
            }
        }

        private static string LaneToString(int lane)
        {
            switch (lane)
            {
                case 0:
                    return "I";
                case 1:
                    return "O";
                default:
                    throw new ArgumentOutOfRangeException(nameof(lane));
            }
        }

        private static string TimeSpanToString(TimeSpan? time)
        {
            return time?.ToString(@"mm\:ss\.ffff", CultureInfo.InvariantCulture) ?? "??:??.????";
        }

        private static string PointsToString(decimal? points)
        {
            return points?.ToString("000.000", CultureInfo.InvariantCulture) ?? "???.???";
        }

        private static string RaceToTimeInfoString(Race race)
        {
            if (race.PresentedResult?.TimeInvalidReason != null)
                switch (race.PresentedResult.TimeInvalidReason.Value)
                {
                    case TimeInvalidReason.Unknown:
                        return "??";
                    case TimeInvalidReason.NotStarted:
                        return "NS";
                    case TimeInvalidReason.NotFinished:
                        return "NF";
                    case TimeInvalidReason.Disqualified:
                        return "DQ";
                    case TimeInvalidReason.Withdrawn:
                        return "WD";
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            if (race.PresentedTime == null)
                return "__";

            if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.OutOfCompetition))
                return "OC";
            if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.WorldRecord))
                return "WR";
            if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.NationalRecord))
                return "NR";
            if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.TrackRecord))
                return "TR";
            if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.PersonalBest))
                return "PB";
            if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.Fall))
                return "FL";
            if (race.PresentedTime.TimeInfo.HasFlag(TimeInfo.Restart))
                return "RS";

            return "OK";
        }

        private static bool TryParseTimeInfo(string input, out TimeInfo timeInfo)
        {
            switch (input)
            {
                case "WR":
                    timeInfo = TimeInfo.WorldRecord;
                    return true;
                case "NR":
                    timeInfo = TimeInfo.NationalRecord;
                    return true;
                case "TR":
                    timeInfo = TimeInfo.TrackRecord;
                    return true;
                case "PB":
                    timeInfo = TimeInfo.PersonalBest;
                    return true;
                case "FL":
                    timeInfo = TimeInfo.Fall;
                    return true;
                case "RS":
                    timeInfo = TimeInfo.Restart;
                    return true;
                case "OC":
                    timeInfo = TimeInfo.OutOfCompetition;
                    return true;
                case "MT":
                case "OK":
                    timeInfo = TimeInfo.None;
                    return true;
                default:
                    timeInfo = default(TimeInfo);
                    return false;
            }
        }

        private static bool TryParseTimeInvalidReason(string input, out TimeInvalidReason? timeInvalidReason)
        {
            switch (input)
            {
                case "NS":
                    timeInvalidReason = TimeInvalidReason.NotStarted;
                    return true;
                case "NF":
                    timeInvalidReason = TimeInvalidReason.NotFinished;
                    return true;
                case "DQ":
                    timeInvalidReason = TimeInvalidReason.Disqualified;
                    return true;
                case "WD":
                    timeInvalidReason = TimeInvalidReason.Withdrawn;
                    return true;
                default:
                    timeInvalidReason = null;
                    return false;
            }
        }

        public async Task ImportAsync(Guid competitionId, string name, Stream stream, CultureInfo cultureInfo)
        {
            var number = DistanceNumberFromFileName(name);

            using (var context = contextFactory())
            using (var transaction = context.BeginTransaction(IsolationLevel.RepeatableRead))
                try
                {
                    var distance = await context.Distances
                        .Include(d => d.Races)
                        .Include(d => d.Combinations)
                        .FirstOrDefaultAsync(d => d.CompetitionId == competitionId && d.Number == number);
                    if (distance == null)
                        throw new DistanceNotFoundException();

                    foreach (var lap in await context.RaceLaps.Where(l => l.Race.DistanceId == distance.Id && l.InstanceName == ResultInstanceName).ToListAsync())
                        context.RaceLaps.Remove(lap);
                    foreach (var passing in await context.RacePassings.Where(p => p.Race.DistanceId == distance.Id && p.InstanceName == ResultInstanceName).ToListAsync())
                        context.RacePassings.Remove(passing);
                    foreach (var result in await context.RaceResults.Where(r => r.Race.DistanceId == distance.Id && r.InstanceName == ResultInstanceName).ToListAsync())
                        context.RaceResults.Remove(result);
                    foreach (var time in await context.RaceTimes.Where(t => t.Race.DistanceId == distance.Id && t.InstanceName == ResultInstanceName).ToListAsync())
                        context.RaceTimes.Remove(time);
                    await context.SaveChangesAsync();

                    var calculator = calculatorManager.Get(distance.Discipline);
                    var laps = calculator.Laps(distance);

                    using (var reader = new StreamReader(stream, Encoding.GetEncoding(1252)))
                    {
                        reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (!string.IsNullOrWhiteSpace(line))
                                continue;

                            var headerLine = reader.ReadLine();
                            var titleLine = reader.ReadLine();
                            if (headerLine == null || titleLine == null)
                                break;

                            DateTime started;
                            if (!DateTime.TryParseExact(headerLine.Substring(13, 20), "dd-MM-yyyy  HH:mm:ss", CultureInfo, DateTimeStyles.None, out started))
                                continue;

                            int pair;
                            if (!int.TryParse(titleLine.Substring(8, 3), NumberStyles.None, CultureInfo, out pair))
                                continue;

                            var innerRace = ReadRace(distance, pair, Lane.Inner, headerLine, titleLine);
                            var outerRace = ReadRace(distance, pair, Lane.Outer, headerLine, titleLine);

                            for (var lapIndex = 0; lapIndex < laps; lapIndex++)
                            {
                                var lapLine = reader.ReadLine();
                                if (string.IsNullOrEmpty(lapLine))
                                    break;

                                if (innerRace != null)
                                    AddRaceLap(innerRace, lapLine, lapIndex, started);

                                if (outerRace != null)
                                    AddRaceLap(outerRace, lapLine, lapIndex, started);
                            }

                            if (innerRace != null)
                                CommitRace(distance, distance.Combinations, innerRace, calculator, headerLine, 40);
                            if (outerRace != null)
                                CommitRace(distance, distance.Combinations, outerRace, calculator, headerLine, 45);

                            await context.SaveChangesAsync();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
        }

        private static Race ReadRace(Distance distance, int pair, Lane lane, string headerLine, string titleLine)
        {
            int startNumberOffset;
            switch (lane)
            {
                case Lane.Inner:
                    startNumberOffset = 13;
                    break;
                case Lane.Outer:
                    startNumberOffset = 48;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lane), lane, null);
            }

            int startNumber;
            if (!int.TryParse(titleLine.Substring(startNumberOffset, 3), NumberStyles.None, CultureInfo, out startNumber))
                return null;

            if (startNumber == 0)
                return null;

            var race = distance.Races.FirstOrDefault(r => r.Heat == pair && r.Lane == (int)lane);
            if (race == null)
                throw new RaceNotFoundException(string.Format(Resources.RaceNotFoundFormat, pair, lane));

            RaceResult result;
            return TryAddRaceResult(race, headerLine, out result) ? race : null;
        }

        private static bool TryAddRaceResult(Race race, string headerLine, out RaceResult result)
        {
            int timeInvalidReasonOffset;
            switch ((Lane)race.Lane)
            {
                case Lane.Inner:
                    timeInvalidReasonOffset = 40;
                    break;
                case Lane.Outer:
                    timeInvalidReasonOffset = 45;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            TimeInvalidReason? timeInvalidReason;
            if (!TryParseTimeInvalidReason(headerLine.Substring(timeInvalidReasonOffset, 2), out timeInvalidReason))
                timeInvalidReason = null;

            result = new RaceResult
            {
                Race = race,
                InstanceName = ResultInstanceName,
                TimeInvalidReason = timeInvalidReason,
                Status = RaceStatus.Done
            };

            if (race.Results == null)
                race.Results = new Collection<RaceResult>();
            race.Results.Add(result);

            return true;
        }

        private static RaceLap AddRaceLap(Race race, string lapLine, int lapIndex, DateTime started)
        {
            int timeOffset;
            switch ((Lane)race.Lane)
            {
                case Lane.Inner:
                    timeOffset = 17;
                    break;
                case Lane.Outer:
                    timeOffset = 52;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            TimeSpan time;
            if (!TimeSpan.TryParseExact(lapLine.Substring(timeOffset, 10), @"mm\:ss\.ffff", CultureInfo, out time))
                return null;

            if (time == TimeSpan.Zero)
                return null;

            var lap = new RaceLap
            {
                Race = race,
                InstanceName = ResultInstanceName,
                ApplianceInstanceName = "",
                ApplianceName = "",
                How = "Optical",
                Flags = RaceEventFlags.Present,
                Time = time,
                When = started + time,
                FixedIndex = lapIndex
            };

            if (race.Laps == null)
                race.Laps = new Collection<RaceLap>();
            race.Laps.Add(lap);

            return lap;
        }

        private static void CommitRace(IDistance distance, IEnumerable<DistanceCombination> combinations, Race race, IDistanceDisciplineCalculator calculator, string headerLine, int timeInfoOffset)
        {
            TimeInfo timeInfo;
            if (!TryParseTimeInfo(headerLine.Substring(timeInfoOffset, 2), out timeInfo))
                timeInfo = TimeInfo.None;

            race.PresentedInstanceName = ResultInstanceName;

            var result = race.Results?.SingleOrDefault(r => r.InstanceName == ResultInstanceName);
            var finalLap = race.Laps?.Presented().ElementAtOrDefault(calculator.Laps(distance) - 1);
            if (result == null || finalLap == null)
                return;

            var time = new RaceTime
            {
                Race = race,
                InstanceName = ResultInstanceName,
                PresentationSource = finalLap.PresentationSource,
                Time = finalLap.Time,
                TimeInfo = timeInfo
            };

            if (race.Times == null)
                race.Times = new Collection<RaceTime>();
            race.Times.Add(time);

            var classificationWeight = combinations.FirstOrDefault()?.ClassificationWeight ?? DefaultClassificationWeight;
            if (calculator.CanCalculateRacePoints(distance, classificationWeight, distance.ClassificationPrecision, result, time))
                result.Points = calculator.CalculateRacePoints(distance, classificationWeight, distance.ClassificationPrecision, result, time);
        }

        private static int DistanceNumberFromFileName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var match = Regex.Match(name, "^[CATLR][0-9]{5}M\\.D([0-9]{2})$");
            if (!match.Success)
                throw new FormatException(Resources.InvalidFileNamePattern);

            return int.Parse(match.Groups[1].Value);
        }
    }
}