using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Emando.Vantage.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Components.Competitions;
using Emando.Vantage.Components.Competitions.SpeedSkating.LongTrack;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Windows.Controls.Competitions.SpeedSkating.LongTrack
{
    public class IndividualPairsDistancePaths : IDistanceDisciplinePathProvider
    {
        private static readonly IDictionary<int, IDictionary<int, IDictionary<Lane, PathFigure>>> Openings = new Dictionary
            <int, IDictionary<int, IDictionary<Lane, PathFigure>>>
        {
            {
                400, new Dictionary<int, IDictionary<Lane, PathFigure>>
                {
                    {
                        100, new Dictionary<Lane, PathFigure>
                        {
                            { Lane.Inner, new PathFigure(new Point(35, 62.5), new PathSegment[] { new LineSegment(new Point(135, 62.5), false) }, false) },
                            { Lane.Outer, new PathFigure(new Point(35, 67.5), new PathSegment[] { new LineSegment(new Point(135, 67.5), false) }, false) }
                        }
                    },
                    {
                        300, new Dictionary<Lane, PathFigure>
                        {
                            {
                                Lane.Inner, new PathFigure(new Point(145 + 7.85, 7.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 7.5), false),
                                    new ArcSegment(new Point(35, 62.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 62.5), false)
                                }, false)
                            },
                            {
                                Lane.Outer, new PathFigure(new Point(145 - 7.85, 2.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 2.5), false),
                                    new ArcSegment(new Point(35, 67.5), new Size(32.5, 32.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 67.5), false)
                                }, false)
                            }
                        }
                    },
                    {
                        500, new Dictionary<Lane, PathFigure>
                        {
                            { Lane.Inner, new PathFigure(new Point(35, 62.5), new PathSegment[] { new LineSegment(new Point(135, 62.5), false) }, false) },
                            { Lane.Outer, new PathFigure(new Point(35, 67.5), new PathSegment[] { new LineSegment(new Point(135, 67.5), false) }, false) }
                        }
                    },
                    {
                        700, new Dictionary<Lane, PathFigure>
                        {
                            {
                                Lane.Inner, new PathFigure(new Point(145 + 7.85, 7.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 7.5), false),
                                    new ArcSegment(new Point(35, 62.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 62.5), false)
                                }, false)
                            },
                            {
                                Lane.Outer, new PathFigure(new Point(145 - 7.85, 2.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 2.5), false),
                                    new ArcSegment(new Point(35, 67.5), new Size(32.5, 32.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 67.5), false)
                                }, false)
                            }
                        }
                    },
                    {
                        1000, new Dictionary<Lane, PathFigure>
                        {
                            {
                                Lane.Inner, new PathFigure(new Point(90 + 7.85, 7.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 7.5), false),
                                    new ArcSegment(new Point(35, 62.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 62.5), false)
                                }, false)
                            },
                            {
                                Lane.Outer, new PathFigure(new Point(90 - 7.85, 2.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 2.5), false),
                                    new ArcSegment(new Point(35, 67.5), new Size(32.5, 32.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 67.5), false)
                                }, false)
                            }
                        }
                    },
                    {
                        1500, new Dictionary<Lane, PathFigure>
                        {
                            {
                                Lane.Inner, new PathFigure(new Point(145 + 7.85, 7.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 7.5), false),
                                    new ArcSegment(new Point(35, 62.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 62.5), false)
                                }, false)
                            },
                            {
                                Lane.Outer, new PathFigure(new Point(145 - 7.85, 2.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 2.5), false),
                                    new ArcSegment(new Point(35, 67.5), new Size(32.5, 32.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 67.5), false)
                                }, false)
                            }
                        }
                    },
                    {
                        3000, new Dictionary<Lane, PathFigure>
                        {
                            {
                                Lane.Inner, new PathFigure(new Point(35 + 21.46, 7.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 7.5), false),
                                    new ArcSegment(new Point(35, 62.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 62.5), false)
                                }, false)
                            },
                            {
                                Lane.Outer, new PathFigure(new Point(35 + 5.75, 2.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 2.5), false),
                                    new ArcSegment(new Point(35, 67.5), new Size(32.5, 32.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 67.5), false)
                                }, false)
                            }
                        }
                    },
                    {
                        5000, new Dictionary<Lane, PathFigure>
                        {
                            {
                                Lane.Inner, new PathFigure(new Point(35 + 21.46, 7.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 7.5), false),
                                    new ArcSegment(new Point(35, 62.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 62.5), false)
                                }, false)
                            },
                            {
                                Lane.Outer, new PathFigure(new Point(35 + 5.75, 2.5), new PathSegment[]
                                {
                                    new LineSegment(new Point(35, 2.5), false),
                                    new ArcSegment(new Point(35, 67.5), new Size(32.5, 32.5), 0, true, SweepDirection.Counterclockwise, false),
                                    new LineSegment(new Point(135, 67.5), false)
                                }, false)
                            }
                        }
                    },
                    {
                        10000, new Dictionary<Lane, PathFigure>
                        {
                            { Lane.Inner, new PathFigure(new Point(135, 62.5), new PathSegment[0], false) },
                            { Lane.Outer, new PathFigure(new Point(135, 67.5), new PathSegment[0], false) }
                        }
                    }
                }
            }
        };

        private static readonly IDictionary<int, IDictionary<int, IDictionary<Lane, PathSegment[]>>> Closings = new Dictionary
            <int, IDictionary<int, IDictionary<Lane, PathSegment[]>>>
        {
            {
                400, new Dictionary<int, IDictionary<Lane, PathSegment[]>>
                {
                    {
                        100, new Dictionary<Lane, PathSegment[]>
                        {
                            { Lane.Inner, new PathSegment[] { new LineSegment(new Point(135, 67.5), false) } },
                            { Lane.Outer, new PathSegment[] { new LineSegment(new Point(135, 62.5), false) } }
                        }
                    },
                    {
                        300, new Dictionary<Lane, PathSegment[]>
                        {
                            { Lane.Inner, new PathSegment[] { new LineSegment(new Point(135, 67.5), false) } },
                            { Lane.Outer, new PathSegment[] { new LineSegment(new Point(135, 62.5), false) } }
                        }
                    },
                    {
                        500, new Dictionary<Lane, PathSegment[]>
                        {
                            { Lane.Inner, new PathSegment[] { new LineSegment(new Point(135, 67.5), false) } },
                            { Lane.Outer, new PathSegment[] { new LineSegment(new Point(135, 62.5), false) } }
                        }
                    },
                    {
                        700, new Dictionary<Lane, PathSegment[]>
                        {
                            { Lane.Inner, new PathSegment[] { new LineSegment(new Point(135, 67.5), false) } },
                            { Lane.Outer, new PathSegment[] { new LineSegment(new Point(135, 62.5), false) } }
                        }
                    },
                    {
                        1000, new Dictionary<Lane, PathSegment[]>
                        {
                            { Lane.Inner, new PathSegment[] { new LineSegment(new Point(90, 67.5), false) } },
                            { Lane.Outer, new PathSegment[] { new LineSegment(new Point(90, 62.5), false) } }
                        }
                    },
                    {
                        1500, new Dictionary<Lane, PathSegment[]>
                        {
                            { Lane.Inner, new PathSegment[] { new LineSegment(new Point(135, 67.5), false) } },
                            { Lane.Outer, new PathSegment[] { new LineSegment(new Point(135, 62.5), false) } }
                        }
                    },
                    {
                        3000, new Dictionary<Lane, PathSegment[]>
                        {
                            { Lane.Inner, new PathSegment[] { new LineSegment(new Point(135, 67.5), false) } },
                            { Lane.Outer, new PathSegment[] { new LineSegment(new Point(135, 62.5), false) } }
                        }
                    },
                    {
                        5000, new Dictionary<Lane, PathSegment[]>
                        {
                            { Lane.Inner, new PathSegment[] { new LineSegment(new Point(135, 67.5), false) } },
                            { Lane.Outer, new PathSegment[] { new LineSegment(new Point(135, 62.5), false) } }
                        }
                    },
                    {
                        10000, new Dictionary<Lane, PathSegment[]>
                        {
                            { Lane.Inner, new PathSegment[] { new LineSegment(new Point(135, 67.5), false) } },
                            { Lane.Outer, new PathSegment[] { new LineSegment(new Point(135, 62.5), false) } }
                        }
                    }
                }
            }
        };
        private static readonly IDictionary<int, IDictionary<Lane, IEnumerable<PathSegment>>> RoundOpenings = new Dictionary<int, IDictionary<Lane, IEnumerable<PathSegment>>>
        {
            {
                400, new Dictionary<Lane, IEnumerable<PathSegment>>
                {
                    {
                        Lane.Inner, new PathSegment[]
                        {
                            new LineSegment(new Point(145, 62.5), false),
                            new ArcSegment(new Point(145, 7.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false),
                            new LineSegment(new Point(35, 2.5), false),
                            new ArcSegment(new Point(35, 67.5), new Size(32.5, 32.5), 0, true, SweepDirection.Counterclockwise, false)
                        }
                    },
                    {
                        Lane.Outer, new PathSegment[]
                        {
                            new LineSegment(new Point(145, 67.5), false),
                            new ArcSegment(new Point(145, 2.5), new Size(32.5, 32.5), 0, true, SweepDirection.Counterclockwise, false),
                            new LineSegment(new Point(35, 7.5), false),
                            new ArcSegment(new Point(35, 62.5), new Size(27.5, 27.5), 0, true, SweepDirection.Counterclockwise, false)
                        }
                    }
                }
            }
        };
        private static readonly IDictionary<int, IDictionary<Lane, PathSegment[]>> RoundClosings = new Dictionary<int, IDictionary<Lane, PathSegment[]>>
        {
            {
                400, new Dictionary<Lane, PathSegment[]>
                {
                    { Lane.Inner, new PathSegment[] { new LineSegment(new Point(135, 67.5), false) } },
                    { Lane.Outer, new PathSegment[] { new LineSegment(new Point(135, 62.5), false) } }
                }
            }
        };

        #region IDistanceDisciplinePathProvider Members

        public PathGeometry CreatePath(Distance distance, int lane)
        {
            IDictionary<int, IDictionary<Lane, PathFigure>> trackLaneOpenings;
            if (!Openings.TryGetValue((int)distance.TrackLength, out trackLaneOpenings))
                return null;

            IDictionary<int, IDictionary<Lane, PathSegment[]>> trackLaneClosings;
            if (!Closings.TryGetValue((int)distance.TrackLength, out trackLaneClosings))
                return null;

            IDictionary<Lane, PathSegment[]> trackRoundClosings;
            if (!RoundClosings.TryGetValue((int)distance.TrackLength, out trackRoundClosings))
                return null;

            IDictionary<Lane, IEnumerable<PathSegment>> trackRoundOpenings;
            if (!RoundOpenings.TryGetValue((int)distance.TrackLength, out trackRoundOpenings))
                return null;

            IDictionary<Lane, PathFigure> laneOpenings;
            if (!trackLaneOpenings.TryGetValue(distance.Value, out laneOpenings))
                return null;

            var opening = laneOpenings[(Lane)lane];
            var segments = new List<PathSegment>();
            segments.AddRange(opening.Segments);

            var startLap = opening.Segments.Count > 0 ? 2 : 1;

            var lapCount = Calculator.Laps(distance);
            if (lapCount > 1)
            {
                for (var lap = startLap; lap < lapCount; lap++, lane ^= 1)
                {
                    segments.AddRange(trackRoundOpenings[(Lane)lane]);
                    segments.AddRange(trackRoundClosings[(Lane)lane]);
                }
                segments.AddRange(trackRoundOpenings[(Lane)lane]);

                IDictionary<Lane, PathSegment[]> closing;
                if (trackLaneClosings.TryGetValue(distance.Value, out closing))
                    segments.AddRange(closing[(Lane)lane]);
            }

            var figure = new PathFigure(opening.StartPoint, segments, false);
            var path = new PathGeometry(new[] { figure });
            path.Freeze();

            return path;
        }

        public IDistanceDisciplineCalculator Calculator => IndividualPairsDistanceCalculator.Default;

        #endregion
    }
}