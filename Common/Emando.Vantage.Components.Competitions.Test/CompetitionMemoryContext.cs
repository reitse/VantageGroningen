using System.Data.Entity;
using Emando.Vantage.Components.Test;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Competitions.Test
{
    public class CompetitionMemoryContext : VantageMemoryContext, ICompetitionContext
    {
        private readonly IDbSet<Competition> competitions = new MemoryDbSet<Competition>();
        private readonly IDbSet<CompetitionSerie> competitionSeries = new MemoryDbSet<CompetitionSerie>();
        private readonly IDbSet<CompetitorListBase> competitorLists = new MemoryDbSet<CompetitorListBase>();
        private readonly IDbSet<CompetitorBase> competitors = new MemoryDbSet<CompetitorBase>();
        private readonly IDbSet<DistanceCombinationCompetitor> distanceCombinationCompetitors = new MemoryDbSet<DistanceCombinationCompetitor>();
        private readonly IDbSet<DistanceCombination> distanceCombinations = new MemoryDbSet<DistanceCombination>();
        private readonly IDbSet<DistanceDrawSettings> distanceDrawSettings = new MemoryDbSet<DistanceDrawSettings>();
        private readonly IDbSet<DistancePoints> distancePoints = new MemoryDbSet<DistancePoints>();
        private readonly IDbSet<DistancePointsTable> distancePointsTables = new MemoryDbSet<DistancePointsTable>();
        private readonly IDbSet<Distance> distances = new MemoryDbSet<Distance>();
        private readonly IDbSet<PersonTime> personTimes = new MemoryDbSet<PersonTime>();
        private readonly IDbSet<RaceLap> raceLaps = new MemoryDbSet<RaceLap>();
        private readonly IDbSet<RacePassing> racePassings = new MemoryDbSet<RacePassing>();
        private readonly IDbSet<RaceResult> raceResults = new MemoryDbSet<RaceResult>();
        private readonly IDbSet<Race> races = new MemoryDbSet<Race>();
        private readonly IDbSet<RaceStart> raceStarts = new MemoryDbSet<RaceStart>();
        private readonly IDbSet<RaceTime> raceTimes = new MemoryDbSet<RaceTime>();
        private readonly IDbSet<RaceTransponder> raceTransponders = new MemoryDbSet<RaceTransponder>();
        private readonly IDbSet<TeamCompetitorMember> teamCompetitorMembers = new MemoryDbSet<TeamCompetitorMember>();
        private readonly IDbSet<ValidDistance> validDistances = new MemoryDbSet<ValidDistance>();

        #region ICompetitionContext Members

        public IDbSet<CompetitionSerie> CompetitionSeries => competitionSeries;

        public IDbSet<Competition> Competitions => competitions;

        public IDbSet<TeamCompetitorMember> TeamCompetitorMembers => teamCompetitorMembers;

        public IDbSet<CompetitorListBase> CompetitorLists => competitorLists;

        public IDbSet<Distance> Distances => distances;

        public IDbSet<DistanceDrawSettings> DistanceDrawSettings => distanceDrawSettings;

        public IDbSet<DistancePointsTable> DistancePointsTables => distancePointsTables;

        public IDbSet<DistancePoints> DistancePoints => distancePoints;

        public IDbSet<ValidDistance> ValidDistances => validDistances;

        public IDbSet<DistanceCombination> DistanceCombinations => distanceCombinations;

        public IDbSet<DistanceCombinationCompetitor> DistanceCombinationCompetitors => distanceCombinationCompetitors;

        public IDbSet<RaceTransponder> RaceTransponders => raceTransponders;

        public IDbSet<Race> Races => races;

        public IDbSet<RaceStart> RaceStarts => raceStarts;

        public IDbSet<RacePassing> RacePassings => racePassings;

        public IDbSet<RaceLap> RaceLaps => raceLaps;

        public IDbSet<CompetitorBase> Competitors => competitors;

        public IDbSet<RaceTime> RaceTimes => raceTimes;

        public IDbSet<RaceResult> RaceResults => raceResults;

        public IDbSet<PersonTime> PersonTimes => personTimes;

        #endregion
    }
}