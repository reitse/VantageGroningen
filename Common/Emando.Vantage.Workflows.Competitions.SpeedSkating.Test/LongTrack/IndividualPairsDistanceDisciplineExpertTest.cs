using System;
using System.Collections.Generic;
using System.Linq;
using Emando.Vantage.Competitions;
using Emando.Vantage.Components.Competitions.Test;
using Emando.Vantage.Entities.Competitions;
using Emando.Vantage.Workflows.Competitions.SpeedSkating.LongTrack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emando.Vantage.Workflows.Competitions.SpeedSkating.Test.LongTrack
{
    [TestClass]
    public class IndividualPairsDistanceDisciplineExpertTest
    {
        private CompetitionMemoryContext context;
        private IndividualPairsDistanceDisciplineExpert expert;
        private Distance distance;
        private DistanceCombination distanceCombination;
        private IList<CompetitorBase> competitors;
        private IList<DrawCompetitor> drawCompetitors;

        [TestInitialize]
        public void Initialize()
        {
            context = new CompetitionMemoryContext();
            expert = new IndividualPairsDistanceDisciplineExpert();

            competitors = new List<CompetitorBase>
            {
                CreateCompetitor("Jantje"),
                CreateCompetitor("Pietje"),
                CreateCompetitor("Klaasje"),
                CreateCompetitor("Petertje"),
                CreateCompetitor("Diederikje")
            };
            distanceCombination = new DistanceCombination
            {
                Id = Guid.NewGuid(),
                Distances = new List<Distance>
                {
                    new Distance
                    {
                        Number = 2,
                        ContinuousNumbering = true,
                        FirstHeat = 9,
                        StartMode = DistanceStartMode.MultipleHeats
                    }
                }
            };
            drawCompetitors = new List<DrawCompetitor>
            {
                new DrawCompetitor(competitors[0], CreateHistoricalTime(12)),
                new DrawCompetitor(competitors[0], CreateHistoricalTime(9)),
                new DrawCompetitor(competitors[0], null),
                new DrawCompetitor(competitors[0], CreateHistoricalTime(13)),
                new DrawCompetitor(competitors[0], CreateHistoricalTime(10)),
            };
            distance = distanceCombination.Distances.Single(d => d.Number == 2);
        }

        private static CompetitorBase CreateCompetitor(string name)
        {
            return new PersonCompetitor
            {
                Id = Guid.NewGuid(),
                ShortName = name
            };
        }

        private static IPersonLicenseTime CreateHistoricalTime(double seconds)
        {
            return new PersonTime
            {
                Time = TimeSpan.FromSeconds(seconds)
            };
        }

        [TestMethod]
        public void EstimatedLapsTest()
        {
            var race = new Race
            {
                Distance = new Distance
                {
                    Value = 1500,
                    TrackLength = 400
                },
                PersonalBest = new TimeSpan(0, 0, 1, 49, 541),
                SeasonBest = new TimeSpan(0, 0, 1, 51, 949)
            };

            var laps = expert.Calculator.GetEstimatedLapTimes(race.Distance, race).ToList();

            Assert.AreEqual(4, laps.Count);
        }

        [TestMethod]
        public void GroupOddDrawCompetitors()
        {
            var settings = new DistanceDrawSettings
            {
                GroupMode = DistanceDrawGroupMode.Time,
                GroupSize = 2
            };

            var sortedCompetitors = expert.SortDrawCompetitors(distance, drawCompetitors, settings);
            var groups = expert.GroupDrawCompetitors(distance, 1, sortedCompetitors, settings);

            Assert.AreEqual(3, groups.Count);
            Assert.AreEqual(2, groups[0].Count);
            Assert.AreEqual(2, groups[1].Count);
            Assert.AreEqual(1, groups[2].Count);

            Assert.IsTrue(groups.SelectMany(g => g).SequenceEqual(new[]
            {
                drawCompetitors[1],
                drawCompetitors[4],
                drawCompetitors[0],
                drawCompetitors[3],
                drawCompetitors[2]
            }));
        }

        [TestMethod]
        public void GroupEvenDrawCompetitors()
        {
            var settings = new DistanceDrawSettings
            {
                GroupMode = DistanceDrawGroupMode.Time,
                GroupSize = 2
            };

            var sortedCompetitors = expert.SortDrawCompetitors(distance, drawCompetitors.Take(4), settings);
            var groups = expert.GroupDrawCompetitors(distance, 1, sortedCompetitors, settings);

            Assert.AreEqual(2, groups.Count);
            Assert.AreEqual(2, groups[0].Count);
            Assert.AreEqual(2, groups[1].Count);

            Assert.IsTrue(groups.SelectMany(g => g).SequenceEqual(new[]
            {
                drawCompetitors[1],
                drawCompetitors[0],
                drawCompetitors[3],
                drawCompetitors[2]
            }));
        }

        [TestMethod]
        public void DrawOddQuartetsRandom()
        {
            var settings = new DistanceDrawSettings
            {
                GroupMode = DistanceDrawGroupMode.Time,
                GroupSize = 2
            };

            var sortedCompetitors = expert.SortDrawCompetitors(distance, drawCompetitors, settings);
            var groups = expert.GroupDrawCompetitors(distance, 1, sortedCompetitors, settings);

            var competitorGroups = new List<IReadOnlyList<CompetitorBase>>();
            foreach (var group in groups)
            {
                var competitorGroup = group.Select(g => g.Competitor).ToList();
                expert.DrawCompetitorGroupAsync(distance, new[] { distanceCombination.Id }, 1, competitorGroup, context, settings).Wait();
                competitorGroups.Add(competitorGroup);
            }

            var heats = expert.FillCompetitorsInHeatsAsync(distance, new[] { distanceCombination.Id }, 1, competitorGroups, context, settings).Result;
            var races = heats.SelectMany(r => r.Value).ToList();
            
            Assert.IsTrue(Enumerable.Range(distance.FirstHeat, 4).SequenceEqual(heats.Keys));
            Assert.AreEqual(1, heats[9].Count);
            Assert.AreEqual(0, heats[10].Count);
            Assert.AreEqual(2, heats[11].Count);
            Assert.AreEqual(2, heats[12].Count);
            Assert.AreSame(heats[9].Single().Competitor, drawCompetitors[2].Competitor);
            Assert.AreEqual(5, races.Count);
            Assert.AreEqual(0, races[0].Lane);
            Assert.AreEqual(0, races[1].Lane);
            Assert.AreEqual(1, races[2].Lane);
            Assert.AreEqual(0, races[3].Lane);
            Assert.AreEqual(1, races[4].Lane);
        }

        [TestMethod]
        public void DrawOddQuartetsRandomReverse()
        {
            var settings = new DistanceDrawSettings
            {
                GroupMode = DistanceDrawGroupMode.Time,
                GroupSize = 2,
                ReverseFilling = true
            };

            var sortedCompetitors = expert.SortDrawCompetitors(distance, drawCompetitors, settings);
            var groups = expert.GroupDrawCompetitors(distance, 1, sortedCompetitors, settings);

            var competitorGroups = new List<IReadOnlyList<CompetitorBase>>();
            foreach (var group in groups)
            {
                var competitorGroup = group.Select(g => g.Competitor).ToList();
                expert.DrawCompetitorGroupAsync(distance, new[] { distanceCombination.Id }, 1, competitorGroup, context, settings).Wait();
                competitorGroups.Add(competitorGroup);
            }

            var heats = expert.FillCompetitorsInHeatsAsync(distance, new[] { distanceCombination.Id }, 1, competitorGroups, context, settings).Result;
            var races = heats.SelectMany(r => r.Value).ToList();

            Assert.IsTrue(Enumerable.Range(distance.FirstHeat, 4).SequenceEqual(heats.Keys));
            Assert.AreEqual(2, heats[9].Count);
            Assert.AreEqual(2, heats[10].Count);
            Assert.AreEqual(0, heats[11].Count);
            Assert.AreEqual(1, heats[12].Count);
            Assert.AreEqual(5, races.Count);
            Assert.AreEqual(0, races[0].Lane);
            Assert.AreEqual(1, races[1].Lane);
            Assert.AreEqual(0, races[2].Lane);
            Assert.AreEqual(1, races[3].Lane);
            Assert.AreEqual(0, races[4].Lane);
        }
    }
}
