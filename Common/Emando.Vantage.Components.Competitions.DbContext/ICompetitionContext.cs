using System.Data.Entity;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Components.Competitions
{
    public interface ICompetitionContext : IVantageContext
    {
        IDbSet<CompetitionSerie> CompetitionSeries { get; }

        IDbSet<Competition> Competitions { get; }

        IDbSet<CompetitorListBase> CompetitorLists { get; }

        IDbSet<CompetitorBase> Competitors { get; }
        
        IDbSet<TeamCompetitorMember> TeamCompetitorMembers { get; }

        IDbSet<Distance> Distances { get; }

        IDbSet<DistanceDrawSettings> DistanceDrawSettings { get; }
        
        IDbSet<DistancePointsTable> DistancePointsTables { get; }

        IDbSet<DistancePoints> DistancePoints { get; }

        IDbSet<ValidDistance> ValidDistances { get; }

        IDbSet<DistanceCombination> DistanceCombinations { get; }

        IDbSet<DistanceCombinationCompetitor> DistanceCombinationCompetitors { get; }

        IDbSet<RaceTransponder> RaceTransponders { get; }

        IDbSet<Race> Races { get; }

        IDbSet<RaceStart> RaceStarts { get; }

        IDbSet<RacePassing> RacePassings { get; }

        IDbSet<RaceLap> RaceLaps { get; }

        IDbSet<RaceTime> RaceTimes { get; }

        IDbSet<RaceResult> RaceResults { get; }

        IDbSet<PersonTime> PersonTimes { get; }

        IDbSet<RecordTime> RecordTimes { get; } 
    }
}