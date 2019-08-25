using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Reporting;
using Telerik.Reporting;
using System;
using Emando.Vantage.Competitions;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack.Reporting
{
    [DisciplineReport(typeof(MassStartDistanceResultReportLoader), "SpeedSkating.LongTrack.MassStartDistance", "Result", 210)]
    public partial class MassStartDistanceResultReport : Report
    {
        public MassStartDistanceResultReport()
        {
            InitializeComponent();
        }

        public IEnumerable<PointsRankedRace> Races
        {
            get { return racesDataSource.DataSource as IEnumerable<PointsRankedRace>; }
            set { racesDataSource.DataSource = value; }
        }

        public static string FormatRaceTime(Race race, long digits)
        {
            if (race?.PresentedResult == null)
                return null;

            TimeSpan? time;
            if (race.PresentedResult.TimeInvalidReason == TimeInvalidReason.NotFinished)
            {
                var lastLap = race.PresentedLaps.LastOrDefault();
                time = lastLap?.Time;
            }
            else if (race.PresentedResult.TimeInvalidReason != null)
                return Functions.FormatTimeInvalidReason(race.PresentedResult.TimeInvalidReason.Value);
            else
                time = race.PresentedTime?.Time;

            return time != null ? Functions.FormatTime(time.Value, digits) : null;
        }

        public static decimal? Points(IReadOnlyDictionary<int, decimal> lapPoints, long? index)
        {
            if (!index.HasValue)
                return null;

            decimal points;
            if (lapPoints.TryGetValue((int)index, out points))
                return points;

            return null;
        }

        public static string FormatTotalPoints(Race race, decimal totalPoints)
        {
            return race.PresentedResult.TimeInvalidReason != null
                ? "0"
                : totalPoints.ToString("0");
        }
    }
}