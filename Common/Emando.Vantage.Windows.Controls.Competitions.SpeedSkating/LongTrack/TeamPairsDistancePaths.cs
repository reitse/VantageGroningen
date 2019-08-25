using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    public class TeamPairsDistancePaths : IDistanceDisciplinePathProvider
    {
        private static readonly IDictionary<int, Point> StartPoints = new Dictionary<int, Point>
        {
            { 0, new Point(90, 62.5) },
            { 1, new Point(90, 7.5) }
        };
        private static readonly IDictionary<int, PathSegment[]> Rounds = new Dictionary<int, PathSegment[]>
        {
            {
                0, new PathSegment[]
                {
                    new LineSegment(new Point(145, 62.5), false),
                    new ArcSegment(new Point(145, 7.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false),
                    new LineSegment(new Point(90, 7.5), false)
                }
            },
            {
                1, new PathSegment[]
                {
                    new LineSegment(new Point(35, 7.5), false),
                    new ArcSegment(new Point(35, 62.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false),
                    new LineSegment(new Point(90, 62.5), false)
                }
            }
        };

        #region IDistanceDisciplinePathProvider Members

        public PathGeometry CreatePath(Distance distance, int lane)
        {
            var segments = new List<PathSegment>();

            var lapCount = Calculator.Laps(distance);
            for (var lap = 1; lap <= lapCount; lap++, lane ^= 1)
                segments.AddRange(Rounds[lane]);

            var startPoint = StartPoints[lane];
            var figure = new PathFigure(startPoint, segments, false);
            var path = new PathGeometry(new[] { figure });
            path.Freeze();

            return path;
        }

        public IDistanceDisciplineCalculator Calculator => TeamPairsDistanceCalculator.Default;

        #endregion
    }
}